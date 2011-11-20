using System;
using System.Reflection;
using Spring.Aop;

namespace Aspect
{
    public class LogExThrowsAdvice : Spring.Aspects.Exceptions.ExceptionHandlerAdvice, IThrowsAdvice
    {
        public void AfterThrowing(MethodInfo method, Object[] args,
            Object target, Exception exception)
        {
            Console.WriteLine(exception.Message);

        }
    }
}