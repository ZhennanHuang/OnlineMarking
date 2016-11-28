using OnlineMarking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineMarking.Models
{
    public class Record
    {
        [Required]
        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        public string studentName { get; set; }
        [Required]
        public string filePath { get; set; }
        public string fileName { get; set; }
        public string marks { get; set; }
        public string feedback { get; set; }  
        public virtual ApplicationUser User { get; set; }
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