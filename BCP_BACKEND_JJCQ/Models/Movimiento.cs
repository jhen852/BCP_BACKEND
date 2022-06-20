using System.ComponentModel.DataAnnotations;

namespace BCP_BACKEND_JJCQ.Models
{
    public class Movimiento
    {
        [Key]
        public int Id { get; set; }
        public string nro_cuenta { get; set; }
        public DateTime fecha { get; set; }
        public string tipo { get; set; }
        public double importe { get; set; }

    }
}
