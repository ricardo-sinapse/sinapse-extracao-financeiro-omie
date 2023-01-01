namespace Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus
{
    public class RateioCategoria
    {
        public string CodigoCategoria { get; set; }
        public string NomeCategoria { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Percentual { get; set; }
    }
}
