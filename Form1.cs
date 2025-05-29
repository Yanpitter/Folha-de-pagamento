using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

using FolhaDePagamento;

namespace FolhaDePagamento
{
    public partial class Form1: Form
    {
        private bool temaEscuroAtivo = false; //Variavel de tema
        public int yOffset = 0; //Variavel para Panel Beneficios
        public int yOffset2 = 0; //Variavel para Panel Descontos
        FormulasFolha formulas = new FormulasFolha(); // Objeto para formulas
        CalculadoraImpostos impostos = new CalculadoraImpostos("caminho/para/tabelas.txt"); //Objeto para tabelas
        Validacoes validar = new Validacoes(); // Objeto para validações
        Acessibilidade acessibilidade = new Acessibilidade(); //Objeto para Acessibilidade
        Parametros parametros = new Parametros();
        BaseDeParametros parametrosBase = new BaseDeParametros();

        public bool validarStatus = true;

        public string nomeFuncionario, cargo, cepEmpresa , logradouroFuncionario, bairroFuncionario, cidadeFuncionario, razaoSocial, logradouroEmpresa, numeroEmpresa, bairroEmpresa, cidadeEmpresa, tipoPensao, matricula, pis, numeroFuncionario, cep, cnpj, cpf;

        public int familiaDependentes, descancoRemunerado, diasRemunerado, ano;

        public decimal salarioBase, participacaoLucroPorcentagem, participacaoLucroLiquido, adicionalNoturno, ajudaCusto, comissaoValorTotal, adiantamento, familiaCota, hora50, hora100, beneficio, seguroVida, planoSaude, previdencia, faltas, emprestimo, valeTransporte, comissaoPorcentagem, abono, auxilioCreche, insalubridade, pensao, valeAlimentacao, sindicato, desconto, valorPlucro, valorAn, valorAc, valorInsalubridade, valorCreche, valorAbono, valorComissao, valorFamilia, valorDescanco, valorHora50, valorHora100, valorTransporte, valorSeguro, valorSaude, valorPrevidencia, valorEmprestimo, valorFaltas, valorPensaobruto, valorAlimentacao, valorSindicato, valorPericulosidade, valorPensaoliquido, valorAdiantamento, valorFgts, valorInss, ValorIrrf, salarioLiquido, salarioBruto;

        private void Form1_Load(object sender, EventArgs e)
        {
            txtNome.AccessibleDescription = "Este campo é obrigatório, por favor insira o nome do funcionário";
            txtMatricula.AccessibleDescription = "Este campo é obrigatório, por favor insira a matricula do funcionário, para preencher este campo utilize apenas números.";
            maskCpf.AccessibleDescription = "Este campo é obrigatório, por favor insira o CPF do funcionário,para preencher este campo utilize apenas números.";
            maskPis.AccessibleDescription = "Este campo é obrigatório, por favor insira o número do PIS/PASEP do funcionário, para preencher este campo utilize apenas números.;";
            txtCargo.AccessibleDescription = "Este campo é obrigatório, por favor insira o cargo do funcionário";
            txtLogradouro_Funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira o nome da rua do funcionário";
            txtNumero_Funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira o número da casa do funcionário, para preencher este campo utilize apenas números.";
            txtBairro_funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira o bairro do funcionário";
            txtCidade_funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira a cidade do funcionário";
            maskCep_funcionario.AccessibleDescription = "Este campo é obrigatório, por favor insira o CEP do funcionário, para preencher este campo utilize apenas números.";
            txtSalario_base.AccessibleDescription = "Este campo é obrigatório, por favor insira o valor do salário base do funcionário, para preencher este campo utilize apenas números.";

            txtRazao_Social.AccessibleDescription = "Este campo é obrigatório, por favor insira a Razão Social da empresa.";
            maskCnpj.AccessibleDescription = "Este campo é obrigatório, por favor insira o número do CNPJ da empresa para preencher este campo utilize apenas números.";
            txtLogradouro_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira o logradouro da empresa";
            txtnumero_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira o número de endereço da empresa, para preencher este campo utilize apenas números.";
            txtBairro_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira o bairro da empresa.";
            txtCidade_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira a cidade da empresa.";
            maskCep_empresa.AccessibleDescription = "Este campo é obrigatório, por favor insira o CEP da empresa, para preencher este campo utilize apenas números.";
            button1.Enabled = false;
        }

