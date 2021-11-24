using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Models
{
    public class ApiUser : IdentityUser
    {
        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Transportation> Transportations { get; set; }
    }
}
