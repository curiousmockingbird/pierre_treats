using System.ComponentModel.DataAnnotations;
using System;

namespace PierresBakery.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "User Name")]
		public string UserName {get; set;}
    
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email {get; set;}

		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Full Name")]
		public string FullName { get; set; }

		[Required]
		[Display(Name = "Last time by Pierre's")]
		[DataType(DataType.Date)]
		public DateTime LastTimeVisiting { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password {get; set;}

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword {get; set;}
	}
}