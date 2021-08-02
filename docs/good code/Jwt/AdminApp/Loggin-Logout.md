# Logout
- Sử  dụng [CookieAuthenticationDefaults.AuthenticationScheme]  để Login cơ bản
    <!-- await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  -->

#  Login
- [1] Gọi [API] để truy xuất xuống [API-Server]  và lấy về  một [Object], trong đó có một chuổi [Token]  
- [2] [result.ResultObj] là một chuổi [Token], Sử  dụng [Authenticate] để chuyển từ  chuổi [Token] thành [ClaimsPrincipal]
- [3] [IsPersistent=false] tức là không lưu trữ [Login]
- [4] Tạo [Cookie-Session] cho [ClaimsPrincipal]
- [5] Login  với  [ClaimsPrincipal] đó

    <!-- 
        var result = await _userApiClient.Authenticate(request);    // 1
        if (result.ResultObj == null)
        {
            ModelState.AddModelError("", result.Message);
            return View();
        }
        var userPrincipal = this.ValidateToken(result.ResultObj);   // 2
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            IsPersistent = false    //  3
        };  // 4
        HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
        HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
        await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    authProperties);    // 5
    -->