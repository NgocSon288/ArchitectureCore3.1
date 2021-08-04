# Config Authorize
- Config cho [Startup], để khi [User] truy cập vào các trang mà không có quyền hoặc chưa đăng nhập thì  [Redirect] tới.
    <!-- 
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/Index";
                options.AccessDeniedPath = "/User/Forbidden/";
            }); 
    -->

- Và Cần [UserAuthentication]