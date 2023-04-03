using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{                                  //Kullanılacak şemayı veriyoruz
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true, // İzin verilecek sitelerin denetlenip denetlenmeyeceği
        ValidateIssuer = true, // Hangi sitenin bunlara izin vereceğini denetlenip denetlenmeyeceği
        ValidateLifetime = true, // Tokenın yaşam süresi olup olmayacağı
        ValidateIssuerSigningKey = true, // Tokenın bize ait olup olmadığının kontrolü

        // appsettings.json'daki verilere erişiyoruz
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        
        ClockSkew = TimeSpan.Zero // Tokenın üzerine ekstra bir süre eklenip eklenmeyeceği
                                  // Sunuculardaki zaman farkı sorun çıkartmaması için zero verdik
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Uygulama içinde authentication yaptığımızı programa bildirdik.
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
