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

        public Resource Read(string typeName, string id, Parameters parameters)
        {
            return Endpoint.Get($"{typeName}/{id}");
        }

        public Resource ReadCodeSystem(Parameters parameters, string id)
        {
            return Endpoint.Read<CodeSystem>($"{FHIRAllTypes.CodeSystem.GetLiteral()}/{id}");
        }


        public Resource ValidateCode(Parameters parameters, string typeName, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(typeName)) throw Error.ArgumentNullOrEmpty(nameof(typeName));
            if (typeName != FHIRAllTypes.CodeSystem.GetLiteral() && typeName != FHIRAllTypes.ValueSet.GetLiteral())
                throw Error.Argument(nameof(Type), "Valid values for argument typeName is 'CodeSystem' and 'ValueSet'");

            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperation.VALIDATE_CODE, typeName, parameters, useGet);
            else
                return Endpoint.InstanceOperation(ResourceIdentity.Build(typeName, id), RestOperation.VALIDATE_CODE, parameters, useGet);
        }

        public Resource Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation<ValueSet>(RestOperation.EXPAND_VALUESET, parameters, useGet);
            else
                return Endpoint.InstanceOperation(ResourceIdentity.Build(FHIRAllTypes.ValueSet.GetLiteral(), id), RestOperation.EXPAND_VALUESET, parameters, useGet);
        }

        public Resource Lookup(Parameters parameters, string id = null, bool useGet = false)
        {
            if(string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation<CodeSystem>(RestOperation.CONCEPT_LOOKUP, parameters, useGet);
            else
                return Endpoint.InstanceOperation(ResourceIdentity.Build(FHIRAllTypes.CodeSystem.GetLiteral(), id), RestOperation.CONCEPT_LOOKUP, parameters, useGet);
        }

        public Resource Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperation.TRANSLATE, FHIRAllTypes.ConceptMap.GetLiteral(), parameters, useGet);
            else
                return Endpoint.InstanceOperation(ResourceIdentity.Build(FHIRAllTypes.ConceptMap.GetLiteral(), id), RestOperation.TRANSLATE, parameters, useGet);
        }

        public Resource Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperationCustom.SUBSUMES, FHIRAllTypes.CodeSystem.GetLiteral(), parameters, useGet);
            else
                return Endpoint.InstanceOperation(ResourceIdentity.Build(FHIRAllTypes.CodeSystem.GetLiteral(), id), RestOperationCustom.SUBSUMES, parameters, useGet);
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
