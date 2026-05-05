using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Globalization;
using System.Reflection;


namespace FolhaDePagamento
{
    public partial class FormResumo : Form
    {
        decimal salarioBrutoPdf;
        decimal salarioLiquidoPdf;
        decimal salarioBasePdf;
        decimal fgtsPdf;
        string nomeFuncionarioPdf;
        string matriculaPdf;
        string cpfPdf;
        string cargoPdf;
        string pisPdf;
        string logradouroFuncionarioPdf;
        string numeroFuncionarioPdf;
        string bairroFuncionarioPdf;
        string cidadeFuncionarioPdf;
        string cepPdf;
        string razaoSocialPdf;
        string cnpjPdf;
        string logradouroEmpresaPdf;
        string numeroEmpresaPdf;
        string bairroEmpresaPdf;
        string cidadeEmpresaPdf;
        string cepEmpresaPdf;

        // =================== CONTEXTO TEMPORAL AUTOMATICO ===================
        // O sistema identifica e preenche automaticamente o mes/ano referente
        string mesSelecionado;
        string anoSelecionado;

        // Lista dos meses em portugues
        private static readonly string[] NomesMeses = {
            "Janeiro", "Fevereiro", "Marco", "Abril", "Maio", "Junho",
            "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
        };
        // adicionamos necessário para armazenar os dados e gerar os dois holerites no PDF
        List<(string nome, decimal valor)> ganhosPdf; //LINHA ADICIONADA
        List<(string nome, decimal valor)> descontosPdf;// LINHA ADICIONADA 

        public FormResumo
            (
            // campos adicionados ganhospdf e descontospdf
            List<(string nome, decimal valor)> ganhos,
            List<(string nome, decimal valor)> descontos,
            decimal salarioBruto,
            decimal salarioLiquido,
            decimal salarioBase,
            decimal fgts,
            string nomeFuncionario,
            string matricula,
            string cpf,
            string cargo,
            string pis,
            string logradouroFuncionario,
            string numeroFuncionario,
            string bairroFuncionario,
            string cidadeFuncionario,
            string cep,
            string razaoSocial,
            string cnpj,
            string logradouroEmpresa,
            string numeroEmpresa,
            string bairroEmpresa,
            string cidadeEmpresa,
            string cepEmpresa
            )
        {
            InitializeComponent();

            ganhosPdf = ganhos;
            descontosPdf = descontos;

            salarioBrutoPdf = salarioBruto;
            salarioLiquidoPdf = salarioLiquido;
            salarioBasePdf = salarioBase;
            fgtsPdf = fgts;
            nomeFuncionarioPdf = nomeFuncionario;
            matriculaPdf = matricula;
            cpfPdf = cpf;
            cargoPdf = cargo;
            pisPdf = pis;
            logradouroFuncionarioPdf = logradouroFuncionario;
            numeroFuncionarioPdf = numeroFuncionario;
            bairroFuncionarioPdf = bairroFuncionario;
            cidadeFuncionarioPdf = cidadeFuncionario;
            cepPdf = cep;
            razaoSocialPdf = razaoSocial;
            cnpjPdf = cnpj;
            logradouroEmpresaPdf = logradouroEmpresa;
            numeroEmpresaPdf = numeroEmpresa;
            bairroEmpresaPdf = bairroEmpresa;
            cidadeEmpresaPdf = cidadeEmpresa;
            cepEmpresaPdf = cepEmpresa;

            // =================== CONTEXTO TEMPORAL AUTOMATICO ===================
            // Preenche automaticamente com o mes e ano atual do sistema
            DateTime agora = DateTime.Now;
            mesSelecionado = NomesMeses[agora.Month - 1];
            anoSelecionado = agora.Year.ToString();

            // Preenche tabela de ganhos com decimais formatados (N2 = 0,00)
            foreach (var item in ganhos)
            {
                dgvGanhos.Rows.Add(item.nome, item.valor.ToString("N2", new CultureInfo("pt-BR")));
            }

            // Preenche tabela de descontos com decimais formatados
            foreach (var item in descontos)
            {
                dgvDescontos.Rows.Add(item.nome, item.valor.ToString("N2", new CultureInfo("pt-BR")));
            }

            lblBruto.Text = "Salario Bruto: R$ " + salarioBruto.ToString("N2", new CultureInfo("pt-BR"));
            lblLiquido.Text = "Salario Liquido: R$ " + salarioLiquido.ToString("N2", new CultureInfo("pt-BR"));
        }

