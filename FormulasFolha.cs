using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaDePagamento
{
    public partial class FormulasFolha 
    {
        
        public decimal calcularHoraExtra50(decimal salario, decimal horas)
        {
            decimal horaExtra50 = (salario / 220) * 1.5m * horas;
            return horaExtra50;
        }

        public decimal calcularHoraExtra100(decimal salario, decimal horas)
        {
            decimal horaExtra100 = ((salario / 220) * 2) * horas;
            return horaExtra100;
        }

        public decimal calcularComissao(decimal valorVenda, decimal percentualComissao)
        {
            {
                decimal comissao = valorVenda * (percentualComissao / 100);
                return comissao;
            }
        }

        public decimal calcularRepousoRemunerado(decimal salarioMensal, int diasUteis, int diasDeDSR)
        {
            decimal repousoRemunerado = (salarioMensal / diasUteis) * diasDeDSR;
            return repousoRemunerado;
        }

        public decimal calcularAdicionalNoturno(decimal salarioBase, decimal horasNoturnas)
        {
            decimal valorHorasDiurnas = (salarioBase / 220);
            decimal adicionalNoturno = (valorHorasDiurnas * 0.2m) + valorHorasDiurnas;
            return adicionalNoturno * horasNoturnas;
        }

        public decimal calcularInsalubridade(decimal salarioMinimo, decimal percentualInsalubridade)
        {
            decimal adicionalInsalubridade = salarioMinimo * (percentualInsalubridade / 100);
            return adicionalInsalubridade;
        }

        public decimal calcularPericulosidade(decimal salarioBase)
        {
            decimal adicionalPericulosidade = (salarioBase * 0.3m);
            return adicionalPericulosidade;
        }

        public decimal calcularSalarioFamilia(int dependentes, decimal valorCotaDependente)
        {
            decimal salarioFamilia = (dependentes * valorCotaDependente);
            return salarioFamilia;
        }

        public decimal calcularFerias(decimal salarioBruto, decimal INSS, decimal IRRF)
        {
            decimal salarioFerias = salarioBruto + (salarioBruto / 3);
            decimal descontos = (salarioFerias * INSS) + (salarioFerias * IRRF);
            return salarioFerias - descontos;
        }

        public decimal calcularDecimoTerceiro(decimal salarioBruto, int mesesTrabalhados)
        {
            decimal decimoTerceiro = (salarioBruto / 12) * mesesTrabalhados;
            return decimoTerceiro;
        }

        public decimal calcularParticipacaoLucros(decimal lucroLiquido, decimal percentualPLR)
        {
            decimal plr = lucroLiquido * percentualPLR / 100;
            return plr;
        }

        public decimal calculoAbonosalarial(decimal SalarioMinimo, decimal MesesTrabalhados)
        {

            decimal Abonosalarial = (SalarioMinimo / 12) * MesesTrabalhados;
            return Abonosalarial;
        }

        public decimal CalcularFaltas(decimal Salario, decimal Faltas)
        {
            decimal ValorFaltas = (Salario / 220) * Faltas;
            return ValorFaltas;
        }
        public decimal CalcularTransporte(decimal Salario, decimal PorcentagemValeT)
        {
            decimal valorfinal;
            decimal ValorVT = (Salario * (PorcentagemValeT / 100));
            valorfinal = ValorVT > 200 ? 200 : ValorVT;
            return valorfinal;
        }
        public decimal CalcularAdiantamento(decimal Salario, decimal PorcentagemAdiantamento)
        {
            decimal CalculoAdiantamento = (Salario * (PorcentagemAdiantamento / 100));
            return CalculoAdiantamento;
        }
        public decimal CalcularPensaoBruto(decimal SalarioBruto, decimal PorcentagemPensao)
        {
            decimal CalculoPensaoBruto = (SalarioBruto * (PorcentagemPensao / 100));
            return CalculoPensaoBruto;

        }
        public decimal CalcularPensaoliquido(decimal salarioliquido, decimal PorcentagemPensao)
        {
            decimal cauculoPensaoliquido = (salarioliquido * PorcentagemPensao / 100);
            return cauculoPensaoliquido;


        }


        public decimal CalcularEmprestimoConsignado(decimal salarioLiquido, decimal porcentagem_emprestimo)
        {
            decimal emprestimoConsignado = salarioLiquido * (porcentagem_emprestimo / 100);
            return emprestimoConsignado;

        }
        public decimal SomarAdicionais(Panel painel)
        {
            decimal total = 0;

            for (int i = 0; i < painel.Controls.Count; i++)
            {
                Control c = painel.Controls[i];

                if (c is TextBox txt && txt.Name.Contains("Valor"))
                {
                    if (decimal.TryParse(txt.Text.Replace("R$", "").Trim(), out decimal valor))
                    {
                        total += valor;
                    }
                }
            }

            return total;
        }

    }
}
