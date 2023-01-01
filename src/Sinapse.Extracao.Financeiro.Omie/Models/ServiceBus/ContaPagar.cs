namespace Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus
{
    public class ContaPagar
    {
        public string CodigoLancamento { get; set; }
        public string CodigoIntegracao { get; set; }
        public string Status { get; set; }
        public string CodigoPessoa { get; set; }        
        public string NomePessoa { get; set; }        
        public string CpfCnpjPessoa { get; set; }        
        public string CodigoBanco { get; set; }        
        public string CodigoAgencia { get; set; }        
        public string NumeroContaCorrente { get; set; }        
        public DateTime DataEntrada { get; set; }        
        public DateTime DataVencimento { get; set; }        
        public string CodigoTipoDocumento { get; set; }        
        public decimal ValorDocumento { get; set; }        
        public DateTime? DataPrevisao { get; set; }        
        public IEnumerable<RateioCategoria> RateioCategorias { get; set; }        
        public IEnumerable<RateioDepartamento> RateioDepartamentos { get; set; }        
        public string CodigoContaCorrente { get; set; }        
        public string NumeroNotaFiscal { get; set; }        
        public string NumeroDocumento { get; set; }        
        public DateTime? DataEmissao { get; set; }        
        public string CodigoProjeto { get; set; }        
        public string Observacao { get; set; }        
        public string NumeroPedido { get; set; }
        public string OcorrenciaPagamento { get; set; }        
        public string NumeroParcela { get; set; }        
        public decimal? ValorPIS { get; set; }        
        public bool? RetemPIS { get; set; }        
        public decimal? ValorCOFINS { get; set; }        
        public bool? RetemCOFINS { get; set; }        
        public decimal? ValorCSLL { get; set; }        
        public bool? RetemCSLL { get; set; }        
        public decimal? ValorIR { get; set; }        
        public bool? RetemIR { get; set; }        
        public decimal? ValorISS { get; set; }        
        public bool? RetemISS { get; set; }        
        public decimal? ValorINSS { get; set; }        
        public bool? RetemINSS { get; set; }       
    }
}
