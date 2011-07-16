using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace SmartTrack.Web.Configuration
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            For<IDocumentStore>().Singleton().Use(() => 
                new DocumentStore { Url = "http://localhost:8080" }.Initialize());

            For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(c => 
                c.GetInstance<IDocumentStore>().OpenSession());
        }
    }
}