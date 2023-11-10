using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SRVP.Data;
using SRVP.Interfaces;
using SRVP.Servicios;
using SRVP.Helpers;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SRVPContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<ISistemaExternoService, SistemaExternoService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddAuthentication(opcionesDeAutenticacion =>
{
    opcionesDeAutenticacion.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opcionesDeAutenticacion.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("SymmetricScheme", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:ClavePrivada"] ?? string.Empty)
        )
    };
})
.AddJwtBearer("AsymmetricScheme", opcionesDeJWT =>
{
    XmlDocument doc = new XmlDocument();
    doc.Load("ClavePublica.xml");
    string contenidoXML = doc.InnerXml;
    var claveDelFirmante = Asimetria.GenerarClaveDeFirmaRSA(contenidoXML);

    opcionesDeJWT.RequireHttpsMetadata = false;
    opcionesDeJWT.SaveToken = true;
    opcionesDeJWT.IncludeErrorDetails = true;
    opcionesDeJWT.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = claveDelFirmante,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = "TPIntegrador" //DUDAS
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
