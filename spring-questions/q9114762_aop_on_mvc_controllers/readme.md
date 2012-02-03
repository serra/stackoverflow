http://stackoverflow.com/questions/9114762/unobtrusive-aop-with-spring-net

This has to do with the fact that the controller is created and then actually calls `DoStuff()` on it self. The controller obviously does not hold a proxy to itself and therefore the call to `DoStuff()` does not get intercepted by Spring.Net AOP.

As tobsen mentions in his answer, you will have to get the controller from spring, otherwise interception will not take place. I assume you're using spring mvc support here to create controllers, but this does not clearly show from your question and you might have left it out.

### How to intercept methods on MVC 3 controllers

*Summary*

See below for details and an example.

 1. Use an `InheritanceBasedAopConfigurer`
 1. Declare methods you want to intercept as virtual
 1. Configure your interceptors

*Spring's default interception mechanism does not work ...*

When a request is made to an MVC app, then from the request url a controller is chosen by the MVC framework. On this controller, the `Execute()` method is called, which in turn is responsible for invoking the action methods. It is important to realize that **action methods are always called from within the controller**. 

Spring.NET aop uses dynamic weaving. By default, at runtime a proxy is created for objects for which aop advisors are declared in the configuration. This proxy intercepts calls and forwards calls to the target instance. This is done when proxying [interfaces][2] and [classes (using `proxy-target-type="true"`)][3]. When the target object invokes a method on it self, it will not do this through the spring proxy and the method *does not get intercepted*. This why the default aop mechanism doesn't work for mvc controllers.

*... but using an `InheritanceBasedAopConfigurer` does the trick*

To intercept calls on action methods, you should use an [`InheritanceBasedAopConfigurer`][4]. This will create an inheritance based proxy that does not delegate to a target object, instead interception advice is added directly in the method body before invoking the base class method.

Note that for this interception method to work, the methods have to be virtual.

The following xml config works:

    <!-- 
    When not specifying an object id or name, 
    spring will assign a name to it like [typename]#[0,1,2,..]  
    -->  
    <object type="MyApp.Controllers.HomeController, MyApp" 
            singleton="false" />
    
    <object id="myInterceptor" type="Spring.Aop.Support.AttributeMatchMethodPointcutAdvisor, Spring.Aop">
      <property name="Attribute" value="MyApp.MyAttribute, MyApp" />
      <property name="Advice">
        <object type="MyApp.MyAdvice, MyApp" />
      </property>
    </object>
     
    <object type="Spring.Aop.Framework.AutoProxy.InheritanceBasedAopConfigurer, Spring.Aop">
      <property name="ObjectNames">
        <list>
          <value>*Controller#*</value>
        </list>
      </property>
      <property name="InterceptorNames">
        <list>
          <value>myInterceptor</value>
        </list>
      </property>
    </object>

A working example is [available on github][5]. It is based on a standard mvc 3 application with [Spring.Net Mvc3 support][6]. Relevant files are:

 * [xml configuration](https://github.com/serra/stackoverflow/blob/master/spring-questions/q9114762_aop_on_mvc_controllers/Config/objects.xml) 
 * [`HomeController`](https://github.com/serra/stackoverflow/blob/master/spring-questions/q9114762_aop_on_mvc_controllers/Controllers/HomeController.cs)
 * [attribute and interceptor](https://github.com/serra/stackoverflow/blob/master/spring-questions/q9114762_aop_on_mvc_controllers/Controllers/SetMethodInfoAsMessageAdvice.cs)


*References*

 * Spring.net docs: [proxying interfaces][2] 
   vs [proxying classes][3] 
   vs using an [inheritance based aop configurer][4]
 * A similar question with regard to transaction interception is here: http://stackoverflow.com/questions/4280143/asp-net-mvc-controller-declarative-aop-with-spring-net/4346791#4346791. Solution can be to delegate calls to injected services or to use an inheritance based proxy, see Mark's answer to the question mentioned above. In the latter case `DoStuff()` should be declared virtual.
 * [spring.net forum post][1]


  [1]: http://forum.springframework.net/showthread.php?7338-Asp.Net-MVC-Controller-declarative-AOP-with-Spring.Net
  [2]: http://www.springframework.net/doc-latest/reference/html/aop.html#aop-proxying-interfaces
  [3]: http://www.springframework.net/doc-latest/reference/html/aop.html#d4e4281
  [4]: http://www.springframework.net/doc-latest/reference/html/aop.html#aop-inheritancebasedaopconfigurer
  [5]: https://github.com/serra/stackoverflow/tree/master/spring-questions/q9114762_aop_on_mvc_controllers
  [6]: http://www.springframework.net/doc-latest/reference/html/web-mvc3.html