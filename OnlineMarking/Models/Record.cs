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
        public int studentID { get; set; }
        public int teacherID { get; set; }
        public string filePath { get; set; }
        public int marks { get; set; }
    }
 
    /*public static class MyExtensionMethods
    {
        public static Record FindNumber(this IEnumerable<Record> record, int id)
        {
            return (from r in record where r.ID == id select r).First();
        }
    }*/
}