using AgenciaOpen.Services.Resquest.Commands;
using AgenciaOpen.Services.Resquest.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgenciaOpen.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;

        public ClientController(
            ILogger<ClientController> logger
            , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obter Clientes
        /// </summary>
        /// <remarks> Obter Clientes</remarks>
        /// <response code="200"> Requisição efetuada com sucesso. </response>
        /// <response code="400"> Erro ao efetuar requisição. </response>
        /// <response code="500"> Erro interno do servidor. Contate a administração do sistema. </response>
        [HttpGet("obter-clientes")]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _mediator.Send(new GetAllClientsQuery()));
        }

        /// <summary>
        /// Adicionar Clientes
        /// </summary>
        /// <remarks> Adicionar Clientes </remarks>
        /// <response code="200"> Requisição efetuada com sucesso. </response>
        /// <response code="400"> Erro ao efetuar requisição. </response>
        /// <response code="500"> Erro interno do servidor. Contate a administração do sistema. </response>
        [HttpPost("adicionar-clientes")]
        public async Task<IActionResult> CreateClient(CreateUpdateClienteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
