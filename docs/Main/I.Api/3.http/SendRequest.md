# Mỗi Request cần có
- Tạo đối tượng  [HttpClient] từ [IHttpClientFactory]
- Set [BaseAddress] cho [HttpClient]
    - Tạo [HttpContent] để truyền vào phương thức nếu là [Post], [Put], [Path]
    - Có thể Chèn [Token] vào Header
        <!-- var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token"); -->
        <!-- client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions); -->    [1]
- Gửi Request

# Chú thích:
- [_httpContextAccessor] là đối tượng của [IHttpContextAccessor] vì nếu ta chỉ có thể lấy [Session] thông qua [HttpContext] mà phải ở Project khác [MVC], nếu ở [MVC] thì dùng [HttpContext] trực tiếp
- [1] là add vào [Header] một Token