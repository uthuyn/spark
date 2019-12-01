using Hl7.Fhir.Model;
using Spark.Core;
using System;

namespace Spark.Facade.Store
{
    public class GuidGenerator : IGenerator
    {
        public virtual string NextResourceId(Resource resource)
        {
            return Guid.NewGuid().ToString("N");
        }

        public virtual string NextVersionId(string resourceIdentifier)
        {
            throw new NotImplementedException();
        }

        public virtual string NextVersionId(string resourceType, string resourceIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
