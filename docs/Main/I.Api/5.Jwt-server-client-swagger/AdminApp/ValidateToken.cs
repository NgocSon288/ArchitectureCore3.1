public class ABC 
{
    private ClaimsPrincipal ValidateToken(string jwtToken)
    {
        IdentityModelEventSource.ShowPII = true;

        SecurityToken validatedToken;
        TokenValidationParameters validationParameters = new TokenValidationParameters();

        validationParameters.ValidateLifetime = true;

        validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
        validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

        ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

        return principal;
    }
}

/* 
    - Hàm có chức năng nhận vào một Token và giải mã Token đó
    - Giaỉ mã với Key đã tạo như trong [appsettings.json] trong BackendAPI




*/