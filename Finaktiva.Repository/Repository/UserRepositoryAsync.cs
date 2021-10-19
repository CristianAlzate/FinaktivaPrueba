using Finaktiva.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finaktiva.Repository.Repository
{
    public class UserRepositoryAsync : IUserRepositoryAsync
    {
        private readonly Context _context;

        private IQueryable<User> GetUserContext()
        {
            return _context.Users.Include(x => x.Role);
        }
        public UserRepositoryAsync(Context context)
        {
            _context = context;
        }
        public async Task<User> GetAsync(int id)
        {
            return await GetUserContext().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetAsync(string name, string pass)
        {
            return await GetUserContext().FirstOrDefaultAsync(x => x.Name == name && x.Password == Encrypt(pass));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await GetUserContext().ToListAsync();
        }

        public async Task<int> InsertAsync(User entity)
        {
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == entity.Role.Id);
            if (userRole == null)
                throw new InvalidOperationException("No existe el rol");
            entity.Role = userRole;
            entity.Password = Encrypt(entity.Password);
            entity.Active = true;
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            var user = await GetAsync(entity.Id);
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == entity.Role.Id);
            if (user == null)
                return false;
            user.Name = entity.Name;
            user.Role = userRole;
            user.Active = entity.Active;
            user.Password = entity.Password == user.Password? user.Password : Encrypt(entity.Password);
            return await _context.SaveChangesAsync() > 0;
        }

        private string Encrypt(string _pass)
        {
            byte[] encryted = Encoding.Unicode.GetBytes(_pass);
            string result = Convert.ToBase64String(encryted);
            return result;
        }
    }
}
