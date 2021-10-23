using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Models
{
    public class ApiUser
    {
        public string CompanyName { get; set; }
        public virtual IList<Transportation> Transportations { get; set; }
    }
}
