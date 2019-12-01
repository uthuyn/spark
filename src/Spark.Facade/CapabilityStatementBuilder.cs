using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Spark.Facade
{
    public class CapabilityStatementBuilder
    {
        public static CapabilityStatement CreateServer(string server, string serverVersion, string publisher, FHIRVersion fhirVersion)
        {
            CapabilityStatement capabilityStatement = new CapabilityStatement();
            capabilityStatement.Name = server;
            capabilityStatement.Publisher = publisher;
            capabilityStatement.Version = serverVersion;
            capabilityStatement.FhirVersion = fhirVersion;
            capabilityStatement.Date = Date.Today().Value;
            capabilityStatement.AddServer();

            return capabilityStatement;
        }
    }

    public static class CapabilityStatementBuilderExtensions
    {
        public static CapabilityStatement AddServer(this CapabilityStatement capabilityStatement)
        {
            capabilityStatement.AddRestComponent(isServer: true);

            return capabilityStatement;
        }

        public static CapabilityStatement.RestComponent AddRestComponent(this CapabilityStatement capabilityStatement, Boolean isServer, Markdown documentation = null)
        {
            var server = new CapabilityStatement.RestComponent
            {
                Mode = (isServer) ? CapabilityStatement.RestfulCapabilityMode.Server : CapabilityStatement.RestfulCapabilityMode.Client
            };

            if (documentation != null)
            {
                server.Documentation = documentation;
            }
            capabilityStatement.Rest.Add(server);

            return server;
        }

        public static CapabilityStatement AddSingleResourceComponent(this CapabilityStatement capabilityStatement, ResourceType resourcetype, Boolean readhistory = false, Boolean updatecreate = false, CapabilityStatement.ResourceVersionPolicy versioning = CapabilityStatement.ResourceVersionPolicy.NoVersion, Canonical profile = null)
        {
            var resource = new CapabilityStatement.ResourceComponent
            {
                Type = resourcetype,
                Profile = profile,
                ReadHistory = readhistory,
                UpdateCreate = updatecreate,
                Versioning = versioning
            };
            capabilityStatement.Server().Resource.Add(resource);

            return capabilityStatement;
        }

        public static CapabilityStatement AddInteraction(this CapabilityStatement capabilityStatement, ResourceType resourceType, params CapabilityStatement.TypeRestfulInteraction[] interactionType)
        {
            CapabilityStatement.ResourceComponent component = capabilityStatement.Server().Resource.SingleOrDefault(r => r.Type == resourceType);
            if (component != null)
            {
                foreach (CapabilityStatement.TypeRestfulInteraction interaction in interactionType)
                {
                    component.Interaction.Add(
                        new CapabilityStatement.ResourceInteractionComponent
                        {
                            Code = interaction
                        });
                }
            }

            return capabilityStatement;
        }

        public static CapabilityStatement.RestComponent Server(this CapabilityStatement capabilityStatement)
        {
            var server = capabilityStatement.Rest.FirstOrDefault(r => r.Mode == CapabilityStatement.RestfulCapabilityMode.Server);
            return (server == null) ? capabilityStatement.AddRestComponent(isServer: true) : server;
        }

        public static void AddSystemInteraction(this CapabilityStatement capabilityStatement, CapabilityStatement.SystemRestfulInteraction code)
        {
            var interaction = new CapabilityStatement.SystemInteractionComponent
            {
                Code = code
            };

            capabilityStatement.Rest().Interaction.Add(interaction);
        }

        public static CapabilityStatement.RestComponent Rest(this CapabilityStatement capabilityStatement)
        {
            return capabilityStatement.Rest.FirstOrDefault();
        }
    }
}
