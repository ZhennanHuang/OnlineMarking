using OnlineMarking.Models;
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

        
    }
    public static class MyExtensionMethods
    {
        public static Record[] FindByName(this IEnumerable<Record> record, string sName)
        {
            if (sName != null)
                return (from r in record where r.studentName.Equals(sName) select r).ToArray();
            return null;
        }
        public static Record FindByID(this IEnumerable<Record> record, int id)
        {
            if(id != 0)
                return (from r in record where (r.ID == id) select r).First();
            return null;
        }
    }
}