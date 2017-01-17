using System;

namespace CobolWow.DBC
{
   public class DBCFileAttribute : Attribute
    {
        public string DBCName { get; protected set; }
        public DBCFileAttribute(string dbcName)
        {
            DBCName = dbcName;
        }
    }
}
