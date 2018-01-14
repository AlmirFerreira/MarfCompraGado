using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarfrigCompras
{
    public partial class CadastraGado : Form
    {
        List<Animal> animalLista = new List<Animal>();
        List<CompraGadoItens> compraGadoItens = new List<CompraGadoItens>();
        double totalCompra = 0;

        #region Eventos
        public CadastraGado()
        {
            InitializeComponent();
            LoadPecuaristas();
            LoadAnimais();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Hide();
            var menuPrincipal = new Form1();
            menuPrincipal.Closed += (s, args) => this.Close();
            menuPrincipal.Show();
        }

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            if (ValidaItens())
            {
                var compraGadoItem = new CompraGadoItens();

                compraGadoItem.AnimalID = int.Parse(ddlAnimal.SelectedValue.ToString());
                var animal = animalLista.Where(x => x.Id.Equals(compraGadoItem.AnimalID)).FirstOrDefault();
                compraGadoItem.AnimalNome = animal.Descricao;
                int.TryParse(txtQuantidade.Text, out int quantidade);
                compraGadoItem.Quantidade = quantidade;
                compraGadoItem.Valor = double.Parse(animal.Preco);
                compraGadoItem.Total = (compraGadoItem.Quantidade * compraGadoItem.Valor);

                compraGadoItens.Add(compraGadoItem);

                if (dataGridView1.DataSource != null)
                {
                    dataGridView1.DataSource = null;
                }

                LoadGridItens();

                totalCompra += compraGadoItem.Total;
                string totalCompraString = string.Format("{0:#.00}", Convert.ToDecimal(totalCompra));

                lblTotal.Text = totalCompraString;

                ddlAnimal.SelectedIndex = 0;
                txtQuantidade.Text = string.Empty;

            }
        }

        private void btnConcluirPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaInformacoes())
                {
                    
                    var conteudoCompra = new FormUrlEncodedContent(new[]
                                {
                        new KeyValuePair<string, string>("DataEntrega", dtEntrega.Text),
                        new KeyValuePair<string, string>("PecuaristaID", ddlPecuarista.SelectedValue.ToString())
                    });

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:49585/");
                    var responseCompra = client.PostAsync("api/Compras/InsereCompra", conteudoCompra).Result;

                    int compraID = responseCompra.Content.ReadAsAsync<int>().Result;
                    
                    foreach (var item in compraGadoItens)
                    {
                        var conteudoCompraItensContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("AnimalID",item.AnimalID.ToString()),
                            new KeyValuePair<string, string>("Quantidade",item.Quantidade.ToString()),
                            new KeyValuePair<string, string>("CompraGadoId",compraID.ToString())
                        });

                        HttpClient clientItem = new HttpClient();
                        clientItem.BaseAddress = new Uri("http://localhost:49585/");
                        var responseCompraItens = clientItem.PostAsync("api/Compras/InsereItens", conteudoCompraItensContent).Result;

                    }

                    MessageBox.Show("Compra Inserida com Sucesso!");
                    btnSair_Click(null, null);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na comunicação com o Web.API");
            }
        }
 
        private bool ValidaInformacoes()
        {
            bool informacoesValidas = true;

            if (ddlPecuarista.SelectedValue.Equals(0))
            {
                informacoesValidas = false;
                MessageBox.Show("Por favor informe qual o Pecuarista.");
            }

            if (compraGadoItens.Count.Equals(0))
            {
                informacoesValidas = false;
                MessageBox.Show("Por favor insire pelo menos um item");
            }

            return informacoesValidas;
        }

        private void btnLimparItens_Click(object sender, EventArgs e)
        {
            if (

                MessageBox.Show("Tem certeza que deseja remover todos os itens?", "Limpar Itens",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes
                )
            {
                LimparGridItens();
            }
        }

        #endregion

        #region Metodos
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

        private void LoadAnimais()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:49585/");
                HttpResponseMessage response = client.GetAsync("api/Animal/ListaAnimais").Result;

                var animalRetorno = response.Content.ReadAsAsync<IEnumerable<Animal>>().Result;
                animalLista = animalRetorno.ToList();

                ddlAnimal.DisplayMember = "Descricao";
                ddlAnimal.ValueMember = "Id";

                var animaisLista = new List<Animal>();
                animaisLista.Add(new Animal { Id = 0, Descricao = "Selecione..." });
                animaisLista.AddRange(animalRetorno);

                ddlAnimal.DataSource = animaisLista;
            }
            catch (Exception)
            {
                MessageBox.Show("Falha na comunicação com o Web.API");
            }
        }

        private bool ValidaItens()
        {
            bool itensValidos = true;

            if (ddlAnimal.SelectedValue.ToString().Equals("0"))
            {
                itensValidos = false;
                MessageBox.Show("Por favor selecione um animal para adicionar na compra");
            }
            if (string.IsNullOrEmpty("txtQuantidade.Text"))
            {
                itensValidos = false;
                MessageBox.Show("Por favor informe a quantidade à ser adicionada na compra");
            }

            return itensValidos;
        }

        private void LoadGridItens()
        {
            dataGridView1.Update();
            dataGridView1.Refresh();
            dataGridView1.DataSource = compraGadoItens;
            dataGridView1.Columns["AnimalID"].Visible = false;
        }

        private void LimparGridItens()
        {
            compraGadoItens = new List<CompraGadoItens>();
            dataGridView1.DataSource = null;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }
        #endregion

    }
}
