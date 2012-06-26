using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spring.Context;
using Spring.Context.Support;

namespace PatternsAndRefactorings
{
    [TestFixture]
    public class AbstractFactoryTests
    {
        [Test]
        public void Main()
        {
            var ctx = new XmlApplicationContext("abstract-factory.xml");
            var o = (Consumer)ctx.GetObject("consumer");
            Assert.AreEqual("Illnois", o.Process("Illnois"));
            Assert.AreEqual("aksalA", o.Process("Alaska"));
            Assert.AreEqual("nisnocsiW", o.Process("Wisconsin"));
        }
    }

    public class Consumer
    {
        private readonly IStateAlgorithmFactory _factory;

        public Consumer(IStateAlgorithmFactory factory)
        {
            _factory = factory;
        }

        public string Process(string state)
        {
            var alg = _factory.Create(state);
            return alg.ProcessState(state);
        }
    }

    public interface IStateAlgorithmFactory
    {
        IStateAlgorithm Create(string state);
    }

    public class StateAlgorithmFactory : IStateAlgorithmFactory
    {
        private string[] _reverseStates = new[] {"Wisconsin", "Alaska"};

        public IStateAlgorithm Create(string state)
        {
            if(_reverseStates.Contains(state))
                return new ReverseEchoingStateAlgorithm();

            return new EchoingStateAlgorithm();
        }
    }

    public class LookupStateAlgorithmFactory : IStateAlgorithmFactory, IApplicationContextAware
    {
        private readonly IDictionary<string, string> _stateToAlgorithmMap;
        private readonly string _defaultObjectName;
        private IApplicationContext _ctxt;

        public LookupStateAlgorithmFactory(IDictionary<string,string> stateToAlgorithmMap, string defaultObjectName)
        {
            _stateToAlgorithmMap = stateToAlgorithmMap;
            _defaultObjectName = defaultObjectName;
        }

        public IStateAlgorithm Create(string state)
        {
            string objectName;
            if (!_stateToAlgorithmMap.TryGetValue(state, out objectName))
                objectName = _defaultObjectName;

            return (IStateAlgorithm) _ctxt[objectName];
        }

        public IApplicationContext ApplicationContext
        {
            set { _ctxt = value; }
        }
    }

    public interface IStateAlgorithm
    {
        string ProcessState(string stateName);
    }

    public class EchoingStateAlgorithm : IStateAlgorithm
    {
        public string ProcessState(string stateName)
        {
            return stateName;
        }
    }

    public class ReverseEchoingStateAlgorithm : IStateAlgorithm
    {
        public string ProcessState(string stateName)
        {
            return new string(stateName.Reverse().ToArray());
        }
    }

}