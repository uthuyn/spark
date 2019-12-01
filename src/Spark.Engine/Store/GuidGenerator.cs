using Hl7.Fhir.Model;
using Spark.Core;
using System;

namespace Spark.Engine.Storage
{
    public class GuidGenerator : IGenerator
    {
        private readonly string _formatSpecifier;

        public GuidGenerator(string formatSpecifier = "D")
        {
            _formatSpecifier = formatSpecifier;
        }
        public virtual string NextResourceId(Resource resource)
        {
            return Guid.NewGuid().ToString(_formatSpecifier);
        }

        public virtual string NextVersionId(string resourceIdentifier)
        {
            return null;
        }

        public virtual string NextVersionId(string resourceType, string resourceIdentifier)
        {
            return null;
        }
    }
}
