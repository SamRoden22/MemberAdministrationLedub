using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubCore.Models;

namespace MemberAdministrationLedubCore.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public List<Member> GetAll()
        {
            return _memberRepository.GetAll();
        }

        public Member Get(int id)
        {
            return _memberRepository.Get(id);
        }

        public Member Create(Member member)
        {
            return _memberRepository.Create(member);
        }

        public Member Update(int id, Member member)
        {
            return _memberRepository.Update(id, member);
        }

        public Member Delete(int id)
        {
            return _memberRepository.Delete(id);
        }
    }
}
