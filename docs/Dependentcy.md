# Install Package
## EF
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.Tools
## ConfigurationBuilder
    - Microsoft.Extensions.Configuration.FileExtensions

## Json
    - Microsoft.Extensions.Configuration.Json


# ----------------------------------------------------------------
# Link

## EF Core
- Tạo [Fluent-API] để config [Migration] thay vì dùng [Annotation]
    <!-- https://www.learnentityframeworkcore.com/configuration/fluent-api -->

### Relationship
- Tạo quan hệ 1 nhiều
    <!-- https://www.entityframeworktutorial.net/efcore/configure-one-to-many-relationship-using-fluent-api-in-ef-core.aspx -->  
- Tạo quan hệ nhiều nhiều
    <!-- https://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx -->

### Design-time DbContext Creation
- Inject Option, tạo DBContext
    <!-- https://docs.microsoft.com/vi-vn/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli --> 

### ConfigurationBuilder
- Dùng để nạp các setting từ file [appsettings.json]
    <!-- https://xuanthulab.net/dependency-injection-di-trong-c-voi-servicecollection.html -->

### Seeding Data
- Khởi tạo data
    <!-- https://docs.microsoft.com/vi-vn/ef/core/modeling/data-seeding -->