using System.ComponentModel.DataAnnotations;

namespace BCP_BACKEND_JJCQ.Models
{
    public class Cuenta
    {
        [Key]
        public string nro_cuenta { get; set; }
        public string tipo { get; set; }
        public string moneda { get; set; }
        public string nombre { get; set; }
        public double saldo { get; set; }
    }
}
