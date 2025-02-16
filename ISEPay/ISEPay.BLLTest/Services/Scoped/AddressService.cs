using System;
using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.DAL.Persistence.Repositories;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.BLL.Services.Scoped
{
    public interface IAddressService
    {
         
        void AddAddress(AddressDto addressDto, User user); 
        void EditAddress(Guid id, AddressDto updatedAddressDto);
        Address GetAddressByUserId(Guid userId);
        
        

    }

    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUsersRepository _usersRepository;

        public AddressService(IAddressRepository addressRepository, IUsersRepository usersRepository)
        {
            _addressRepository = addressRepository;
            _usersRepository = usersRepository;
        }

        public void AddAddress(AddressDto addressDto, User user)
        {

    
            var address = new Address
            {
                Country = addressDto.Country,
                City = addressDto.City,
                Street = addressDto.Street,
                Zipcode = addressDto.Zipcode,
                UserId = user.Id
            };

         
            _addressRepository.Add(address);
            _addressRepository.SaveChanges();

          
            user.AdressID = address.Id;

            
            _usersRepository.SaveChanges();
        }



        
        
        public Address GetAddressByUserId(Guid userId)
        {
            var address = _addressRepository.GetByUserId(userId);  
            if (address == null)
            {
                throw new Exception("Address for user not found.");
            }
            return address;
        }

        
        public void EditAddress(Guid id, AddressDto updatedAddressDto)
        {
            if (updatedAddressDto == null)
            {
                throw new ArgumentNullException(nameof(updatedAddressDto), "AddressDto cannot be null");
            }

            
            var address = _addressRepository.GetById(id);
            if (address == null)
            {
                throw new Exception("Address does not exist");
            }

            
            address.Street = updatedAddressDto.Street;
            address.City = updatedAddressDto.City;
            address.Zipcode = updatedAddressDto.Zipcode;
            address.Country = updatedAddressDto.Country;
           // address.UpdatedAt = DateTime.Now;

            
            _addressRepository.SaveChanges();
        }
    }
}
