# IHttpClientFactory
- Cần đăng ký trong [services]
    <!-- services.AddHttpClient(); -->
- Là đối tượng giúp giao tiếp với [API] thông qua giao thức [HTTP]

# -----------------------------------------------------------

# IConfiguration
- Giúp lấy các thông tin từ file [appsettings.json]. File này ta có thể lưu các [Domain]

# -----------------------------------------------------------

# Tạo Request tới API
1. Chuyển đối tượng thành chuổi [Json], Và chuyển thành đối tượng [HttpContent]
    <!-- 
        var json = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); 
    -->

2. Tạo ra đối tượng [HttpClient] từ [IHttpClientFactory]. Và định nghĩa [URL]
    <!-- 
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_configuration["BaseAddress"]); 
    -->

3. Thực hiện gửi [Request] tới [API-Server]
    <!-- var response = await client.PostAsync("/api/users/authenticate", httpContent); -->

    - Response trả về tùy thuộc vào [Server]

4. [Convert] dữ liệu nhận được khi đã biết chính xác kiểu dữ liệu, bằng [JsonConvert.DeserializeObject]
    <!-- JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync()); -->

5. Sau khi tạo [Class] [Interface] thì thực hiện đăng ký [Services]
    <!-- services.AddTransient<IUserService, UserService>(); -->

## Giả sử
- Dữ liệu
    <!-- 
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var response = await client.PostAsync("/api/users/authenticate", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync());
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync());
        } 
    -->

# -----------------------------------------------------------
# Config [Port] 
- Để cho 2 [Server-API] và [Server-Web] chạy được thì ta phải [Config] lại [Port] cho 2 [Server] đó không bị đụng độ
- Vào [Properties/lauchSettings.json] sửa lại [port] cho khác nhau, và duy nhất là được
- Đặc  biệt khi ta chạy [Multiple-Project] thì 
    - Chọn [Properties] của [Solution]
    - Chọn [Multiple-Startup-Projects]
    - Chọn [Start] với các [Project] mình cần. Ở đây là [API-Project],  [Web-Project]

# -----------------------------------------------------------
# -----------------------------------------------------------
# -----------------------------------------------------------
