using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Spark.Engine.Service.FhirServiceExtensions;
using System;

namespace Spark.Engine.Terminology
{
    public class ExternalTerminologyService : Hl7.Fhir.Specification.Terminology.ExternalTerminologyService, ITerminologyService
    {
        public ExternalTerminologyService(IFhirClient client) : base(client)
        {
        }

        public Resource ValidateCode(Parameters parameters, string typeName, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(typeName)) throw Error.ArgumentNullOrEmpty(nameof(typeName));
            if (typeName != FHIRAllTypes.CodeSystem.GetLiteral() && typeName != FHIRAllTypes.ValueSet.GetLiteral())
                throw Error.Argument(nameof(Type), "Valid values for argument typeName is 'CodeSystem' and 'ValueSet'");

            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperation.VALIDATE_CODE, typeName, parameters, useGet);
            else
                return Endpoint.InstanceOperation(new Uri($"{typeName}/{id}"), RestOperation.VALIDATE_CODE, parameters, useGet);
        }

        public Resource Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperation.EXPAND_VALUESET, FHIRAllTypes.ValueSet.GetLiteral(), parameters, useGet);
            else
                return Endpoint.InstanceOperation(new Uri($"{FHIRAllTypes.ValueSet.GetLiteral()}/{id}"), RestOperation.EXPAND_VALUESET, parameters, useGet);
        }

        public Resource Lookup(Parameters parameters, bool useGet = false)
        {
            return Endpoint.TypeOperation<CodeSystem>(RestOperation.CONCEPT_LOOKUP, parameters, useGet);
        }

        public Resource Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperation.TRANSLATE, FHIRAllTypes.ConceptMap.GetLiteral(), parameters, useGet);
            else
                return Endpoint.InstanceOperation(new Uri($"{FHIRAllTypes.ConceptMap.GetLiteral()}/{id}"), RestOperation.TRANSLATE, parameters, useGet);
        }

        public Resource Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperationCustom.SUBSUMES, FHIRAllTypes.CodeSystem.GetLiteral(), parameters, useGet);
            else
                return Endpoint.InstanceOperation(new Uri($"{FHIRAllTypes.CodeSystem.GetLiteral()}/{id}"), RestOperationCustom.SUBSUMES, parameters, useGet);
        }

        public Resource Closure(Parameters parameters, bool useGet = false)
        {
            return Endpoint.WholeSystemOperation(RestOperationCustom.CLOSURE, parameters);
        }
    }

    public class RestOperationCustom
    {
        /// <summary>
        /// "closure" operation
        /// </summary>
        public const string CLOSURE = "closure";

        /// <summary>
        /// "subsumes" operation
        /// </summary>
        public const string SUBSUMES = "subsumes";
    }
}
