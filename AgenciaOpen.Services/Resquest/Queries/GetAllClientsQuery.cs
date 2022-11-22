using AgenciaOpen.Common.Attributes;
using AgenciaOpen.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace AgenciaOpen.Services.Resquest.Queries
{
    [DisplayName("Listar Clientes")]
    [DependencyInjection(ServiceLifetime.Scoped, ServiceType = typeof(IRequestHandler<GetAllClientsQuery, List<Cliente>>), ImplementType = typeof(QueryHandler))]
    public class GetAllClientsQuery : IRequest<List<Cliente>>
    {
    }

}
