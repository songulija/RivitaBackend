using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Models
{
    public class Transportation
    {
        public int id { get; set; }
        public int TransportationNumber { get; set; }
        public string Type { get; set; }
        public string DepartureStation { get; set; }
        public string DestinationStation { get; set; }
        public DateTime MovementStartDate { get; set; }
        public DateTime MovementEndDate { get; set; }
        public int NumberOfWagons { get; set; }
        [ForeignKey(nameof(User))]
        public int Userid { get; set; }
        public ApiUser User { get; set; }
    }
}
