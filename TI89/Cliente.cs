using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TI89
{
    public class Cliente
    {
        //declaração de variaveis
        public int id;
        public string mensagem;
        public bool achou = false;

        // propriedades da class
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        //construtor
        public Cliente () { }

        //alterar
        public Cliente(int nCodigo,string nNome,string nEmail) 
        {
            //recebendo 
            this.ID = nCodigo;
            this.Nome = nNome;
            this.Email = nEmail;
        }

        //  inserir  nao pode haver id pois o mesmo é auto increment
        public Cliente(string nNome, string nEmail)
        {
            //recebendo 
            this.Nome = nNome;
            this.Email = nEmail;
        }

        // metodo para inserir  void ( não retorna nada pois nao importa o irá retornar )
        public void Inserir()
        {
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = Banco.Abrir();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_InsertUpdate";
            // paramentros como está no banco de dados
            comm.Parameters.AddWithValue("_id", 0);
            comm.Parameters.AddWithValue("_Nome",Nome);
            comm.Parameters.AddWithValue("_Email",Email);
            comm.Parameters.AddWithValue("_acao",MySqlDbType.Int32).Value=1;  // quando for 1 faz insert

            comm.ExecuteNonQuery(); // grava atributos no banco
            mensagem = "Cadastro Realizado";
            // retorna o auto_incremento
            comm = new MySqlCommand("select max(id) from cadastro",Banco.Abrir());  //retorna o ultimo id da tabela
            id = (int)comm.ExecuteScalar(); // id vai receber
        }

        public void Alterar ()
        {
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = Banco.Abrir();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_InsertUpdate";
            // paramentros como está no banco de dados
            comm.Parameters.AddWithValue("_id", this.ID);
            comm.Parameters.AddWithValue("_Nome", Nome);
            comm.Parameters.AddWithValue("_Email", Email);
            comm.Parameters.AddWithValue("_acao", MySqlDbType.Int32).Value = 2;  // quando for 1 faz in
            comm.ExecuteNonQuery(); //
            mensagem = "Registro Alterado com Sucesso"; 
        }

        //metodo consultar
        public void Consultar(int _ID) 
        {
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = Banco.Abrir();
            comm.CommandText = "select * from cadastro where id =" + _ID ;
            MySqlDataReader dr = comm.ExecuteReader();

            // hasrows , teve retorno ou nao
            if (!dr.HasRows) // se não houve linhas de retorno
            {
                mensagem = "Registro não encotrado";
                achou = false;
                return;    
            }
            else
            {
                achou = true;
                while (dr.Read())
                {
                    ID = dr.GetInt32(0); // trazendo do banco de dados PRIMEIRA COLUNA é 0
                    Nome = dr.GetString(1);
                    Email = dr.GetString(2);
                }
            }
        }
    }
}
