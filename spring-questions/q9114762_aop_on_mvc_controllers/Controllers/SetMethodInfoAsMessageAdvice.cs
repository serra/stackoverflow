using System;
using System.Reflection;
using Spring.Aop;

namespace q9114762_aop_on_mvc_controllers.Controllers
{
    public class SetMethodInfoAsMessageAdvice : IMethodBeforeAdvice
    {
        public void Before(MethodInfo method, object[] args, object target)
        {
            var postman = target as IPostMan;
            if (postman == null)
                return;

            postman.AddMessage(string.Format("Intercepted call to {0} on {1}", method.Name, method.DeclaringType.Name));
        }
    }

    public interface IPostMan
    {
        void AddMessage(string message);
    }

    public class SetMethodInfoAsMessageAttribute : Attribute
    {
    }

}