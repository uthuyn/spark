using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Spark.Engine.Core;
using Spark.Engine.FhirResponseFactory;
using Spark.Engine.Service;
using Spark.Engine.Service.FhirServiceExtensions;
using Spark.Service;
using System.IO;
using System.Net;

namespace Spark.Facade.Services
{
    public class PatientService : FhirService
    {
        private readonly FileStoreSettings _storeSettings;

        public PatientService(FileStoreSettings storeSettings, 
            IFhirServiceExtension[] extensions, 
            IFhirResponseFactory responseFactory, 
            ITransfer transfer,
            ICompositeServiceListener serviceListener = null) : base(extensions, responseFactory, transfer, serviceListener)
        {
            _storeSettings = storeSettings;
        }

        public override FhirResponse Read(IKey key, ConditionalHeaderParameters parameters = null)
        {
            ValidateKey(key);
            Entry entry = GetFeature<IResourceStorageService>().Get(key);

            return _responseFactory.GetFhirResponse(entry, key, parameters);
        }

        public override FhirResponse Create(IKey key, Resource resource)
        {
            Validate.Key(key);
            Validate.HasTypeName(key);
            Validate.ResourceType(key, resource);

            Validate.HasNoResourceId(key);
            Validate.HasNoVersion(key);

            Entry result = Store(Entry.POST(key, resource));

            return Respond.WithResource(HttpStatusCode.Created, result);
        }
    }
}
