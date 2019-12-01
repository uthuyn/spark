using Hl7.Fhir.Model;
using Spark.Engine.Core;
using Spark.Engine.FhirResponseFactory;
using Spark.Engine.Service;
using Spark.Engine.Service.FhirServiceExtensions;
using Spark.Service;

namespace Spark.Facade.Services
{
    public class SystemService : FhirService
    {
        public SystemService(IFhirServiceExtension[] extensions, 
            IFhirResponseFactory responseFactory, 
            ITransfer transfer, 
            ICompositeServiceListener serviceListener = null) 
            : base(extensions, responseFactory, transfer, serviceListener)
        {
        }

        public override FhirResponse CapabilityStatement(string sparkVersion)
        {
            var capabilityStatement = CapabilityStatementBuilder
                .CreateServer("Spark Facade", sparkVersion, "Kufu", FHIRVersion.N0_4_0)
                .AddSingleResourceComponent(ResourceType.Patient)
                .AddInteraction(ResourceType.Patient,
                    Hl7.Fhir.Model.CapabilityStatement.TypeRestfulInteraction.Read,
                    Hl7.Fhir.Model.CapabilityStatement.TypeRestfulInteraction.Create);

            return Respond.WithResource(capabilityStatement);
        }
    }
}
