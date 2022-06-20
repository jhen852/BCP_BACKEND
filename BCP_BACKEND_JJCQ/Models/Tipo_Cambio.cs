using System.ComponentModel.DataAnnotations;

namespace BCP_BACKEND_JJCQ.Models
{
    public class Tipo_Cambio
    {
        [Key]
        public int Id { get; set; }
        public int moneda_inicial { get; set; }
        public int moneda_cambio { get; set; }
        public double compra { get; set; }
        public double venta { get; set; }
    }
}
