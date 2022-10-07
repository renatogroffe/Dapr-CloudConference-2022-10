using Microsoft.AspNetCore.Mvc;
using Dapr.Client;
using APIContagem.Models;
using APIContagem.Logging;

namespace APIContagem.Controllers;

[ApiController]
[Route("[controller]")]
public class ContadorController : ControllerBase
{
    private static readonly Contador _CONTADOR = new Contador();
    private readonly ILogger<ContadorController> _logger;
    private readonly IConfiguration _configuration;
    private readonly DaprClient _daprClient;

    public ContadorController(ILogger<ContadorController> logger,
        IConfiguration configuration,
        DaprClient daprClient)
    {
        _logger = logger;
        _configuration = configuration;
        _daprClient = daprClient;
    }

    [HttpGet]
    public async Task<ResultadoContador> Get()
    {
        var pubSubName = _configuration["Dapr:PubSubName"];
        var topic = _configuration["Dapr:Topic"];
        int valorAtualContador;

        lock (_CONTADOR)
        {
            _CONTADOR.Incrementar();
            valorAtualContador = _CONTADOR.ValorAtual;
        }

        _logger.LogValorAtual(valorAtualContador);
        var resultado = new ResultadoContador()
        {
            ValorAtual = valorAtualContador,
            Producer = _CONTADOR.Local,
            Kernel = _CONTADOR.Kernel,
            Framework = _CONTADOR.Framework,
            Mensagem = _configuration["MensagemVariavel"]
        };

        await _daprClient.PublishEventAsync<ResultadoContador>(pubSubName, topic, resultado);
        return resultado;
    }
}