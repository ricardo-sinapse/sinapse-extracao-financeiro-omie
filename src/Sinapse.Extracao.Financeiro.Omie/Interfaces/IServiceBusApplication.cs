using Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus;

namespace Sinapse.Extracao.Financeiro.Omie.Interfaces
{
    public interface IServiceBusApplication
    {
        Task<IEnumerable<MovimentoFinanceiro>> ConsultarMovimentosPelaDataInclusaoAsync(string fonteDados,
            DateTime dataInicial, DateTime dataFinal);

        Task<ContaCorrente> ConsultarContaCorrentePeloCodigoAsync(string fonteDados, string codigoContaCorrente);

        Task<Pessoa> ConsultarPessoaPeloCodigoAsync(string fonteDados, string codigoPessoa);

        Task<Projeto> ConsultarProjetoPeloCodigoAsync(string fonteDados, string codigoProjeto);

        Task<Vendedor> ConsultarVendedorPeloCodigoAsync(string fonteDados, string codigoVendedor);
    }
}
