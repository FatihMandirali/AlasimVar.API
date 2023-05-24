using AlasimVar.Domain.Entities;
using Nest;

namespace AlasimVar.Application.IServices;

public interface IElasticsearchService
{
    Task ChekIndex(string indexName);
    Task InsertDocument(string indexName, User product);
    Task InsertDocuments(string indexName, List<User> products);
    Task<User> GetDocument(string indexName, int id);
    Task<List<User>> Query(string indexName, QueryContainer predicate);
    Task<List<User>> GetDocuments(string indexName);
    Task BulkInsert(string indexName, List<User> userList);
}