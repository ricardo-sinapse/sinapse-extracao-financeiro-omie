namespace Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus
{
    public class MovimentoFinanceiro
    {
        public string CodigoTitulo { get; set; }

        public string NumeroTitulo { get; set; }

        public DateTime? DataEmissao { get; set; }

        public DateTime? DataVencimento { get; set; }

        public DateTime? DataPrevisao { get; set; }

        public DateTime? DataPagamento { get; set; }

        public string CodigoPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string CpfCnpj { get; set; }

        public string NumeroContrato { get; set; }

        public string NumeroOrdemServico { get; set; }

        public string NumeroParcela { get; set; }

        public string CodigoContaCorrente { get; set; }
        public string NomeContaCorrente { get; set; }

        public string Grupo { get; set; }

        public string Status { get; set; }

        public string Natureza { get; set; }

        public string Tipo { get; set; }

        public string Operacao { get; set; }

        public string NumeroDocumentoFiscal { get; set; }

        public decimal? ValorTitulo { get; set; }

        public decimal? ValorLiquido { get; set; }

        public decimal? ValorPago { get; set; }

        public decimal? ValorJuros { get; set; }

        public decimal? ValorMulta { get; set; }

        public decimal? ValorDesconto { get; set; }

        public decimal? ValorPIS { get; set; }

        public decimal? ValorCOFINS { get; set; }

        public decimal? ValorCSLL { get; set; }

        public decimal? ValorIR { get; set; }

        public decimal? ValorISS { get; set; }

        public decimal? ValorINSS { get; set; }

        public string Observacoes { get; set; }

        public string CodigoProjeto { get; set; }
        public string NomeProjeto { get; set; }

        public string CodigoVendedor { get; set; }
        public string NomeVendedor { get; set; }

        public DateTime? DataRegistro { get; set; }

        public string NumeroBoleto { get; set; }

        public IEnumerable<RateioDepartamento> Departamentos { get; set; }

        public IEnumerable<RateioCategoria> Categorias { get; set; }
    }
}
