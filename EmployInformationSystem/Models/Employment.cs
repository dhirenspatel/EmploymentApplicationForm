using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployInformationSystem.Models
{
    public class Employment
    {
        #region Property
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Designation { get; set; }
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public ApplicationInfo ApplicationInfo { get; set; }

        [NotMapped]
        public bool IsChanged { get; set; } = false;
        #endregion
    }
}