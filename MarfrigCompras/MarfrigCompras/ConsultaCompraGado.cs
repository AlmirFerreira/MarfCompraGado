using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace MarfrigCompras
{
    public partial class ConsultaCompraGado : Form
    {

        #region Variaveis Publicas
        public int paginacao { get; set; }
        public int paginacaoVoltar { get; set; }
        public int itensPorPagina = 3;
        public List<CompraGadoGrid> resultadoConsulta { get; set; }
        #endregion

        #region Eventos
        public ConsultaCompraGado()
        {
            InitializeComponent();
            BloqueiaCampos();
            LoadPecuaristas();

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            FormUrlEncodedContent content = AlimentaFiltros();
            ConsultaComprasGado(content);

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Hide();
            var menuPrincipal = new Form1();
            menuPrincipal.Closed += (s, args) => this.Close();
            menuPrincipal.Show();
        }

        private void btAnterior_Click(object sender, EventArgs e)
        {
            btnProxima.Visible = true;

            if (resultadoConsulta.Count <= (paginacao + itensPorPagina))
            {
                int primeiraPagina = Math.Max(0, paginacao - itensPorPagina);
                dataGridView1.DataSource = resultadoConsulta.GetRange(primeiraPagina, itensPorPagina);
                paginacao = primeiraPagina;
            }
            else
            {
                int primeiraPagina = (resultadoConsulta.Count - (paginacao + itensPorPagina));
                if (primeiraPagina >= itensPorPagina)
                {
                    dataGridView1.DataSource = resultadoConsulta.GetRange(0, primeiraPagina);
                }

                btAnterior.Visible = false;
            }

        }

        private void btnProxima_Click(object sender, EventArgs e)
        {
            btAnterior.Visible = true;

            if (paginacao.Equals(0))
            {
                paginacao = itensPorPagina;
            }
            if (resultadoConsulta.Count >= (paginacao + itensPorPagina))
            {
                dataGridView1.DataSource = resultadoConsulta.GetRange(paginacao, itensPorPagina);
                paginacao += itensPorPagina;
            }
            else
            {
                int ultimaPagina = Math.Max(0, resultadoConsulta.Count - paginacao);
                if (ultimaPagina > 0)
                {
                    dataGridView1.DataSource = resultadoConsulta.GetRange(paginacao, ultimaPagina);
                    paginacao += ultimaPagina;
                }

                btnProxima.Visible = false;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[0].Value = "Imprimir";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            var Id = row.Cells[1].Value.ToString();

            RelatorioCompraGado relatorioCompraGado = new RelatorioCompraGado(Id);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("compraGadoId", Id.ToString())
            });

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49585/");
            var response = client.PostAsync("api/Compras/ComprasItem", content).Result;

            var compra = response.Content.ReadAsAsync<IEnumerable<CompraGadoItensReport>>().Result;

            foreach (var item in compra)
            {
                var compraItem = new MarfrigCompras.CompraGadoItensReportAdapter()
                {
                    Id = item.Animal,
                    Quantidade = item.Quantidade,
                    CompraGadoId = item.Preco,
                    AnimalId = item.Total
                };

                relatorioCompraGado.CompraGadoItens.Add(compraItem);
            }
                       
            relatorioCompraGado.ShowDialog();
        }
        #endregion

        #region Metodos
        private void BloqueiaCampos()
        {
            btAnterior.Visible = false;
            btnProxima.Visible = false;
            dataGridView1.Visible = false;
            lblConsultaVazia.Visible = false;

        }

        private void LoadPecuaristas()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:49585/");
                HttpResponseMessage response = client.GetAsync("api/Pecuarista/ListaPecuaristas").Result;

                var pecuaristaRetorno = response.Content.ReadAsAsync<IEnumerable<Pecuarista>>().Result;

                ddlPecuarista.DisplayMember = "Nome";
                ddlPecuarista.ValueMember = "Id";

                var pecuaristasLista = new List<Pecuarista>();
                pecuaristasLista.Add(new Pecuarista { Id = 0, Nome = "Selecione..." });
                pecuaristasLista.AddRange(pecuaristaRetorno);

                ddlPecuarista.DataSource = pecuaristasLista;
            }
            catch (Exception)
            {
                MessageBox.Show("Falha na comunicação com o Web.API");
            }
        }

        private void ConsultaComprasGado(FormUrlEncodedContent content)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:49585/");
                var response = client.PostAsync("api/Compras/Compras", content).Result;

                var compra = response.Content.ReadAsAsync<IEnumerable<CompraGadoGrid>>().Result;

                resultadoConsulta = compra.ToList();
                dataGridView1.Visible = (resultadoConsulta.Count > 0);
                lblConsultaVazia.Visible = (resultadoConsulta.Count.Equals(0));

                if (resultadoConsulta.Count > itensPorPagina)
                {
                    dataGridView1.DataSource = resultadoConsulta.GetRange(0, itensPorPagina);
                    paginacao = itensPorPagina;
                    paginacaoVoltar = itensPorPagina;

                    btnProxima.Visible = true;
                }
                else
                {
                    dataGridView1.DataSource = resultadoConsulta;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Falha na comunicação com o Web.API");
            }
        }

        private FormUrlEncodedContent AlimentaFiltros()
        {
            var compraGadoFilters = new CompraGadoFilter();

            if (!string.IsNullOrEmpty(txtId.Text))
            {
                int compraGadoID;

                if (int.TryParse(txtId.Text, out compraGadoID))
                {
                    compraGadoFilters.Id = compraGadoID;
                }

            }

            if (!string.IsNullOrEmpty(ddlPecuarista.SelectedValue.ToString()))
            {
                compraGadoFilters.PecuaristaID = int.Parse(ddlPecuarista.SelectedValue.ToString());
            }

            if (!string.IsNullOrEmpty(dtBeginDate.Text))
            {
                compraGadoFilters.BeginDate = dtBeginDate.Text;
            }

            if (!string.IsNullOrEmpty(dtEndDate.Text))
            {
                compraGadoFilters.EndDate = dtEndDate.Text;
            }

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", compraGadoFilters.Id.ToString()),
                new KeyValuePair<string, string>("PecuaristaID", compraGadoFilters.PecuaristaID.ToString()),
                new KeyValuePair<string, string>("BeginDate", compraGadoFilters.BeginDate),
                new KeyValuePair<string, string>("EndDate", compraGadoFilters.EndDate),
            });
            return content;
        }

        #endregion

    }
}
