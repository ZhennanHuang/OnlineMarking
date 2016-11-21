using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineMarking.Models
{
    public class Record
    {
        public int ID { get; set; }
        public string studentName { get; set; }
        public string filePath { get; set; }
        public string fileName { get; set; }
        public string marks { get; set; }
        public string feedback { get; set; }

        public static implicit operator string(Record v)
        {
            throw new NotImplementedException();
        }
    }

    public static class MyExtensionMethods
    {
        public static Record[] FindByName(this IEnumerable<Record> record, string sName)
        {
            return ((from r in record where r.studentName.Equals(sName) select r).ToArray());
        }
    }
}