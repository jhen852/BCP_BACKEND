using System.ComponentModel.DataAnnotations;

namespace BCP_BACKEND_JJCQ.Models
{
    public class Moneda
    {
        [Key]
        public int Id { get; set; }
        public string moneda { get; set; }
        public string sigla { get; set; }
    }
}
