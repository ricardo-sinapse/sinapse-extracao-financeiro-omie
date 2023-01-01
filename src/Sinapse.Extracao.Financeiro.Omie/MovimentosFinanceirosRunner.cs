using OfficeOpenXml;
using Sinapse.Extracao.Financeiro.Omie.Interfaces;
using Sinapse.Extracao.Financeiro.Omie.Models;
using Sinapse.Extracao.Financeiro.Omie.Models.ServiceBus;
using System.Globalization;
using System.Text.Json;

namespace Sinapse.Extracao.Financeiro.Omie
{
    public class MovimentosFinanceirosRunner
    {
        private readonly IServiceBusApplication _serviceBusApp;

        public MovimentosFinanceirosRunner(IServiceBusApplication serviceBusApp)
        {
            _serviceBusApp = serviceBusApp;
        }

        public async Task StartRunner()
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            //using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo("c:\\sinapse\\files\\temp\\CAP_Planejamento.xlsx")))
            //{
            //    var myWorksheet = xlPackage.Workbook.Worksheets["Planilha1"]; //select sheet here
            //    var totalRows = myWorksheet.Dimension.End.Row;
            //    var totalColumns = myWorksheet.Dimension.End.Column;

            //    for (int rowNum = 3; rowNum <= totalRows; rowNum++) //select starting row here
            //    {
            //        try
            //        {
            //            if (myWorksheet.GetValue(rowNum, 15)!.ToString() == "Lançamento Manual de Despesa")
            //                throw new Exception("Lançamento Manual de Despesa");

            //            Console.Write(rowNum);

            //            Pessoa pessoaOmie = new();
            //            if(!string.IsNullOrWhiteSpace(myWorksheet.GetValue(rowNum, 7)?.ToString()))
            //                pessoaOmie = await _serviceBusApp.ConsultarPessoaPeloCpfCnpjAsync("OMIE", myWorksheet.GetValue(rowNum, 7)?.ToString());
            //            else
            //                pessoaOmie = await _serviceBusApp.ConsultarPessoaPeloNomeFantasiaAsync("OMIE", myWorksheet.GetValue(rowNum, 8)?.ToString());

            //            if (pessoaOmie is null)
            //                throw new Exception("Cliente não encontrado no Omie");

            //            var dataPesquisa = ObterData(myWorksheet.GetValue(rowNum, 3)!.ToString());
            //            var contasPagar = await _serviceBusApp.ConsultarContasPagarPelaPessoaEDataRegistroAsync("OMIE", pessoaOmie.CodigoPessoa, dataPesquisa, dataPesquisa);
            //            if (contasPagar is null || contasPagar.Count() == 0)
            //                throw new Exception("Não foi possível localizar o conta pagar no Omie");

            //            ContaPagar contaPagar = new();
            //            if (contasPagar.Count() > 1)
            //                contaPagar = ObterContaPagar(contasPagar.ToList(), myWorksheet, rowNum);
            //            else
            //                contaPagar = contasPagar.FirstOrDefault();

            //            if (contaPagar is null)
            //                throw new Exception("Não foi possível identificar o título na planilha");

            //            Console.WriteLine($" => {contaPagar.CodigoLancamento}");
            //            //if (contaPagar.Status == "Pago")
            //            //    await _serviceBusApp.CancelarPagamentoAsync("OMIE", contaPagar.CodigoLancamento);

            //            //contaPagar.DataEmissao = ObterData(myWorksheet.GetValue(rowNum, 2)!.ToString());
            //            //contaPagar.RateioCategorias = new List<RateioCategoria>
            //            //{
            //            //    new RateioCategoria
            //            //    {
            //            //        CodigoCategoria = (await _serviceBusApp.ConsultarCategoriaPeloNomeAsync("OMIE", myWorksheet.GetValue(rowNum, 12)!.ToString())).CodigoCategoria,
            //            //        Percentual = 100,
            //            //        Valor = Math.Abs(decimal.Parse(myWorksheet.GetValue(rowNum, 16)!.ToString()))
            //            //    }
            //            //};
            //            //contaPagar.RateioDepartamentos = await ObterRateioDepartamentoAsync(myWorksheet, rowNum);
            //            //contaPagar.ValorDocumento = Math.Abs(decimal.Parse(myWorksheet.GetValue(rowNum, 16)!.ToString()));

            //            //await _serviceBusApp.AtualizarContaPagarAsync("OMIE", contaPagar);

            //            //if (!string.IsNullOrWhiteSpace(myWorksheet.GetValue(rowNum, 6)!.ToString()))
            //            //{
            //            //    await _serviceBusApp.LancarPagamentoAsync("OMIE", new Pagamento
            //            //    {
            //            //        CodigoLancamento = contaPagar.CodigoLancamento,
            //            //        CodigoContaCorrente = contaPagar.CodigoContaCorrente,
            //            //        DataPagamento = ObterData(myWorksheet.GetValue(rowNum, 6)!.ToString()),
            //            //        Valor = Math.Abs(decimal.Parse(myWorksheet.GetValue(rowNum, 19)!.ToString())),
            //            //        Desconto = Math.Abs(decimal.Parse(myWorksheet.GetValue(rowNum, 23)!.ToString())),
            //            //        Juros = Math.Abs(decimal.Parse(myWorksheet.GetValue(rowNum, 24)!.ToString())),
            //            //        Multa = Math.Abs(decimal.Parse(myWorksheet.GetValue(rowNum, 25)!.ToString())),
            //            //    });
            //            //}

