using AspWebsite.Models;
using MongoDB.Driver;

namespace AspWebsite.Services
{
    /// <summary>
    /// CRUD Repository Service for mongodb.
    /// You can inherit this class to extend the function, or directly add service: <code>CRUDService&lt;DataModel></code>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CRUDService<T> 
        where T : MongoModel
    {
        protected IMongoCollection<T> _colle;

        public CRUDService(DatabaseService databaseService)
        {
            _colle = databaseService.MainDatabase.GetCollection<T>(typeof(T).ToString()+"s");
        }

        public virtual async Task<List<T>> GetAsync()
        {
            return (await _colle.FindAsync(blog => true)).ToList();
        }

        public virtual async Task<T> GetOneAsync(string id)
        {
            return (await _colle.FindAsync(t => t.id == id)).FirstOrDefault();
        }

        public virtual async Task CreateAsync(T t)
        {
            t.id = "";
            await _colle.InsertOneAsync(t);
        }

        public virtual async Task UpdateAsync(string id, T newData)
        {
            newData.id = id;
            await _colle.ReplaceOneAsync(t => t.id == id, newData);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _colle.DeleteOneAsync(t => t.id == id);
        }
    }
}
