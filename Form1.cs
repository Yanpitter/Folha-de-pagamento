using FolhaDePagamento;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaDePagamento
{
    public partial class Form1 : Form
    {
        private bool temaEscuroAtivo = false;
        public int yOffset = 0;
        public int yOffset2 = 0;
        FormulasFolha formulas = new FormulasFolha();
        CalculadoraImpostos impostos = new CalculadoraImpostos("caminho/para/tabelas.txt");
        Validacoes validar = new Validacoes();
        Acessibilidade acessibilidade = new Acessibilidade();
        Parametros parametros = new Parametros();
        BaseDeParametros parametrosBase = new BaseDeParametros();

        public bool validarStatus = true;

        public string nomeFuncionario, cargo, cepEmpresa, logradouroFuncionario, bairroFuncionario,
            cidadeFuncionario, razaoSocial, logradouroEmpresa, numeroEmpresa, bairroEmpresa,
            cidadeEmpresa, tipoPensao, matricula, pis, numeroFuncionario, cep, cnpj, cpf;
        public int familiaDependentes, descancoRemunerado, diasRemunerado, ano;
        public decimal salarioBase, participacaoLucroPorcentagem, participacaoLucroLiquido,
            adicionalNoturno, ajudaCusto, comissaoValorTotal, adiantamento, familiaCota,
            hora50, hora100, beneficio, seguroVida, planoSaude, previdencia, faltas,
            emprestimo, valeTransporte, comissaoPorcentagem, abono, auxilioCreche,
            insalubridade, pensao, valeAlimentacao, sindicato, desconto, valorPlucro,
            valorAn, valorAc, valorInsalubridade, valorCreche, valorAbono, valorComissao,
            valorFamilia, valorDescanco, valorHora50, valorHora100, valorTransporte,
            valorSeguro, valorSaude, valorPrevidencia, valorEmprestimo, valorFaltas,
            valorPensaobruto, valorAlimentacao, valorSindicato, valorPericulosidade,
            valorPensaoliquido, valorAdiantamento, valorFgts, valorInss, ValorIrrf,
            salarioLiquido, salarioBruto;

        private void cmbMesAbono_Click(object sender, EventArgs e)
        {
            // Verifica se tem algo selecionado para o sistema não dar erro
            if (cmbMesAbono.SelectedItem != null)
            {
                // Salva o mês escolhido em uma variável
                string mesEscolhido = cmbMesAbono.SelectedItem.ToString();

                // Aqui você continua a sua lógica! 
                // Exemplo de teste para ver se funcionou:
            }
            else
            {
                MessageBox.Show("Por favor, selecione o mês do abono na lista.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton3_click(object sender, EventArgs e)
        {
            bool ativar = (validarStatus == false);
            validarStatus = ativar;

            bool estado = ativar;
            txtNome.Enabled = estado; txtMatricula.Enabled = estado; maskCpf.Enabled = estado;
            txtCargo.Enabled = estado; txtLogradouro_Funcionario.Enabled = estado;
            txtCidade_funcionario.Enabled = estado; txtBairro_funcionario.Enabled = estado;
            maskPis.Enabled = estado; txtNumero_Funcionario.Enabled = estado;
            txtRazao_Social.Enabled = estado; txtBairro_empresa.Enabled = estado;
            txtCidade_empresa.Enabled = estado; txtLogradouro_empresa.Enabled = estado;
            maskCnpj.Enabled = estado; maskCep_empresa.Enabled = estado;
            maskCep_funcionario.Enabled = estado; txtnumero_empresa.Enabled = estado;
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            temaEscuroAtivo = !temaEscuroAtivo;
            acessibilidade.AplicarTema(this, temaEscuroAtivo);
        }

        private void txtnumero_empresa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRazao_Social_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_click(object sender, EventArgs e)
        {
            #region Adicionar validação no campo CPF, para aceitar apenas CPFs validos.
            // Permite apenas CPFs validos
            if (!Validacoes.ValidarCPF(maskCpf.Text))
            {
                MessageBox.Show("O CPF informado é inválido!", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskCpf.Focus();
                return;
            }
            #endregion

            #region Nos campos de CPF, PIS-PASEP, CEP, CNPJ está aceitando sem estar corretamente preenchido. 
            // Validação para CNPJ (deve ter 14 números)
            if (maskCnpj.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim().Length < 14)
            {
                MessageBox.Show("CNPJ incompleto!");
                maskCnpj.Focus();
                return;
            }

            // Validação para PIS (deve ter 11 números)
            if (maskPis.Text.Replace(".", "").Replace("-", "").Trim().Length < 11)
            {
                MessageBox.Show("PIS incompleto!");
                maskPis.Focus();
                return;
            }
            #endregion

            familiaDependentes = 0; descancoRemunerado = 0; diasRemunerado = 0; ano = 0;
            salarioBase = 0; participacaoLucroPorcentagem = 0; participacaoLucroLiquido = 0;
            adicionalNoturno = 0; ajudaCusto = 0; comissaoValorTotal = 0; adiantamento = 0;
            familiaCota = 0; hora50 = 0; hora100 = 0; beneficio = 0; seguroVida = 0;
            planoSaude = 0; previdencia = 0; faltas = 0; emprestimo = 0; valeTransporte = 0;
            comissaoPorcentagem = 0; abono = 0; auxilioCreche = 0; insalubridade = 0;
            pensao = 0; valeAlimentacao = 0; sindicato = 0; desconto = 0;
            valorPlucro = 0; valorAn = 0; valorAc = 0; valorInsalubridade = 0; valorCreche = 0;
            valorAbono = 0; valorComissao = 0; valorFamilia = 0; valorDescanco = 0;
            valorHora50 = 0; valorHora100 = 0; valorTransporte = 0; valorSeguro = 0;
            valorSaude = 0; valorPrevidencia = 0; valorEmprestimo = 0; valorFaltas = 0;
            valorPensaobruto = 0; valorAlimentacao = 0; valorSindicato = 0; valorPericulosidade = 0;
            valorPensaoliquido = 0; valorAdiantamento = 0; salarioLiquido = 0; salarioBruto = 0;

            //if (validarStatus == true && (validar.ValidarDados(txtNome) || validar.ValidarDados(txtLogradouro_Funcionario) || validar.ValidarDados(txtBairro_funcionario) || validar.ValidarDados(txtCidade_funcionario) || validar.ValidarDados(txtCargo) || validar.ValidarDados(txtBairro_empresa) || validar.ValidarDados(txtCidade_empresa) || validar.ValidarDados(txtRazao_Social) || validar.ValidarDados(txtLogradouro_empresa) || validar.ValidarDados(txtnumero_empresa) || validar.ValidarDados(txtNumero_Funcionario))) return;
            if (validar.ValidarDados(txtSalario_base)) return;

            if (validar.ValidarDadosDecimal(chkParticipacaoLucros, txtParticipacao_de_lucro_percentual) ||
                validar.ValidarDadosDecimal(chkAdicionalNoturno, txtAdicional_noturno) ||
                validar.ValidarDadosDecimal(chkAjudaCusto, txtAjuda_de_custo) ||
                validar.ValidarDadosDecimal(chkComissao, txtComissao_valor_total) ||
                validar.ValidarDadosDecimal(chkComissao, txtComissao_percentual) ||
                validar.ValidarDadosDecimal(chkAuxilioCreche, txtAuxilio_creche) ||
                validar.ValidarDadosDecimal(chkAuxilioFamilia, txtAuxilio_familia_valor_da_cota) ||
                validar.ValidarDadosDecimal(chkValeTransporte, txtVale_transporte) ||
                validar.ValidarDadosDecimal(chkSeguroVida, txtSeguro_de_vida) ||
                validar.ValidarDadosDecimal(chkPlanoSaude, txtPlano_de_saude) ||
                validar.ValidarDadosDecimal(chkPrevidencia, txtPrevidencia_privada) ||
                validar.ValidarDadosDecimal(chkEmprestimo, txtEmprestimo_consiganado) ||
                validar.ValidarDadosDecimal(chkFaltas, txtFaltas) ||
                validar.ValidarDadosDecimal(chkValeAlimentacao, txtVale_alimentacao) ||
                validar.ValidarDadosDecimal(chkSindicato, txtSindicato) ||
                validar.ValidarDadosDecimal(chkAdiantamento, txtAdiantamento) ||
                validar.ValidarDadosDecimal(chk50He, txtHora_extra_50) ||
                validar.ValidarDadosDecimal(chk100He, txtHora_extra_100) ||
                validar.ValidarDadosDecimal(chkPensao, txtPensao_porcentagem, cmbPensao_tipo) ||
                validar.ValidarDadosDecimal(chkParticipacaoLucros, txtParticipacao_de_lucros_valor_liquido)) return;

            if (validar.ValidarDadosInt(chkDescancoRemunerado, txtDescanco_remunerado_dias_uteis) ||
                validar.ValidarDadosInt(chkAuxilioFamilia, txtAuxilio_familia_numero_de_dependentes) ||
                validar.ValidarDadosInt(chkDescancoRemunerado, txtDescanco_remunerado_dias_descanco) ||
                validar.ValidarDadosInt(chkInsalubridade, cmbInsalubridade_porcentagem)) return;

            if (validar.ValidarDadosAdicionais(panelBeneficios) || validar.ValidarDadosAdicionais(PanelDescontos)) return;

            nomeFuncionario = txtNome.Text;
            matricula = txtMatricula.Text;
            cpf = maskCpf.Text;
            cargo = txtCargo.Text;
            pis = maskPis.Text;
            logradouroFuncionario = txtLogradouro_Funcionario.Text;
            numeroFuncionario = txtNumero_Funcionario.Text;
            bairroFuncionario = txtBairro_funcionario.Text;
            cidadeFuncionario = txtCidade_funcionario.Text;
            cep = maskCep_funcionario.Text;
            razaoSocial = txtRazao_Social.Text;
            cnpj = maskCnpj.Text;
            tipoPensao = cmbPensao_tipo.Text;
            logradouroEmpresa = txtLogradouro_empresa.Text;
            numeroEmpresa = txtnumero_empresa.Text;
            bairroEmpresa = txtBairro_empresa.Text;
            cidadeEmpresa = txtCidade_empresa.Text;
            cepEmpresa = maskCep_empresa.Text;

            decimal.TryParse(txtSalario_base.Text, out salarioBase);
            decimal.TryParse(txtParticipacao_de_lucro_percentual.Text, out participacaoLucroPorcentagem);
            decimal.TryParse(txtParticipacao_de_lucros_valor_liquido.Text, out participacaoLucroLiquido);

            // #12 - Insalubridade: remover % do texto
            string insalubridadeTexto = cmbInsalubridade_porcentagem.Text.Replace("%", "").Trim();
            decimal.TryParse(insalubridadeTexto, out insalubridade);

            decimal.TryParse(txtAjuda_de_custo.Text, out ajudaCusto);
            decimal.TryParse(cmbMesAbono.SelectedItem.ToString(), out abono);
            decimal.TryParse(txtComissao_valor_total.Text, out comissaoValorTotal);
            decimal.TryParse(txtComissao_percentual.Text, out comissaoPorcentagem);
            decimal.TryParse(txtHora_extra_50.Text, out hora50);
            decimal.TryParse(txtAuxilio_familia_valor_da_cota.Text, out familiaCota);
            decimal.TryParse(txtHora_extra_100.Text, out hora100);
            decimal.TryParse(txtVale_transporte.Text, out valeTransporte);
            decimal.TryParse(txtSeguro_de_vida.Text, out seguroVida);
            decimal.TryParse(txtPlano_de_saude.Text, out planoSaude);
            decimal.TryParse(txtPrevidencia_privada.Text, out previdencia);
            decimal.TryParse(txtEmprestimo_consiganado.Text, out emprestimo);
            decimal.TryParse(txtFaltas.Text, out faltas);
            decimal.TryParse(txtPensao_porcentagem.Text, out pensao);
            decimal.TryParse(txtVale_alimentacao.Text, out valeAlimentacao);
            decimal.TryParse(txtSindicato.Text, out sindicato);
            decimal.TryParse(txtAdiantamento.Text, out adiantamento);
            decimal.TryParse(txtAdicional_noturno.Text, out adicionalNoturno);
            decimal.TryParse(txtAuxilio_creche.Text, out auxilioCreche);

            int.TryParse(txtAuxilio_familia_numero_de_dependentes.Text, out familiaDependentes);
            int.TryParse(cmbAno.Text, out ano);
            int.TryParse(txtDescanco_remunerado_dias_uteis.Text, out diasRemunerado);
            int.TryParse(txtDescanco_remunerado_dias_descanco.Text, out descancoRemunerado);

            if (adiantamento > 40)
            {
                MessageBox.Show("Valor do adiantamento não pode ser superior a 40%", "Dado inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdiantamento.Focus(); return;
            }
            if (valeTransporte > 6)
            {
                MessageBox.Show("Valor do vale transporte não pode ser superior a 6%", "Dado inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVale_transporte.Focus(); return;
            }
            if (emprestimo > 30)
            {
                MessageBox.Show("Valor do empréstimo não pode ser superior a 30%", "Dado inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmprestimo_consiganado.Focus(); return;
            }

            // Cálculos usando CheckBoxes (#15)
            valorPlucro = participacaoLucroLiquido != 0 && chkParticipacaoLucros.Checked ? formulas.calcularParticipacaoLucros(participacaoLucroLiquido, participacaoLucroPorcentagem) : 0;
            valorAn = adicionalNoturno != 0 && chkAdicionalNoturno.Checked ? formulas.calcularAdicionalNoturno(salarioBase, adicionalNoturno) : 0;
            valorAc = chkAjudaCusto.Checked ? ajudaCusto : 0;
            valorInsalubridade = insalubridade != 0 && chkInsalubridade.Checked ? formulas.calcularInsalubridade(parametrosBase.SalarioMinimoValor(ano.ToString()), insalubridade) : 0;
            valorCreche = chkAuxilioCreche.Checked ? auxilioCreche : 0;
            valorAbono = abono != 0 && chkAbono.Checked ? formulas.calculoAbonosalarial(parametrosBase.SalarioMinimoValor(ano.ToString()), abono) : 0;
            valorComissao = comissaoPorcentagem != 0 && chkComissao.Checked ? formulas.calcularComissao(comissaoValorTotal, comissaoPorcentagem) : 0;
            valorFamilia = familiaCota != 0 && chkAuxilioFamilia.Checked ? formulas.calcularSalarioFamilia(familiaDependentes, familiaCota) : 0;
            valorDescanco = descancoRemunerado != 0 && chkDescancoRemunerado.Checked ? formulas.calcularRepousoRemunerado(salarioBase, diasRemunerado, descancoRemunerado) : 0;
            valorHora50 = hora50 != 0 && chk50He.Checked ? formulas.calcularHoraExtra50(salarioBase, hora50) : 0;
            valorHora100 = hora100 != 0 && chk100He.Checked ? formulas.calcularHoraExtra100(salarioBase, hora100) : 0;
            valorPericulosidade = chkPericulosidade.Checked ? formulas.calcularPericulosidade(salarioBase) : 0;

            valorTransporte = valeTransporte != 0 && chkValeTransporte.Checked ? formulas.CalcularTransporte(salarioBase, valeTransporte) : 0;
            valorSeguro = chkSeguroVida.Checked ? seguroVida : 0;
            valorSaude = chkPlanoSaude.Checked ? planoSaude : 0;
            valorPrevidencia = chkPrevidencia.Checked ? previdencia : 0;
            valorFaltas = faltas != 0 && chkFaltas.Checked ? formulas.CalcularFaltas(salarioBase, faltas) : 0;
            valorAlimentacao = chkValeAlimentacao.Checked ? valeAlimentacao : 0;
            valorSindicato = chkSindicato.Checked ? sindicato : 0;
            valorAdiantamento = adiantamento != 0 && chkAdiantamento.Checked ? formulas.CalcularAdiantamento(salarioBase, adiantamento) : 0;
            valorFgts = parametrosBase.CalcularFgts(salarioBase, ano.ToString());
            valorInss = parametrosBase.CalcularINSS(salarioBase, ano.ToString());
            ValorIrrf = parametrosBase.CalcularIRRF(salarioBase, ano.ToString());

            salarioBruto = salarioBase + valorPlucro + valorAn + valorAc + valorInsalubridade + valorCreche + valorAbono + valorComissao + valorFamilia + valorDescanco + valorHora50 + valorHora100 + valorPericulosidade + formulas.SomarAdicionais(panelBeneficios);

            valorPensaobruto = tipoPensao == "Sobre provento bruto" && chkPensao.Checked ? formulas.CalcularPensaoBruto(salarioBruto, pensao) : 0;
            if (tipoPensao == "Sobre provento bruto") salarioBruto -= valorPensaobruto;

            salarioLiquido = salarioBruto - valorTransporte - valorSeguro - valorSaude - valorPrevidencia - valorFaltas - valorAlimentacao - valorSindicato - valorAdiantamento - valorInss - ValorIrrf - formulas.SomarAdicionais(PanelDescontos);

            valorPensaoliquido = tipoPensao == "Sobre provento liquido" && chkPensao.Checked ? formulas.CalcularPensaoliquido(salarioLiquido, pensao) : 0;
            if (tipoPensao == "Sobre provento liquido") salarioLiquido -= valorPensaoliquido;

            valorEmprestimo = emprestimo != 0 && chkEmprestimo.Checked ? formulas.CalcularEmprestimoConsignado(salarioLiquido, emprestimo) : 0;
            if (emprestimo != 0) salarioLiquido -= valorEmprestimo;

            var ganhos = new List<(string, decimal)>();
            var descontos = new List<(string, decimal)>();

            if (valorPlucro > 0) ganhos.Add(("Participação de Lucros", valorPlucro));
            if (valorAn > 0) ganhos.Add(("Adicional Noturno", valorAn));
            if (valorAc > 0) ganhos.Add(("Ajuda de Custo", valorAc));
            if (valorInsalubridade > 0) ganhos.Add(("Insalubridade", valorInsalubridade));
            if (valorCreche > 0) ganhos.Add(("Auxílio Creche", valorCreche));
            if (valorAbono > 0) ganhos.Add(("Abono", valorAbono));
            if (valorComissao > 0) ganhos.Add(("Comissão", valorComissao));
            if (valorFamilia > 0) ganhos.Add(("Salário Família", valorFamilia));
            if (valorDescanco > 0) ganhos.Add(("Descanço Remunerado", valorDescanco));
            if (valorHora50 > 0) ganhos.Add(("Hora Extra 50%", valorHora50));
            if (valorHora100 > 0) ganhos.Add(("Hora Extra 100%", valorHora100));
            if (valorPericulosidade > 0) ganhos.Add(("Periculosidade", valorPericulosidade));

            foreach (Control c in panelBeneficios.Controls)
            {
                if (c is TextBox txtN && txtN.Name.Contains("Nome"))
                {
                    string nome = txtN.Text.Trim();
                    int index = panelBeneficios.Controls.IndexOf(txtN);
                    if (index + 1 < panelBeneficios.Controls.Count &&
                        panelBeneficios.Controls[index + 1] is TextBox txtV &&
                        decimal.TryParse(txtV.Text, out decimal valor))
                        ganhos.Add((nome, valor));
                }
            }

            if (valorTransporte > 0) descontos.Add(("Vale Transporte", valorTransporte));
            if (valorSeguro > 0) descontos.Add(("Seguro de Vida", valorSeguro));
            if (valorSaude > 0) descontos.Add(("Plano de Saúde", valorSaude));
            if (valorPrevidencia > 0) descontos.Add(("Previdência Privada", valorPrevidencia));
            if (valorFaltas > 0) descontos.Add(("Faltas", valorFaltas));
            if (valorAlimentacao > 0) descontos.Add(("Vale Alimentação", valorAlimentacao));
            if (valorSindicato > 0) descontos.Add(("Sindicato", valorSindicato));
            if (valorAdiantamento > 0) descontos.Add(("Adiantamento", valorAdiantamento));
            if (valorInss > 0) descontos.Add(("INSS", valorInss));
            if (ValorIrrf > 0) descontos.Add(("IRRF", ValorIrrf));
            if (valorPensaobruto > 0) descontos.Add(("Pensão (sobre bruto)", valorPensaobruto));
            if (valorPensaoliquido > 0) descontos.Add(("Pensão (sobre líquido)", valorPensaoliquido));
            if (valorEmprestimo > 0) descontos.Add(("Empréstimo Consignado", valorEmprestimo));

            foreach (Control c in PanelDescontos.Controls)
            {
                if (c is TextBox txtN && txtN.Name.Contains("Nome"))
                {
                    string nome = txtN.Text.Trim();
                    int index = PanelDescontos.Controls.IndexOf(txtN);
                    if (index + 1 < PanelDescontos.Controls.Count &&
                        PanelDescontos.Controls[index + 1] is TextBox txtV &&
                        decimal.TryParse(txtV.Text, out decimal valor))
                        descontos.Add((nome, valor));
                }
            }

            var formResumo = new FormResumo(ganhos, descontos, salarioBruto, salarioLiquido, salarioBase, valorFgts,
                nomeFuncionario, matricula, cpf, cargo, pis, logradouroFuncionario, numeroFuncionario,
                bairroFuncionario, cidadeFuncionario, cep, razaoSocial, cnpj, logradouroEmpresa,
                numeroEmpresa, bairroEmpresa, cidadeEmpresa, cepEmpresa);
            formResumo.ShowDialog();
        }

        private void toolStripButton2_click(object sender, EventArgs e)
        {
            temaEscuroAtivo = !temaEscuroAtivo;
            acessibilidade.AplicarTema(this, temaEscuroAtivo);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void temaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void txtBairro_empresa_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void txtCidade_empresa_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskCep_empresa_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label68_Click(object sender, EventArgs e)
        {

        }

        private void tabDados_Click(object sender, EventArgs e)
        {

        }

        private void txtAjuda_de_custo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label63_Click(object sender, EventArgs e)
        {

        }

        private void chkPericulosidade_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private async void maskCep_empresa_Leave(object sender, EventArgs e)
        {
            if (!TemRede())
            {
                return;
            }
            if (!await TemInternet())
            {
                return;
            }
            string cep = maskCep_empresa.Text.Replace(".", "").Replace("-", "").Replace("_", "").Replace(" ", "").Trim();

            if (cep.Length == 8 && !string.IsNullOrWhiteSpace(cep))
            {
                await BuscarCEPEmpresa(cep);
            }
            else if (!string.IsNullOrWhiteSpace(maskCep_empresa.Text) && cep.Length != 8)
            {
                MessageBox.Show($"CEP inválido. Digite um CEP com 8 dígitos.\nCEP digitado: '{maskCep_empresa.Text}'\nCEP limpo: '{cep}' (tamanho: {cep.Length})", "CEP Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void maskCep_funcionario_Leave_1(object sender, EventArgs e)
        {
            if (!TemRede())
            {
                return;
            }
            if (!await TemInternet())
            {
                return;
            }

            string cep = maskCep_funcionario.Text.Replace(".", "").Replace("-", "").Replace("_", "").Replace(" ", "").Trim();

            if (cep.Length == 8 && !string.IsNullOrWhiteSpace(cep))
            {
                await BuscarCEPFuncionario(cep);
            }
            else if (!string.IsNullOrWhiteSpace(maskCep_funcionario.Text) && cep.Length != 8)
            {
                MessageBox.Show($"CEP inválido. Digite um CEP com 8 dígitos.\nCEP digitado: '{maskCep_funcionario.Text}'\nCEP limpo: '{cep}' (tamanho: {cep.Length})", "CEP Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void cmbInsalubridade_porcentagem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private Size tamanhoOriginalForm;

        public Form1()
        {
            InitializeComponent();
            tamanhoOriginalForm = this.ClientSize;

            cmbAno.Items.Add(2026); cmbAno.Items.Add(2025); cmbAno.Items.Add(2024); cmbAno.Items.Add(2023);
            cmbAno.Items.Add(2022); cmbAno.Items.Add(2021); cmbAno.Items.Add(2020);
            cmbAno.SelectedItem = 2026;

            // #12 - ComboBox Insalubridade com % explícito
            cmbInsalubridade_porcentagem.Items.Add("Não");
            cmbInsalubridade_porcentagem.Items.Add("10%");
            cmbInsalubridade_porcentagem.Items.Add("20%");
            cmbInsalubridade_porcentagem.Items.Add("40%");

            cmbPensao_tipo.Items.Add("Não");
            cmbPensao_tipo.Items.Add("Sobre provento liquido");
            cmbPensao_tipo.Items.Add("Sobre provento bruto");

            // Campos dependentes de checkbox iniciam desabilitados
            txtParticipacao_de_lucro_percentual.Enabled = false;
            txtParticipacao_de_lucros_valor_liquido.Enabled = false;
            txtAdicional_noturno.Enabled = false;
            txtAjuda_de_custo.Enabled = false;
            cmbInsalubridade_porcentagem.Enabled = false;
            txtAuxilio_creche.Enabled = false;
            cmbMesAbono.Enabled = false;
            txtComissao_valor_total.Enabled = false;
            txtComissao_percentual.Enabled = false;
            txtAuxilio_familia_numero_de_dependentes.Enabled = false;
            txtAuxilio_familia_valor_da_cota.Enabled = false;
            txtDescanco_remunerado_dias_uteis.Enabled = false;
            txtDescanco_remunerado_dias_descanco.Enabled = false;
            txtHora_extra_50.Enabled = false;
            txtHora_extra_100.Enabled = false;
            txtVale_transporte.Enabled = false;
            txtSeguro_de_vida.Enabled = false;
            txtPlano_de_saude.Enabled = false;
            txtPrevidencia_privada.Enabled = false;
            txtEmprestimo_consiganado.Enabled = false;
            txtFaltas.Enabled = false;
            cmbPensao_tipo.Enabled = false;
            txtVale_alimentacao.Enabled = false;
            txtSindicato.Enabled = false;
            txtAdiantamento.Enabled = false;
            txtPensao_porcentagem.Enabled = false;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // AccessibleDescriptions
            txtNome.AccessibleDescription = "Este campo é obrigatório, por favor insira o nome do funcionário";
            txtMatricula.AccessibleDescription = "Este campo é obrigatório, por favor insira a matricula do funcionário, para preencher este campo utilize apenas números.";
            maskCpf.AccessibleDescription = "Este campo é obrigatório, por favor insira o CPF do funcionário, para preencher este campo utilize apenas números.";
            maskPis.AccessibleDescription = "Este campo é obrigatório, por favor insira o número do PIS/PASEP do funcionário, para preencher este campo utilize apenas números.";
            txtCargo.AccessibleDescription = "Este campo é obrigatório, por favor insira o cargo do funcionário";
            txtLogradouro_Funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira o nome da rua do funcionário";
            txtNumero_Funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira o número da casa do funcionário, para preencher este campo utilize apenas números.";
            txtBairro_funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira o bairro do funcionário";
            txtCidade_funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira a cidade do funcionário";
            maskCep_funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira o CEP do funcionário, para preencher este campo utilize apenas números.";
            txtSalario_base.AccessibleDescription = "Este campo é obrigatório, por favor insira o valor do salário base do funcionário, para preencher este campo utilize apenas números.";
            txtRazao_Social.AccessibleDescription = "Este campo é obrigatório, por favor insira a Razão Social da empresa.";
            maskCnpj.AccessibleDescription = "Este campo é obrigatório, por favor insira o número do CNPJ da empresa, para preencher este campo utilize apenas números.";
            txtLogradouro_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira o logradouro da empresa";
            txtnumero_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira o número de endereço da empresa, para preencher este campo utilize apenas números.";
            txtBairro_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira o bairro da empresa.";
            txtCidade_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira a cidade da empresa.";
            maskCep_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira o CEP da empresa, para preencher este campo utilize apenas números.";

            #region Limitar caracteres todos os campos de proventos e descontos.
            //DADOS COLABORADOR//
            txtNome.MaxLength = 150;
            txtMatricula.MaxLength = 20;
            txtCargo.MaxLength = 50;
            txtLogradouro_Funcionario.MaxLength = 150;
            txtNumero_Funcionario.MaxLength = 10;
            txtBairro_funcionario.MaxLength = 60;
            txtCidade_funcionario.MaxLength = 60;
            txtSalario_base.MaxLength = 7;

            //DADOS DA EMPRESA//
            txtRazao_Social.MaxLength = 150;
            txtLogradouro_empresa.MaxLength = 150;
            txtnumero_empresa.MaxLength = 10;
            txtBairro_empresa.MaxLength = 60;
            txtCidade_empresa.MaxLength = 60;

            //PROVENTOS//
            txtAdicional_noturno.MaxLength = 7;
            txtAjuda_de_custo.MaxLength = 7;
            txtAuxilio_creche.MaxLength = 7;
            txtParticipacao_de_lucros_valor_liquido.MaxLength = 7;
            txtParticipacao_de_lucro_percentual.MaxLength = 5;
            txtComissao_valor_total.MaxLength = 7;
            txtComissao_percentual.MaxLength = 5;
            txtAuxilio_familia_numero_de_dependentes.MaxLength = 10;
            txtAuxilio_familia_valor_da_cota.MaxLength = 10;
            txtDescanco_remunerado_dias_uteis.MaxLength = 2;
            txtDescanco_remunerado_dias_descanco.MaxLength = 2;
            txtHora_extra_50.MaxLength = 3;
            txtHora_extra_100.MaxLength = 3;

            //DESCONTOS//
            txtVale_transporte.MaxLength = 5;
            txtSeguro_de_vida.MaxLength = 7;
            txtPlano_de_saude.MaxLength = 7;
            txtPrevidencia_privada.MaxLength = 7;
            txtEmprestimo_consiganado.MaxLength = 5;
            txtFaltas.MaxLength = 3;
            txtPensao_porcentagem.MaxLength = 5;
            txtVale_alimentacao.MaxLength = 7;
            txtSindicato.MaxLength = 7;
            txtAdiantamento.MaxLength = 5;

            #endregion

            if (!TemRede())
            {
                MessageBox.Show("Sem acesso à rede, algumas funcionalidades estarão desabilitadas.");
                return;
            }
            if (!await TemInternet())
            {
                MessageBox.Show("Sem acesso à internet, algumas funcionalidades estarão desabilitadas.");
                return;
            }

            txtLogradouro_Funcionario.Enabled = false;
            txtBairro_funcionario.Enabled = false;
            txtCidade_funcionario.Enabled = false;
            txtLogradouro_empresa.Enabled = false;
            txtBairro_empresa.Enabled = false;
            txtCidade_empresa.Enabled = false;

            
            cmbMesAbono.Items.Clear();

           
            string[] meses = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
                       "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

           
            cmbMesAbono.Items.AddRange(meses);

           
            cmbMesAbono.DropDownStyle = ComboBoxStyle.DropDownList;

           
            cmbMesAbono.SelectedIndex = 0;
        }




        // ===== NAVEGAÇÃO ENTRE ABAS =====
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // ===== #16 - LIMPAR COM CONFIRMAÇÃO =====
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            DialogResult confirmacao = MessageBox.Show(
                "Você tem certeza que deseja excluir todos os campos?",
                "Confirmar Limpeza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirmacao == DialogResult.Yes)
                LimparTodosOsCampos();
        }

        private void LimparTodosOsCampos()
        {
            LimparControles(this);

            if (cmbAno.Items.Contains(2025)) cmbAno.SelectedItem = 2025;
            if (cmbInsalubridade_porcentagem.Items.Count > 0) cmbInsalubridade_porcentagem.SelectedIndex = 0;
            if (cmbPensao_tipo.Items.Count > 0) cmbPensao_tipo.SelectedIndex = 0;

            txtParticipacao_de_lucro_percentual.Enabled = false;
            txtParticipacao_de_lucros_valor_liquido.Enabled = false;
            txtAdicional_noturno.Enabled = false;
            txtAjuda_de_custo.Enabled = false;
            cmbInsalubridade_porcentagem.Enabled = false;
            txtAuxilio_creche.Enabled = false;
            cmbMesAbono.Enabled = false;
            txtComissao_valor_total.Enabled = false;
            txtComissao_percentual.Enabled = false;
            txtAuxilio_familia_numero_de_dependentes.Enabled = false;
            txtAuxilio_familia_valor_da_cota.Enabled = false;
            txtDescanco_remunerado_dias_uteis.Enabled = false;
            txtDescanco_remunerado_dias_descanco.Enabled = false;
            txtHora_extra_50.Enabled = false;
            txtHora_extra_100.Enabled = false;
            txtVale_transporte.Enabled = false;
            txtSeguro_de_vida.Enabled = false;
            txtPlano_de_saude.Enabled = false;
            txtPrevidencia_privada.Enabled = false;
            txtEmprestimo_consiganado.Enabled = false;
            txtFaltas.Enabled = false;
            cmbPensao_tipo.Enabled = false;
            txtVale_alimentacao.Enabled = false;
            txtSindicato.Enabled = false;
            txtAdiantamento.Enabled = false;
            txtPensao_porcentagem.Enabled = false;

            panelBeneficios.Controls.Clear();
            PanelDescontos.Controls.Clear();
            yOffset = 0;
            yOffset2 = 0;
        }

        private void LimparControles(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txt)
                    txt.Clear();
                else if (ctrl is CheckBox chk)
                    chk.Checked = false;
                else if (ctrl is MaskedTextBox mask)
                    mask.Clear();

                if (ctrl.HasChildren)
                    LimparControles(ctrl);
            }
        }

        // ===== MENU VALIDAÇÃO =====
        private void desativarModoDeValidaçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
        }

        // ===== CALCULAR =====
        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        // ===== PAINÉIS BENEFÍCIOS E DESCONTOS =====
        private void btnAdicionarBeneficio_Click(object sender, EventArgs e)
        {
            TextBox txtNome = new TextBox { Name = "txtAdicionalNome" + panelBeneficios.Controls.Count, Width = 100, Location = new Point(10, yOffset) };
            TextBox txtValor = new TextBox { Name = "txtAdicionalValor" + panelBeneficios.Controls.Count, Width = 80, Location = new Point(120, yOffset) };
            txtValor.KeyPress += validar.ValidacaoNumeros;
            panelBeneficios.Controls.Add(txtNome);
            panelBeneficios.Controls.Add(txtValor);
            yOffset += 30;
        }

        private void btnRemoverBeneficio_Click(object sender, EventArgs e)
        {
            if (panelBeneficios.Controls.Count >= 2)
            {
                panelBeneficios.Controls.RemoveAt(panelBeneficios.Controls.Count - 1);
                panelBeneficios.Controls.RemoveAt(panelBeneficios.Controls.Count - 1);
                yOffset -= 30;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TextBox txtNome = new TextBox { Name = "txtDescontolNome" + PanelDescontos.Controls.Count, Width = 100, Location = new Point(10, yOffset2) };
            TextBox txtValor = new TextBox { Name = "txtDescontoValor" + PanelDescontos.Controls.Count, Width = 80, Location = new Point(120, yOffset2) };
            txtValor.KeyPress += validar.ValidacaoNumeros;
            PanelDescontos.Controls.Add(txtNome);
            PanelDescontos.Controls.Add(txtValor);
            yOffset2 += 30;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PanelDescontos.Controls.Count >= 2)
            {
                PanelDescontos.Controls.RemoveAt(PanelDescontos.Controls.Count - 1);
                PanelDescontos.Controls.RemoveAt(PanelDescontos.Controls.Count - 1);
                yOffset2 -= 30;
            }
        }

        // ===== #18 - ALTO CONTRASTE (laranja) =====
        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        // ===== #15 - CHECKBOXES PROVENTOS =====
        private void chkParticipacaoLucros_CheckedChanged(object sender, EventArgs e)
        {
            txtParticipacao_de_lucro_percentual.Enabled = chkParticipacaoLucros.Checked;
            txtParticipacao_de_lucros_valor_liquido.Enabled = chkParticipacaoLucros.Checked;
            if (!chkParticipacaoLucros.Checked) { txtParticipacao_de_lucro_percentual.Clear(); txtParticipacao_de_lucros_valor_liquido.Clear(); }
        }
        private void chkAdicionalNoturno_CheckedChanged(object sender, EventArgs e)
        {
            txtAdicional_noturno.Enabled = chkAdicionalNoturno.Checked;
            if (!chkAdicionalNoturno.Checked) txtAdicional_noturno.Clear();
        }
        private void chkAjudaCusto_CheckedChanged(object sender, EventArgs e)
        {
            txtAjuda_de_custo.Enabled = chkAjudaCusto.Checked;
            if (!chkAjudaCusto.Checked) txtAjuda_de_custo.Clear();
        }
        private void chkInsalubridade_CheckedChanged(object sender, EventArgs e)
        {
            cmbInsalubridade_porcentagem.Enabled = chkInsalubridade.Checked;
            if (!chkInsalubridade.Checked) cmbInsalubridade_porcentagem.SelectedIndex = 0;
        }
        private void chkAuxilioCreche_CheckedChanged(object sender, EventArgs e)
        {
            txtAuxilio_creche.Enabled = chkAuxilioCreche.Checked;
            if (!chkAuxilioCreche.Checked) txtAuxilio_creche.Clear();
        }
        private void chkAbono_CheckedChanged(object sender, EventArgs e)
        {
            cmbMesAbono.Enabled = chkAbono.Checked;

            // Para limpar um ComboBox, mudamos o índice dele para -1 (nenhum item selecionado)
            if (!chkAbono.Checked)
            {
                cmbMesAbono.SelectedIndex = -1;
            }
        }
        private void chkComissao_CheckedChanged(object sender, EventArgs e)
        {
            txtComissao_valor_total.Enabled = chkComissao.Checked;
            txtComissao_percentual.Enabled = chkComissao.Checked;
            if (!chkComissao.Checked) { txtComissao_valor_total.Clear(); txtComissao_percentual.Clear(); }
        }
        private void chkAuxilioFamilia_CheckedChanged(object sender, EventArgs e)
        {
            txtAuxilio_familia_numero_de_dependentes.Enabled = chkAuxilioFamilia.Checked;
            txtAuxilio_familia_valor_da_cota.Enabled = chkAuxilioFamilia.Checked;
            if (!chkAuxilioFamilia.Checked) { txtAuxilio_familia_numero_de_dependentes.Clear(); txtAuxilio_familia_valor_da_cota.Clear(); }
        }
        private void chkDescancoRemunerado_CheckedChanged(object sender, EventArgs e)
        {
            txtDescanco_remunerado_dias_uteis.Enabled = chkDescancoRemunerado.Checked;
            txtDescanco_remunerado_dias_descanco.Enabled = chkDescancoRemunerado.Checked;
            if (!chkDescancoRemunerado.Checked) { txtDescanco_remunerado_dias_uteis.Clear(); txtDescanco_remunerado_dias_descanco.Clear(); }
        }
        private void chk50He_CheckedChanged(object sender, EventArgs e)
        {
            txtHora_extra_50.Enabled = chk50He.Checked;
            if (!chk50He.Checked) txtHora_extra_50.Clear();
        }
        private void chk100He_CheckedChanged(object sender, EventArgs e)
        {
            txtHora_extra_100.Enabled = chk100He.Checked;
            if (!chk100He.Checked) txtHora_extra_100.Clear();
        }

        // ===== #15 - CHECKBOXES DESCONTOS =====
        private void chkValeTransporte_CheckedChanged(object sender, EventArgs e)
        {
            txtVale_transporte.Enabled = chkValeTransporte.Checked;
            if (!chkValeTransporte.Checked) txtVale_transporte.Clear();
        }
        private void chkSeguroVida_CheckedChanged(object sender, EventArgs e)
        {
            txtSeguro_de_vida.Enabled = chkSeguroVida.Checked;
            if (!chkSeguroVida.Checked) txtSeguro_de_vida.Clear();
        }
        private void chkPlanoSaude_CheckedChanged(object sender, EventArgs e)
        {
            txtPlano_de_saude.Enabled = chkPlanoSaude.Checked;
            if (!chkPlanoSaude.Checked) txtPlano_de_saude.Clear();
        }
        private void chkPrevidencia_CheckedChanged(object sender, EventArgs e)
        {
            txtPrevidencia_privada.Enabled = chkPrevidencia.Checked;
            if (!chkPrevidencia.Checked) txtPrevidencia_privada.Clear();
        }
        private void chkEmprestimo_CheckedChanged(object sender, EventArgs e)
        {
            txtEmprestimo_consiganado.Enabled = chkEmprestimo.Checked;
            if (!chkEmprestimo.Checked) txtEmprestimo_consiganado.Clear();
        }
        private void chkFaltas_CheckedChanged(object sender, EventArgs e)
        {
            txtFaltas.Enabled = chkFaltas.Checked;
            if (!chkFaltas.Checked) txtFaltas.Clear();
        }
        private void chkPensao_CheckedChanged(object sender, EventArgs e)
        {
            txtPensao_porcentagem.Enabled = chkPensao.Checked;
            cmbPensao_tipo.Enabled = chkPensao.Checked;
            if (!chkPensao.Checked) { txtPensao_porcentagem.Clear(); cmbPensao_tipo.SelectedIndex = 0; }
        }
        private void chkValeAlimentacao_CheckedChanged(object sender, EventArgs e)
        {
            txtVale_alimentacao.Enabled = chkValeAlimentacao.Checked;
            if (!chkValeAlimentacao.Checked) txtVale_alimentacao.Clear();
        }
        private void chkSindicato_CheckedChanged(object sender, EventArgs e)
        {
            txtSindicato.Enabled = chkSindicato.Checked;
            if (!chkSindicato.Checked) txtSindicato.Clear();
        }
        private void chkAdiantamento_CheckedChanged(object sender, EventArgs e)
        {
            txtAdiantamento.Enabled = chkAdiantamento.Checked;
            if (!chkAdiantamento.Checked) txtAdiantamento.Clear();
        }

        private async Task BuscarCEPFuncionario(string cep)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(
                        $"https://viacep.com.br/ws/{cep}/json/"
                    );

                    Cep dados = JsonConvert.DeserializeObject<Cep>(response);

                    if (dados.erro)
                    {
                        MessageBox.Show("CEP não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    txtLogradouro_Funcionario.Text = dados.logradouro;
                    txtBairro_funcionario.Text = dados.bairro;
                    txtCidade_funcionario.Text = dados.localidade;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar CEP: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task BuscarCEPEmpresa(string cep)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(
                        $"https://viacep.com.br/ws/{cep}/json/"
                    );

                    Cep dados = JsonConvert.DeserializeObject<Cep>(response);

                    if (dados.erro)
                    {
                        MessageBox.Show("CEP não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    txtLogradouro_empresa.Text = dados.logradouro;
                    txtBairro_empresa.Text = dados.bairro;
                    txtCidade_empresa.Text = dados.localidade;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar CEP: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TemRede()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        private async Task<bool> TemInternet()
        {
            try
            {
                using (HttpClient cliente = new HttpClient())
                {
                    cliente.Timeout = TimeSpan.FromSeconds(3);

                    var reposta = await cliente.GetAsync("https://viacep.com.br/ws/01001000/json/");

                    return reposta.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }



    }
}