        private void desativarModoDeValidaçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (validarStatus == false)
            {
                validarStatus = true;
                desativarModoDeValidaçãoToolStripMenuItem1.Text = "Desativar modo de validação";
                txtNome.Enabled = true;
                txtMatricula.Enabled = true;
                maskCpf.Enabled = true;
                txtCargo.Enabled = true;
                txtLogradouro_Funcionario.Enabled = true;
                txtCidade_funcionario.Enabled = true;
                txtBairro_funcionario.Enabled = true;
                maskPis.Enabled = true;
                txtNumero_Funcionario.Enabled = true;
                txtRazao_Social.Enabled = true;
                txtBairro_empresa.Enabled = true;
                txtCidade_empresa.Enabled = true;
                txtLogradouro_empresa.Enabled = true;
                maskCnpj.Enabled = true;
                maskCep_empresa.Enabled = true;
                maskCep_funcionario.Enabled = true;
                txtnumero_empresa.Enabled = true;
                return;
            }
            validarStatus = false;
            desativarModoDeValidaçãoToolStripMenuItem1.Text = "Ativar modo de validação";
            txtNome.Enabled = false;
            txtMatricula.Enabled = false;
            maskCpf.Enabled = false;
            txtCargo.Enabled = false;
            txtLogradouro_Funcionario.Enabled = false;
            txtCidade_funcionario.Enabled = false;
            txtBairro_funcionario.Enabled = false;
            maskPis.Enabled = false;
            txtNumero_Funcionario.Enabled = false;
            txtRazao_Social.Enabled = false;
            txtBairro_empresa.Enabled = false;
            txtCidade_empresa.Enabled = false;
            txtLogradouro_empresa.Enabled = false;
            maskCnpj.Enabled = false;
            maskCep_empresa.Enabled = false;
            maskCep_funcionario.Enabled = false;
            txtnumero_empresa.Enabled= false;


        }

        private List<ControlLayout> originalLayout = new List<ControlLayout>();
        private float fatorZoomAtual = 1.0f;
        private Size tamanhoOriginalForm;


        
        public Form1()
        {
            InitializeComponent();
            ArmazenarLayout(this);
            tamanhoOriginalForm = this.ClientSize;
            cmbAno.Items.Add(2025);
            cmbAno.Items.Add(2024);
            cmbAno.Items.Add(2023);
            cmbAno.Items.Add(2022);
            cmbAno.Items.Add(2021);
            cmbAno.Items.Add(2020);
            cmbAno.SelectedItem = 2025;
            cmbInsalubridade_porcentagem.Items.Add("Não");
            cmbInsalubridade_porcentagem.Items.Add(10);
            cmbInsalubridade_porcentagem.Items.Add(20);
            cmbInsalubridade_porcentagem.Items.Add(40);
            cmbPensao_tipo.Items.Add("Não");
            cmbPensao_tipo.Items.Add("Sobre provento liquido");
            cmbPensao_tipo.Items.Add("Sobre provento bruto");
            txtParticipacao_de_lucro_percentual.Enabled = false;
            txtAdicional_noturno.Enabled = false;
            txtAjuda_de_custo.Enabled = false;
            cmbInsalubridade_porcentagem.Enabled = false;
            txtAuxilio_creche.Enabled = false;
            txtAbono.Enabled = false;
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
        }


