﻿using System;
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
            var drive = ctx.GetObject("driver");
            Console.WriteLine(drive);

            var genImp = ctx.GetObject("GenericImplementation");
            Console.WriteLine(genImp);

            var genUsr = ctx.GetObject("GenUser");
            Console.WriteLine(genUsr);

        }

        [Test]
        public void ConstructFromCode()
        {
            var drive = new Driver();
            var genImp = new GenericImplementation {DriveImplementation = drive};
            var genUser = new GenericUser {GenericImplementation = genImp};
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

    public interface IDrive<in T>
    {
        void Drive(T vehicle);
    }

    public class Driver : IDrive<ICar>
    {
        public void Drive(ICar car)
        {
        }
    }

    public class GenericUser : IUser
    {
        public IGeneric<ICar> GenericImplementation { get; set; }
    }
}