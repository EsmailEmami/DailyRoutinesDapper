using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyRoutines.Domain.Entities.Routine
{
    public class Category 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required] 
        public bool IsDelete { get; set; } = false;

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;

        #region Relations

        public User.User User { get; set; }
        public ICollection<Action> Actions { get; set; }

        #endregion
    }
}