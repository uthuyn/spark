using Hl7.Fhir.Model;
using Spark.Engine.Core;
using Spark.Facade.Store;
using System;

namespace Spark.Facade
{
    internal static class ResourceHelper
    {
        internal static FileDocument ToFileDocument(this Entry entry)
        {
            //AddMetaData(entry);
            return FileDocument.ToFileDocument(entry.Resource);
        }

        internal static Entry ToEntry(this Resource resource)
        {
            if (resource == null) return null;

            Entry entry = ExtractMetadata(resource);
            entry.Resource = resource;

            return entry;
        }

        private static FileDocument CreateDocument(Resource resource)
        {
            if (resource == null) return null; // TODO: Better handling, look at SparkBsonHelper.CreateDocument

            return FileDocument.ToFileDocument(resource);
        }

        private static void AddMetaData(Entry entry)
        {
            if (entry.Resource.Meta == null)
                entry.Resource.Meta = new Meta { LastUpdated = DateTimeOffset.UtcNow };
            else
                entry.Resource.Meta.LastUpdated = DateTimeOffset.UtcNow;
        }

        private static void AssertKeyIsValid(IKey key)
        {
            bool valid = (key.Base == null) && (key.TypeName != null) && (key.ResourceId != null) && (key.VersionId != null);
            if (!valid)
            {
                throw new Exception("This key is not valid for storage: " + key.ToString());
            }
        }

        internal static Entry ExtractMetadata(Resource resource)
        {
            if (resource == null) return null;

            DateTimeOffset when = GetVersionDate(resource);
            IKey key = GetKey(resource);

            return Entry.Create(Bundle.HTTPVerb.PUT, key, when);
        }

        internal static DateTimeOffset GetVersionDate(Resource resource)
        {
            DateTimeOffset? versionDate = null;
            if(resource.Meta != null)
            {
                versionDate = resource.Meta.LastUpdated;
            }

            return versionDate ?? DateTimeOffset.UtcNow;
        }

        internal static IKey GetKey(Resource resource)
        {
            return new Key
            {
                TypeName = resource.TypeName,
                ResourceId = resource.Id,
                VersionId = resource.Meta?.VersionId
            };
        }
    }
}
