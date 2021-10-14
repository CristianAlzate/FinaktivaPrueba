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
        public UserRepositoryAsync(Context context)
        {
            _context = context;
        }
        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<int> InsertAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            var user = await GetAsync(entity.Id);
            if (user == null)
                return false;
            user.Name = entity.Name;
            user.Role.Id = entity.Role.Id;
            user.Active = entity.Active;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
