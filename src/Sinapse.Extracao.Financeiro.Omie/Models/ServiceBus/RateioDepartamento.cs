namespace Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus
{
    public class RateioDepartamento
    {
        public string CodigoDepartamento { get; set; }
        public string NomeDepartamento { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Percentual { get; set; }
    }
}
