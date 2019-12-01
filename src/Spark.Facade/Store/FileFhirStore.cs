using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Spark.Engine;
using Spark.Engine.Core;
using Spark.Engine.Store.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Spark.Facade.Store
{
    public class FileFhirStore : IFhirStore
    {
        private readonly FileStoreSettings _storeSettings;
        private readonly SparkSettings _sparkSettings;

        public FileFhirStore(FileStoreSettings storeSettings, SparkSettings sparkSettings)
        {
            _storeSettings = storeSettings;
            _sparkSettings = sparkSettings;
        }

        public void Add(Entry entry)
        {
            FileDocument document = entry.ToFileDocument();
            using (var stream = new StreamWriter(File.OpenWrite(Path.Combine(_storeSettings.Path, document.FileName))))
            {
                stream.Write(document.Content);
            }
        }

        public Entry Get(IKey key)
        {
            string path = Path.Combine(_storeSettings.Path, $"{key.TypeName}-{key.ResourceId}.json");
            if (!File.Exists(path)) throw new SparkException(HttpStatusCode.NotFound);

            Resource resource;
            using (var stream = new StreamReader(File.OpenRead(path)))
            {
                FhirJsonParser parser = new FhirJsonParser(_sparkSettings.ParserSettings);
                resource = parser.Parse<Patient>(stream.ReadToEnd());
            }

            return resource.ToEntry();
        }

        public IList<Entry> Get(IEnumerable<IKey> localIdentifiers)
        {
            throw new NotImplementedException();
        }
    }
}
