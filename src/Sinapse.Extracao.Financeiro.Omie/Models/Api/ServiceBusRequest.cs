namespace Sinapse.Extracao.Financeiro.Omie.Models.Api
{
    public class ServiceBusRequest
    {
        public string FonteDados { get; set; }
        public string Metodo { get; set; }
        public Dictionary<string, string> DadosAutenticacao { get; set; }
        public Dictionary<string, string> Parametros { get; set; }
        public object Dados { get; set; }
        public string Ambiente { get; set; }
    }
}
