# Cấu trúc Solution
- Cấu trúc theo tedu
    [0.eShopSolution]
        [1.eShopSolution.WebApp]
        [2.eShopSolution.Data] 
        [3.eShopSolution.Application]  
        [4.eShopSolution.ViewModels] 
        [5.eShopSolution.Utilities]
        [6.eShopSolution.BackendApi]
        [7.eShopSolution.AdminApp]

# 1.WebApp

# ---------------------------------------------------------------------------
# 2.Data
- [2.4.1] [*]: Fluent API
    <!-- https://www.learnentityframeworkcore.com/configuration/fluent-api -->
- [2.5.1] là file [Init-Data]

# ---------------------------------------------------------------------------
# 3.Application
- Các [Folder] tầng [Application] và tầng [ViewModels] giống nhau
    - [3.1] là Folder phân chia theo [Module]
    - [3.1.1] là một folder [Entity]
    - [3.1.1.1] trong mỗi [3.1.1] có 2 file, 1 file là [Interface], một file là [Implement-Interface] đó. 
- [3.2] chứa các [Service] chung cho tất cả các [Module] có thể dùng chung như: [Save-File], [Delete-File]

# ---------------------------------------------------------------------------
# 4.ViewModels
- Các [Folder] tầng [Application] và tầng [ViewModels] giống nhau
- Các thư mục có chức năng tương tự tầng [Application]
- Nhưng tầng [ViewModels] chỉ chứa các [DTO] [Request] [Response] cho [Application] chuyển và nhận đến và từ tầng [Application] với các tầng khác

- [4.2] chứa các [DTO] dùng chung như: [PagedResult], [API-Result]



# ---------------------------------------------------------------------------
# 5.Utilities
- Chứa các code dùng chun cho các Project như: các biến [Constants], các [Custom-Exceptions]

# ---------------------------------------------------------------------------
# 6.BackendAPI
- Tầng này thực hiện tạo các [API] để có thể cho các [Application] khác thao tác dữ liệu tới
- Xem thêm (Struct.BackendApi.md)

# ---------------------------------------------------------------------------
# 7.AdminApp
- Là [Project-MVC] chứa code phần [Admin]. Ta có thể tạo  phần [Admin] trong  [Area] có một [Project] chung với [Client]

# ---------------------------------------------------------------------------
# Quy trình tạo DB basic [2.4.1]
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
- Tạo một file mở rộng để thực hiện [Init-data]
- Thực hiện Insert bằng phương thức [HasData(*)]
    - [*] là danh sách các [Object]
- Phải có ID, không giống như Insert bằng EF

    <!-- 
        modelBuilder.Entity<AppConfig>().HasData(
            new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
            new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" }
        );
        modelBuilder.Entity<Language>().HasData(
            new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
            new Language() { Id = "en", Name = "English", IsDefault = false }
        ); 
    -->
- Sau đó dùng trong [OnModelCreating]
    <!-- modelBuilder.Seed(); -->
- Thực hiện migration, update database

# ---------------------------------------------------------------------------
# Tạo Identity  
- Lớp DbContext kế thừa [IdentityDbContext] thay vỉ [DbContext]
    <!-- public class EShopDbContext : IdentityDbContext<AppUser, AppRole, Guid> -->
- Tạo lớp [AppUser] kế thừa từ lớp [IdentityUser] để có thêm thuộc tính cần thiết
    <!-- 
        public class AppUser : IdentityUser<Guid>
        {
            public string FirstName { get; set; }
        } 
    -->
- Tạo lớp [AppRole] kế thừa từ lớp [IdentityRole] để có thêm thuộc tính cần thiết
    <!-- 
        public class AppRole : IdentityRole<Guid>
        {
            public string Description { get; set; }
        } 
    -->
- Và tạo [Config] cho [AppUser,AppRole] như các file Config trước
- Còn những [Class] khác của [Identity] thì ta [Config] trực tiếp trong [OnModelCreating]
    - Những lớp có class kế thừa
        <!-- modelBuilder.ApplyConfiguration(new AppUserConfiguration()); -->
    - Nhưng lớp không có class kế thừa
        <!-- modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims"); -->

- Có thể tạo thêm Seed data cho Identity

# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------
# ---------------------------------------------------------------------------
# Temp
## Layer Data
- Version 1
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
            [2.5.Extensions]
                <!-- ModelBuilderExtension.cs -->   [2.5.1]
            [*2.6*Repository]
        [3.eShopSolution.Application]
            [3.1.Catalog]
                [3.1.1.Product]
                    <!-- IProductService.cs --> [3.1.1.1]
                    <!-- ProductService.cs -->
                [3.1.2.Categories]
                    <!-- ICategoryService.cs -->
                    <!-- CategoryService.cs -->
            [3.2.Common]
                <!-- IFileStorageService.cs -->
                <!-- FileStorageService.cs -->
        [4.eShopSolution.ViewModels]
            [4.1.Catalog]
                [4.1.1.Product]
                    <!-- ProductCreateRequest.cs --> [4.1.1.1]
                    <!-- ProductUpdateRequest.cs -->
                [4.1.2.Categories]
                    <!-- CategoryCreateRequest.cs -->
                    <!-- CategoryUpdateRequest.cs -->
            [4.2.Common]
                <!-- PageRequestBase.cs -->
                <!-- ResultBase.cs -->
                <!-- ErrorResult.cs -->
                <!-- SuccessResult.cs -->
        [5.eShopSolution.Utilities]   
            [5.1.Constants]
            [5.2.Exceptions]

- Tầng [3], [4] có thể không cần chia thành [Module] mà tạo trực tiếp Folder theo từng [Entity] hoặc trực tiếp file
- Chú ý:    
    - [3.Common] chứa các [Service] tiện ích cho [3]
    - [4.Common] chứa các [Khung-dữ-liệu] để giúp thêm thông tin vào [DTO]
- Tầng [5.Utilities] giống như [Common] chung cho tất cả các [Project]