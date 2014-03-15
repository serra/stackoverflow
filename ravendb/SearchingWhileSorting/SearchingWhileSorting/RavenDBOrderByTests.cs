using System;
using System.Linq;
using NUnit.Framework;
using Raven.Client.Indexes;
using Raven.Client.Linq;

namespace SearchingWhileSorting
{
    [TestFixture]
    public class RavenDBOrderByTests : TestFixtureBase
    {
        [Test]
        [TestCase(IndexOptions.None)]
        [TestCase(IndexOptions.Sort)]
        [TestCase(IndexOptions.Search)]
        [TestCase(IndexOptions.Sort | IndexOptions.Search)]
        public void OneWordSorts(IndexOptions indexOptions)
        {
            var users = new[]
                {
                    new User {Name = "Abc"},
                    new User {Name = "Ghi"},
                    new User {Name = "Def"},
                };

            SetIndicesAndAssertWeCanRetrieveUsersOrderedByName(indexOptions, users);
        }

        [Test]
        [TestCase(IndexOptions.None)]
        [TestCase(IndexOptions.Sort)]
        [TestCase(IndexOptions.Search)]
        [TestCase(IndexOptions.Sort | IndexOptions.Search)]
        public void TwoWordSorts(IndexOptions indexOptions)
        {
            var users = new[]
                {
                    new User {Name = "Abc Xyz"},
                    new User {Name = "Ghi Rst"},
                    new User {Name = "Def Uvw"},
                };

            SetIndicesAndAssertWeCanRetrieveUsersOrderedByName(indexOptions, users);
        }

        [Test]
        [TestCase(IndexOptions.None)]
        [TestCase(IndexOptions.Sort)]
        [TestCase(IndexOptions.Search)]
        [TestCase(IndexOptions.Sort | IndexOptions.Search)]
        public void TwoWordSortsSameStartingWord(IndexOptions indexOptions)
        {
            var users = new[]
                {
                    new User {Name = "Abc Xyz"},
                    new User {Name = "Abc Rst"},
                    new User {Name = "Abc Uvw"},
                };

            SetIndicesAndAssertWeCanRetrieveUsersOrderedByName(indexOptions, users);
        }

        [Test]
        public void TwoWordSortsSameStartingWord_UsingQueryIndex()
        {
            var users = new[]
                {
                    new User {Name = "Abc Xyz"},
                    new User {Name = "Abc Rst"},
                    new User {Name = "Abc Uvw"},
                };

            DocumentStore.ExecuteIndex(new UserSearchAsQueryIndex());

            StoreItems(users);

            using (var s = DocumentStore.OpenSession())
            {
                var query = s.Query<User, UserSearchAsQueryIndex>();
                query = query.OrderBy(dto => dto.Name);
                AssertRetrievedOrdered(query);
            }
        }

        private void SetIndicesAndAssertWeCanRetrieveUsersOrderedByName(IndexOptions indexOptions, User[] users)
        {
            PutIndex(indexOptions);

            StoreItems(users);

            using (var s = DocumentStore.OpenSession())
            {
                IRavenQueryable<User> query;
                if(indexOptions.HasFlag(IndexOptions.Sort))
                    query = s.Query<User, UserSortIndex>(); // use it if specified
                else
                    query = s.Query<User>();

                query = query.OrderBy(dto => dto.Name);
                AssertRetrievedOrdered(query);
            }
        }


        private void StoreItems(User[] users)
        {
            using (var s = DocumentStore.OpenSession())
            {
                foreach (var mc in users)
                {
                    s.Store(mc);
                }
                s.SaveChanges();
            }
        }

        private static void AssertRetrievedOrdered(IRavenQueryable<User> query)
        {
            var all = query
                .Customize(x => x.WaitForNonStaleResults())
                .ToList();
            foreach (var mc in all)
            {
                Console.WriteLine(mc);
            }

            CollectionAssert.IsOrdered(all, new UserComparer());
        }

        private void PutIndex(IndexOptions options)
        {
            if (options.HasFlag(IndexOptions.Search))
                DocumentStore.ExecuteIndex(new UserSearchIndex());
            if (options.HasFlag(IndexOptions.Sort))
                DocumentStore.ExecuteIndex(new UserSortIndex());
        }
    }

    [Flags]
    public enum IndexOptions
    {
        None = 0,
        Sort = 1,
        Search = 2
    }
}