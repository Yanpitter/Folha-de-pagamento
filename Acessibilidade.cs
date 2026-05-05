using System;
using System.Drawing;
using System.Windows.Forms;

namespace FolhaDePagamento
{
    internal class Acessibilidade
    {
        public void AplicarTema(Control controlePai, bool escuro)
        {
            Color corFundo = escuro ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            Color corTexto = escuro ? Color.Orange : Color.Black;

            AplicarTemaRecursivo(controlePai, escuro, corFundo, corTexto);
        }

        private void AplicarTemaRecursivo(Control ctrl, bool escuro, Color corFundo, Color corTexto)
        {
            ctrl.BackColor = corFundo;
            ctrl.ForeColor = corTexto;

            if (ctrl is DataGridView dgv)
            {
                dgv.BackgroundColor = corFundo;
                dgv.DefaultCellStyle.BackColor = corFundo;
                dgv.DefaultCellStyle.ForeColor = corTexto;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = corFundo;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = corTexto;
            }

            // Trata botões comuns (caso adicione ícones a eles no futuro)
            if (ctrl is Button btn && btn.Image != null)
            {
                btn.Image = ColorirIcone(btn.Image, escuro ? Color.Orange : Color.Black);
            }

            // CORREÇÃO: Trata a barra de ferramentas (ToolStrip) e seus botões (ToolStripButton)
            if (ctrl is ToolStrip ts)
            {
                ts.BackColor = corFundo;
                ts.ForeColor = corTexto;

                // O ToolStrip usa .Items em vez de .Controls
                foreach (ToolStripItem item in ts.Items)
                {
                    item.BackColor = corFundo;
                    item.ForeColor = corTexto;

                    if (item is ToolStripButton btnTs && btnTs.Image != null)
                    {
                        btnTs.Image = ColorirIcone(btnTs.Image, escuro ? Color.Orange : Color.Black);
                    }
                }
            }

            // Continua buscando em outros controles da tela
            foreach (Control filho in ctrl.Controls)
            {
                AplicarTemaRecursivo(filho, escuro, corFundo, corTexto);
            }
        }

        private Image ColorirIcone(Image imagemOriginal, Color corDesejada)
        {
            if (imagemOriginal == null) return null;

            Bitmap bmp = new Bitmap(imagemOriginal);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color pixel = bmp.GetPixel(x, y);
                    // Pinta apenas os pixels que não são transparentes
                    if (pixel.A > 0)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(pixel.A, corDesejada.R, corDesejada.G, corDesejada.B));
                    }
                }
            }
            return bmp;
        }
    }
}