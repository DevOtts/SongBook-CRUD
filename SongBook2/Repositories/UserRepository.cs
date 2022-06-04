using System;
using SongBook2.Models;
using SongBook2.Models.Entities.Client;

namespace SongBook2.Repositories
{
    public interface IUserRepository {

        public bool Create(UserDetail client);
    }

    public class UserRepository: IUserRepository
    {
        private readonly _DbContext db;
        public UserRepository(_DbContext _db)
        {
            db = _db;
        }

        public bool Create(UserDetail client)
        {
            var client_db = new User()
            {
                Name = client.Name,
                Email = client.Email       

            };

            //db.User.Add(client_db);
            db.SaveChanges();

            return true;
        }
    }
}
