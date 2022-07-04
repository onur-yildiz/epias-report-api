using MongoDB.Bson;
using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.EpiasReports.Models;
using System.Linq.Expressions;

namespace SP.DAL
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : MongoDbEntity
    {
        protected readonly IMongoCollection<T> Collection;

        protected GenericRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<T>(collectionName);
        }

        public virtual IEnumerable<T> Get() => Collection.Find(_ => true).ToEnumerable();
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate) => Collection.Find(predicate).ToEnumerable();
        public virtual T? GetFirst(Expression<Func<T, bool>> predicate) => Collection.Find(predicate).FirstOrDefault();
        public virtual T? GetById(ObjectId id) => Collection.Find(x => x.Id == id).FirstOrDefault();
        public virtual T? GetById(string id) => this.GetById(ObjectId.Parse(id));
        public virtual void AddOne(T entity) => Collection.InsertOne(entity);
        public virtual T? RemoveOne(ObjectId id) => Collection.FindOneAndDelete(x => x.Id == id);
        public virtual T? RemoveOne(string id) => this.RemoveOne(ObjectId.Parse(id));
        public virtual T? RemoveOne(Expression<Func<T, bool>> predicate) => Collection.FindOneAndDelete(predicate);
        public virtual T? UpdateOne_Set<U>(ObjectId id, string field, U value) => Collection.FindOneAndUpdate(u => u.Id == id, Builders<T>.Update.Set(field, value));
        public virtual T? UpdateOne_Set<U>(string id, string field, U value) => this.UpdateOne_Set(ObjectId.Parse(id), field, value);
        public virtual T? UpdateOne_Set<U>(Expression<Func<T, bool>> predicate, string field, U value) => Collection.FindOneAndUpdate(predicate, Builders<T>.Update.Set(field, value));
        public virtual UpdateResult UpdateMany_Pull<U>(Expression<Func<T, bool>> predicate, string field, U value) => Collection.UpdateMany(predicate, Builders<T>.Update.Pull(field, value));
        public virtual T? ReplaceOne_Upsert(Expression<Func<T, bool>> predicate, T replacement) => Collection.FindOneAndReplace(predicate, replacement, new FindOneAndReplaceOptions<T, T> { IsUpsert = true });
    }
}
