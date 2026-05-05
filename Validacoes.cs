using System;
using System.Linq;
using System.Windows.Forms;

namespace FolhaDePagamento
{
    // #22 - Acentuação corrigida em todas as mensagens de erro
    internal class Validacoes
    {
        private string GerarNome(Control ctrl)
        {
            string nome = ctrl.Name;
            if (nome.StartsWith("txt") || nome.StartsWith("lbl") || nome.StartsWith("cmb") || nome.StartsWith("chk") || nome.StartsWith("mask"))
                nome = nome.Substring(3);
            nome = nome.Replace("_", " ");
            nome = System.Text.RegularExpressions.Regex.Replace(nome, "([a-z])([A-Z])", "$1 $2");
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome.ToLower());
        }

        public bool ValidarDados(TextBox campo)
        {
            if (string.IsNullOrWhiteSpace(campo.Text))
            {
                // #22 - "requer" com grafia correta
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDados(RadioButton botao, TextBox campo)
        {
            if (botao.Checked == true && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        // Sobrecarga para RadioButton (mantida para compatibilidade)
        public bool ValidarDadosDecimal(RadioButton botao, TextBox campo)
        {
            if (botao.Checked && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked && !decimal.TryParse(campo.Text, out _))
            {
                // #22 - "numéricos" com acento
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores numéricos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDadosInt(RadioButton botao, TextBox campo)
        {
            if (botao.Checked && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked && !int.TryParse(campo.Text, out _))
            {
                // #22 - "numéricos inteiros" com acento
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores numéricos inteiros.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        // #15 - Sobrecarga para CheckBox (substitui RadioButton em Proventos e Descontos)
        public bool ValidarDadosDecimal(CheckBox botao, TextBox campo)
        {
            if (botao.Checked && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked && !decimal.TryParse(campo.Text, out _))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores numéricos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDadosDecimal(CheckBox botao, TextBox campo, ComboBox opcao)
        {
            if (botao.Checked && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked && !decimal.TryParse(campo.Text, out _))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores numéricos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDadosInt(CheckBox botao, TextBox campo)
        {
            if (botao.Checked && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked && !int.TryParse(campo.Text, out _))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores numéricos inteiros.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDadosInt(CheckBox botao, ComboBox opcao)
        {
            if (botao.Checked && string.IsNullOrWhiteSpace(opcao.Text))
            {
                MessageBox.Show($"O campo {GerarNome(opcao)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                opcao.Focus();
                return true;
            }
            return false;
        }

        // Mantida para compatibilidade
        public bool ValidarDadosDecimal(RadioButton botao, TextBox campo, ComboBox opcao)
        {
            if (botao.Checked && string.IsNullOrWhiteSpace(campo.Text) && string.IsNullOrWhiteSpace(opcao.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked && !decimal.TryParse(campo.Text, out _))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores numéricos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDadosInt(RadioButton botao, ComboBox opcao)
        {
            if (botao.Checked && string.IsNullOrWhiteSpace(opcao.Text))
            {
                MessageBox.Show($"O campo {GerarNome(opcao)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                opcao.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDados(TextBox campo1, TextBox campo2)
        {
            if (string.IsNullOrWhiteSpace(campo1.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo1)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo1.Focus();
                return true;
            }
            if (string.IsNullOrWhiteSpace(campo2.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo2)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo2.Focus();
                return true;
            }
            if (!decimal.TryParse(campo2.Text, out _))
            {
                MessageBox.Show($"O campo {GerarNome(campo2)} só aceita valores numéricos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo2.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDadosAdicionais(System.Windows.Forms.Panel painel)
        {
            foreach (Control controle in painel.Controls)
            {
                if (controle is TextBox txt && string.IsNullOrWhiteSpace(txt.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos dos adicionais.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txt.Focus();
                    return true;
                }
            }
            return false;
        }

        public void ValidacaoNumeros(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                e.Handled = true;
        }

        #region Adicionar validação no campo CPF, para aceitar apenas CPFs validos.
        public static bool ValidarCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11) return false;

            if (new string(cpf[0], 11) == cpf) return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
        #endregion
    }
}
