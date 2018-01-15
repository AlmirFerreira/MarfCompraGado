using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarfrigCompras.WebApi
{
    public static class Constants
    {
        //Alterar o valor dessa string de conexao para o local onde estiver o arquivo ( Marfrig.mdf )
        public const string MarfrigDbConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Marfrig.mdf;Integrated Security=True;Connect Timeout=30";
    }
}
