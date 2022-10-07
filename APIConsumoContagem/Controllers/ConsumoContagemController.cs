using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Dapr;
using APIConsumoContagem.Models;

namespace APIConsumoContagem.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsumoContagemController : ControllerBase
{
    private readonly ILogger<ConsumoContagemController> _logger;

    public ConsumoContagemController(ILogger<ConsumoContagemController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Topic("contagem-pubsub", "testes")]
    public ActionResult Post(ResultadoContador resultadoContador)
    {
        _logger.LogInformation($"Valor recebido para o contador: {resultadoContador.ValorAtual}");
        _logger.LogInformation($"Dados: {JsonSerializer.Serialize(resultadoContador)}");
        return Ok();
    }
}