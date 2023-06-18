using FamWallet.Services.Campaign.Context;
using FamWallet.Services.Campaign.Services;
using FamWallet.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(opt => {
    opt.Filters.Add(new AuthorizeFilter()); //tüm controller'lara ekledik
});
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ISharedIdentityService, SharedIdentityService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"]; //appsetting.json
    options.Audience = "resource_discount";
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication(); //authentication iþlemi
app.MapControllers();

app.Run();
