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
    public class TimeStampDao : ITimeStampDao
    {

        private string dataSource;

        public TimeStampDao(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentNullException();
            }
            this.dataSource = dataSource;
        }

        public DateTime? Next()
        {
            return DbTemplate.Get<DateTime>(dataSource,
                (command) =>
                {
                    command.CommandText = @"select now()";
                },
                (reader) =>
                {
                    return reader.GetDateTime(0);
                });
        }

    }
}
