using HM.Product.API;
using HM.Product.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(options =>
//{
//    Configuration.Bind("AzureAdB2C", options);
//    options.TokenValidationParameters.NameClaimType = "name";
//},
//           options => { Configuration.Bind("AzureAdB2C", options); });
builder.Services.AddDbContext<HMProductContext>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpServiceProvider, HttpServiceProvider>();
builder.Services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers().AddJsonOptions(opts => { opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    opts.JsonSerializerOptions.PropertyNamingPolicy = null;
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
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

app.UseAuthorization();

app.MapControllers();

app.Run();
