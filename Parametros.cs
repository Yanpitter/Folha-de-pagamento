using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaDePagamento
{
    public class Parametros
    {
        public decimal InssFaixas1 { get; set; }
        public decimal InssFaixas2 { get; set; }
        public decimal InssFaixas3 { get; set; }
        public decimal InssFaixas4 { get; set; }
        public decimal InssAliquotas1 { get; set; }
        public decimal InssAliquotas2 { get; set; }
        public decimal InssAliquotas3 { get; set; }
        public decimal InssAliquotas4 { get; set; }
        public decimal IrrfFaixas1 { get; set; }
        public decimal IrrfFaixas2 { get; set; }
        public decimal IrrfFaixas3 { get; set; }
        public decimal IrrfFaixas4 { get; set; }
        public decimal IrrfAliquotas1 { get; set; }
        public decimal IrrfAliquotas2 { get; set; }
        public decimal IrrfAliquotas3 { get; set; }
        public decimal IrrfAliquotas4 { get; set; }
        public decimal IrrfDeducoes1 { get; set; }
        public decimal IrrfDeducoes2 { get; set; }
        public decimal IrrfDeducoes3 { get; set; }
        public decimal IrrfDeducoes4 { get; set; }
        public decimal Fgts { get; set; }
        public decimal SalarioFamilia { get; set; }
        public decimal LimiteSalarioFamilia { get; set; }
        public decimal SalarioMinimo { get; set; }
    }
    public class BaseDeParametros
    {
        private readonly string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parametros.txt");

        public Parametros CarregarParametrosPorAno(string ano)
        {
            var linhas = File.ReadAllLines(path);
            var parametros = new Parametros();
            bool anoEncontrado = false;

            foreach (var linha in linhas)
            {
                if (linha.Trim() == $"[{ano}]")
                {
                    anoEncontrado = true;
                    continue;
                }

                if (anoEncontrado)
                {
                    if (string.IsNullOrWhiteSpace(linha)) break;

                    var partes = linha.Split('=');
                    if (partes.Length != 2) continue;

                    string chave = partes[0];
                    string valor = partes[1];

                    switch (chave)
                    {
                        case "INSS FAIXAS 1":
                            parametros.InssFaixas1 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "INSS FAIXAS 2":
                            parametros.InssFaixas2 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "INSS FAIXAS 3":
                            parametros.InssFaixas3 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "INSS FAIXAS 4":
                            parametros.InssFaixas4 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "INSS ALIQUOTAS 1":
                            parametros.InssAliquotas1 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "INSS ALIQUOTAS 2":
                            parametros.InssAliquotas2 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "INSS ALIQUOTAS 3":
                            parametros.InssAliquotas3 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "INSS ALIQUOTAS 4":
                            parametros.InssAliquotas4 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF FAIXAS 1":
                            parametros.IrrfFaixas1 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF FAIXAS 2":
                            parametros.IrrfFaixas2 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF FAIXAS 3":
                            parametros.IrrfFaixas3 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF FAIXAS 4":
                            parametros.IrrfFaixas4 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF ALIQUOTAS 1":
                            parametros.IrrfAliquotas1 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF ALIQUOTAS 2":
                            parametros.IrrfAliquotas2 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF ALIQUOTAS 3":
                            parametros.IrrfAliquotas3 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF ALIQUOTAS 4":
                            parametros.IrrfAliquotas4 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF DEDUCOES 1":
                            parametros.IrrfDeducoes1 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF DEDUCOES 2":
                            parametros.IrrfDeducoes2 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF DEDUCOES 3":
                            parametros.IrrfDeducoes3 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "IRRF DEDUCOES 4":
                            parametros.IrrfDeducoes4 = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "FGTS":
                            parametros.Fgts = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "SALARIO_FAMILIA":
                            parametros.SalarioFamilia = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "LIMITE_SALARIO_FAMILIA":
                            parametros.LimiteSalarioFamilia = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                        case "SALARIO_MINIMO":
                            parametros.SalarioMinimo = decimal.Parse(valor.Trim(), CultureInfo.InvariantCulture);
                            break;
                    }
                }
            }

            return parametros;
        }
        public decimal SalarioMinimoValor(string ano)
        {
            Parametros p = CarregarParametrosPorAno(ano);
            return p.SalarioMinimo;
        }
        public decimal CalcularINSS(decimal salario, string ano)
        {
            Parametros p = CarregarParametrosPorAno(ano);
            decimal total = 0;
            decimal faixaAnterior = 0;
            if (salario > p.InssFaixas1)
            {
                total += (p.InssFaixas1 - faixaAnterior) * p.InssAliquotas1;
                faixaAnterior = p.InssFaixas1;
            }
            else
            {
                total += (salario - faixaAnterior) * p.InssAliquotas1;
                return Math.Round(total, 2);
            }

            // Faixa 2
            if (salario > p.InssFaixas2)
            {
                total += (p.InssFaixas2 - faixaAnterior) * p.InssAliquotas2;
                faixaAnterior = p.InssFaixas2;
            }
            else
            {
                total += (salario - faixaAnterior) * p.InssAliquotas2;
                return Math.Round(total, 2);
            }

            // Faixa 3
            if (salario > p.InssFaixas3)
            {
                total += (p.InssFaixas3 - faixaAnterior) * p.InssAliquotas3;
                faixaAnterior = p.InssFaixas3;
            }
            else
            {
                total += (salario - faixaAnterior) * p.InssAliquotas3;
                return Math.Round(total, 2);
            }

            // Faixa 4 (última)
            if (salario > p.InssFaixas4)
            {
                total += (p.InssFaixas4 - faixaAnterior) * p.InssAliquotas4;
            }
            else
            {
                total += (salario - faixaAnterior) * p.InssAliquotas4;
            }

            return Math.Round(total, 2);

        }
        public decimal CalcularIRRF(decimal baseCalculo, string ano)
        {
            Parametros p = CarregarParametrosPorAno(ano);
            if (baseCalculo <= p.IrrfFaixas1)
                return 0;

            if (baseCalculo <= p.IrrfFaixas2)
                return Math.Round((baseCalculo * p.IrrfAliquotas1) - p.IrrfDeducoes1, 2);

            if (baseCalculo <= p.IrrfFaixas3)
                return Math.Round((baseCalculo * p.IrrfAliquotas2) - p.IrrfDeducoes2, 2);

            if (baseCalculo <= p.IrrfFaixas4)
                return Math.Round((baseCalculo * p.IrrfAliquotas3) - p.IrrfDeducoes3, 2);

            // Acima da última faixa
            return Math.Round((baseCalculo * p.IrrfAliquotas4) - p.IrrfDeducoes4, 2);

        }
        public decimal CalcularFgts(decimal baseCalculo, string ano)
        {
            Parametros p = CarregarParametrosPorAno(ano);
            return Math.Round((baseCalculo * p.Fgts), 2);

        }
    }

}
