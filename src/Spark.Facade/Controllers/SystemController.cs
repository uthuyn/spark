using Microsoft.AspNetCore.Mvc;
using Spark.Engine;
using Spark.Engine.Core;
using Spark.Facade.Services;
using System.Web.Http;

namespace Spark.Facade.Controllers
{
    [Route("fhir")]
    public class SystemController : ApiController
    {
        private readonly SystemService _systemService;
        private readonly SparkSettings _settings;

        public SystemController(SystemService systemService, SparkSettings settings)
        {
            _systemService = systemService;
            _settings = settings;
        }

        [HttpGet, Route("metadata")]
        public FhirResponse Metadata()
        {
            return _systemService.CapabilityStatement(_settings.Version);
        }
    }
}
