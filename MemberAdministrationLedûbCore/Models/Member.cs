using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MemberAdministrationLedûbCore.Models.Enums;

namespace MemberAdministrationLedûbCore.Models
{
    public class Member
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public List<Team> Teams { get; set; }
    }
}
