using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Di.Examples;
using NUnit.Framework;

namespace di
{
    // insipiration: http://stackoverflow.com/questions/11936862/windsor-transient-lifestyle

    [TestFixture]
    public class LifeStyleTests
    {
        [Test]
        public void ANewPersonHasAUniqueId()
        {
            var p1 = new Person();
            var p2 = new Person();
            Assert.AreNotEqual(p1.Id, p2.Id);
        }

        [Test]
        public void TransientPersonsHaveUniqueIds()
        {
            using (var c = new WindsorContainer())
            {
                c.Register(Component.For<Person>().ImplementedBy<Person>().LifestyleTransient());

                var p1 = c.Resolve<Person>();
                var p2 = c.Resolve<Person>();

                Assert.AreNotEqual(p1.Id, p2.Id);
            }
        }

        [Test]
        public void SingletonPersonsHaveUniqueIds()
        {
            using (var c = new WindsorContainer())
            {
                c.Register(Component.For<Person>().ImplementedBy<Person>());

                var p1 = c.Resolve<Person>();
                var p2 = c.Resolve<Person>();

                Assert.AreEqual(p1.Id, p2.Id);
            }
        }

        [Test]
        public void HandlingComponentModelCreatedCanSetLifestyle()
        {
            using (var c = new WindsorContainer())
            {
                c.Kernel.ComponentModelCreated += SetUndefinedLifeStyleToTransient;
                c.Register(Component.For<Person>().ImplementedBy<Person>());

                var p1 = c.Resolve<Person>();
                var p2 = c.Resolve<Person>();

                Assert.AreNotEqual(p1.Id, p2.Id);
            }
        }

        void SetUndefinedLifeStyleToTransient(Castle.Core.ComponentModel model)
        {
            if (model.LifestyleType == LifestyleType.Undefined)
                model.LifestyleType = LifestyleType.Transient;

        }
    }
}