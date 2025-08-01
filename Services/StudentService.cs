//using MongoCrudApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Crud.Models;

namespace MongoCrudApp.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _students = database.GetCollection<Student>(settings.Value.CollectionName);
        }

        public async Task<List<Student>> GetAllAsync() =>
            await _students.Find(_ => true).ToListAsync();

        public async Task<Student> GetByIdAsync(string id) =>
            await _students.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
        }
    }
}
