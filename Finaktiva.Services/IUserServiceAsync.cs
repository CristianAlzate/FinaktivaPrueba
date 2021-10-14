using Finaktiva.Services.ModelView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finaktiva.Services
{
    public interface IUserServiceAsync
    {
        Task<List<UserModelView>> GetAsync();
        Task<UserModelView> GetAsync(int id);
        Task<int> PostAsync(UserModelView user);
        Task<bool> PutAsync(UserModelView user);

    }
}
