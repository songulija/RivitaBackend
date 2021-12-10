using RivitaBackend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.ModelsDTO
{
    public class LoginUserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    /// <summary>
    /// UserDTO has to have same fields as ApiUser. It also inherits from LoginDTO
    /// Also user can have Roles(user or admin). 
    /// </summary>
    public class UserDTO : LoginUserDTO
    {
        [Required]
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserType UserType { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public virtual IList<TransportationDTO> Transportations { get; set; }
    }

    public class DisplayUserDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserType UserType { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public virtual IList<TransportationDTO> Transportations { get; set; }
    }
}
