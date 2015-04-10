using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using StructureMap.Configuration.DSL;
using StructureMap.Web.Pipeline;

namespace Proto2.DependencyResolution
{
    public class RavenRegistry : Registry
    {
        public RavenRegistry()
        {
            For<IDocumentStore>()
                .Singleton().Use(() => CreateDocumentStore());

            For<IDocumentSession>()
                .LifecycleIs<HttpContextLifecycle>()
                .Use(c => c.GetInstance<IDocumentStore>().OpenSession());
            
            Policies.SetAllProperties(c => c.OfType<IDocumentSession>());
        }

        private IDocumentStore CreateDocumentStore()
        {
            var store = new DocumentStore {ConnectionStringName = "RavenHQ"}.Initialize();
            return store;
        }
    }
}