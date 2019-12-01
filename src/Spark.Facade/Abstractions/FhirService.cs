using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Spark.Engine.Core;
using Spark.Engine.FhirResponseFactory;
using Spark.Engine.Service;
using Spark.Engine.Service.FhirServiceExtensions;
using Spark.Engine.Storage;
using Spark.Service;
using System;
using System.Collections.Generic;

namespace Spark.Facade
{
    public abstract class FhirService : ExtendableWith<IFhirServiceExtension>, IFhirService
    {
        protected readonly IFhirResponseFactory _responseFactory;
        protected readonly ITransfer _transfer;
        protected readonly ICompositeServiceListener _serviceListener;

        protected FhirService(IFhirServiceExtension[] extensions,
            IFhirResponseFactory responseFactory,
            ITransfer transfer,
            ICompositeServiceListener serviceListener = null)
        {
            _responseFactory = responseFactory;
            _transfer = transfer;
            _serviceListener = serviceListener;

            foreach (IFhirServiceExtension serviceExtension in extensions)
            {
                AddExtension(serviceExtension);
            }
        }

        protected T GetFeature<T>() where T : IFhirServiceExtension
        {
            //TODO: return 501 - 	Requested HTTP operation not supported?

            T feature = this.FindExtension<T>();
            if (feature == null)
                throw new NotSupportedException("Operation not supported");

            return feature;
        }

        internal Entry Store(Entry entry)
        {
            Entry result = GetFeature<IResourceStorageService>()
             .Add(entry);
            _serviceListener.Inform(entry);
            return result;
        }

        protected static void ValidateKey(IKey key, bool withVersion = false)
        {
            Validate.HasTypeName(key);
            Validate.HasResourceId(key);
            if (withVersion)
            {
                Validate.HasVersion(key);
            }
            else
            {
                Validate.HasNoVersion(key);
            }
            Validate.Key(key);
        }

        public virtual FhirResponse AddMeta(IKey key, Parameters parameters)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse CapabilityStatement(string sparkVersion)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse ConditionalCreate(IKey key, Resource resource, IEnumerable<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse ConditionalCreate(IKey key, Resource resource, SearchParams parameters)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse ConditionalDelete(IKey key, IEnumerable<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse ConditionalUpdate(IKey key, Resource resource, SearchParams _params)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Create(IKey key, Resource resource)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Delete(IKey key)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Delete(Entry entry)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Document(IKey key)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Everything(IKey key)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse GetPage(string snapshotkey, int index)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse History(HistoryParameters parameters)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse History(string type, HistoryParameters parameters)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse History(IKey key, HistoryParameters parameters)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Mailbox(Bundle bundle, Binary body)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Put(IKey key, Resource resource)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Put(Entry entry)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Read(IKey key, ConditionalHeaderParameters parameters = null)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse ReadMeta(IKey key)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Search(string type, SearchParams searchCommand, int pageIndex = 0)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Transaction(IList<Entry> interactions)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Transaction(Bundle bundle)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse Update(IKey key, Resource resource)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse ValidateOperation(IKey key, Resource resource)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse VersionRead(IKey key)
        {
            throw new NotImplementedException();
        }

        public virtual FhirResponse VersionSpecificUpdate(IKey versionedkey, Resource resource)
        {
            throw new NotImplementedException();
        }
    }
}
