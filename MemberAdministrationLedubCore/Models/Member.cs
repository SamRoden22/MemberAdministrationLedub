﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MemberAdministrationLedubCore.Models.Enums;

namespace MemberAdministrationLedubCore.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        [JsonIgnore]
        public List<Team> Teams { get; set; } = new();
    }
}
