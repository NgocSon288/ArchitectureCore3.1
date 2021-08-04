# Fluent Validation
- Dùng Fluent Validation  để tạo [Validate] cho các lớp [ViewModels]. Các bước

1. Install Package
    <!-- FluentValidation.AspNetCore -->

2. Add vào [Controllers](Ở .net cũ thì add vào mvc). Đăng ký tất cả các File kế  thừa từ [AbstractValidator] cùng [Project] với [LoginRequestValidator]
    <!-- 
        services.AddControllers(setup => {
            ...mvc setup...
        }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
    -->
3. Tạo lớp [PersonValidatior]  kế thừa từ lớp [AbstractValidator<Person>] để có thể thực hiện [Validate]. VD:
    <!--  
        public class Person {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
        }

        public class PersonValidator : AbstractValidator<Person> {
            public PersonValidator() {
                RuleFor(x => x.Id).NotNull().WithMessage("Không được bỏ trông nha!");
                RuleFor(x => x.Name).Length(0, 10);
                RuleFor(x => x.Email).EmailAddress().Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                RuleFor(x => x.Age).InclusiveBetween(18, 60);
            }
        }
    -->

4. Dùng như dùng [Annotation]
    <!-- 
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        } 
    -->