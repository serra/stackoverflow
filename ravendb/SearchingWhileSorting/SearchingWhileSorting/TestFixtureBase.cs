using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Raven.Abstractions.Indexing;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace SearchingWhileSorting
{
    public class TestFixtureBase
    {
        protected EmbeddableDocumentStore DocumentStore;

        [SetUp]
        public void SetUpContext()
        {
            DocumentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };
            DocumentStore.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            DocumentStore.Dispose();
        }
    }

    public class UserComparer : IComparer<User>, IComparer
    {
        public int Compare(User x, User y)
        {
            return String.CompareOrdinal(x.Name, y.Name);
        }

        public int Compare(object x, object y)
        {
            return Compare((User)x, (User)y);
        }
    }

    public class UserSortIndex : AbstractIndexCreationTask<User>
    {
        public UserSortIndex()
        {
            Map = dtos => from dto in dtos select new { dto.Name };
            Index(dto => dto.Name, FieldIndexing.NotAnalyzed);
            Sort(x => x.Name, SortOptions.String);
        }
    }

    public class UserSearchIndex : AbstractIndexCreationTask<User>
    {
        public UserSearchIndex()
        {
            Map = dtos => from dto in dtos select new { dto.Name };
            Index(dto => dto.Name, FieldIndexing.Analyzed);
            Analyze(dto => dto.Name, "Lucene.Net.Analysis.Standard.StandardAnalyzer");
        }
    }
    
    public class UserSearchAsQueryIndex : AbstractIndexCreationTask<User, UserSearchAsQueryIndex.ReduceResult>
    {
        public class ReduceResult
        {
            public string Query { get; set; }
        }

        public UserSearchAsQueryIndex()
        {
            Map = dtos => from dto in dtos select new { Query = dto.Name };
            Index(dto => dto.Query, FieldIndexing.Analyzed);
            Analyze(dto => dto.Query, "Lucene.Net.Analysis.Standard.StandardAnalyzer");
        }
    }

    public class User
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("User '{0}'", Name);
        }
    }
}
