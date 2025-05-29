using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FolhaDePagamento
{
    public partial class TabelaImposto
    {
        public int Ano { get; set; }
        public string Tipo { get; set; } // INSS, IRRF, FGTS
        public double FaixaInicial { get; set; }
        public double FaixaFinal { get; set; }
        public double Aliquota { get; set; }
        public double Deducao { get; set; }
    }

    public class CalculadoraImpostos
    {
        private List<TabelaImposto> _tabelas;

        public CalculadoraImpostos(string caminhoArquivo)
        {
            caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tabelas.txt");
            _tabelas = CarregarTabelas(caminhoArquivo);
        }

        private List<TabelaImposto> CarregarTabelas(string caminho)
        {
            var linhas = File.ReadAllLines(caminho);
            var tabelas = new List<TabelaImposto>();

            foreach (var linha in linhas)
            {
                if (string.IsNullOrWhiteSpace(linha) || linha.StartsWith("#")) continue;

                var partes = linha.Split(';');
                var tabela = new TabelaImposto
                {
                    Ano = int.Parse(partes[0]),
                    Tipo = partes[1],
                    FaixaInicial = double.Parse(partes[2], CultureInfo.InvariantCulture),
                    FaixaFinal = double.Parse(partes[3], CultureInfo.InvariantCulture),
                    Aliquota = double.Parse(partes[4], CultureInfo.InvariantCulture),
                    Deducao = double.Parse(partes[5], CultureInfo.InvariantCulture)
                };

                tabelas.Add(tabela);
            }

            return tabelas;
        }

        public (double inss, double irrf, double fgts) Calcular(int ano, double salarioBruto)
        {
            double inss = CalcularImposto(ano, salarioBruto, "INSS");
            double irrf = CalcularImposto(ano, salarioBruto - inss, "IRRF");
            double fgts = CalcularImposto(ano, salarioBruto, "FGTS");

            return (inss, irrf, fgts);
        }

        private double CalcularImposto(int ano, double baseCalculo, string tipo)
        {
            var faixas = _tabelas
                .Where(t => t.Ano == ano && t.Tipo == tipo)
                .OrderBy(t => t.FaixaInicial)
                .ToList();

            if (tipo == "IRRF")
            {
                var faixa = faixas.FirstOrDefault(f => baseCalculo >= f.FaixaInicial && baseCalculo <= f.FaixaFinal);
                if (faixa != null)
                {
                    double irrf = (baseCalculo * faixa.Aliquota) - faixa.Deducao;
                    return Math.Max(irrf, 0);
                }
                return 0;
            }
            else
            {
                double total = 0;
                foreach (var faixa in faixas)
                {
                    if (baseCalculo > faixa.FaixaFinal)
                    {
                        total += (faixa.FaixaFinal - faixa.FaixaInicial) * faixa.Aliquota;
                    }
                    else if (baseCalculo > faixa.FaixaInicial)
                    {
                        total += (baseCalculo - faixa.FaixaInicial) * faixa.Aliquota;
                        break;
                    }
                }
                return Math.Max(total, 0);
            }
        }
    }
}

