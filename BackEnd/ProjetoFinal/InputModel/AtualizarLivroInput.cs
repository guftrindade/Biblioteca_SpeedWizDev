namespace ProjetoFinal.InputModel
{
    public class AtualizarLivroInput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int AnoLancamento { get; set; }
        public int AutorId { get; set; }
        public int ISBN { get; set; }
    }
}
