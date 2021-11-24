using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.ModelsDTO
{
    /// <summary>
    /// When making HTTP POST request have to provide all these fields to Create new Record in Transporations table
    /// </summary>
    public class CreateTransportationDTO
    {
        //[Required]
        public Guid UserId { get; set; }
        [Required]
        public int TransportationNumber { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int WagonsCount { get; set; }
        [Required]
        public string TransportationStatus { get; set; }
        [Required]
        public string TransportationType { get; set; }
        [Required]
        public int TransportationSubCode { get; set; }
        [Required]
        // All Dates of Transportation
        public DateTime CargoAcceptanceDate { get; set; }
        [Required]
        public DateTime MovementStartDateInBelarus { get; set; }
        //not required becouse admin will be able to update it later
        public DateTime MovementEndDateInBelarus { get; set; }
        // Info about Cargo(gruze)
        [Required]
        public int EtsngCargoCode { get; set; }
        [Required]
        public string EtsngCargoTitle { get; set; }
        [Required]
        public int GngCargoCode { get; set; }
        [Required]
        public string GngCargoTitle { get; set; }
        // Information about Stations
        [Required]
        public int DepartureStationCode { get; set; }
        [Required]
        public string DepartureStationTitle { get; set; }
        [Required]
        public int DepartureCountryCode { get; set; }
        [Required]
        public string DepartureCountryTitle { get; set; }
        [Required]
        public int DestinationStationCode { get; set; }
        [Required]
        public string DestinationStationTitle { get; set; }
        [Required]
        public int DestinationCountryCode { get; set; }
        [Required]
        public string DestinationCountryTitle { get; set; }
        [Required]
        public int StationMovementBeginingBelarusCode { get; set; }
        [Required]
        public string StationMovementBeginingBelarusTitle { get; set; }
        // Not required if its still in belarus. When movemement in Belarus ends
        // admin will update it
        public int StationMovementEndBelarusCode { get; set; }
        public string StationMovementEndBelarusTitle { get; set; }
    }

    public class UpdateTransportationDTO : CreateTransportationDTO
    {

    }
    /// <summary>
    /// Inherits fields from CreateTransportationDTO. When getting can get Id field becouse i specified it,
    /// and also ApiUser, WagonDTO objects associated with it if they are included.
    /// </summary>
    public class TransportationDTO : CreateTransportationDTO
    {
        public Guid Id { get; set; }
        public UserDTO User { get; set; }
        public virtual IList<WagonDTO> Wagons { get; set; }

    }
}
