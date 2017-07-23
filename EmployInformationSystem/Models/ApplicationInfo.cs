using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployInformationSystem.Models
{
    public class CheckBoxModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class ApplicationInfo
    {
        #region Property
        [Key]
        public int Id { get; set; }
        public string ApplyFor { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Sign { get; set; }
        public DateTime SignDate { get; set; }
        public string Desired { get; set; }
        [NotMapped]
        public List<CheckBoxModel> DesiredChkItems { get; set; }
        public string Note { get; set; }
        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<Employment> Employments { get; set; }
        #endregion
    }
}