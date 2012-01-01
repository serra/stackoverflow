using System;
using NUnit.Framework;
using Spring.Context.Support;

namespace q8689750
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Main()
        {
            var ctx = new XmlApplicationContext("objects.xml");
            var drive = ctx.GetObject("Drive");
            Console.WriteLine(drive);

            var genImp = ctx.GetObject("GenericImplementation");
            Console.WriteLine(genImp);

            var genUsr = ctx.GetObject("GenUser");
            Console.WriteLine(genUsr);

        }

        [Test]
        public void ConstructFromCode()
        {
            var drive = new Drive();
            var genImp = new GenericImplementation();
            genImp.DriveImplementation = drive;

            var genUser = new GenericUser();
            genUser.GenericImplementation = genImp;
        }
    }

    public interface ICar
    {
    }

    public class GenericImplementation : IGeneric<ICar>
    {
        public IDrive<ICar> DriveImplementation { get; set; }
    }

    public interface IGeneric<T>
    {
    }

    public interface IUser
    {
    }

    public interface IDrive<T>
    {
        void Drive(T vehicle);
    }

    public class Drive : IDrive<ICar>
    {
        void IDrive<ICar>.Drive(ICar car)
        {
        }
    }

    public class GenericUser : IUser
    {
        public IGeneric<ICar> GenericImplementation { get; set; }
    }
}