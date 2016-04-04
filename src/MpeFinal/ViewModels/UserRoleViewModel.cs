using Microsoft.AspNet.Identity.EntityFramework;
using MpeFinal.Models;


namespace MpeFinal.ViewModels
{
	public class UserRoleViewModel
	{
		public ApplicationUser User { get; set; }

		public IdentityRole[] Roles { get; set; }
	}
}

