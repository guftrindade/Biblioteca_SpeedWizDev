using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjetoFinal.Services.Auth.JWT;
using System.Text;

namespace ProjetoFinal.Configuracao
{
    public static class AuthConfiguracaoExtensions
    {
        public static void AdicionarConfiguracaoAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguracoes = configuration.GetSection("JwtConfiguracoes").Get<JwtConfiguracoes>();
            var chave = Encoding.ASCII.GetBytes(jwtConfiguracoes.Segredo);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = true;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfiguracoes.Emissor,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidAudience = jwtConfiguracoes.Audiencia,
                    ValidateAudience = true
                };
            });
        }
    }
}
