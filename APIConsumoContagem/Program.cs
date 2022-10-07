var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

// Middleware necessário na integração com Dapr
app.UseCloudEvents();

app.MapControllers();

// Configurando a aplicação como um Subscriber com Dapr
app.MapSubscribeHandler();

app.Run();