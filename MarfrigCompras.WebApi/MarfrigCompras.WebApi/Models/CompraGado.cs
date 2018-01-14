using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarfrigCompras.WebApi.Models
{
    public class CompraGado
    {
        #region Properties
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public int PecuaristaID { get; set; }
        #endregion

        #region OrMapping
        private List<CompraGadoGrid> OrMapping(SqlDataReader reader)
        {
            var compraGadoList = new List<CompraGadoGrid>();
            while (reader.Read())
            {
                var compraGado = new CompraGadoGrid()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    DataEntrega = Convert.ToDateTime(reader["DataEntrega"]),
                    Pecuarista = reader["Pecuarista"].ToString(),
                    ValorTotal = Convert.ToDouble(reader["ValorTotal"])
                };

                compraGadoList.Add(compraGado);

            }

            return compraGadoList;
        }

        private List<CompraGado> OrMappingBasico(SqlDataReader reader)
        {
            var compraGadoList = new List<CompraGado>();
            while (reader.Read())
            {
                var compraGado = new CompraGado()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    DataEntrega = Convert.ToDateTime(reader["DataEntrega"]),
                    PecuaristaID = Convert.ToInt32(reader["PecuaristaID"])
                };

                compraGadoList.Add(compraGado);

            }

            return compraGadoList;
        }

        private List<CompraGadoItensReport> OrMappingItens(SqlDataReader reader)
        {
            var compraGadoItensList = new List<CompraGadoItensReport>();
            while (reader.Read())
            {
                var compraGadoItem = new CompraGadoItensReport()
                {
                    Animal = reader["Animal"].ToString(),
                    Preco = reader["Preco"].ToString(),
                    Quantidade = reader["Quantidade"].ToString(),
                    Total = reader["Total"].ToString()
                };

                compraGadoItensList.Add(compraGadoItem);

            }

            return compraGadoItensList;
        }
        #endregion

        #region Methods
        public List<CompraGadoGrid> ConsultaCompraGado(CompraGadoFilter filter)
        {
            var connString = Constants.MarfrigDbConnection;
            var compraGadoList = new List<CompraGadoGrid>();

            using (SqlConnection conn = new SqlConnection(connString))
            {

                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(" SELECT cg.Id, cg.DataEntrega, p.Nome AS Pecuarista, SUM(cgi.Quantidade * a.Preco) AS ValorTotal ");
                    sql.AppendLine(" FROM CompraGado cg ");
                    sql.AppendLine(" INNER JOIN Pecuarista p ON p.Id = cg.PecuaristaId ");
                    sql.AppendLine(" INNER JOIN CompraGadoItem cgi ON cgi.CompraGadoId = cg.Id ");
                    sql.AppendLine(" INNER JOIN Animal a ON a.Id = cgi.AnimalId ");
                    sql.AppendLine(" WHERE 1 = 1 ");

                    if(filter.Id > 0)
                    {
                        sql.AppendFormat(" AND cg.ID = {0} ", filter.Id);
                    }

                    if(filter.PecuaristaID > 0)
                    {
                        sql.AppendFormat(" AND p.Id = {0} ", filter.PecuaristaID);
                    }

                    if (filter.BeginDate != DateTime.MinValue)
                    {
                        sql.AppendFormat(" AND cg.DataEntrega >= '{0}' ", filter.BeginDate.ToString("yyyy-MM-dd"));
                    }

                    if (filter.EndDate != DateTime.MinValue)
                    {
                        sql.AppendFormat(" AND cg.DataEntrega <= '{0}' ", filter.EndDate.ToString("yyyy-MM-dd"));
                    }

                    sql.AppendLine(" GROUP BY cg.Id, cg.DataEntrega, p.Nome ");

                    SqlCommand sqlCommand = new SqlCommand(sql.ToString(), conn);
                    conn.Open();

                    var objDataReader = sqlCommand.ExecuteReader();
                    if (objDataReader.HasRows)
                    {
                        compraGadoList = OrMapping(objDataReader);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

            return compraGadoList;
        }

        public List<CompraGadoItensReport> ConsultaCompraGadoItens(int compraGadoId)
        {
            var connString = Constants.MarfrigDbConnection;
            var compraGadoItensList = new List<CompraGadoItensReport>();

            using (SqlConnection conn = new SqlConnection(connString))
            {

                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(" SELECT a.Descricao AS Animal, a.Preco, cgi.Quantidade, SUM(cgi.Quantidade * a.Preco) AS Total ");
                    sql.AppendLine(" FROM CompraGado cg ");
                    sql.AppendLine(" INNER JOIN CompraGadoItem cgi ON cgi.CompraGadoId = cg.Id ");
                    sql.AppendLine(" INNER JOIN Animal a ON a.Id = cgi.AnimalId ");
                    sql.AppendLine(" WHERE 1 = 1 ");

                    if (compraGadoId > 0)
                    {
                        sql.AppendFormat(" AND cg.Id = {0} ", compraGadoId);
                    }

                    sql.AppendLine(" GROUP BY a.Descricao, a.Preco, cgi.Quantidade ");

                    SqlCommand sqlCommand = new SqlCommand(sql.ToString(), conn);
                    conn.Open();

                    var objDataReader = sqlCommand.ExecuteReader();
                    if (objDataReader.HasRows)
                    {
                        compraGadoItensList = OrMappingItens(objDataReader);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

            return compraGadoItensList;
        }

        public int Save(CompraGado compraGado)
        {
            var connString = Constants.MarfrigDbConnection;
            int novaCompraId = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(" INSERT INTO CompraGado ");
                    sql.AppendLine(" (DataEntrega, PecuaristaID) ");
                    sql.AppendLine(" VALUES ");
                    sql.AppendFormat("('{0}', {1}) ; ", compraGado.DataEntrega.ToString("yyyy-MM-dd"), compraGado.PecuaristaID);

                    SqlCommand sqlCommand = new SqlCommand(sql.ToString(), conn);
                    conn.Open();

                    sqlCommand.ExecuteNonQuery();
                    conn.Close();

                    string sqlRetorno = "SELECT TOP 1 * FROM CompraGado ORDER BY Id DESC";

                    SqlCommand sqlCommandRetorno = new SqlCommand(sqlRetorno, conn);
                    conn.Open();

                    var objDataReader = sqlCommandRetorno.ExecuteReader();

                    if (objDataReader.HasRows)
                    {
                        var compraRetorno = OrMappingBasico(objDataReader);
                        novaCompraId = compraRetorno.FirstOrDefault().Id;
                    }
                    conn.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

            return novaCompraId;
        }

        public void SaveItens(CompraGadoItem compraGadoItem)
        {
            var connString = Constants.MarfrigDbConnection;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(" INSERT INTO CompraGadoItem ");
                    sql.AppendLine(" (Quantidade, CompraGadoID, AnimalId) ");
                    sql.AppendLine(" VALUES ");
                    sql.AppendFormat(" ({0}, {1}, {2}); ", compraGadoItem.Quantidade, compraGadoItem.CompraGadoId, compraGadoItem.AnimalId);

                    sql.AppendLine(" SELECT @@IDENTITY FROM CompraGado ");

                    SqlCommand sqlCommand = new SqlCommand(sql.ToString(), conn);
                    conn.Open();

                    sqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
        }


    } 
    #endregion

    public class CompraGadoGrid
    {
        #region Properties
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Pecuarista { get; set; } 
        public double ValorTotal { get; set; }
        #endregion
    }

    public class CompraGadoFilter
    {
        #region Properties
        public int Id { get; set; }
        public int PecuaristaID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; } 
        #endregion
    }

}
