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

# Sử dụng các đối tượng của Identity
- Thựa hiện Inject 3 đối tượng chính [UserManager], [SignInManager], [RoleManager]
    <!-- 
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<EShopDbContext>()
            .AddDefaultTokenProviders();
        services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
        services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
        services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>(); 
    -->

# Sử dụng Jwt
- Coi trong [jwt.md]