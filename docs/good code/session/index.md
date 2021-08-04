# Install Session
- Cài đặt Package
    <!-- Microsoft.AspNetCore.Session -->

- Config [Service]
    <!-- 
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        }); 
    -->

- Thêm vào [Pipeline]
    <!-- app.UseSession(); -->


# Sử dụng thông  qua [HttpContext]
- Sử dụng trong  [Controller]
    <!-- HttpContext.Session.SetString("Token", result.ResultObj); -->

- Nếu sử dụng ở các vị trí khác thì [Inject] và sử dụng đối tượng của [IHttpContextAccessor]
    <!-- _httpContextAccessor.HttpContext.Session.GetString("Token"); -->