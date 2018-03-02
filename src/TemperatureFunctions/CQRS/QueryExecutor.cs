using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace TemperatureFunctions.CQRS
{
    public class QueryExecutor
    {
        private const string DatabaseName = "TemperatureDB";
        private const string CollectionName = "Temperatures";

        private readonly DocumentClient _documentClient;
        private readonly Uri _documentCollectionUri;

        public QueryExecutor()
        {
            _documentClient = new DocumentClient(new Uri(ConfigurationManager.AppSettings["CosmosDbUri"]),
                ConfigurationManager.AppSettings["CosmosDbPrimaryKey"]);
            _documentCollectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
        }

        public async Task<IEnumerable<T>> Execute<T>(SqlQuerySpec querySpec)
        {
            var queryResult = new List<T>();
            var querySetup = _documentClient.CreateDocumentQuery<T>(_documentCollectionUri, querySpec).AsDocumentQuery();

            while (querySetup.HasMoreResults)
            {
                queryResult.AddRange(await querySetup.ExecuteNextAsync<T>());
            }

            return queryResult.AsEnumerable();
        }
    }
}