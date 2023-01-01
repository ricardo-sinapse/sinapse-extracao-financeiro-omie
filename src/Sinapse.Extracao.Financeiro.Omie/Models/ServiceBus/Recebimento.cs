namespace Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus
{
    public class Recebimento
    {
        public string CodigoRecebimento { get; set; }
        public string CodigoLancamento { get; set; }
        public string CodigoMovimentoContaCorrente { get; set; }
        public string CodigoIntegracao { get; set; }
        public string CodigoContaCorrente { get; set; }
        public string NumeroTitulo { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroParcela { get; set; }
        public string CodigoLancamentoExtrato { get; set; }
        public decimal Valor { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Multa { get; set; }
        public decimal? Tarifa { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string Observacao { get; set; }
    }
}
