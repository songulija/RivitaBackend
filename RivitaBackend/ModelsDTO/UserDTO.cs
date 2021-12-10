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
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Password is limited to {2} to {1} characters",MinimumLength = 6)]
        public string Password { get; set; }
    }
    /// <summary>
    /// UserDTO has to have same fields as ApiUser. It also inherits from LoginDTO
    /// Also user can have Roles(user or admin). 
    /// </summary>
    public class UserDTO : LoginUserDTO
    {
        public string PhoneNumber { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<string> Roles { get; set; }
        public virtual IList<TransportationDTO> Transportations { get; set; }

    }
}
