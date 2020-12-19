using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciudadano.Models
{
    public class CiudadanoI
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public int EstadoCivilId { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
    }
}
