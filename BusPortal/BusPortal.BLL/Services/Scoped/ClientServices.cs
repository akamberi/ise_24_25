using AutoMapper;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using BusPortal.DAL.Persistence.Entities;
using BusPortal.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity; 
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusPortal.BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Client> _passwordHasher;

        public ClientService(IClientRepository clientRepository, IMapper mapper
            )
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _mapper = mapper;
        }

        public async Task<bool> RegisterClient(RegisterViewModel model)
        {
            var clientEntity = _mapper.Map<Client>(model);

           
            var existingClient = _clientRepository.GetAll().FirstOrDefault(c => c.Email == clientEntity.Email);
            if (existingClient != null)
            {
                return false; 
            }

            
            _clientRepository.Add(clientEntity);
            _clientRepository.SaveChanges();
            return true;
        }

        public async Task<bool> AuthenticateClient(LoginViewModel model)
        {

            var clientEntity = _clientRepository.GetAll().FirstOrDefault(c => c.Name == model.Username);
            if (clientEntity == null)
            {
                return false; 
            }

            
            var result = _passwordHasher.VerifyHashedPassword(clientEntity, clientEntity.Password, model.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return false;
            }

            return true;
        }

        public Task Logout()
        {
           
            throw new NotImplementedException();
        }

        async Task<Domain.Models.Client> IClientService.FindByName(string name)
        {
            var client = _clientRepository.FindByName(name);
            if(client!= null){
                return new Domain.Models.Client
                {
                    Name = client.Name,
                    Admin = client.Admin,
                    Email = client.Email,
                    Id = client.Id,
                };
            }
            return null;
        }
    }
}
