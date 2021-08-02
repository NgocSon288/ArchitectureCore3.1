public class ABC
{
    public async Task<ApiResult<string>> Authencate(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
        if (!result.Succeeded)
        {
            return new ApiErrorResult<string>("Đăng nhập không đúng");
        }
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new[]
        {
        new Claim(ClaimTypes.Email,user.Email),
        new Claim(ClaimTypes.GivenName,user.FirstName),
        new Claim(ClaimTypes.Role, string.Join(";",roles)),
        new Claim(ClaimTypes.Name, request.UserName)
    };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Tokens:Issuer"],
            _config["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);

        return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public async Task<ApiResult<bool>> Register(RegisterRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user != null)
        {
            return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
        }
        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            return new ApiErrorResult<bool>("Emai đã tồn tại");
        }

        user = new AppUser()
        {
            Dob = request.Dob,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            return new ApiSuccessResult<bool>();
        }
        return new ApiErrorResult<bool>("Đăng ký không thành công");
    }
}