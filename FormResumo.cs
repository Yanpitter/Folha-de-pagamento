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
        // mes e ano que vai aparecer no contracheque
        string mesSelecionado = "Maio";
        string anoSelecionado = "2025";
        public FormResumo
            (
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
            salarioBrutoPdf = salarioBruto ;
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
            cnpjPdf  = cnpj;
            logradouroEmpresaPdf = logradouroEmpresa;
            numeroEmpresaPdf = numeroEmpresa;
            bairroEmpresaPdf = bairroEmpresa;
            cidadeEmpresaPdf = cidadeEmpresa;
            cepEmpresaPdf = cepEmpresa;

            foreach (var item in ganhos)
            {
                dgvGanhos.Rows.Add(item.nome, item.valor.ToString("C2"));
            }

            // Preencher tabela de descontos
            foreach (var item in descontos)
            {
                dgvDescontos.Rows.Add(item.nome, item.valor.ToString("C2"));
            }

            lblBruto.Text = "Salário Bruto: " + salarioBruto.ToString("C2");
            lblLiquido.Text = "Salário Líquido: " + salarioLiquido.ToString("C2");
            
        }
        

        

        private void button1_Click(object sender, EventArgs e)
        {
           
           
           


            // =================== DIÁLOGO PARA SALVAR O ARQUIVO ===================
            // Instancia um diálogo de salvamento de arquivo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // Define filtro para arquivos PDF
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            // Título da janela de salvamento
            saveFileDialog.Title = "Salvar Folha de pagamento";
            // Nome padrão do arquivo, removendo espaços do nome do colaborador
            saveFileDialog.FileName = $"Folha_de_pagamento_{nomeFuncionarioPdf.Replace(" ", "_")}_{mesSelecionado}_{anoSelecionado}.pdf";

            // Se o usuário clicar em "Salvar"
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Armazena o caminho completo escolhido pelo usuário
                string caminhoArquivo = saveFileDialog.FileName;

                // Define o tamanho da página como A4
                iTextSharp.text.Rectangle pageSize = iTextSharp.text.PageSize.A4;

                // Abre um FileStream para criar o arquivo PDF
                using (FileStream stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    // Cria o documento e o writer para gerar o PDF
                    Document doc = new Document(pageSize);
                    PdfWriter writer = PdfWriter.GetInstance(doc, stream);

                    // Abre o documento para edição
                    doc.Open();

                    // Define fontes padrão e negrito para o PDF
                    var fonte = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                    var fonteNegrito = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

                    // ======= TÍTULO DO DOCUMENTO =======
                    Paragraph titulo = new Paragraph(new Chunk("FOLHA DE PAGAMENTO", fonteNegrito));
                    titulo.Alignment = Element.ALIGN_CENTER; // Centraliza o título
                    doc.Add(titulo); // Adiciona ao documento
                    doc.Add(new Paragraph("\n")); // Linha em branco


                    // ======= DADOS DA EMPRESA =======
                    doc.Add(new Paragraph(new Chunk("DADOS DA EMPRESA", fonteNegrito)));
                    doc.Add(new Paragraph(new Chunk($"Empresa: {razaoSocialPdf}", fonte)));
                    doc.Add(new Paragraph(new Chunk($"CNPJ: {cnpjPdf}", fonte)));
                    doc.Add(new Paragraph(new Chunk($"Endereço: {logradouroEmpresaPdf}, {numeroEmpresaPdf} - {bairroEmpresaPdf} - {cidadeEmpresaPdf}", fonte)));
                    doc.Add(new Paragraph($"CEP da Empresa: {cepEmpresaPdf}"));
                    doc.Add(new Paragraph("\n")); // Linha em branco

                    // ======= DADOS DO COLABORADOR =======
                    doc.Add(new Paragraph(new Chunk("DADOS DO COLABORADOR", fonteNegrito)));
                    doc.Add(new Paragraph(new Chunk($"Nome: {nomeFuncionarioPdf}", fonte)));
                    doc.Add(new Paragraph(new Chunk($"CPF: {cpfPdf}", fonte)));
                    doc.Add(new Paragraph(new Chunk($"Cargo: {cargoPdf}", fonte)));
                    doc.Add(new Paragraph(new Chunk($"Endereço: {logradouroFuncionarioPdf}, {numeroFuncionarioPdf} - {bairroFuncionarioPdf}", fonte)));
                    doc.Add(new Paragraph($"Matrícula: {matriculaPdf}"));
                    doc.Add(new Paragraph($"PIS/PASEP: {pisPdf}"));
                    doc.Add(new Paragraph($"Cidade: {cidadeFuncionarioPdf}"));
                    doc.Add(new Paragraph($"CEP: {cepPdf}"));

                    doc.Add(new Paragraph("\n"));

                    // ======= COMPETÊNCIA (MÊS/ANO) =======
                    doc.Add(new Paragraph(new Chunk($"Referente a: {mesSelecionado} de {anoSelecionado}", fonteNegrito)));
                    doc.Add(new Paragraph("\n"));

                    // ======= TABELA DE PROVENTOS =======
                    PdfPTable tabelaProventos = new PdfPTable(2); // Cria tabela com 2 colunas
                    tabelaProventos.WidthPercentage = 100; // Ocupa 100% da largura
                    tabelaProventos.AddCell("PROVENTOS");
                    tabelaProventos.AddCell("VALOR");

                    foreach( DataGridViewRow row in dgvGanhos.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                        {
                            tabelaProventos.AddCell(row.Cells[0].Value.ToString());
                            tabelaProventos.AddCell(row.Cells[1].Value.ToString());
                        }
                    }

                    
                    doc.Add(tabelaProventos); // Adiciona a tabela ao PDF
                    doc.Add(new Paragraph("\n"));

                    // ======= TABELA DE DESCONTOS =======
                    PdfPTable tabelaDescontos = new PdfPTable(2);
                    tabelaDescontos.WidthPercentage = 100;
                    tabelaDescontos.AddCell("DESCONTOS");
                    tabelaDescontos.AddCell("VALOR");

                    foreach (DataGridViewRow row in dgvDescontos.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                        {
                            tabelaDescontos.AddCell(row.Cells[0].Value.ToString());
                            tabelaDescontos.AddCell(row.Cells[1].Value.ToString());
                        }
                    }
                    // Adiciona a tabela de descontos ao PDF
                    doc.Add(tabelaDescontos);
                    doc.Add(new Paragraph("\n"));

                    // ======= SALÁRIO BRUTO =======
                    doc.Add(new Paragraph(new Chunk($"Salário Bruto: R$ {salarioBrutoPdf:0.00}", fonteNegrito)));
                    // ======= SALÁRIO LÍQUIDO =======
                    doc.Add(new Paragraph(new Chunk($"Salário Líquido: R$ {salarioLiquidoPdf:0.00}", fonteNegrito)));
                    // ======= FGTS =======
                    doc.Add(new Paragraph(new Chunk($"Fgts: R$ {fgtsPdf:0.00}", fonteNegrito)));

                    // Fecha o documento e o escritor
                    doc.Close();
                    writer.Close();
                }

                // Exibe mensagem de sucesso após gerar o PDF
                MessageBox.Show("Contracheque PDF gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
   