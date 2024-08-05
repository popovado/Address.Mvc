using System.Net.Http.Headers;
using Address.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var dadataConfig = builder.Configuration.GetSection("Dadata").Get<DadataConfig>();

builder.Services.Configure<DadataConfig>(builder.Configuration.GetSection("Dadata"));

builder.Services.AddHttpClient("DadataClient", client =>
{
    var configuration = builder.Configuration;
    client.BaseAddress = new Uri(configuration["Dadata:BaseUrl"]);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", configuration["Dadata:ApiKey"]);
    client.DefaultRequestHeaders.Add("X-Secret", configuration["Dadata:SecretKey"]);
}
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IDadataService, DadataService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();