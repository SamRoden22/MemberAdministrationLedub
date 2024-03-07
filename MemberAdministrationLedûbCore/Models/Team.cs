using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MemberAdministrationLedûbCore.Models
{
    public class Team
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Member> Members { get; set; }
    }
}
