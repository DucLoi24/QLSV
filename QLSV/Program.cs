using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); // Tạo một đối tượng WebApplicationBuilder để cấu hình ứng dụng web

// Thêm các dịch vụ cần thiết vào container DI (Dependency Injection)

// Cho phép truy cập từ các nguồn khác nhau (CORS)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // Cho phép truy cập từ bất kỳ nguồn nào
              .AllowAnyMethod() // Cho phép tất cả các phương thức HTTP (GET, POST, PUT, DELETE, v.v.)
              .AllowAnyHeader(); // Cho phép tất cả các header trong yêu cầu
    });
});

// JSON Serializer Settings
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver(); // Sử dụng DefaultContractResolver để giữ nguyên tên thuộc tính trong JSON
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Ngăn chặn vòng lặp tham chiếu khi serialize đối tượng
    });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // Kích hoạt CORS để cho phép truy cập từ các nguồn khác nhau

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
