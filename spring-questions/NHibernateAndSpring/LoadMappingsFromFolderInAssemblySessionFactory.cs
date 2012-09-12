using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate.Cfg;
using Spring.Data.NHibernate;

namespace NHibernateAndSpring
{
    public class LoadMappingsFromFolderInAssemblySessionFactory : LocalSessionFactoryObject
    {
        public string MyMappingAssembly { get; set; }
        public string MyFolder { get; set; }

        protected override void PostProcessConfiguration(Configuration config)
        {
            base.PostProcessConfiguration(config);
            // guard methods omitted
            Assembly asm = GetAssembly(MyMappingAssembly);
            IEnumerable<string> paths = GetResourcePaths(MyFolder, asm);
            config.AddResources(paths, asm);
        }

        private IEnumerable<string> GetResourcePaths(string myFolder, Assembly asm)
        {
            throw new NotImplementedException();
        }

        private Assembly GetAssembly(string myMappingAssembly)
        {
            throw new NotImplementedException();
        }
    }
}