using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace campeonato.Banco
{
    public class DB
    {
        #region Declarations

        private SqlConnection Sobj_conn = null;
        private SqlCommand Sobj_cmd = null;
        private String Svar_StrConnection = "";

        public SqlDataReader Sobj_reader = null;
        private SqlDataAdapter Sobj_Adapter = null;

        #endregion
        #region Properties
        public String String_Connection
        {
            get { return Svar_StrConnection; }
            set { Svar_StrConnection = value; }
        }

        #endregion
        private void create_Connection()
        {
            if (Sobj_conn == null)
                Sobj_conn = new SqlConnection();
        }

        private void create_command(string sql)
        {
            Sobj_cmd = new SqlCommand();
            Sobj_cmd.CommandType = CommandType.Text;
            Sobj_cmd.CommandText = sql;
            Sobj_cmd.Connection = Sobj_conn;
        }

        public bool Connect()
        {          
            create_Connection();          
            if (Sobj_conn.State == ConnectionState.Broken || Sobj_conn.State == ConnectionState.Closed)
            {
                Sobj_conn.ConnectionString = Svar_StrConnection;
                Sobj_conn.Open();
            }
            if (Sobj_conn.State != ConnectionState.Broken && Sobj_conn.State != ConnectionState.Closed)
                return true;
            return false;
        }

        public bool Execute(string command, bool select)
        {
            try
            {
                create_command(command);
                if (select)
                {
                    Sobj_reader = Sobj_cmd.ExecuteReader();
                    return true;
                }
                else
                {
                    return (Sobj_cmd.ExecuteNonQuery() > 0);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Get_Data(DataSet ds, string tablename)
        {
            create_DataAdapter();
            Sobj_Adapter.Fill(ds);
            ds.Tables[ds.Tables.Count - 1].TableName = tablename;
            return true;
        }

        private bool create_DataAdapter()
        {
            Sobj_Adapter = new SqlDataAdapter();
            return true;
        }
    }
}