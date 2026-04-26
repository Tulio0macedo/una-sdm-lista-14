using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CacauShowApi32427421.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UnidadeId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
    }
}