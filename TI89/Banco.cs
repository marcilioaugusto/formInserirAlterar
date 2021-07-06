using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace TI89
{
    public class Banco
    {
        public static MySqlConnection Abrir()
        {
            string strConexao = @"server=127.0.0.1;database=ti89;user id=root;password=usbw;port=3306";
            MySqlConnection cn = new MySqlConnection(strConexao);
            cn.Open();
            return cn;
        }
    }
}
