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
        [Required]
        public string marks { get; set; }
        [StringLength(200, MinimumLength = 50)]
        public string feedback { get; set; }  
        public virtual ApplicationUser User { get; set; }
    }
    public static class MyExtensionMethods
    {
        public static Record[] FindByUserId(this IEnumerable<Record> record, string UserId)
        {
            if (UserId != null)
                return (from r in record where r.UserId.Equals(UserId) select r).ToArray();
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