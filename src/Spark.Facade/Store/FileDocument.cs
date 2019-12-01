using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Spark.Facade.Store
{
    internal class FileDocument
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }

        public static FileDocument ToFileDocument(Resource resource)
        {
            // TODO: No handling of Resource.Id, could potentially be null and thereby corrupt FileName.
            FhirJsonSerializer serializer = new FhirJsonSerializer();
            return new FileDocument
            {
                FileName = $"{resource.TypeName}-{resource.Id}.json",
                Content = serializer.SerializeToString(resource),
                ContentType = Hl7.Fhir.Rest.ContentType.JSON_CONTENT_HEADER
            };
        }
    }
}
