using System;
using System.Reflection;
using Spring.Aop;

namespace Aspect
{
    public class DbAccessAdvice:IMethodBeforeAdvice
    {
        #region Implementation of IMethodBeforeAdvice

        public void Before(MethodInfo method, object[] args, object target)
        {
            Console.WriteLine("You try access to DB");
        }

        #endregion
    }
}