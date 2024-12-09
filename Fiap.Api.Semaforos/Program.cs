using System.Text;
using AutoMapper;
using Fiap.Api.Semaforos.Data.Contexts;
using Fiap.Api.Semaforos.Data.Repository;
using Fiap.Api.Semaforos.Models;
using Fiap.Api.Semaforos.Services;
using Fiap.Api.Semaforos.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // Import necessário para a configuração do Swagger

var builder = WebApplication.CreateBuilder(args);

#region Database
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("DatabaseConnection"))
           .EnableSensitiveDataLogging();
});

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT no formato 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddScoped<ICondicaoClimaticaRepository, CondicaoClimaticaRepository>();
builder.Services.AddScoped<IFluxoVeiculoRepository, FluxoVeiculoRepository>();
builder.Services.AddScoped<ISemaforoRepository, SemaforoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ICondicaoClimaticaService, CondicaoClimaticaService>();
builder.Services.AddScoped<IFluxoVeiculoService, FluxoVeiculoService>();
builder.Services.AddScoped<ISemaforoService, SemaforoService>();
builder.Services.AddScoped<IAuthService, AuthService>();

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c =>
{
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Auth
var key = Encoding.UTF8.GetBytes("2141251231251");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "fiap",
        ValidAudience = "fiap",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
#endregion

var app = builder.Build();

// Configuração específica apenas para rotas com autorização
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
