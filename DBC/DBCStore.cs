using System;
using System.Reflection;
using System.Collections.Generic;
using CobolWow.Tools.Config;

namespace CobolWow.DBC
{
   public class DBCStore<T> : List<T> where T : struct
    {
        public DBCReader<T> Reader;
        public DBCStore()
            : base()
        {
            Reader = new DBCReader<T>(buildURL());
            AddRange(Reader.Records);
        }

        private string buildURL()
        {
            return ConfigManager.DBC_LOCATION + "/" + getDBCFileName() + ".dbc";
        }

        public string getDBCFileName()
        {
            MemberInfo info = typeof(T);
            object[] attributes = info.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i] is DBCFileAttribute)
                {
                    return (attributes[i] as DBCFileAttribute).DBCName;
                }
            }

            throw new Exception("Couldn't find DBCFile Attribute");
        }
    }
}
