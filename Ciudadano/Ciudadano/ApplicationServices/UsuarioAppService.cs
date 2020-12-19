using Ciudadano.DataContext;
using Ciudadano.DomainService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciudadano.ApplicationServices
{
        public class UsuarioAppService
        {

            private readonly CiudadanoDataContext _baseDatos;
            private readonly UsuarioDomainService _usuarioDomainServices;

            public UsuarioAppService(CiudadanoDataContext baseDatos, UsuarioDomainService usuarioDomainServiceaseDatos)
            {
                _baseDatos = baseDatos;
                _usuarioDomainServices = usuarioDomainServiceaseDatos;
            }


            public async Task<string> TieneAccesoUsuario(string usuarioId, string contrasenia)
            {
                var usuario = await _baseDatos.Usuarios.FirstOrDefaultAsync(q => q.UsuarioId == usuarioId
                && q.Contrasenia == contrasenia);


                var respuestaDomain = _usuarioDomainServices.TieneAcceso(usuario);

                bool vieneConErrorEnElDomain = respuestaDomain != null;
                if (vieneConErrorEnElDomain)
                {
                    return respuestaDomain;
                }

                return "sucess";

            }

        }
}
