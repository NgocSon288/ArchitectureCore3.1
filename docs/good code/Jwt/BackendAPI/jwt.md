# Inject các đối tượng Identity cần dùng
    services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<EShopDbContext>()
        .AddDefaultTokenProviders();
    services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
    services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
    services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();

# Xây dụng [Authencate] chức năng [login]
- Tìm ra User
    <!-- var user = await _userManager.FindByNameAsync(request.UserName); -->

- Kiểm tra có đúng [Password]
    <!-- var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true); -->

- Nếu dùng thì lấy ra các [roles] của User
    <!-- var roles = await _userManager.GetRolesAsync(user); -->

- Tạo ra danh sách các [Claim] để trả về client
    <!-- 
        var claims = new[]
        {
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.GivenName,user.FirstName),
            new Claim(ClaimTypes.Role, string.Join(";",roles)),
            new Claim(ClaimTypes.Name, request.UserName)
        }; 
    -->

- Cần tạo ra key cho [Jwt]
    "Tokens": {
        "Key": "0123456789ABCDEF",
        "Issuer": "https://webapi.tedu.com.vn"
    }

- Tạo ra [Token] từ [Claim] và [key] đó
    <!-- 
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Tokens:Issuer"],
            _config["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds); 
    -->

- Tạo ra chuổi [Token] bằng hàm [*]
    <!-- new JwtSecurityTokenHandler().WriteToken(token) -->