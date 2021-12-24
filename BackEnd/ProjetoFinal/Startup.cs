using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjetoFinal.Configuracao;
using ProjetoFinal.Context;
using ProjetoFinal.Services.Auth.JWT;
using ProjetoFinal.Services.Auth.JWT.Interfaces;

namespace ProjetoFinal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();


            //Configuracoes JWT
            var sessao = Configuration.GetSection("JwtConfiguracoes");
            services.Configure<JwtConfiguracoes>(sessao);


            //Servicos
            services.AddScoped<IJwtAuthGerenciador, JwtAuthGerenciador>();


            //Autenticacao
            services.AdicionarConfiguracaoAuth(Configuration);


            //Conexao_BancoDeDados_Context
            services.AddDbContext<BibliotecaDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            //ConfiguraçãoSwagger - Documentacao API
            services.AdicionarConfiguracaoSwagger();


            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Configuracao_CORS
            app.UseCors(x => x.AllowAnyMethod()
                              .AllowAnyHeader()
                              .SetIsOriginAllowed(origin => true)
                              .AllowCredentials());
            #endregion

            app.UseConfiguracaoSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
