# Cấu trúc Solution
- Cấu trúc theo tedu
    [0.eShopSolution]
        [1.eShopSolution.WebApp]
        [2.eShopSolution.Data]
            [2.1.Entities]
                <!-- Product.cs -->
            [2.2.EF]
                <!-- EShopDbContext.cs -->
            [2.3.Enums]
            [2.4.Configurations]
                <!-- ProductConfiguration.cs --> [2.4.1]
        [3.eShopSolution.Application]

# 1.WebApp

# ---------------------------------------------------------------------------
# 2.Data
- [2.4.1] [*]: Fluent API
    <!-- https://www.learnentityframeworkcore.com/configuration/fluent-api -->

# ---------------------------------------------------------------------------
# 3.Application

# ---------------------------------------------------------------------------
# Quy trình tạo DB [2.4.1]
- Tạo các [Entities] đặt trong thư mục [2.1]
    - Nếu các dữ liệu dạng [Enum] thì tạo các enum trong thư mục [2.3]
- Tạo lớp [DbContext] trong [2.2]
    - Có [Constructor] nhận Inject [Options]
        <!-- public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options) -->
    - Khai báo các [DbSet]
        <!-- public DbSet<Product> Products { get; set; } -->

## Dùng [Fluent-API] thay cho [Atrribute] (Dependentcy)
- Tạo các file [Configuration] cho mỗi [Entity] tương ứng, và các class đó phải kế thừa từ [IEntityTypeConfiguration<Product>], và implement phương thức [Configure]
    <!-- 
        class ProductConfiguration : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
                builder.ToTable("AppConfigs");

                builder.HasKey(p => p.Key);

                builder.Property(p => p.Value)
                    .IsRequired(true);
            }
        } 
    -->

- Cần [Override] phương thức [OnModelCreating] trong [DbContext]
    - Ta có thể [Config] trực tiếp vào đối tượng [modelBuilder] 
        <!-- modelBuilder.Entity<Product>().Property(t => t.ProductDate).IsRequired(); -->
    - Nhưng khi nhiều [Entity] thì rất khó nhìn, nên tách file riêng, và khai báo lại trong phương thức [OnModelCreating]
        <!-- modelBuilder.ApplyConfiguration(new ProductConfiguration()); -->

## Tạo migration trong Project khác
- Nếu ta tạo DbContext trong cùng một project với [Application]
    - Nếu ta dùng các [Service] từ các [Application], thì ta có thể khai báo đăng ký nó ngay trong file [Startup]
        <!-- services.AddDbContext<AppDbContext>(options =>
            {  
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"));
            }); 
        -->
    - Vào lúc đó ta [Inject] vào sử dụng bình thường
        <!-- public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } -->

- Đôi khi ta tạo [DbContext] ở một [Project] khác, thì ta phải có tạo ra một lớp và [Implement] interface [IDesignTimeDbContextFactory<EShopDbContext>]. Nó có chức năng là mỗi khi trong project có bất cứ class nào mà [Implement] interface đó thì nó sẽ tự gọi phương thức CreateDbContext. Cách làm:
    1. Tạo file [appsettings.json] và chuổi [ConnectionStrings]
        "ConnectionStrings": {
            "eShopSolutionDb": "Server=NGOCSON\SQLEXPRESS01;Database=eShopSolution;Trusted_Connection=True;"
        }
    2. Tạo file và [Implement] interface [IDesignTimeDbContextFactory]
        - Ở đây ta dùng [ConfigurationBuilder] để nạp cấu hình từ file [appsettings.json]
        - Khi lấy được [ConnectionStrings] ta tạo [DbContextOptionsBuilder] và truyền vào [ConnectionString] lấy được và truyền vào builder, sau đó dùng builder làm tham số truyền vào đối tường [DbContext] thay vì [Inject] như ta dùng các ứng dụng [Application]
        <!-- 
            public class EShopDbContextFactory : IDesignTimeDbContextFactory<EShopDbContext>
            {
                public EShopDbContext CreateDbContext(string[] args)
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

                    var connectionString = configuration.GetConnectionString("eShopSolutionDb");

                    var optionsBuilder = new DbContextOptionsBuilder<EShopDbContext>();
                    optionsBuilder.UseSqlServer(connectionString);

                    return new EShopDbContext(optionsBuilder.Options);
                }
            } 
        -->
    3. Thực hiện migration để update databse

## Tạo database seeding


# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------