using campeonato.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace campeonato.Banco
{
    public class DB_Torneio
    {
        DB db;

        public string Error { get; set; }

        public bool conectar()
        {
            db = new DB();

            db.String_Connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jeanf\Desktop\Documents\DB_Torneio.mdf;Integrated Security=True;Connect Timeout=30";

            return db.Connect();
        }

        public List<CampeonatoModelo> getTime(string search)
        {
            conectar();

            string comando = "select * from Torneio";

            db.Execute(comando, true);

            List<CampeonatoModelo> retorno = new List<CampeonatoModelo>();

            CampeonatoModelo aux;

            while (db.Sobj_reader.Read())
            {
                aux = new CampeonatoModelo();
                aux.id = (int)db.Sobj_reader["Id"];
                aux.nome = db.Sobj_reader["nome"].ToString();
                aux.pontos = (int)db.Sobj_reader["pontos"];
                aux.chave = (int)db.Sobj_reader["chave"];

                retorno.Add(aux);
            }

            return retorno;
        }

        public DataTable get_Time(string search)
        {
            conectar();

            string comando = "select * from Torneio";

            db.Execute(comando, true);

            DataTable dt = new DataTable();

            dt.Load(db.Sobj_reader);

            return dt;
        }

        public bool InsertTime(CampeonatoModelo _user)
        {
            bool retorno = false;

            try
            {
                conectar();

                string comando = "insert into Torneio " +
                       " (nome,pontos,chave) " +
                       " values " +
                       " ('{0}','{1}','{2}')";

                comando = string.Format(comando, _user.nome,
                                                 _user.pontos,
                                                 _user.chave);

                retorno = db.Execute(comando, false);

            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return retorno;
        }

        public bool UpdateTime(CampeonatoModelo _user)
        {
            bool retorno = false;

            try
            {
                conectar();

                string comando = @"update Torneio set
                                     pontos = '{2}',
                                     chave = '{3}', nome = '{1}'
                               where Id = '{0}'";

                comando = string.Format(comando, _user.id,
                                                 _user.nome,
                                                 _user.pontos,
                                                 _user.chave);

                retorno = db.Execute(comando, false);
            }
            catch (Exception ex)
            {

                Error = ex.Message;
            }

            return retorno;
        }

        public bool DeleteTime(int id)
        {
            bool retorno = false;

            try
            {
                conectar();

                string comando = "delete Torneio where Id = '{0}'";

                comando = string.Format(comando, id);

                retorno = db.Execute(comando, false);

            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return retorno;
        }
    }
}