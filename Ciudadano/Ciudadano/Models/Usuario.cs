using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciudadano.Models
{
    public class Usuario
    {
        public string UsuarioId { get; set; }
        public string Contrasenia { get; set; }
        public bool EstaActivo { get; set; }
    }
}
