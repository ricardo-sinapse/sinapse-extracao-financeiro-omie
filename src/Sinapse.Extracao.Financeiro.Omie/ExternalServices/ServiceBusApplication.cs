using Sinapse.Extracao.Financeiro.Omie.Interfaces;
using Sinapse.Extracao.Financeiro.Omie.Models;
using Sinapse.Extracao.Financeiro.Omie.Models.Api;
using Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sinapse.Extracao.Financeiro.Omie.ExternalServices
{
    public class ServiceBusApplication : IServiceBusApplication
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppSettings _appSettings;
        private Dictionary<string, Dictionary<string, string>> _acessos;

        public ServiceBusApplication(AppSettings appSettings, IHttpClientFactory clientFactory)
        {
            _appSettings = appSettings;
            _clientFactory = clientFactory;

            _acessos = new()
            {
                { "OMIE", new Dictionary<string, string>() { { "APP_KEY", _appSettings.OmieAppKey }, { "APP_SECRET", _appSettings.OmieAppSecret } } },
            };
        }

        private HttpClient CreateHttpClient()
        {
            return _clientFactory.CreateClient("service-bus");
        }

        private async Task<string> EnviarRequestSSBUS(string endpoint, ServiceBusRequest request)
        {
            var client = CreateHttpClient();

            var response = await client.PostAsJsonAsync(endpoint, request);
            var content = JsonSerializer.Deserialize<ApiResponse>(await response.Content.ReadAsStringAsync());

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                else throw new Exception(content.Message);
            }

            return content.Data?.ToString();
        }

        public async Task<IEnumerable<MovimentoFinanceiro>> ConsultarMovimentosPelaDataInclusaoAsync(string fonteDados,
            DateTime dataInicial, DateTime dataFinal)
        {
            var request = new ServiceBusRequest
            {
                FonteDados = fonteDados,
                Metodo = "ConsultarMovimentosFinanceiros",
                DadosAutenticacao = _acessos[fonteDados],
                Parametros = new Dictionary<string, string>
                {
                    { "TipoData", "DataInclusao" },
                    { "DataInicial", dataInicial.ToString("dd/MM/yyyy") },
                    { "DataFinal", dataFinal.ToString("dd/MM/yyyy") }
                }
            };

            var data = await EnviarRequestSSBUS("v1/movimentos-financeiros", request);
            if (data is null)
                return null;

            return JsonSerializer.Deserialize<IEnumerable<MovimentoFinanceiro>>(data);
        }

        public async Task<ContaCorrente> ConsultarContaCorrentePeloCodigoAsync(string fonteDados, string codigoContaCorrente)
        {
            var request = new ServiceBusRequest
            {
                FonteDados = fonteDados,
                Metodo = "ConsultarContasCorrentes",
                DadosAutenticacao = _acessos[fonteDados],
                Parametros = new Dictionary<string, string>
                {
                    { "CodigoContaCorrente", codigoContaCorrente }
                }
            };

            var data = await EnviarRequestSSBUS("v1/contas-correntes", request);
            if (data is null)
                return null;

            return JsonSerializer.Deserialize<ContaCorrente>(data);
        }

        public async Task<Pessoa> ConsultarPessoaPeloCodigoAsync(string fonteDados, string codigoPessoa)
        {
            var request = new ServiceBusRequest
            {
                FonteDados = fonteDados,
                Metodo = "ConsultarPessoas",
                DadosAutenticacao = _acessos[fonteDados],
                Parametros = new Dictionary<string, string>
                {
                    { "CodigoPessoa", codigoPessoa }
                }
            };

            var data = await EnviarRequestSSBUS("v1/pessoas", request);
            if (data is null)
                return null;

            return JsonSerializer.Deserialize<Pessoa>(data);
        }

        public async Task<Projeto> ConsultarProjetoPeloCodigoAsync(string fonteDados, string codigoProjeto)
        {
            var request = new ServiceBusRequest
            {
                FonteDados = fonteDados,
                Metodo = "ConsultarProjetos",
                DadosAutenticacao = _acessos[fonteDados],
                Parametros = new Dictionary<string, string>
                {
                    { "CodigoProjeto", codigoProjeto }
                }
            };

            var data = await EnviarRequestSSBUS("v1/projetos", request);
            if (data is null)
                return null;

            return JsonSerializer.Deserialize<Projeto>(data);
        }

        public async Task<Vendedor> ConsultarVendedorPeloCodigoAsync(string fonteDados, string codigoVendedor)
        {
            var request = new ServiceBusRequest
            {
                FonteDados = fonteDados,
                Metodo = "ConsultarVendedores",
                DadosAutenticacao = _acessos[fonteDados],
                Parametros = new Dictionary<string, string>
                {
                    { "CodigoVendedor", codigoVendedor }
                }
            };

            var data = await EnviarRequestSSBUS("v1/vendedores", request);
            if (data is null)
                return null;

            return JsonSerializer.Deserialize<Vendedor>(data);
        }
    }
}
