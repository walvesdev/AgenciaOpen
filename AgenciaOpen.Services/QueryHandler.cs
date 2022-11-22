using AgenciaOpen.Database;
using AgenciaOpen.Domain.Models;
using AgenciaOpen.Services.Resquest.Queries;
using MediatR;

namespace AgenciaOpen.Services
{
    public class QueryHandler :
        IRequestHandler<GetAllClientsQuery, List<Cliente>>
    {
        private readonly IRepository<Cliente> _clienteRepository;
        public QueryHandler(IRepository<Cliente> clienteRepository = null)
        {
            _clienteRepository = clienteRepository;
        }


        public async Task<List<Cliente>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            return await _clienteRepository.GetAllAsync();
        }
    }

}
