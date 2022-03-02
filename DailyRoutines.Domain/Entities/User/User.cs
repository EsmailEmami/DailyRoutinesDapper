using System;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Entities;
using DailyRoutines.Domain.Entities.Routine;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyRoutines.Domain.Entities.User
{
    public class User 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LastName { get; set; }

        [Display(Name = "شماره تلفن")]
        [MaxLength(12, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Email { get; set; }

        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Required]
        public bool IsBlock { get; set; } = false;

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        #region Relations

        public ICollection<Category> UserCategories { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}
