using System.ComponentModel.DataAnnotations;
using MemberAdministrationLedubCore.Models;

namespace MemberAdministrationLedub.ViewModels
{
    public class TeamViewModel
    {
        [Required]
        public Team Team { get; set; }
        [Required]
        public List<int> MemberIds { get; set; }
    }
}
