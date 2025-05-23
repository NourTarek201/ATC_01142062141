﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookingSystem.DTOs;
using BookingSystem.Models;
using BookingSystem.services;
using BookingSystem.Models.PlaceManagement;
using BookingSystem.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using BookingSystem.services.Authentication;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenController : ControllerBase
    {
        private readonly AuthenService authenService;

        public AuthenController( AuthenService authenService)
        {
            this.authenService = authenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterationDTO user)
        {
            if (ValidationService.isEmptyDTO(user)){
                return BadRequest("fill required fields to proceed");
            }
            var token = await authenService.Registeration(user, "Customer");
            return Ok(new { token });
        }
        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterationDTO user)
        {
            if (ValidationService.isEmptyDTO(user))
            {
                return BadRequest("fill required fields to proceed");
            }
            var token = await authenService.Registeration(user, "Admin");
            return Ok(new { token });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            if (ValidationService.isEmptyDTO(user))
            {
                return BadRequest("fill required fields to proceed");
            }
            var token = await authenService.Signin(user);
            return Ok(new { token });
        }

    }
}
