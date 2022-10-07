// Para executar a aplicação:
// dapr run --app-id APIContagem --components-path ..\components dotnet run

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Middleware necessário na integração com Dapr
app.UseCloudEvents();

app.Run();