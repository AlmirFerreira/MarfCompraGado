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
    public partial class RelatorioCompraGado : Form
    {
        public List<CompraGadoItensReportAdapter> CompraGadoItens = new List<MarfrigCompras.CompraGadoItensReportAdapter>();
        

        public RelatorioCompraGado(string Id)
        {
            InitializeComponent();

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", Id.ToString())
            });

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49585/");
            var response = client.PostAsync("api/Compras/Compras", content).Result;

            var compra = response.Content.ReadAsAsync<IEnumerable<CompraGadoGrid>>().Result.FirstOrDefault();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource =
                "MarfrigCompras.ReportCompra.rdlc";

            Microsoft.Reporting.WinForms.ReportParameter[] p = new
                Microsoft.Reporting.WinForms.ReportParameter[4];

            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("Id", Id.ToString());
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("Dataentrega", compra.DataEntrega.ToShortDateString());
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("Pecuarista", compra.Pecuarista);
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("Total", compra.ValorTotal.ToString());


            reportViewer1.LocalReport.SetParameters(p);
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();

        }

        private void RelatorioCompraGado_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", CompraGadoItens));
            this.reportViewer1.RefreshReport();
        }
    }
}
