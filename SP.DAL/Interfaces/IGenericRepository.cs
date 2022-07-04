using MongoDB.Bson;
using MongoDB.Driver;
using SP.EpiasReports.Models;
using System.Linq.Expressions;

namespace SP.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : MongoDbEntity
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T? GetFirst(Expression<Func<T, bool>> predicate);
        T? GetById(ObjectId id);
        T? GetById(string id);
        void AddOne(T entity);
        T? RemoveOne(ObjectId id);
        T? RemoveOne(string id);
        T? RemoveOne(Expression<Func<T, bool>> predicate);
        T? UpdateOne_Set<U>(ObjectId id, string field, U value);
        T? UpdateOne_Set<U>(string id, string field, U value);
        T? UpdateOne_Set<U>(Expression<Func<T, bool>> predicate, string field, U value);
        UpdateResult UpdateMany_Pull<U>(Expression<Func<T, bool>> predicate, string field, U value);
        T? ReplaceOne_Upsert(Expression<Func<T, bool>> predicate, T replacement);
    }
}