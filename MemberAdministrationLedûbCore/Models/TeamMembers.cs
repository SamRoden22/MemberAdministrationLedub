using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberAdministrationLedûbCore.Models
{
    public class TeamMembers
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int MemberId { get; set; }
    }
}
