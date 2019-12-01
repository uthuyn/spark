using System.Web.Http;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;
using Spark.Engine;
using Spark.Engine.Core;
using Spark.Engine.Extensions;
using Spark.Facade.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spark.Facade
{
    [Route("fhir/[controller]")]
    public class PatientController : ApiController
    {
        private readonly PatientService _patientService;
        private readonly SparkSettings _settings;

        public PatientController(PatientService fhirService, SparkSettings settings)
        {
            _patientService = fhirService;
            _settings = settings;
        }

        [HttpGet("{id}")]
        public ActionResult<FhirResponse> Read(string id)
        {
            ConditionalHeaderParameters parameters = new ConditionalHeaderParameters(Request);
            Key key = Key.Create(ResourceTypes.Patient, id);
            return new ActionResult<FhirResponse>(_patientService.Read(key, parameters));
        }

        [HttpPost]
        public ActionResult<FhirResponse> Create([FromBody] Resource resource)
        {
            Key key = Key.Create(ResourceTypes.Patient, resource?.Id);

            return _patientService.Create(key, resource);
        }
    }
}
