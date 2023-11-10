using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sleekflow.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
