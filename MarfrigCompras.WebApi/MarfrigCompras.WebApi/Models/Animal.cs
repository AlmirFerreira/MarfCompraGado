using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarfrigCompras.WebApi.Models
{
    public class Animal
    {
        #region Properties
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Preco { get; set; } 
        #endregion

        #region OrMapping
        private List<Animal> OrMapping(SqlDataReader reader)
        {
            var animalList = new List<Animal>();
            while (reader.Read())
            {
                var animal = new Animal()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Descricao = reader["Descricao"].ToString(),
                    Preco = reader["Preco"].ToString()
                };

                animalList.Add(animal);

            }

            return animalList;
        }
        #endregion

        #region Methods
        public List<Animal> ListaAnimais()
        {
            var connString = Constants.MarfrigDbConnection;
            var animalList = new List<Animal>();

            using (SqlConnection conn = new SqlConnection(connString))
            {

                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(" SELECT Id, Descricao, Preco FROM Animal ");

                    SqlCommand sqlCommand = new SqlCommand(sql.ToString(), conn);
                    conn.Open();

                    var objDataReader = sqlCommand.ExecuteReader();
                    if (objDataReader.HasRows)
                    {
                        animalList = OrMapping(objDataReader);
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

            return animalList;
        } 
        #endregion

    }
}
