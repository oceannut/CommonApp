﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ThinkInBio.MySQL
{

    /// <summary>
    /// 
    /// </summary>
    public static class DbTemplate
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="commandBuilder"></param>
        /// <returns></returns>
        public static bool Save(string dataSource,
            Action<IDbCommand> commandBuilder)
        {
            return Save(dataSource, commandBuilder, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="commandBuilder"></param>
        /// <param name="autoIncrementIdAccessor"></param>
        /// <returns></returns>
        public static bool Save(string dataSource, 
            Action<IDbCommand> commandBuilder, 
            Action<long> autoIncrementIdAccessor)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            int rowsAffected = 0;
            using (IDbConnection connection = DbFactory.CreateConnection(dataSource))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    if (commandBuilder != null)
                    {
                        commandBuilder(command);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    if (autoIncrementIdAccessor != null && rowsAffected > 0)
                    {
                        command.CommandText = "select LAST_INSERT_ID()";
                        autoIncrementIdAccessor(Convert.ToInt64(command.ExecuteScalar()));
                    }
                }
            }
            return rowsAffected > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="commandBuilder"></param>
        /// <returns></returns>
        public static bool UpdateOrDelete(string dataSource, 
            Action<IDbCommand> commandBuilder)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            int rowsAffected = 0;
            using (IDbConnection connection = DbFactory.CreateConnection(dataSource))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    if (commandBuilder != null)
                    {
                        commandBuilder(command);
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            return rowsAffected > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="commandBuilder"></param>
        /// <param name="populate"></param>
        /// <returns></returns>
        public static T Get<T>(string dataSource, 
            Action<IDbCommand> commandBuilder, 
            Func<IDataReader, T> populate)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            T entity = default(T);
            using (IDbConnection connection = DbFactory.CreateConnection(dataSource))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    if (commandBuilder != null)
                    {
                        commandBuilder(command);
                        if (populate != null)
                        {
                            using (IDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    entity = populate(reader);
                                }
                            }
                        }
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="commandBuilder"></param>
        /// <returns></returns>
        public static int GetCount(string dataSource, 
            Action<IDbCommand> commandBuilder)
        {
            return GetCount(dataSource, commandBuilder, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="commandBuilder"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int GetCount(string dataSource,
            Action<IDbCommand> commandBuilder,
            ICollection<KeyValuePair<string, object>> parameters)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            int count = 0;
            using (IDbConnection connection = DbFactory.CreateConnection(dataSource))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    if (commandBuilder != null)
                    {
                        commandBuilder(command);
                        if (parameters != null && parameters.Count > 0)
                        {
                            foreach (KeyValuePair<string, object> item in parameters)
                            {
                                command.Parameters.Add(DbFactory.CreateParameter(item.Key, item.Value));
                            }
                        }
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="commandBuilder"></param>
        /// <param name="populate"></param>
        /// <returns></returns>
        public static IList<T> GetList<T>(string dataSource,
            Action<IDbCommand> commandBuilder,
            Func<IDataReader, T> populate)
        {
            return GetList<T>(dataSource, commandBuilder, null, populate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="commandBuilder"></param>
        /// <param name="parameters"></param>
        /// <param name="populate"></param>
        /// <returns></returns>
        public static IList<T> GetList<T>(string dataSource,
            Action<IDbCommand> commandBuilder,
            ICollection<KeyValuePair<string, object>> parameters,
            Func<IDataReader, T> populate)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            List<T> list = new List<T>();
            using (IDbConnection connection = DbFactory.CreateConnection(dataSource))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    if (commandBuilder != null)
                    {
                        commandBuilder(command);
                        if (parameters != null && parameters.Count > 0)
                        {
                            foreach (KeyValuePair<string, object> item in parameters)
                            {
                                command.Parameters.Add(DbFactory.CreateParameter(item.Key, item.Value));
                            }
                        }
                        if (populate != null)
                        {
                            using (IDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    list.Add(populate(reader));
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

    }
}
