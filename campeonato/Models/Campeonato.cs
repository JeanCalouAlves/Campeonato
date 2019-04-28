using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using campeonato.Banco;
using campeonato.Models;

namespace campeonato.Controllers
{
    public class Campeonato
    {
        public List<CampeonatoModelo> lst_camp = new List<CampeonatoModelo>();

        DB_Torneio db_time = new DB_Torneio();

        public Campeonato()
        {
            lst_camp = db_time.getTime("");
        }

        public bool InserirTime(CampeonatoModelo _time)
        {
            bool retorno = false;

            if (lst_camp.Where(p => p.id == _time.id).Count() > 0)
                return retorno;

            db_time.InsertTime(_time);
            retorno = true;

            return retorno;
        }
        public void DeletarTime(int id)
        {
            db_time.DeleteTime(id);
        }

        public void UpdateTime(CampeonatoModelo _time)
        {
            db_time.UpdateTime(_time);
        }
    }
}