using System;
using api.Dtos;
using api.Models;

namespace api.Services
{
    public interface ITokenService
    {
        string GenerateToken(LoginDto login);
    }
}