        // Formata decimal sempre com duas casas decimais no padrao BR
        private string FormatarDecimal(decimal valor)
        {
            return valor.ToString("N2", new CultureInfo("pt-BR"));
        }

        // Cria uma celula de cabecalho para as tabelas PDF
        private PdfPCell CriarCelulaHeader(string texto, iTextSharp.text.Font fonte)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fonte));
            cell.BackgroundColor = new BaseColor(220, 220, 220);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 4;
            return cell;
        }

        // Cria uma celula normal para as tabelas PDF
        private PdfPCell CriarCelula(string texto, iTextSharp.text.Font fonte, int alinhamento = Element.ALIGN_LEFT)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fonte));
            cell.HorizontalAlignment = alinhamento;
            cell.Padding = 3;
            return cell;
        }

        // Cria tabela com dados da empresa e do colaborador
        private PdfPTable CriarTabelaDados(iTextSharp.text.Font fontePequena)
        {
            PdfPTable tabelaDados = new PdfPTable(2);
            tabelaDados.WidthPercentage = 100;

            StringBuilder sbEmpresa = new StringBuilder();
            sbEmpresa.AppendLine("DADOS DA EMPRESA");
            sbEmpresa.AppendLine("Empresa: " + razaoSocialPdf);
            sbEmpresa.AppendLine("CNPJ: " + cnpjPdf);
            sbEmpresa.AppendLine("Endereco: " + logradouroEmpresaPdf + ", " + numeroEmpresaPdf);
            sbEmpresa.AppendLine(bairroEmpresaPdf + " - " + cidadeEmpresaPdf);
            sbEmpresa.AppendLine("CEP: " + cepEmpresaPdf);

            PdfPCell celulaEmpresa = new PdfPCell(new Phrase(sbEmpresa.ToString(), fontePequena));
            celulaEmpresa.Padding = 3;
            tabelaDados.AddCell(celulaEmpresa);

            StringBuilder sbFuncionario = new StringBuilder();
            sbFuncionario.AppendLine("DADOS DO COLABORADOR");
            sbFuncionario.AppendLine("Nome: " + nomeFuncionarioPdf);
            sbFuncionario.AppendLine("CPF: " + cpfPdf + "   Matricula: " + matriculaPdf);
            sbFuncionario.AppendLine("Cargo: " + cargoPdf);
            sbFuncionario.AppendLine("PIS/PASEP: " + pisPdf);
            sbFuncionario.AppendLine("Cidade: " + cidadeFuncionarioPdf + "  CEP: " + cepPdf);

            PdfPCell celulaFuncionario = new PdfPCell(new Phrase(sbFuncionario.ToString(), fontePequena));
            celulaFuncionario.Padding = 3;
            tabelaDados.AddCell(celulaFuncionario);

            return tabelaDados;
        }

        // Cria tabela de proventos e descontos lado a lado
        private PdfPTable CriarTabelaProventosDescontos(iTextSharp.text.Font fonte, iTextSharp.text.Font fonteNegrito)
        {
            PdfPTable tabelaPrincipal = new PdfPTable(2);
            tabelaPrincipal.WidthPercentage = 100;

            // Proventos
            PdfPTable tabelaProventos = new PdfPTable(2);
            tabelaProventos.WidthPercentage = 100;
            tabelaProventos.AddCell(CriarCelulaHeader("PROVENTOS", fonteNegrito));
            tabelaProventos.AddCell(CriarCelulaHeader("VALOR (R$)", fonteNegrito));

            foreach (var item in ganhosPdf)
            {
                tabelaProventos.AddCell(CriarCelula(item.nome, fonte));
                tabelaProventos.AddCell(CriarCelula(FormatarDecimal(item.valor), fonte, Element.ALIGN_RIGHT));
            }

            PdfPCell celulaProventos = new PdfPCell();
            celulaProventos.AddElement(tabelaProventos);
            celulaProventos.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabelaPrincipal.AddCell(celulaProventos);

            // Descontos
            PdfPTable tabelaDescontos = new PdfPTable(2);
            tabelaDescontos.WidthPercentage = 100;
            tabelaDescontos.AddCell(CriarCelulaHeader("DESCONTOS", fonteNegrito));
            tabelaDescontos.AddCell(CriarCelulaHeader("VALOR (R$)", fonteNegrito));

            foreach (var item in descontosPdf)
            {
                tabelaDescontos.AddCell(CriarCelula(item.nome, fonte));
                tabelaDescontos.AddCell(CriarCelula(FormatarDecimal(item.valor), fonte, Element.ALIGN_RIGHT));
            }

            PdfPCell celulaDescontos = new PdfPCell();
            celulaDescontos.AddElement(tabelaDescontos);
            celulaDescontos.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabelaPrincipal.AddCell(celulaDescontos);

            return tabelaPrincipal;
        }

        // Cria tabela de totais com valores decimais
        private PdfPTable CriarTabelaTotais(iTextSharp.text.Font fonteNegrito)
        {
            PdfPTable tabelaTotais = new PdfPTable(3);
            tabelaTotais.WidthPercentage = 100;

            tabelaTotais.AddCell(CriarCelulaHeader("Salario Bruto: R$ " + FormatarDecimal(salarioBrutoPdf), fonteNegrito));
            tabelaTotais.AddCell(CriarCelulaHeader("Salario Liquido: R$ " + FormatarDecimal(salarioLiquidoPdf), fonteNegrito));
            tabelaTotais.AddCell(CriarCelulaHeader("FGTS: R$ " + FormatarDecimal(fgtsPdf), fonteNegrito));

            return tabelaTotais;
        }

        // Cria linha de assinaturas
        private PdfPTable CriarTabelaAssinaturas(iTextSharp.text.Font fontePequena)
        {
            PdfPTable tabelaAssinatura = new PdfPTable(2);
            tabelaAssinatura.WidthPercentage = 100;

            PdfPCell celulaAssEmpresa = new PdfPCell(new Phrase("_______________________________\nAssinatura da Empresa", fontePequena));
            celulaAssEmpresa.HorizontalAlignment = Element.ALIGN_CENTER;
            celulaAssEmpresa.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabelaAssinatura.AddCell(celulaAssEmpresa);

            PdfPCell celulaAssFuncionario = new PdfPCell(new Phrase("_______________________________\nAssinatura do Colaborador", fontePequena));
            celulaAssFuncionario.HorizontalAlignment = Element.ALIGN_CENTER;
            celulaAssFuncionario.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tabelaAssinatura.AddCell(celulaAssFuncionario);

            return tabelaAssinatura;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // =================== DIALOGO PARA SALVAR O ARQUIVO ===================
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Salvar Folha de pagamento";
            saveFileDialog.FileName = "Folha_de_pagamento_" + nomeFuncionarioPdf.Replace(" ", "_") + "_" + mesSelecionado + "_" + anoSelecionado + ".pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoArquivo = saveFileDialog.FileName;

                // =================== ORIENTACAO PAISAGEM (HORIZONTAL) ===================
                // PageSize.A4.Rotate() gera o PDF em modo paisagem
                iTextSharp.text.Rectangle pageSize = iTextSharp.text.PageSize.A4.Rotate();

                using (FileStream stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    Document doc = new Document(pageSize, 20, 20, 20, 20);
                    PdfWriter writer = PdfWriter.GetInstance(doc, stream);
                    doc.Open();

                    // Fontes
                    var fonte = FontFactory.GetFont(FontFactory.HELVETICA, 8);
                    var fonteNegrito = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
                    var fontePequena = FontFactory.GetFont(FontFactory.HELVETICA, 7);

                    // =================== DOIS RELATORIOS NA MESMA PAGINA ===================
                    // Usa uma tabela com 2 colunas para colocar os dois holerites lado a lado
                    PdfPTable tabelaDupla = new PdfPTable(2);
                    tabelaDupla.WidthPercentage = 100;
                    tabelaDupla.SetWidths(new float[] { 1f, 1f });

                    // --- Holerite 1 (esquerda - via empresa) ---
                    PdfPCell celulaHolerite1 = new PdfPCell();
                    celulaHolerite1.Padding = 5;
                    celulaHolerite1.PaddingRight = 10; // LINHA ADICIONADA 

                    Paragraph titulo1 = new Paragraph("FOLHA DE PAGAMENTO", fonteNegrito);
                    titulo1.Alignment = Element.ALIGN_CENTER;
                    celulaHolerite1.AddElement(titulo1);

                    Paragraph mesRef1 = new Paragraph("Mes de Referencia: " + mesSelecionado + " / " + anoSelecionado, fontePequena);
                    mesRef1.Alignment = Element.ALIGN_CENTER;
                    celulaHolerite1.AddElement(mesRef1);
                    celulaHolerite1.AddElement(new Paragraph(" "));

                    celulaHolerite1.AddElement(CriarTabelaDados(fontePequena));
                    celulaHolerite1.AddElement(new Paragraph(" "));
                    celulaHolerite1.AddElement(CriarTabelaProventosDescontos(fonte, fonteNegrito));
                    celulaHolerite1.AddElement(new Paragraph(" "));
                    celulaHolerite1.AddElement(CriarTabelaTotais(fonteNegrito));
                    celulaHolerite1.AddElement(new Paragraph(" "));
                    celulaHolerite1.AddElement(CriarTabelaAssinaturas(fontePequena));

                    tabelaDupla.AddCell(celulaHolerite1);

                    // --- Holerite 2 (direita - via funcionario) ---
                    PdfPCell celulaHolerite2 = new PdfPCell();
                    celulaHolerite2.Padding = 5;
                    celulaHolerite2.PaddingLeft = 10; // LINHA ADICIONADA 

                    Paragraph viaFuncionario = new Paragraph("VIA DO FUNCIONARIO", fonteNegrito);
                    viaFuncionario.Alignment = Element.ALIGN_CENTER;
                    celulaHolerite2.AddElement(viaFuncionario);

                    Paragraph titulo2 = new Paragraph("FOLHA DE PAGAMENTO", fonteNegrito);
                    titulo2.Alignment = Element.ALIGN_CENTER;
                    celulaHolerite2.AddElement(titulo2);

                    Paragraph mesRef2 = new Paragraph("Mes de Referencia: " + mesSelecionado + " / " + anoSelecionado, fontePequena);
                    mesRef2.Alignment = Element.ALIGN_CENTER;
                    celulaHolerite2.AddElement(mesRef2);
                    celulaHolerite2.AddElement(new Paragraph(" "));

                    celulaHolerite2.AddElement(CriarTabelaDados(fontePequena));
                    celulaHolerite2.AddElement(new Paragraph(" "));
                    celulaHolerite2.AddElement(CriarTabelaProventosDescontos(fonte, fonteNegrito));
                    celulaHolerite2.AddElement(new Paragraph(" "));
                    celulaHolerite2.AddElement(CriarTabelaTotais(fonteNegrito));
                    celulaHolerite2.AddElement(new Paragraph(" "));
                    celulaHolerite2.AddElement(CriarTabelaAssinaturas(fontePequena));

                    tabelaDupla.AddCell(celulaHolerite2);

                    doc.Add(tabelaDupla);

                    doc.Close();
                    writer.Close();
                }

                MessageBox.Show("Contracheque PDF gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}