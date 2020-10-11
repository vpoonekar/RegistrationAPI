using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace RegistrationAPI.Models
{
    public class DBContext
    {
        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; }
        public DBContext(string connectionString)
        {
            this.ConnectionString = connectionString;
            Connection = new MySqlConnection(connectionString);
        }

        public async Task<int> InsertToTableAsync(params DBParams[] param)
        {

            int i =0;
            var t = Task.Run(() => { i = 1; return i;});
            t.Wait();
            return t.Result;
        }

        public async Task<int> ExecureSTPAsync(string _procName, params DBParams[] _param)
        {

            int i = 0;
            var t = Task.Run(() => { i = 1; return i; });
            t.Wait();
            return t.Result;
        }

        public void Dispose() => Connection.Dispose();
    }
}
