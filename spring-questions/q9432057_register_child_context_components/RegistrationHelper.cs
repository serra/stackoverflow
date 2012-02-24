using System.Reflection;
using Spring.Context;
using Spring.Objects.Factory.Support;

namespace q9432057_register_child_context_components
{
    public class RegistrationHelper : IMethodReplacer
    {
        private ComponentRepository _repo;

        public RegistrationHelper(ComponentRepository repo)
        {
        }

        public object Implement(object target, MethodInfo method, object[] arguments)
        {
            var component = target as IComponent;
            if (target == null)
                return 0;

            _repo.Register(component);
            return 1;
        }
    }

    /*
    public class ServiceLocatorImplementer : IMethodReplacer
    {
        protected IEnumerable<ICustomInterfaceThatDoesSomething> GetAllImplementers()
        {
            var idObjects = Spring.Context.Support.ContextRegistry.GetContext()
                .GetObjectsOfType(typeof(ICustomInterfaceThatDoesSomething));

            return idObjects.Values.Cast<ICustomInterfaceThatDoesSomething>();
        }

        public object Implement(object target, MethodInfo method, object[] arguments)
        {
            return GetAllImplementers();
        }
    }
    */
}