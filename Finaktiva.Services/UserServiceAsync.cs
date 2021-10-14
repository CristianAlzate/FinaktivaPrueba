using Finaktiva.Repository.Repository;
using Finaktiva.Services.ModelView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finaktiva.Services
{
    public class UserServiceAsync : IUserServiceAsync
    {
        private readonly IUserRepositoryAsync _repository;

        public UserServiceAsync(IUserRepositoryAsync repository)
        {
            _repository = repository;
        }
        public async Task<List<UserModelView>> GetAsync()
        {
            var users = await _repository.GetAllAsync();
            return new List<UserModelView>();
        }

        public async Task<UserModelView> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PostAsync(UserModelView user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PutAsync(UserModelView user)
        {
            throw new NotImplementedException();
        }
    }
}
