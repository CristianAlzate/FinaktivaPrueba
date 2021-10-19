using AutoMapper;
using Finaktiva.Repository.Entities;
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
        private readonly IMapper _mapper;

        public UserServiceAsync(IUserRepositoryAsync repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<UserModelView>> GetAsync()
        {
            return _mapper.Map<List<UserModelView>>(await _repository.GetAllAsync());
        }

        public async Task<UserModelView> GetAsync(int id)
        {
            return _mapper.Map<UserModelView>(await _repository.GetAsync(id));
        }

        public async Task<UserModelView> GetAsync(string name, string pass)
        {
            return _mapper.Map<UserModelView>(await _repository.GetAsync(name,pass));
        }

        public async Task<int> PostAsync(UserModelView user)
        {
            return await _repository.InsertAsync(_mapper.Map<User>(user));
        }

        public async Task<bool> PutAsync(UserModelView user)
        {
            return await _repository.UpdateAsync(_mapper.Map<User>(user));
        }

    }
}
