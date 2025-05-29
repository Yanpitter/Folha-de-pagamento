using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaDePagamento
{
    internal class Validacoes
    {
        private string GerarNome(Control ctrl)
        {
            // Remove prefixos comuns
            string nome = ctrl.Name;

            // Remove prefixos como "txt", "lbl", "cmb" etc.
            if (nome.StartsWith("txt") || nome.StartsWith("lbl") || nome.StartsWith("cmb") || nome.StartsWith("chk") || nome.StartsWith("mask"))
            {
                nome = nome.Substring(3);
            }

            // Substitui underline por espaço
            nome = nome.Replace("_", " ");

            // Separa palavras compostas com letra maiúscula
            nome = System.Text.RegularExpressions.Regex.Replace(nome, "([a-z])([A-Z])", "$1 $2");

            // Coloca a primeira letra maiúscula
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome.ToLower());
        }

        public bool ValidarDados(TextBox campo)
        {
            if (string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                
                campo.Focus();
                return true;
            }
            return false;
        }
        public bool ValidarDados(RadioButton botao, TextBox campo)
        {
            if (botao.Checked == true & string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }
        public bool ValidarDadosDecimal(RadioButton botao, TextBox campo)
        {
            if (botao.Checked == true && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked == true && !decimal.TryParse((campo.Text), out decimal valor))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores númericos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }
        public bool ValidarDadosInt(RadioButton botao, TextBox campo)
        {
            if (botao.Checked == true && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked == true && !int.TryParse((campo.Text), out int valor))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores númericos inteiros.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                  campo.Focus();
                return true;
            }
            return false;
        }
        public void ValidacaoNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        public bool ValidarDadosDecimal(CheckBox botao, TextBox campo)
        {
            if (botao.Checked == true && string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} requer preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked == true && !decimal.TryParse((campo.Text), out decimal valor))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores númericos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDadosDecimal(RadioButton botao, TextBox campo, ComboBox opcao)
        {
            if (botao.Checked == true && string.IsNullOrWhiteSpace(campo.Text) && string.IsNullOrWhiteSpace(opcao.Text))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} preenchimento.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            if (botao.Checked == true && !decimal.TryParse((campo.Text), out decimal valor))
            {
                MessageBox.Show($"O campo {GerarNome(campo)} só aceita valores númericos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo.Focus();
                return true;
            }
            return false;
        }

        public bool ValidarDadosInt(RadioButton botao, ComboBox opcao)
        {
            if (botao.Checked == true && string.IsNullOrWhiteSpace(opcao.Text))
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
            if (!decimal.TryParse((campo2.Text), out decimal valor))
            {
                MessageBox.Show($"O campo {GerarNome(campo2)} só aceita valores númericos.", "Erro nos dados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                campo2.Focus();
                return true;
            }
            return false;
        }
        public bool ValidarDadosAdicionais(Panel painel)
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

        
    }
}
