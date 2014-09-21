using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Embedded;

namespace WhyIsMyFirstSaveSoSlowRaven25
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class CreateScheduleRavenOnlyPerformanceTests
    {
        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void StoreNSchedules(int n)
        {
            StoreDtos(n);
        }

        private void StoreDtos(int n)
        {
            for (int i = 0; i < n; i++)
            {
                StoreNewSchedule();
            }
        }

        private void StoreNewSchedule()
        {
            var sw = Stopwatch.StartNew();
            using (var session = DocumentStore.OpenSession())
            {
                session.Store(NewSchedule());
                session.SaveChanges();
            }

            Console.WriteLine("Persisting schedule took {0} ms.", sw.ElapsedMilliseconds);
        }

        private Schedule NewSchedule()
        {
            var id = Guid.NewGuid();
            var name = string.Format("new schedule {0}", id.ToString().Substring(0, 5));
            return new Schedule { Id = id, Name = name };
        }

        public class Schedule
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }


        protected IDocumentStore DocumentStore;

        [SetUp]
        public void SetUpContext()
        {
            DocumentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };
            // DocumentStore = new DocumentStore() {ConnectionStringName = "RavenDB"}; 
            DocumentStore.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            DocumentStore.Dispose();
        }
    }

}
