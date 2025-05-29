using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolhaDePagamento
{
    internal class Acessibilidade
    {
        public void AplicarTema(Control controlePai, bool escuro)
        {
            Color corFundo = escuro ? Color.Black : Color.White;
            Color corTexto = escuro ? Color.White : Color.Black;

            controlePai.BackColor = corFundo;
            controlePai.ForeColor = corTexto;

            // Tratamento especial para controles que precisam de configuração adicional
            if (controlePai is DataGridView dgv)
            {
                dgv.BackgroundColor = corFundo;
                dgv.DefaultCellStyle.BackColor = corFundo;
                dgv.DefaultCellStyle.ForeColor = corTexto;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = corFundo;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = corTexto;
            }

            // Aplica nos filhos
            foreach (Control filho in controlePai.Controls)
            {
                AplicarTema(filho, escuro);
            }
        }
    }
}
