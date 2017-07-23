using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployInformationSystem.Models
{
    public class Education
    {
        #region Property
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Degree { get; set; }
        public string Percentage { get; set; }

        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public ApplicationInfo ApplicationInfo { get; set; }

        //Check for changed from database or not
        [NotMapped]
        public bool IsChanged { get; set; } = false;
        #endregion
    }
}