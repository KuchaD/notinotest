using System.Text.Json;
using NotinoTest.api.Convertor;
using NotinoTest.Infrastructure.Email;
using NotinoTest.Infrastructure.Error;
using NotinoTest.Infrastructure.Serializer;

var builder = WebApplication.CreateBuilder(args);

const string EmailSettingOptionsName = "EmailSetting";

builder.Services.AddOptions<JsonSerializerOptions>().Configure(NotinoTest.Infrastructure.Serializer.JsonSerializer.SetupJsonOptions);
builder.Services.AddControllers().AddJsonOptions(options => NotinoTest.Infrastructure.Serializer.JsonSerializer.SetupJsonOptions(options.JsonSerializerOptions));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IEmailClient, EmailClient>();
builder.Services.Configure<EmailSettingOptions>(builder.Configuration.GetSection(EmailSettingOptionsName));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute(typeof(ErrorType), 400));
    options.Filters.Add<HttpResponseExceptionFilter>();
    options.Filters.Add(typeof(ErrorFilter));
});

builder.Services.AddSingleton<IStorage, LocalStorage>();
builder.Services.AddConvertor();
builder.Services.AddScoped<ISerializer, NotinoTest.Infrastructure.Serializer.JsonSerializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleEndpointApp V1"));

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();