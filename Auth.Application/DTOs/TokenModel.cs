﻿
namespace Auth.Application.DTOs;

public class TokenModel
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
