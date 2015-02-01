using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using StructureMap.Configuration.DSL;
using StructureMap.Web.Pipeline;

namespace Proto.DependencyResolution
{
    public class RavenRegistry : Registry
    {
        public RavenRegistry()
        {
            var store = new DocumentStore { ConnectionStringName = "RavenDB" }.Initialize();
            IndexCreation.CreateIndexes(this.GetType().Assembly, store);

            For<IDocumentStore>()
                .Singleton().Use(store);

            For<IDocumentSession>()
                .LifecycleIs<HttpContextLifecycle>()
                .Use(store.OpenSession());

            Policies
                .FillAllPropertiesOfType<IDocumentSession>()
                .Use(c => c.GetInstance<IDocumentSession>());
        }
    }
}