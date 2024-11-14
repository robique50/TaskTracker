using MongoDB.Driver;
using TasksAPI.Models;
using TasksAPI.Settings;

namespace TasksAPI.Services
{
    public class TaskCollectionService : ITaskCollectionService
    {

        private readonly IMongoCollection<TaskModel> _tasks;

        public TaskCollectionService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _tasks = database.GetCollection<TaskModel>(settings.TasksCollectionName);
        }

        //public TaskCollectionService()
        //{
        //    // Initialize the tasks list here, or it could be fetched from a database or another source
        //    _tasks = new List<TaskModel>
        //    {
        //        new TaskModel { Id = Guid.NewGuid(), Title = "First Task", Description = "First Task Description", AssignedTo = "Author_1", Status = "To do"},
        //        new TaskModel { Id = Guid.NewGuid(), Title = "Second Task", Description = "Second Task Description", AssignedTo = "Author_1", Status = "To do"},
        //        new TaskModel { Id = Guid.NewGuid(), Title = "Third Task", Description = "Third Task Description", AssignedTo = "Author_2", Status = "To do"},
        //        new TaskModel { Id = Guid.NewGuid(), Title = "Fourth Task", Description = "Fourth Task Description", AssignedTo = "Author_3", Status = "To do"},
        //        new TaskModel { Id = Guid.NewGuid(), Title = "Fifth Task", Description = "Fifth Task Description", AssignedTo = "Author_4", Status = "To do"}
        //    };
        //}

        public async Task<List<TaskModel>> GetAll()
        {
            return await (await _tasks.FindAsync(task => true)).ToListAsync();
        }

        public async Task<TaskModel> Get(Guid id)
        {
            return await (await _tasks.FindAsync(task => task.Id == id)).FirstOrDefaultAsync();
        }

        public async Task<bool> Create(TaskModel model)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
            await _tasks.InsertOneAsync(model);
            return true;
        }

        public async Task<bool> Update(Guid id, TaskModel model)
        {
            model.Id = id; // Ensure the model ID is set to the provided ID
            var result = await _tasks.ReplaceOneAsync(task => task.Id == id, model);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _tasks.DeleteOneAsync(task => task.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<List<TaskModel>> GetTasksByStatus(string status)
        {
            return await (await _tasks.FindAsync(task => task.Status == status)).ToListAsync();
        }
    }
}
