using Contracts.Models;
using LiteDB;

namespace Core
{
    public class DataRepository : IAppRepository, IDisposable
    {
        private readonly LiteDatabase _db;

        private LiteCollection<User> Users => (LiteCollection<User>)_db.GetCollection<User>("users");
        private LiteCollection<UserStats> UserStats => (LiteCollection<UserStats>)_db.GetCollection<UserStats>("userstats");

        public DataRepository(string dbPath)
        {
            _db = new LiteDatabase(dbPath);
        }
        // ALL USER OPERATIONS HERE
        public Guid CreateUser(User user) => Users.Insert(user);
        public bool UpdateUser(User user) => Users.Update(user);
        public User? GetUser(Guid id) => Users.FindById(id);
        public User? GetUserByUserName(string userId) => Users.FindOne(u => u.UserName == userId);
        public List<User> GetAllUsers() => Users.FindAll().ToList();
        public int RemoveAllUsers() => Users.DeleteAll();

        // ALL USERSTATS OPERATIONS HERE  
        public Guid CreateUserStats(UserStats stats) => UserStats.Insert(stats);
        public UserStats? GetUserStats(Guid id) => UserStats.FindById(id);
        public void Dispose() => _db.Dispose();
    }
}
