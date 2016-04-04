using System.ComponentModel.DataAnnotations;

namespace MpeFinal.ViewModels.Manage
{
  public class ChangePasswordViewModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "Geslo in potrditveno geslo se ne ujemata.")]
    public string ConfirmPassword { get; set; }
  }
}
