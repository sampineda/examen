using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciudadano.Models
{
    public class EstadoCivil
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public List<CiudadanoI> Ciudadanos { get; set; }
    }
}
