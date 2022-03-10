using System.Text.Json.Serialization;
using AccountApi.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAccountLineValidation>(services =>
    new TimedAccountLineValidation(
        new AccountLineValidation(),
        services.GetService<ILogger<TimedAccountLineValidation>>()
    )
);
builder.Services.AddSingleton<IValidateAccountsFileCommand, ValidateAccountsFileCommand>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
