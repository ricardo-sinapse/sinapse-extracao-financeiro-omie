namespace Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus
{
    public class Pessoa
    {
        public string CodigoPessoa { get; set; }
        public string CodigoIntegracao { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeContato { get; set; }
        public string EmailContato { get; set; }
        public string RazaoSocial { get; set; }
        public string TelefoneContato { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }

        public string HomePage { get; set; }
        public string CnpjCpf { get; set; }
        public bool PessoaFisica { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }
        public string DocumentoInternacional { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string ObservacoesInternas { get; set; }
        public Dictionary<string, string> InformacoesAdicionais { get; set; }
    }
}   
