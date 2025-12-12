using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GestaoBiblioteca.Api.Configurations
{
    public static class SwagguerConfiguration
    {
        public static void ConfigureSwagguer(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddSwaggerGen(x =>
            {
                //Especificação da API
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gestão de Bibliotecas API",
                    Version = "v1",
                    Description = "API criada através de um desafio proposto pela Siemens.",
                    TermsOfService = new Uri("https://www.siemens.com/br/pt.html"),
                    Contact = new OpenApiContact
                    {
                        Name = "Siemens LTDA",
                        Email = "atendimento.br@siemens.com",
                        Url = new Uri("https://www.siemens.com/br/pt.html")
                    }
                });

                //Permite a adição de comentários no Swagguer tornando a API mais detalhada.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });
        }
    }
}
