using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjetoFinal.Services.Auth.JWT.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoFinal.Services.Auth.JWT
{
    public class JwtAuthGerenciador : IJwtAuthGerenciador
    {
        #region Campos
        private readonly JwtConfiguracoes jwtConfiguracoes;
        #endregion

        #region Construtor
        public JwtAuthGerenciador(IOptions<JwtConfiguracoes>jwtConfiguracoes)
        {
            this.jwtConfiguracoes = jwtConfiguracoes.Value;
        }
        #endregion

        #region Metodos
        public JwtAuthModelo GerarToken(JwtCredenciais credenciais)
        {
            var key = Base64UrlEncoder.DecodeBytes("YOUR_SECRET");
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(key);

            var declaracoes = new List<Claim>
            {
                new Claim(ClaimTypes.Email, credenciais.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, credenciais.Role)
            };

            var chave = Encoding.ASCII.GetBytes(jwtConfiguracoes.Segredo);

            var jwtToken = new JwtSecurityToken(
                jwtConfiguracoes.Emissor,
                jwtConfiguracoes.Audiencia,
                declaracoes,
                expires: DateTime.Now.AddMinutes(jwtConfiguracoes.ValorMinutos*60),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new JwtAuthModelo
            {
                TokenAcesso = accessToken,
                TokenType = "bearer",
                ExpiraEm = jwtConfiguracoes.ValorMinutos * 60
            };
        }
        #endregion
    }
}
