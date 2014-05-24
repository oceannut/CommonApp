using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ThinkInBio.MySQL
{
    public static class DbTemplate
    {

        public static void Save(string dataSource,
            Action<IDbCommand> commandBuilder)
        {
            Save(dataSource, commandBuilder, null);
        }

        public static void Save(string dataSource, 
            Action<IDbCommand> commandBuilder, 
            Action<long> autoIncrementIdAccessor)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            using (IDbConnection connection = DbFactory.CreateConnection(dataSource))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    if (commandBuilder != null)
                    {
                        commandBuilder(command);
                    }
                    if (autoIncrementIdAccessor != null)
                    {
                        if (command.ExecuteNonQuery() == 1)
                        {
                            command.CommandText = "select LAST_INSERT_ID()";
                            autoIncrementIdAccessor(Convert.ToInt64(command.ExecuteScalar()));
                        }
                    }
                }
            }
        }

    }
}
