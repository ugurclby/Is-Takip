using System.ComponentModel.DataAnnotations;

namespace YSKProje.ToDo.Web.Models
{
    public class AppUserSignInModel
    {
        [Display(Name = "Kullanıcı Adı :")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
        public string UserName { get; set; }

        [Display(Name = "Parola :")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Parola Boş Geçilemez")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla :")]
        public bool RememberMe { get; set; }
    }
}
