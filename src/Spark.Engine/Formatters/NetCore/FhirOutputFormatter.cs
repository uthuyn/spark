#if NETSTANDARD2_0
using Microsoft.AspNetCore.Mvc.Formatters;
using Spark.Engine.Core;
using Spark.Engine.Extensions;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Engine.Formatters
{
    public class FhirOutputFormatter : TextOutputFormatter
    {
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (typeof(FhirResponse).IsAssignableFrom(context.ObjectType))
            {
                context.HttpContext.Response.AcquireHeaders(context.Object as FhirResponse);
            }

            return Task.CompletedTask;
        }
    }
}
#endif