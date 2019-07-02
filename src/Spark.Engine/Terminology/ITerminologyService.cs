using Hl7.Fhir.Model;
using Spark.Engine.Service.FhirServiceExtensions;

namespace Spark.Engine.Terminology
{
    public interface ITerminologyService : Hl7.Fhir.Specification.Terminology.ITerminologyService, IFhirServiceExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Resource Read(string typeName, string id, Parameters parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="typeName"></param>
        /// <param name="id"></param>
        /// <param name="useGet"></param>
        /// <returns></returns>
        Resource ValidateCode(Parameters parameters, string typeName, string id = null, bool useGet = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="id"></param>
        /// <param name="useGet"></param>
        /// <returns></returns>
        Resource Expand(Parameters parameters, string id = null, bool useGet = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="useGet"></param>
        /// <returns></returns>
        Resource Lookup(Parameters parameters, string id = null, bool useGet = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="id"></param>
        /// <param name="useGet"></param>
        /// <returns></returns>
        Resource Translate(Parameters parameters, string id = null, bool useGet = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="id"></param>
        /// <param name="useGet"></param>
        /// <returns></returns>
        Resource Subsumes(Parameters parameters, string id = null, bool useGet = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="useGet"></param>
        /// <returns></returns>
        Resource Closure(Parameters parameters, bool useGet = false);
    }
}