            //            //Console.WriteLine(" => OK");
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine($" => {ex.Message}");
            //            continue;
            //        }
            //    }
            //}

            List<MovimentoFinanceiro> movimentosTratados = new();

            var intervaloDatas = CalcularIntervaloDatas(new DateTime(2022, 11, 1), new DateTime(2022, 11, 10));
            foreach (var intervalo in intervaloDatas)
            {
                try
                {
                    Console.WriteLine($"{intervalo.DataInicial:dd/MM/yyyy} - {intervalo.DataFinal:dd/MM/yyyy}");

                    var movimentos = await _serviceBusApp.ConsultarMovimentosPelaDataInclusaoAsync("OMIE",
                    intervalo.DataInicial, intervalo.DataFinal);


                    foreach (var movimento in movimentos)
                    {
                        var contaCorrente = await _serviceBusApp.ConsultarContaCorrentePeloCodigoAsync("OMIE", movimento.CodigoContaCorrente);

                        Pessoa pessoa = new Pessoa();
                        if (!string.IsNullOrWhiteSpace(movimento.CodigoPessoa))
                            pessoa = await _serviceBusApp.ConsultarPessoaPeloCodigoAsync("OMIE", movimento.CodigoPessoa);
                        else
                            pessoa = null;

                        Projeto projeto = new();
                        if (!string.IsNullOrWhiteSpace(movimento.CodigoProjeto))
                            projeto = await _serviceBusApp.ConsultarProjetoPeloCodigoAsync("OMIE", movimento.CodigoProjeto);
                        else
                            projeto = null;

                        Vendedor vendedor = new();

                        if (!string.IsNullOrWhiteSpace(movimento.CodigoVendedor))
                            vendedor = await _serviceBusApp.ConsultarVendedorPeloCodigoAsync("OMIE", movimento.CodigoVendedor);
                        else
                            vendedor = null;

                        movimentosTratados.Add(new MovimentoFinanceiro
                        {
                            CodigoTitulo = movimento.CodigoTitulo,
                            Categorias = movimento.Categorias,
                            CodigoContaCorrente = movimento.CodigoContaCorrente,
                            NomeContaCorrente = contaCorrente.Descricao,
                            NumeroDocumentoFiscal = movimento.NumeroDocumentoFiscal,
                            CodigoPessoa = movimento.CodigoPessoa,
                            NomePessoa = pessoa?.RazaoSocial,
                            CpfCnpj = pessoa?.CnpjCpf,
                            CodigoProjeto = movimento.CodigoProjeto,
                            NomeProjeto = projeto?.NomeProjeto,
                            CodigoVendedor = movimento.CodigoVendedor,
                            NomeVendedor = vendedor?.NomeVendedor,
                            DataEmissao = movimento.DataEmissao,
                            DataPagamento = movimento.DataPagamento,
                            DataPrevisao = movimento.DataPrevisao,
                            DataRegistro = movimento.DataRegistro,
                            DataVencimento = movimento.DataVencimento,
                            Departamentos = movimento.Departamentos,
                            Grupo = movimento.Grupo,
                            Natureza = movimento.Natureza,
                            NumeroBoleto = movimento.NumeroBoleto,
                            NumeroContrato = movimento.NumeroContrato,
                            NumeroOrdemServico = movimento.NumeroOrdemServico,
                            NumeroParcela = movimento.NumeroParcela,
                            NumeroTitulo = movimento.NumeroTitulo,
                            Operacao = movimento.Operacao,
                            Status = movimento.Status,
                            Tipo = movimento.Tipo,
                            ValorCOFINS = movimento.ValorCOFINS,
                            ValorCSLL = movimento.ValorCSLL,
                            ValorDesconto = movimento.ValorDesconto,
                            ValorINSS = movimento.ValorINSS,
                            ValorIR = movimento.ValorIR,
                            ValorISS = movimento.ValorISS,
                            ValorJuros = movimento.ValorJuros,
                            ValorLiquido = movimento.ValorLiquido,
                            ValorMulta = movimento.ValorMulta,
                            ValorPago = movimento.ValorPago,
                            ValorPIS = movimento.ValorPIS,
                            ValorTitulo = movimento.ValorTitulo
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            File.WriteAllText($"c:\\sinapse\\files\\temp\\extracao-movimentos{DateTime.Now:yyyy-MM-dd_HH-mm}.json", JsonSerializer.Serialize(movimentosTratados));
        }



        private IEnumerable<IntervaloData> CalcularIntervaloDatas(DateTime dataInicial, DateTime dataFinal)
        {
            var intervaloDatas = new List<IntervaloData>();
            if ((dataFinal - dataInicial).TotalDays > 10)
            {
                var data = dataInicial;

                do
                {
                    if (data.AddDays(10) <= dataFinal)
                        intervaloDatas.Add(new IntervaloData { DataInicial = data, DataFinal = data.AddDays(10) });
                    else
                        intervaloDatas.Add(new IntervaloData { DataInicial = data, DataFinal = data.AddDays((dataFinal - data).TotalDays) });

                    data = data.AddDays(11);
                } while (data <= dataFinal);
            }
            else
            {
                intervaloDatas.Add(new IntervaloData { DataInicial = dataInicial, DataFinal = dataFinal });
            }

            return intervaloDatas;
        }
    }
}
