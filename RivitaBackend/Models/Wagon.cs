using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Models
{
    public class Wagon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        // Each transportation can have many waggons
        [ForeignKey(nameof(Transportation))]
        public Guid TransportationId { get; set; }
        public virtual Transportation Transportation { get; set; }
        public int NumberOfWagon { get; set; }
        public string TypeOfWagon { get; set; }
        public int Weight { get; set; }
    }
}
