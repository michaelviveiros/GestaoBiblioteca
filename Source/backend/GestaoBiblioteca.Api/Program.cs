using GestaoBiblioteca.Api.Configurations;
using GestaoBiblioteca.Core.Context.Settings;
using GestaoBiblioteca.Core.Interfaces.Service;
using GestaoBiblioteca.Core.Mappers.ProfilesMappers;
using GestaoBiblioteca.Service;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") //Origem do front do react
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


#region Configuração dos mapeamentos do AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region Classe de documentação e configuração do Swagguer.
builder.Services.ConfigureSwagguer(builder.Configuration);
#endregion

#region Define o idioma do sistema como Portguês Brasil
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.AddSupportedCultures("pt-BR");
    options.AddSupportedUICultures("pt-BR");
    options.SetDefaultCulture("pt-BR");
});
#endregion

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adiciona o suporte ao HttpClient
builder.Services.AddHttpClient();

builder.Services.Configure<SqlServerSettings>(builder.Configuration.GetSection("DatabaseSettings:SqlServerSettings"));

#region Classe de confifuração do SqlServer
SqlServerConfiguration.ConfigureServices(builder.Services, builder.Configuration);
#endregion

builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<ILivroService, LivroService>();


var app = builder.Build();

app.UseCors("AllowReactApp");

#region Aplica a configuração de localização para Português do Brasil
var opcoesLocalizacao = new RequestLocalizationOptions()
    .AddSupportedCultures("pt-BR")
    .AddSupportedUICultures("pt-BR")
    .SetDefaultCulture("pt-BR");

app.UseRequestLocalization(opcoesLocalizacao);

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestão de Bibliotecas API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
