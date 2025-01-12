using System;
using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.DAL.Persistence.Repositories;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.BLL.Services.Scoped
{
    public interface IAddressService
    {
         
        void AddAddress(AddressDto addressDto); 
        void EditAddress(Guid id, AddressDto updatedAddressDto);

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

        public void AddAddress(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto), "AddressDto cannot be null");
            }

            
            var user = _usersRepository.FindById(addressDto.UserId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            
            var addressToSave = new Address
            {
                Street = addressDto.Street,
                City = addressDto.City,
                Zipcode = addressDto.Zipcode,
                Country = addressDto.Country,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            
            _addressRepository.Add(addressToSave);
            _usersRepository.Add(user);  

           

            
            _addressRepository.SaveChanges();
            _usersRepository.SaveChanges();
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
