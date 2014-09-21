using System;
using System.Diagnostics;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Embedded;

namespace WhyIsMyFirstSaveSoSlowRaven20
{
    /// <summary>
    /// This test fixture demonstrates our problem in an isolated way:
    /// 
    /// Persisting schedule took 189 ms. -- first time
    /// Persisting schedule took 2 ms.   -- second time
    /// Persisting schedule took 0 ms.   -- ... etc
    /// Persisting schedule took 0 ms.
    /// Persisting schedule took 0 ms.
    /// Persisting schedule took 0 ms.
    /// Persisting schedule took 0 ms.
    /// Persisting schedule took 0 ms.
    /// Persisting schedule took 0 ms.
    /// Persisting schedule took 1 ms.
    /// 
    /// Similar behavior occurs when running against a remote DocumentStore (resembling more closely our real situation):
    /// 
    /// Persisting schedule took 1116 ms. -- why am I occuring this hit everytime?
    /// Persisting schedule took 37 ms.
    /// Persisting schedule took 14 ms.
    /// Persisting schedule took 23 ms.
    /// Persisting schedule took 85 ms.
    /// Persisting schedule took 6 ms.
    /// Persisting schedule took 9 ms.
    /// Persisting schedule took 7 ms.
    /// Persisting schedule took 28 ms.
    /// Persisting schedule took 9 ms.
    /// 
    /// This also holds when the collection already exists.
    /// 
    /// </summary>
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