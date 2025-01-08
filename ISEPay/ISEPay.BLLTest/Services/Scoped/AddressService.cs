/*using System;
using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.DAL.Persistence.Repositories;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.BLL.Services.Scoped
{
    // Rename interface to AddressService for consistency
    public interface IAddressService
    {
        void AddAddress(AddressDto addressDto);
    }

    // Make AddressService public to be used outside the assembly if needed
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IUsersRepository usersRepository;

        // Constructor to inject repositories
        public AddressService(IAddressRepository addressRepository, IUsersRepository usersRepository)
        {
            this.addressRepository = addressRepository;
            this.usersRepository = usersRepository;
        }

        public void AddAddress(AddressDto addressDto)
        {
            // Check if the addressDto is null
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto), "AddressDto cannot be null");
            }

            // Retrieve the user from the repository
            var user = usersRepository.FindById(addressDto.userId);

            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            // Create the Address object to save
            var addressToSave = new Address
            {
                Country = addressDto.Country,
                City = addressDto.City,
                Zipcode = addressDto.Zipcode,
                Street = addressDto.Street
            };

            // Assign the address to the user
            user.Address = addressToSave;

            // Add the new user and address entities to their respective repositories
            usersRepository.Add(user);
            addressRepository.Add(addressToSave);

            // Save the changes in both repositories
            usersRepository.SaveChanges();
            addressRepository.SaveChanges();
        }
    }
}
*/