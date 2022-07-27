using AspWebsite.Datas;
using AspWebsite.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(option =>
    option.AddPolicy(name: "cors", builder => 
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    ));

// Add services to the container.
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<CRUDService<TodoModel>>();

// JWT, see https://blog.miniasp.com/post/2022/02/13/How-to-use-JWT-token-based-auth-in-aspnet-core-60
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
        options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

        options.TokenValidationParameters = new TokenValidationParameters
        {
            // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

            // 一般我們都會驗證 Issuer
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),

            // 通常不太需要驗證 Audience
            ValidateAudience = false,
            //ValidAudience = "JwtAuthDemo", // 不驗證就不需要填寫

            // 一般我們都會驗證 Token 的有效期間
            ValidateLifetime = true,

            // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
            ValidateIssuerSigningKey = false,

            // "1234567890123456" 應該從 IConfiguration 取得
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey")))
        };
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Authorize JWT in Swagger
    // https://stackoverflow.com/questions/58179180/jwt-authentication-and-swagger-with-net-core-3-0
    OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
    {
        Name = "Bearer",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Description = "把你拿到的 token 放這裡！",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    };
    c.AddSecurityDefinition("jwt_auth", securityDefinition);

    // Make sure swagger UI requires a Bearer token specified
    OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference()
        {
            Id = "jwt_auth",
            Type = ReferenceType.SecurityScheme
        }
    };
    OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement() { { securityScheme, new string[] { } }, };
    c.AddSecurityRequirement(securityRequirements);

    // Swagger XML Comment support
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
if(true) // demo
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
