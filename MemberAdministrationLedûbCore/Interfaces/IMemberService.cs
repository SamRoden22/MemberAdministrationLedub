using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberAdministrationLedûbCore.Models;

namespace MemberAdministrationLedûbCore.Interfaces
{
    public interface IMemberService
    {
        public List<Member> GetAll();

        public Member Get(int id);

        public Member Create(Member member);

        public Member Update(int id, Member member);

        public Member Delete(int id);
    }
}
