using FamWallet.Services.MoneyTransfer.Context;
using FamWallet.Services.MoneyTransfer.Services;
using FamWallet.Services.MoneyTransfer.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quartz;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddQuartz(q => { 
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    var jobKey = new JobKey("Scheduler");

    q.AddJob<Scheduler>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("Schedule-identity")
            .WithCronSchedule("*/15 * * * * ?")
        );
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));

builder.Services.AddDbContext<MoneyTransferDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<RedisService>(sp =>
{
    var redissettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redis = new RedisService(redissettings.Host, redissettings.Port);

    redis.Connect();

    return redis;

});

builder.Services.AddHttpClient("KuveytTurk")
    .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

    });

builder.Services.AddScoped<ITransactionService, GetAccountTransactionsWithKTApi>();
builder.Services.AddScoped<IMoneyTransferService, MoneyTransfer>();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); //artýk sub olarak gelecek map'lenmeden
builder.Services.AddControllers(opt => {
    opt.Filters.Add(new AuthorizeFilter()); //tüm controller'lara ekledik
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"]; //appsetting.json
    options.Audience = "resource_moneytransfer";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
