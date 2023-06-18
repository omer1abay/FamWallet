using FamWallet.Services.AccountTransactions.Services;
using FamWallet.Services.AccountTransactions.Settings;
using FamWallet.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); //art�k sub olarak gelecek map'lenmeden
builder.Services.AddControllers(opt => {
    opt.Filters.Add(new AuthorizeFilter()); //t�m controller'lara ekledik
});
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Projenin ba�l� oldu�u t�m mapper'lar� tarar, Profile'dan inherit alan nesneleri tarar.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(opts =>
{
    return opts.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISharedIdentityService, SharedIdentityService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"]; //appsetting.json
    options.Audience = "resource_transaction";
    options.RequireHttpsMetadata = false;
});

//db verdik


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication(); //authentication i�lemi
app.MapControllers();

app.Run();
