using System.Data.Common;
using System.Data;
using UnitOfWork.Interfaces;
using UnitOfWork.SqlServer;
using Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Repository.Interfaces;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Services
builder.Services.AddTransient<IUnitOfWork, UnitOfWorkSqlServer>();
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<IPatientQueueService, PatientInQueueService>();
builder.Services.AddTransient<IPatientAttentionService, PatientInAttentionService>();
builder.Services.AddTransient<IErrorService, ErrorService>();
builder.Services.AddTransient<IDocumentService, DocumentService>();
builder.Services.AddTransient<IEmployeedService, EmployeedService>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<ISolicitudAttentionService, SolicitudAttentionService>();
builder.Services.AddTransient<IPacketsOrUnitSessionsService, PacketsOrUnitSessionsService>();
builder.Services.AddTransient<ICommonService, CommonService>();
builder.Services.AddTransient<IScheduleService, ScheduleService>();
builder.Services.AddTransient<IPayService, PayService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IMovementService, MovementService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISaleService, SaleService>();
//Services
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
