using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.ModelsDTO
{
    /// <summary>
    /// When making HTTP POST request have to provide CreateWagonDTO with all its fields.
    /// </summary>
    public class CreateWagonDTO
    {
        [Required]
        public Guid TransportationId { get; set; }
        [Required]
        public int NumberOfWagon { get; set; }
        [Required]
        public string TypeOfWagon { get; set; }
        [Required]
        public int Weight { get; set; }
    }
    public class UpdateWagonDTO : CreateWagonDTO
    {

    }
    /// <summary>
    /// Imherits all fields from CreateWagonDTO. When getting info from Database
    /// can get Id becouse i specified it, and Transportation object if its INCLUDED
    /// </summary>
    public class WagonDTO : CreateWagonDTO
    {
        public Guid Id { get; set; }
        public TransportationDTO Transportation { get; set; }
    }
}
