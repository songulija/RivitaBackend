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
        [ForeignKey(nameof(Models.User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int TransportationNumber { get; set; }
        public int Weight { get; set; }
        public int WagonsCount { get; set; }
        public string TransportationType { get; set; }
        // All Dates of Transportation
        public DateTime CargoAcceptanceDate { get; set; }
        public DateTime MovementStartDateInBelarus { get; set; }
        public DateTime MovementEndDateInBelarus { get; set; }
        // Info about Cargo(gruze)
        public int EtsngCargoCode { get; set; }
        public int GngCargoCode { get; set; }
        // Information about Stations
        public string DepartureStationTitle { get; set; }
        public string DepartureCountryTitle { get; set; }
        public string DestinationStationTitle { get; set; }
        public string DestinationCountryTitle { get; set; }
        public string StationMovementBeginingBelarusTitle { get; set; }
        public string StationMovementEndBelarusTitle { get; set; }
        // Information about Wagons. Each Transportation will have number of wagons. So its
        // one to many relationship.
        public virtual ICollection<Wagon> Wagons { get; set; }
    }
}
