using AgenciaOpen.Database;
using AgenciaOpen.Domain.Models;
using AgenciaOpen.Services.Resquest.Commands;
using MediatR;

namespace AgenciaOpen.Services
{
    public class CommandHandler :
    IRequestHandler<CreateUpdateClienteCommand, bool>
    {

        private readonly IRepository<Cliente> _clienteRepository;

        public CommandHandler(IRepository<Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(CreateUpdateClienteCommand request, CancellationToken cancellationToken)
        {
            await _clienteRepository.CreateAsync(request.Cliente);
            await _clienteRepository.SaveChangesAsync();
            return true;
        }
    }

}
