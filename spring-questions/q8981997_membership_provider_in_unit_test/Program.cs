using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Context.Support;
using Spring.Objects.Factory;
using Spring.Testing.NUnit;

namespace q8981997_membership_provider_in_unit_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Example demonstrating unit testing using autowire by name.
    /// </summary>
    /// <remarks>
    /// Make sure you use private fields, not properties and that
    /// you set PopulateProtectedVariables = true in the test
    /// constructor.
    /// This will _not_ inject into properties.
    /// </remarks>
    [TestFixture]
    public class AutoWireByNameTests : AbstractDependencyInjectionSpringContextTests
    {
        protected IMyInterface MyObject;

        public AutoWireByNameTests()
        {
            PopulateProtectedVariables = true;
        }

        [Test]
        public void Main()
        {
            Assert.NotNull(MyObject);
            Console.WriteLine("Name: {0}", MyObject.ObjectName);
        }

        protected override string[] ConfigLocations
        {
            get { return new[] { "objects.xml" }; }
        }
    }

    /// <summary>
    /// Example demonstrating unit testing using autowire by type.
    /// </summary>
    /// <remarks>
    /// This is the default and will work for all public properties.
    /// If your property is not public, it will not be injected.
    /// </remarks>
    [TestFixture]
    public class AutoWireByTypeTests : AbstractDependencyInjectionSpringContextTests
    {
        public IMyInterface Sut { get; set; }
        protected IMyInterface Sut2 { get; set; }

        [Test]
        public void Main()
        {
            Assert.NotNull(Sut);
            Console.WriteLine("Name: {0}", Sut.ObjectName);
        }

        [Test]
        public void Sut2IsNull()
        {
            Assert.Null(Sut2);
        }

        protected override string[] ConfigLocations
        {
            get { return new[] { "objects.xml" }; }
        }
    }

    public class MyClass : IObjectNameAware, IMyInterface
    {
        public override string ToString()
        {
            return ObjectName;
        }

        public string ObjectName { get; set; }
    }

    public interface IMyInterface
    {
        string ObjectName { get; }
    }


}
