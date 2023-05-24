using AlasimVar.Application.IServices;
using AlasimVar.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Nest;

namespace AlasimVar.Infrastructure.Services;

public class ElasticsearchService:IElasticsearchService
{
    private readonly IConfiguration _configuration;
        private readonly IElasticClient _client;

        public ElasticsearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateInstance();
        }

        private ElasticClient CreateInstance()
        {
            string host = _configuration.GetSection("ElasticConfiguration:Uri").Value;

            var settings = new ConnectionSettings(new Uri(host));

            if (!string.IsNullOrEmpty("") && !string.IsNullOrEmpty(""))
                settings.BasicAuthentication("", "");

            return new ElasticClient(settings);
        }


        public async Task ChekIndex(string indexName)
        {
            var anyy = await _client.Indices.ExistsAsync(indexName);
            if (anyy.Exists)
                return;

            var response = await _client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );

            return;

        }

        public async Task InsertDocument(string indexName, User product)
        {

            var response = await _client.CreateAsync(product, q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<User>(product.Id, a => a.Index(indexName).Doc(product));
            }

        }

        public async Task InsertDocuments(string indexName, List<User> products)
        {
            await _client.IndexManyAsync(products, index: indexName);
        }


        public async Task<User> GetDocument(string indexName, int id)
        {
            var response = await _client.GetAsync<User>(id, q => q.Index(indexName));

            return response.Source;

        }
        
        public async Task<List<User>> Query(string indexName, QueryContainer predicate)
        {
            var searchResponse = await _client.SearchAsync<User>(s => s.Index(indexName)
                .Query(q => predicate));
            return searchResponse.Documents.ToList();
        }

        public async Task<List<User>> GetDocuments(string indexName)
        {
            var response = await _client.SearchAsync<User>(q => q.Index(indexName).Scroll("5m"));
            return response.Documents.ToList();
        }

        public async Task BulkInsert(string indexName,List<User> userList)
        {
            var descriptor = new BulkDescriptor();

            foreach (var i in userList)
            {
                descriptor.Index<User>(op => op
                    .Document(new User() {Id = i.Id})
                );
            }

            await _client.BulkAsync(descriptor);
        }
}