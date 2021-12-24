using ProjetoFinal.Models;
using ProjetoFinal.Services.Auth.JWT.Interfaces;
using System;

namespace ProjetoFinal.Adaptadores
{
    public static class ModeloAdaptadores
    {
        public static JwtCredenciais ParaJwtCredenciais(this Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException();
            }

            return new JwtCredenciais
            {
                Email = usuario.Email,
                Senha = usuario.Senha,
                Role = usuario.CodigoRole.ToString()
            };
        }
    }
}
