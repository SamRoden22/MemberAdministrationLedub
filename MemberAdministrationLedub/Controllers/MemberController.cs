﻿using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MemberAdministrationLedub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IHubContext<HubClass> _hubContext;

        public MemberController(IMemberService memberService, IHubContext<HubClass> hubContext)
        {
            _memberService = memberService;
            _hubContext = hubContext;
        }

        // GET: api/<MemberController>
        [HttpGet]
        public List<Member> Get()
        {
            var members = _memberService.GetAll();
            return members;
        }

        // GET api/<MemberController>/5
        [HttpGet("{id}")]
        public Member Get(int id)
        {
            var member = _memberService.Get(id);
            return member;
        }

        // POST api/<MemberController>
        [HttpPost]
        public Member Post([FromBody] Member value)
        {
            var member = _memberService.Create(value);
            return member;
        }

        // PUT api/<MemberController>/5
        [HttpPut("{id}")]
        public ActionResult<Member> Put(int id, [FromBody] Member value)
        {
            var member = _memberService.Update(id, value);
            return member;
        }

        // DELETE api/<MemberController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Member>> Delete(int id)
        {
            var member = _memberService.Delete(id);
            await _hubContext.Clients.All.SendAsync("MemberDeleted", id.ToString());
            return Ok(member);
        }
    }
}
