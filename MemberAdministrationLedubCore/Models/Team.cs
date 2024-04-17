using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MemberAdministrationLedubCore.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Member> Members { get; set; } = new();
    }
}
