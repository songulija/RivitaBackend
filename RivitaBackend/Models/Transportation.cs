using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Models
{
    public class Transportation
    {
        // General info about transportation
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey(nameof(ApiUser))]
        public Guid UserId { get; set; }
        public virtual ApiUser User { get; set; }
        public int TransportationNumber { get; set; }
        public int Weight { get; set; }
        public int WagonsCount { get; set; }
        public string TransportationStatus { get; set; }
        public string TransportationType { get; set; }
        public int TransportationSubCode { get; set; }
        // All Dates of Transportation
        public DateTime CargoAcceptanceDate { get; set; }
        public DateTime MovementStartDateInBelarus { get; set; }
        public DateTime MovementEndDateInBelarus { get; set; }
        // Info about Cargo(gruze)
        public int EtsngCargoCode { get; set; }
        public string EtsngCargoTitle { get; set; }
        public int GngCargoCode { get; set; }
        public string GngCargoTitle { get; set; }
        // Information about Stations
        public int DepartureStationCode { get; set; }
        public string DepartureStationTitle { get; set; }
        public int DepartureCountryCode { get; set; }
        public string DepartureCountryTitle { get; set; }
        public int DestinationStationCode { get; set; }
        public string DestinationStationTitle { get; set; }
        public int DestinationCountryCode { get; set; }
        public string DestinationCountryTitle { get; set; }
        public int StationMovementBeginingBelarusCode { get; set; }
        public string StationMovementBeginingBelarusTitle { get; set; }
        public int StationMovementEndBelarusCode { get; set; }
        public string StationMovementEndBelarusTitle { get; set; }
        // Information about Wagons. Each Transportation will have number of wagons. So its
        // one to many relationship.
        public virtual ICollection<Wagon> Wagons { get; set; }
    }
}
