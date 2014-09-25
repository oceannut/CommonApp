using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ThinkInBio.Common.Data;
using ThinkInBio.MySQL;
using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;

namespace ThinkInBio.CommonApp.MySQL
{
    public class FileTransferLogDao : GenericDao<FileTransferLog>, IFileTransferLogDao
    {

        private string dataSource;

        public FileTransferLogDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public override bool Save(FileTransferLog entity)
        {
            return DbTemplate.Save(dataSource,
                (command) =>
                {
                    command.CommandText = @"insert into cyFileTransferLog (id,direction,title,path,size,user,creation) 
                                                    values (NULL,@direction,@title,@path,@size,@user,@creation)";
                    command.Parameters.Add(DbFactory.CreateParameter("direction", entity.Direction));
                    command.Parameters.Add(DbFactory.CreateParameter("title", entity.Title));
                    command.Parameters.Add(DbFactory.CreateParameter("path", entity.Path));
                    command.Parameters.Add(DbFactory.CreateParameter("size", entity.Size));
                    command.Parameters.Add(DbFactory.CreateParameter("user", entity.User));
                    command.Parameters.Add(DbFactory.CreateParameter("creation", entity.Creation));
                },
                (id) =>
                {
                    entity.Id = id;
                });
        }

        public override void Save(ICollection<FileTransferLog> col)
        {
            DbTemplate.Save(dataSource,
                (command) =>
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.Append("insert into cyFileTransferLog (id,direction,title,path,size,user,creation) values ");
                    for (int i = 0; i < col.Count; i++)
                    {
                        FileTransferLog entity = col.ElementAt(i);
                        buffer.Append("(NULL,")
                            .Append("@direction").Append(i).Append(",")
                            .Append("@title").Append(i).Append(",")
                            .Append("@path").Append(i).Append(",")
                            .Append("@size").Append(i).Append(",")
                            .Append("@user").Append(i).Append(",")
                            .Append("@creation").Append(i).Append("),");
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("direction{0}", i), entity.Direction));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("title{0}", i), entity.Title));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("path{0}", i), entity.Path));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("size{0}", i), entity.Size));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("user{0}", i), entity.User));
                        command.Parameters.Add(DbFactory.CreateParameter(string.Format("creation{0}", i), entity.Creation));
                    }
                    buffer.Length = buffer.Length - 1;
                    command.CommandText = buffer.ToString();
                });
        }

    }
}
