using System.ComponentModel;

namespace AgenciaOpen.Domain.Models
{
    [DisplayName("Item do Pedido")]
    public class ItemPedido : EntityBase
    {

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Valor Unitario
        /// </summary>
        public decimal ValorUnitario { get; set; }
    }
}
