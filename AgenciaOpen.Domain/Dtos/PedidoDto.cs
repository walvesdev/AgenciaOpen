namespace AgenciaOpen.Domain.Dtos
{
    public class PedidoDto : EntityBase
    {
        public string NumeroPedido { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal
        {
            get
            {
                return Items?.Sum(v => v.ValorUnitario++) ?? 0;
            }
        }
        private List<ItemPedidoDto> Items { get; set; }

        PedidoDto(ItemPedidoDto item, Guid clienteId)
        {
            Init();
            AddItem(item);
            ClienteId = clienteId;
        }

        public void AddItem(ItemPedidoDto item)
        {
            Items.Add(item);
        }
        public void RemoveItem(ItemPedidoDto item)
        {
            Items.Remove(item);
        }

        public List<ItemPedidoDto> GetItems() => Items;

        private void Init()
        {
            NumeroPedido = $"000{DateTime.Now.ToString("yyyyMMddHHmmssfff")}";
            DataCriacao = DateTime.Now;
            Items = new List<ItemPedidoDto>();
        }
    }
}
