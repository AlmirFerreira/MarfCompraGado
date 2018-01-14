using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarfrigCompras.WebApi.Models
{
    public class Pecuarista
    {
        #region Properties
        public int Id { get; set; }
        public string Nome { get; set; }
        #endregion

        #region OrMapping
        private List<Pecuarista> OrMapping(SqlDataReader reader)
        {
            var pecuaristaList = new List<Pecuarista>();
            while (reader.Read())
            {
                var pecuarista = new Pecuarista()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString()
                };

                pecuaristaList.Add(pecuarista);

            }

            return pecuaristaList;
        }
        #endregion

        #region Methods
        public List<Pecuarista> ListaPecuaristas()
        {
            var connString = Constants.MarfrigDbConnection;
            var pecuaristaList = new List<Pecuarista>();

            using (SqlConnection conn = new SqlConnection(connString))
            {

                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(" SELECT Id, Nome ");
                    sql.AppendLine(" FROM Pecuarista ");
                    sql.AppendLine(" ORDER BY Nome ASC");

                    SqlCommand sqlCommand = new SqlCommand(sql.ToString(), conn);
                    conn.Open();

                    var objDataReader = sqlCommand.ExecuteReader();
                    if (objDataReader.HasRows)
                    {
                        pecuaristaList = OrMapping(objDataReader);
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

            return pecuaristaList;
        }
        #endregion
    }

}