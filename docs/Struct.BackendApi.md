# Copy [ConnectionStrings] trong Data Layer

# Đăng ký SQL 
- Đăng ký
    <!-- 
        services.AddDbContext<EShopDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString));
        }); 
    -->
# Đăng ký serivce cho tầng [Application]
- Đăng ký
    <!-- services.AddTransient<IProductService, ProductService>(); -->

# Cài đặt và sử dụng Swagger
- Cài Package
    <!-- Swashbuckle.AspNetCore -->

- Add service
    <!-- 
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Movies Demo", Version = "v1" });
        }); 
    -->

- Add pipeline
    <!-- 
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Movies Demo V1");
        }); 
    -->

- Nếu ta muốn chạy một cái là nó vào ngay đường dẫn của [Swagger] thì ta add [*] vào [Properties/lauchSettings.json]
    <!-- "launchUrl": "swagger", -->