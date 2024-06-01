using SignalReactive.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin", b =>
    {
        b.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((Host) => true)
        .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowOrigin");
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapHub<MessageHub>("/message");
});


app.Run();
