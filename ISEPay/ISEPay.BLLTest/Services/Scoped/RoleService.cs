
using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;


namespace ISEPay.BLL.Services.Scoped
{


    public interface IRoleService
    {
        void CreateRole(RoleDto role);

        
    }
    internal class RoleService : IRoleService
    {
        private readonly IRolesRepository roleRepository;
        public RoleService( IRolesRepository roleRepository) 
        {
            this.roleRepository = roleRepository;
        }

        public void CreateRole(RoleDto role)
        {
            var existingRole = roleRepository.GetByName(role.Name);

            if (existingRole != null)
            {
                throw new InvalidOperationException("Role already exists");
            }

            var roleToAdd = new Role
            {
                Name = role.Name,
                Description = role.Description,
                IsActive=true,
                CreatedDate = DateTime.Now
            };

            roleRepository.Add(roleToAdd);
            roleRepository.SaveChanges(); // Assuming SaveChanges persists the role
        }

    }
}
