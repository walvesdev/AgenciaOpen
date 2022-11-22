using AgenciaOpen.Common.Attributes;
using AgenciaOpen.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace AgenciaOpen.Services.Resquest.Commands
{
    [DisplayName("Adicionar Cliente")]
    [DependencyInjection(ServiceLifetime.Scoped, ServiceType = typeof(IRequestHandler<CreateUpdateClienteCommand, bool>), ImplementType = typeof(CommandHandler))]
    public class CreateUpdateClienteCommand : IRequest<bool>
    {
        /// <summary>
        /// Cliente
        /// </summary>
        public Cliente Cliente { get; set; }
    }

}
