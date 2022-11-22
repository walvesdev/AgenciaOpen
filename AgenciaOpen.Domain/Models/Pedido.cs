using System.ComponentModel;

namespace AgenciaOpen.Domain.Models
{
    [DisplayName("Pedido")]
    public class Pedido : EntityBase
    {
        /// <summary>
        /// Numero do Pedido
        /// </summary>
        public string NumeroPedido { get; set; }

        /// <summary>
        /// Data de Criacao
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Valor Total
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Identificador do Cliente
        /// </summary>
        public Guid ClienteId { get; set; }

        /// <summary>
        /// Items do Pedido
        /// </summary>
        public List<ItemPedido> Items { get; set; }

    }
}
