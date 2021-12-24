using ProjetoFinal.Models;
using System.Collections.Generic;

namespace ProjetoFinal.InputModel
{
    public class LivroInput
    {
        public int AutorId{ get; set; }
        public string Descricao { get; set; }
        public int ISBN { get; set; }
        public int AnoLancamento { get; set; }
    }
}
