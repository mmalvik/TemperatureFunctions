using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using TemperatureFunctions.Dto;

namespace TemperatureFunctions.CQRS
{
    public class CommandExecutor
    {
        private const string DatabaseName = "TemperatureDB";
        private const string CollectionName = "Temperatures";

        private readonly DocumentClient _documentClient;
        private readonly Uri _documentCollectionUri;

        public CommandExecutor()
        {
            _documentClient = new DocumentClient
                (new Uri(ConfigurationManager.AppSettings["CosmosDbUri"]), ConfigurationManager.AppSettings["CosmosDbPrimaryKey"]);
            _documentCollectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
        }

        public async Task Handle(TemperatureRegistration temperatureRegistration)
        {
            await _documentClient.CreateDocumentAsync(_documentCollectionUri, temperatureRegistration);
        }
    }
}