        private void button5_Click(object sender, EventArgs e)
        {
            familiaDependentes = 0;
            descancoRemunerado = 0;
            diasRemunerado = 0;
            ano = 0;
            salarioBase = 0;
            participacaoLucroPorcentagem = 0;
            participacaoLucroLiquido = 0;
            adicionalNoturno = 0;
            ajudaCusto = 0;
            comissaoValorTotal = 0;
            adiantamento = 0;
            familiaCota = 0;
            hora50 = 0;
            hora100 = 0;
            beneficio = 0;
            seguroVida = 0;
            planoSaude = 0;
            previdencia = 0;
            faltas = 0;
            emprestimo = 0;
            valeTransporte = 0;
            comissaoPorcentagem = 0;
            abono = 0;
            auxilioCreche = 0;
            insalubridade = 0;
            pensao = 0;
            valeAlimentacao = 0;
            sindicato = 0;
            desconto = 0;
            valorPlucro = 0;
            valorAn = 0;
            valorAc = 0;
            valorInsalubridade = 0;
            valorCreche = 0;
            valorAbono = 0;
            valorComissao = 0;
            valorFamilia = 0;
            valorDescanco = 0;
            valorHora50 = 0;
            valorHora100 = 0;
            valorTransporte = 0;
            valorSeguro = 0;
            valorSaude = 0;
            valorPrevidencia = 0;
            valorEmprestimo = 0;
            valorFaltas = 0;
            valorPensaobruto = 0;
            valorAlimentacao = 0;
            valorSindicato = 0;
            valorPericulosidade = 0;
            valorPensaoliquido = 0;
            valorAdiantamento = 0;
            salarioLiquido = 0;
            salarioBruto = 0;

            if(validarStatus == true && (validar.ValidarDados(txtNome) || validar.ValidarDados(txtLogradouro_Funcionario) || validar.ValidarDados(txtBairro_funcionario) || validar.ValidarDados(txtCidade_funcionario) || validar.ValidarDados(txtCargo) || validar.ValidarDados(txtBairro_empresa) || validar.ValidarDados(txtCidade_empresa) || validar.ValidarDados(txtRazao_Social) || validar.ValidarDados(txtLogradouro_empresa) || validar.ValidarDados(txtnumero_empresa) || validar.ValidarDados(txtNumero_Funcionario))) return;
            if (validar.ValidarDados(txtSalario_base)) return;

            if (validar.ValidarDadosDecimal(rdbSimPdl, txtParticipacao_de_lucro_percentual) || validar.ValidarDadosDecimal(rdbSimAn,txtAdicional_noturno) || validar.ValidarDadosDecimal(rdbSimAdc, txtAjuda_de_custo) || validar.ValidarDadosDecimal(rdbSimComissao, txtComissao_valor_total) || validar.ValidarDadosDecimal(rdbSimComissao, txtComissao_percentual) || validar.ValidarDadosDecimal(rdbSimCreche, txtAuxilio_creche) || validar.ValidarDadosDecimal(rdbSimFamilia, txtAuxilio_familia_valor_da_cota) || validar.ValidarDadosDecimal(rdbSimAdc, txtAjuda_de_custo) || validar.ValidarDadosDecimal(rdbSimAbono, txtAbono) || validar.ValidarDadosDecimal(rdbSimVt, txtVale_transporte) || validar.ValidarDadosDecimal(rdbSimSv, txtSeguro_de_vida) || validar.ValidarDadosDecimal(rdbSimPs, txtPlano_de_saude) || validar.ValidarDadosDecimal(rdbSimPp, txtPrevidencia_privada) || validar.ValidarDadosDecimal(rdbSimEc, txtEmprestimo_consiganado) || validar.ValidarDadosDecimal(rdbSimFaltas, txtFaltas) || validar.ValidarDadosDecimal(rdbSimVa, txtVale_alimentacao) || validar.ValidarDadosDecimal(rdbSimSindicato, txtSindicato) || validar.ValidarDadosDecimal(chk50He, txtHora_extra_50) || validar.ValidarDadosDecimal(chk100He, txtHora_extra_100) || validar.ValidarDadosDecimal(rdbSimPa, txtPensao_porcentagem, cmbPensao_tipo) || validar.ValidarDadosDecimal(rdbSimPdl, txtParticipacao_de_lucros_valor_liquido)) return;

            if (validar.ValidarDadosInt(rdbSimDr, txtDescanco_remunerado_dias_uteis) || validar.ValidarDadosInt(rdbSimFamilia, txtAuxilio_familia_numero_de_dependentes) || validar.ValidarDadosInt(rdbSimDr, txtDescanco_remunerado_dias_descanco) || validar.ValidarDadosInt(rdbSimI, cmbInsalubridade_porcentagem)) return;

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
            bairroEmpresa  = txtBairro_empresa.Text;
            cidadeEmpresa = txtCidade_empresa.Text;
            cepEmpresa = maskCep_empresa.Text;

            
            decimal.TryParse(txtSalario_base.Text, out salarioBase);
            decimal.TryParse(txtParticipacao_de_lucro_percentual.Text, out participacaoLucroPorcentagem);
            decimal.TryParse(txtParticipacao_de_lucros_valor_liquido.Text, out participacaoLucroLiquido); 
            decimal.TryParse(cmbInsalubridade_porcentagem.Text, out insalubridade);  
            decimal.TryParse(txtAjuda_de_custo.Text, out auxilioCreche);  
            decimal.TryParse(txtAbono.Text, out abono);  
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
            decimal.TryParse(txtAjuda_de_custo.Text, out ajudaCusto);

            int.TryParse(txtAuxilio_familia_numero_de_dependentes.Text, out familiaDependentes);
            int.TryParse(cmbAno.Text, out ano);
            int.TryParse(txtDescanco_remunerado_dias_uteis.Text, out diasRemunerado);
            int.TryParse(txtDescanco_remunerado_dias_descanco.Text, out descancoRemunerado);

            if (adiantamento > 40)
            {
                MessageBox.Show("Valor do adiantamento não pode ser superior a 40%", "Dado inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdiantamento.Focus();
                return;
            }
            if (valeTransporte > 6)
            {
                MessageBox.Show("Valor do vale transporte não pode ser superior a 6%", "Dado inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVale_transporte.Focus();
                return;
            }
            if (emprestimo > 30)
            {
                MessageBox.Show("Valor do emprestimo não pode ser superior a 30%", "Dado inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmprestimo_consiganado.Focus();
                return;
            }


            valorPlucro = participacaoLucroLiquido != 0 && rdbSimPdl.Checked? formulas.calcularParticipacaoLucros(participacaoLucroLiquido, participacaoLucroPorcentagem) : 0;
            valorAn = adicionalNoturno != 0 && rdbSimAn.Checked ?  formulas.calcularAdicionalNoturno(salarioBase, adicionalNoturno) : 0;
            valorAc =  rdbSimAdc.Checked ? ajudaCusto : 0;
            valorInsalubridade = insalubridade != 0 && rdbSimI.Checked ? formulas.calcularInsalubridade(parametrosBase.SalarioMinimoValor(ano.ToString()), insalubridade) : 0;
            valorCreche =  rdbSimCreche.Checked ? auxilioCreche : 0;
            valorAbono = abono != 0 && rdbSimAbono.Checked ? formulas.calculoAbonosalarial(parametrosBase.SalarioMinimoValor(ano.ToString()), abono) : 0;
            valorComissao = comissaoPorcentagem != 0 && rdbSimComissao.Checked ? formulas.calcularComissao(comissaoValorTotal, comissaoPorcentagem) : 0;
            valorFamilia = familiaCota != 0 && rdbSimFamilia.Checked ? formulas.calcularSalarioFamilia(familiaDependentes, familiaCota) : 0;
            valorDescanco = descancoRemunerado != 0 && rdbSimDr.Checked ? formulas.calcularRepousoRemunerado(salarioBase, diasRemunerado, descancoRemunerado) : 0;
            valorHora50 = hora50 != 0 && chk50He.Checked ? formulas.calcularHoraExtra50(salarioBase, hora50) : 0;
            valorHora100 = hora100 != 0 && chk100He.Checked ? formulas.calcularHoraExtra50(salarioBase, hora100) : 0;
            valorPericulosidade = rdbSimPericulosidade.Checked ? formulas.calcularPericulosidade(salarioBase) : 0;
            

            valorTransporte = valeTransporte != 0 && rdbSimVt.Checked ? formulas.CalcularTransporte(salarioBase, valeTransporte) : 0;
            valorSeguro =  rdbSimSv.Checked ? seguroVida : 0;
            valorSaude =  rdbSimPs.Checked ? planoSaude : 0;
            valorPrevidencia =  rdbSimPp.Checked ? previdencia : 0;
            valorFaltas = faltas != 0 && rdbSimFaltas.Checked  ? formulas.CalcularFaltas(salarioBase, faltas) : 0;
            valorAlimentacao =  rdbSimVa.Checked ? valeAlimentacao : 0;
            valorSindicato =  rdbSimSindicato.Checked ? sindicato : 0;
            valorAdiantamento = adiantamento != 0 && rdbSimAdiantamento.Checked ? formulas.CalcularAdiantamento(salarioBase, adiantamento) : 0;
            valorFgts = parametrosBase.CalcularFgts(salarioBase, ano.ToString());
            valorInss = parametrosBase.CalcularINSS(salarioBase, ano.ToString());
            ValorIrrf = parametrosBase.CalcularIRRF(salarioBase, ano.ToString());

            salarioBruto = salarioBase + valorPlucro + valorAn + valorAc + valorInsalubridade + valorCreche + valorAbono + valorComissao + valorFamilia + valorDescanco + valorHora50 + valorHora100 + valorPericulosidade + formulas.SomarAdicionais(panelBeneficios);
            valorPensaobruto = tipoPensao == "Sobre provento bruto" && rdbSimPa.Checked  ? formulas.CalcularPensaoBruto(salarioBruto, pensao) : 0;
            if (tipoPensao == "Sobre provento bruto")
            {
                salarioBruto -= valorPensaobruto;
            }
            salarioLiquido = salarioBruto - valorTransporte - valorSeguro - valorSaude - valorPrevidencia - valorFaltas - valorAlimentacao - valorSindicato - valorAdiantamento - valorInss - ValorIrrf - formulas.SomarAdicionais(PanelDescontos);
            valorPensaoliquido = tipoPensao == "Sobre provento liquido" && rdbSimPa.Checked ? formulas.CalcularPensaoliquido(salarioLiquido, pensao) : 0;
            if (tipoPensao == "Sobre provento liquido")
            {
                salarioLiquido -= valorPensaoliquido;
            }
            valorEmprestimo = emprestimo != 0 && rdbSimEc.Checked ? formulas.CalcularEmprestimoConsignado(salarioLiquido, emprestimo) : 0;
            if (emprestimo != 0)
            {
                salarioLiquido -= valorEmprestimo;
            }

            var ganhos = new List<(string, decimal)>{};
            var descontos = new List<(string, decimal)> { };
            if (valorPlucro > 0)
                ganhos.Add(("Participação de lucros", valorPlucro));
            if (valorAn > 0)
                ganhos.Add(("Adicional Noturno", valorAn));
            if (valorAc > 0)
                ganhos.Add(("Ajuda de Custo", valorAc));
            if (valorInsalubridade > 0)
                ganhos.Add(("Insalubridade", valorInsalubridade));
            if (valorCreche > 0)
                ganhos.Add(("Auxilio Creche", valorCreche));
            if (valorAbono > 0)
                ganhos.Add(("Abono", valorAbono));
            if (valorComissao > 0)
                ganhos.Add(("Comissão", valorComissao));
            if (valorFamilia > 0)
                ganhos.Add(("Salário Familia", valorFamilia));
            if (valorDescanco > 0)
                ganhos.Add(("Descanço remunerado", valorDescanco));
            if (valorHora50 > 0)
                ganhos.Add(("Hora extra 50%", valorHora50));
            if (valorHora100 > 0)
                ganhos.Add(("Hora extra 100%", valorHora100));
            if (valorPericulosidade > 0)
                ganhos.Add(("Periculosidade", valorPericulosidade));
            foreach (Control c in panelBeneficios.Controls)
            {
                if (c is TextBox txtNome && txtNome.Name.Contains("Nome"))
                {
                    string nome = txtNome.Text.Trim();

                    // Pegar o TextBox irmão (Valor) baseado na posição
                    int index = panelBeneficios.Controls.IndexOf(txtNome);
                    if (index + 1 < panelBeneficios.Controls.Count &&
                        panelBeneficios.Controls[index + 1] is TextBox txtValor &&
                        decimal.TryParse(txtValor.Text, out decimal valor))
                    {
                        ganhos.Add((nome, valor));
                    }
                }
            }
            if (valorTransporte > 0)
                descontos.Add(("Vale Tranporte", valorTransporte));
            if (valorSeguro > 0)
                descontos.Add(("Seguro de vida", valorSeguro));
            if (valorSaude > 0)
                descontos.Add(("Plano de saúde", valorSaude));
            if (valorPrevidencia > 0)
                descontos.Add(("Previdência privada", valorPrevidencia));
            if (valorFaltas > 0)
                descontos.Add(("Faltas", valorFaltas));
            if (valorAlimentacao > 0)
                descontos.Add(("Vale alimentação", valorAlimentacao));
            if (valorSindicato > 0)
                descontos.Add(("Sindicato", valorSindicato));
            if (valorAdiantamento > 0)
                descontos.Add(("Adiantamento", valorAdiantamento));
            if (valorInss > 0)
                descontos.Add(("Inss", valorInss));
            if (ValorIrrf > 0)
                descontos.Add(("Irrf", ValorIrrf));
            if (valorPensaobruto > 0)
                descontos.Add(("Pensão(sobre bruto)", valorPensaobruto));
            if (valorPensaoliquido > 0)
                descontos.Add(("Pensão(sobre liquido)", valorPensaoliquido));
            if (valorEmprestimo > 0)
                descontos.Add(("Emprestimo", valorEmprestimo));
            foreach (Control c in PanelDescontos.Controls)
            {
                if (c is TextBox txtNome && txtNome.Name.Contains("Nome"))
                {
                    string nome = txtNome.Text.Trim();

                    int index = PanelDescontos.Controls.IndexOf(txtNome);
                    if (index + 1 < PanelDescontos.Controls.Count &&
                        PanelDescontos.Controls[index + 1] is TextBox txtValor &&
                        decimal.TryParse(txtValor.Text, out decimal valor))
                    {
                        descontos.Add((nome, valor));
                    }
                }
            }
            var formResumo = new FormResumo(ganhos, descontos, salarioBruto, salarioLiquido,salarioBase, valorFgts,nomeFuncionario, matricula, cpf, cargo, pis, logradouroFuncionario, numeroFuncionario, bairroFuncionario, cidadeFuncionario, cep, razaoSocial, cnpj, logradouroEmpresa, numeroEmpresa, bairroEmpresa, cidadeEmpresa, cepEmpresa);
            formResumo.ShowDialog();

        }

        private void btnAdicionarBeneficio_Click(object sender, EventArgs e)
        {
            TextBox txtNome = new TextBox
            {
                Name = "txtAdicionalNome" + panelBeneficios.Controls.Count,
                Width = 100,
                Location = new Point(10, yOffset)
            };

            TextBox txtValor = new TextBox
            {
                Name = "txtAdicionalValor" + panelBeneficios.Controls.Count,
                Width = 80,
                Location = new Point(120, yOffset)
            };
            //validar.ValidarDados(txtNome, txtValor);
            

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
            TextBox txtNome = new TextBox
            {
                Name = "txtDescontolNome" + PanelDescontos.Controls.Count,
                Width = 100,
                Location = new Point(10, yOffset2)
            };

            TextBox txtValor = new TextBox
            {
                Name = "txtDescontoValor" + PanelDescontos.Controls.Count,
                Width = 80,
                Location = new Point(120, yOffset2)
            };
            txtValor.KeyPress += validar.ValidacaoNumeros;
            PanelDescontos.Controls.Add(txtNome);
            PanelDescontos.Controls.Add(txtValor);

            //validar.ValidarDados(txtNome, txtValor);

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
        

        private void button6_Click(object sender, EventArgs e)
        {
            temaEscuroAtivo = !temaEscuroAtivo; // alterna entre claro e escuro
            acessibilidade.AplicarTema(this, temaEscuroAtivo);
        }

        private void rdbSimPdl_CheckedChanged(object sender, EventArgs e)
        {
            txtParticipacao_de_lucro_percentual.Enabled = rdbSimPdl.Checked;
            txtParticipacao_de_lucros_valor_liquido.Enabled = rdbSimPdl.Checked;

        }
        private void rdbSimAn_CheckedChanged(object sender, EventArgs e)
        {
            txtAdicional_noturno.Enabled = rdbSimAn.Checked;
        }

        private void rdbSimAdc_CheckedChanged(object sender, EventArgs e)
        {
            txtAjuda_de_custo.Enabled = rdbSimAdc.Checked;
        }
        private void rdbSimAdiantamento_CheckedChanged(object sender, EventArgs e)
        {
            txtAdiantamento.Enabled = rdbSimAdiantamento.Checked;
        }
        private void rdbSimI_CheckedChanged(object sender, EventArgs e)
        {
            cmbInsalubridade_porcentagem.Enabled = rdbSimI.Checked;
        }

        private void rdbSimCreche_CheckedChanged(object sender, EventArgs e)
        {
            txtAuxilio_creche.Enabled = rdbSimCreche.Checked;
        }

        private void rdbSimAbono_CheckedChanged(object sender, EventArgs e)
        {
            txtAbono.Enabled = rdbSimAbono.Checked;
        }

        private void rdbSimComissao_CheckedChanged(object sender, EventArgs e)
        {
            txtComissao_valor_total.Enabled = rdbSimComissao.Checked;
            txtComissao_percentual.Enabled = rdbSimComissao.Checked;
        }

        private void rdbSimFamilia_CheckedChanged(object sender, EventArgs e)
        {
            txtAuxilio_familia_numero_de_dependentes.Enabled = rdbSimFamilia.Checked;
            txtAuxilio_familia_valor_da_cota.Enabled = rdbSimFamilia.Checked;
        }

        private void rdbSimDr_CheckedChanged(object sender, EventArgs e)
        {
            txtDescanco_remunerado_dias_uteis.Enabled = rdbSimDr.Checked;
            txtDescanco_remunerado_dias_descanco.Enabled = rdbSimDr.Checked;
        }

        private void chk50He_CheckedChanged(object sender, EventArgs e)
        {
            txtHora_extra_50.Enabled = chk50He.Checked;
        }

        private void rdbSimVt_CheckedChanged(object sender, EventArgs e)
        {
            txtVale_transporte.Enabled = rdbSimVt.Checked;
        }

        private void rdbSimSv_CheckedChanged(object sender, EventArgs e)
        {
            txtSeguro_de_vida.Enabled = rdbSimSv.Checked;
        }

        private void rdbSimPs_CheckedChanged(object sender, EventArgs e)
        {
            txtPlano_de_saude.Enabled = rdbSimPs.Checked;
        }

        private void rdbSimPp_CheckedChanged(object sender, EventArgs e)
        {
            txtPrevidencia_privada.Enabled = rdbSimPp.Checked;
        }

        private void rdbSimEc_CheckedChanged(object sender, EventArgs e)
        {
            txtEmprestimo_consiganado.Enabled = rdbSimEc.Checked;
        }

        private void rdbSimFaltas_CheckedChanged(object sender, EventArgs e)
        {
            txtFaltas.Enabled = rdbSimFaltas.Checked;
        }

        private void rdbSimPa_CheckedChanged(object sender, EventArgs e)
        {
            txtPensao_porcentagem.Enabled = rdbSimPa.Checked;
            cmbPensao_tipo.Enabled = rdbSimPa.Checked;
        }

        private void rdbSimVa_CheckedChanged(object sender, EventArgs e)
        {
            txtVale_alimentacao.Enabled = rdbSimVa.Checked;
        }

        private void rdbSimSindicato_CheckedChanged(object sender, EventArgs e)
        {
            txtSindicato.Enabled = rdbSimSindicato.Checked;
        }

        private void chk100He_CheckedChanged(object sender, EventArgs e)
        {
            txtHora_extra_100.Enabled = chk100He.Checked;
        }
        public class ControlLayout
        {
            public Control Control { get; set; }
            public Rectangle OriginalBounds { get; set; }
            public float OriginalFontSize { get; set; }
        }

        private void ArmazenarLayout(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                originalLayout.Add(new ControlLayout
                {
                    Control = ctrl,
                    OriginalBounds = ctrl.Bounds,
                    OriginalFontSize = ctrl.Font.Size
                });

                if (ctrl.HasChildren)
                    ArmazenarLayout(ctrl);
            }
        }

        private void AplicarZoom(float fator)
        {
            // Redimensiona o formulário
            //this.ClientSize = new Size(
            //    (int)(tamanhoOriginalForm.Width * fator),
            //    (int)(tamanhoOriginalForm.Height * fator)
            //);

            // Redimensiona os controles
            foreach (var item in originalLayout)
            {
                var ctrl = item.Control;

                ctrl.Bounds = new Rectangle(
                    (int)(item.OriginalBounds.X * fator),
                    (int)(item.OriginalBounds.Y * fator),
                    (int)(item.OriginalBounds.Width * fator),
                    (int)(item.OriginalBounds.Height * fator)
                );

                ctrl.Font = new Font(ctrl.Font.FontFamily, item.OriginalFontSize * fator, ctrl.Font.Style);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {      
            fatorZoomAtual = Math.Max(0.5f,fatorZoomAtual - 0.8f);
            WindowState = FormWindowState.Normal;
            this.AutoScroll = false;
            AplicarZoom(fatorZoomAtual);
            button1.Enabled = false;
            button7.Enabled = true;
            return;  
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fatorZoomAtual += 0.8f;
            this.AutoScroll = true;
            WindowState = FormWindowState.Maximized;
            AplicarZoom(fatorZoomAtual);
            button1.Enabled = true;
            button7.Enabled = false;
        }




    }
}
