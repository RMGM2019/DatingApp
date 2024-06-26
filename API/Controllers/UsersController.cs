﻿using System.Security.Claims;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
{
    [Authorize]
    public class UsersController : BaseAPIController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();           

            return Ok(users);
            
        }

        [HttpGet("{username}")] // /api/usres/2
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
           return await _userRepository.GetMemberAsync(username);
           
        }

        [HttpPut]
             public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto){
                var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userRepository.GetUserByUsernameAsync(username);

                if(user== null) return NotFound();

                _mapper.Map(memberUpdateDto, user);

                if(await _userRepository.SaveAllAsync()) return NoContent();

                return BadRequest("Fail to update user");
             }
    }
}

