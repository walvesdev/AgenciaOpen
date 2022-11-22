namespace AgenciaOpen.Domain.Dtos
{
    public class ClienteDto : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<PedidoDto> Pedidos { get; set; }
    }
}
