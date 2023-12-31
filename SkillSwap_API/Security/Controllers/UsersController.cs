﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillSwap_API.Security.Authorization.Attributes;
using SkillSwap_API.Security.Domain.Models;
using SkillSwap_API.Security.Domain.Services;
using SkillSwap_API.Security.Domain.Services.Communication;
using SkillSwap_API.Security.Resources;

namespace SkillSwap_API.Security.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Microsoft.AspNetCore.Authorization.AllowAnonymous] 
public class UsersController : ControllerBase
{
        private readonly IUserService _userService; 
        private readonly IMapper _mapper; 
        
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        
        [HttpPost("sign-in")]
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var response = await _userService.Authenticate(request); 
            return response; 
        }

        
        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _userService.RegisterAsync(request); 
            return Ok(new { message = "Registration successful" }); 
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.ListAsync(); 
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users); 
            return Ok(resources); 
        }

        [AuthorizeAdmin]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id); 
            var resource = _mapper.Map<User, UserResource>(user); 
            return Ok(resource); 
        }

        
        [AuthorizeAdmin] 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRequest request)
        {
            await _userService.UpdateAsync(id, request); 
            return Ok(new { message = "User updated successfully" }); 
        }

        
        [AuthorizeAdmin] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id); 
            return Ok(new { message = "User deleted successfully" }); 
        }
}

