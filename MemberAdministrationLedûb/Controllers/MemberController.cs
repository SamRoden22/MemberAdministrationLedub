using MemberAdministrationLedûbCore.Interfaces;
using MemberAdministrationLedûbCore.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MemberAdministrationLedûb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: api/<MemberController>
        [HttpGet]
        [Route("GetAllMembers")]
        public List<Member> Get()
        {
            var members = _memberService.GetAll();
            return members;
        }

        // GET api/<MemberController>/5
        [HttpGet]
        [Route("GetSpecificMember/{id}")]
        public Member Get(int id)
        {
            var member = _memberService.Get(id);
            return member;
        }

        // POST api/<MemberController>
        [HttpPost]
        [Route("PostMember")]
        public Member Post([FromBody] Member value)
        {
            var member = _memberService.Create(value);
            return member;
        }

        // PUT api/<MemberController>/5
        [HttpPut]
        [Route("PutMember/{id}")]
        public Member Put(int id, [FromBody] Member value)
        {
            //var existingMember = _memberService.Get(id);
            var member = _memberService.Update(id, value);
            return member;
        }

        // DELETE api/<MemberController>/5
        [HttpDelete]
        [Route("DeleteMember/{id}")]
        public Member Delete(int id)
        {
            var member = _memberService.Delete(id);
            return member;
        }
    }
}
