using Finaktiva.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finaktiva.Repository.Repository
{
    public interface IUserRepositoryAsync : IRepositoryAsync<User>
    {
        Task<User> GetAsync(string name, string pass);
    }
}
