using System.ComponentModel;

namespace AgenciaOpen.Domain.Models
{
    [DisplayName("Cliente")]
    public class Cliente : EntityBase
    {
        /// <summary>
        /// nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lista de Pedidos
        /// </summary>
        public List<Pedido> Pedidos { get; set; }
    }
}
