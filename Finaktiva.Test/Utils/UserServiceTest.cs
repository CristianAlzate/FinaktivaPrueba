using Finaktiva.Services;
using Finaktiva.Services.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finaktiva.Test.Utils
{
    public class UserServiceTest : IUserServiceAsync
    {
        public Task<List<UserModelView>> GetAsync()
        {
            var result = new List<UserModelView> { new UserModelView() { Id = 1, Name = "calzate", Active = true }, new UserModelView() { } };
            return Task.FromResult(result);
        }

        public Task<UserModelView> GetAsync(int id)
        {
            var result = new List<UserModelView> { new UserModelView() { Id = 1, Name = "calzate", Active = true }, new UserModelView() { } };
            return Task.FromResult(result.FirstOrDefault(x => x.Id == id));
        }

        public Task<UserModelView> GetAsync(string name, string pass)
        {
            throw new NotImplementedException();
        }

        public Task<int> PostAsync(UserModelView user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutAsync(UserModelView user)
        {
            var result = new List<UserModelView> { new UserModelView() { Id = 1, Name = "calzate", Active = true }, new UserModelView() { } };
            return Task.FromResult(result.FirstOrDefault(x => x.Id == user.Id) != null);
        }
    }
}
