using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ProjetoFinal.Configuracao
{
    public static class SwaggerConfiguracaoExtensions
    {
        #region Metodo_AdicionarConfiguracaoSwagger

        public static void AdicionarConfiguracaoSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Biblioteca Desafio Final - Bootcamp IGTI",
                    Description = "Api responsável pelo gerenciamento de uma biblioteca",
                    Contact = new OpenApiContact { Name = "Gustavo Ferreira Trindade", Email = "gustavo.trindade@biblioteca.com.br" }
                });


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorizacao JWT bia header (requisicao) utilizando o scheme Bearer. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });


                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                        new string[]{}
                    }
                });
            });
        }
        #endregion

        #region UseConfiguracaoSwagger

        public static void UseConfiguracaoSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "v1");
            });
        }
        #endregion
    }
}
