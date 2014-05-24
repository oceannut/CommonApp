using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data.MySqlClient;

namespace ThinkInBio.MySQL
{

    public static class DbFactory
    {

        public static IDbConnection CreateConnection(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            return new MySqlConnection(dataSource);
        }

        public static IDbDataParameter CreateParameter<T>(string name, T value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }
            return new MySqlParameter(name, value);
        }

    }

}
