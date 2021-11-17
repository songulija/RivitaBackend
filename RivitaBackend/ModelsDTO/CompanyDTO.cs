using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.ModelsDTO
{
    public class CreateCompanyDTO
    {
        [Required]
        public string Name { get; set; }
    }
    public class UpdateCompanyDTO : CreateCompanyDTO
    {

    }
    public class CompanyDTO : CreateCompanyDTO
    {
        public int Id { get; set; }
        public virtual IList<UserDTO> Users { get; set; }
    }
}
