using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacauShowApi32427421.Models
{
    public class LoteProducao
    {
        public int Id { get; set; }
        public string CodigoLote { get; set; }
        public DateTime Datafabricacao { get; set; }
        public int ProdutoId { get; set; }
        public string Status { get; set; } = "Em Produção";

    }
}