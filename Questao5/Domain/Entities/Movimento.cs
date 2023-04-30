using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string IdMovimento { get; set; }
        public string IdConta { get; set; }
        public DateTime DataMovimento { get; set; }
        public TipoMovimento Tipo { get; set; }
        public double Valor { get; set; }
    }
}