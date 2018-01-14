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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var consultaCompraGado = new ConsultaCompraGado();
            consultaCompraGado.Closed += (s, args) => this.Close();
            consultaCompraGado.Show();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            this.Hide();
            var cadastraCompraGado = new CadastraGado();
            cadastraCompraGado.Closed += (s, args) => this.Close();
            cadastraCompraGado.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            string titulo = "Sair";
            string mensagem = "Deseja realmente sair do sistema?";

            if (

                MessageBox.Show(mensagem, titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes
                )
            {
                Application.Exit();
            }
        }

        private void btnPecuarista_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nova Funcionalidade a ser implentada futuramente.");
        }

        private void btnAnimal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nova Funcionalidade a ser implentada futuramente.");
        }
    }
}
