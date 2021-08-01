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