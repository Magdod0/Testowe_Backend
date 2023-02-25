using CommonResources;
using Microsoft.EntityFrameworkCore;
using Testowe_GRPC.Context;
using Testowe_GRPC.Services;
using Testowe_GrpcService.Services;


bool newlyCreadted;
Mutex m = new Mutex(true, AppDomain.CurrentDomain.FriendlyName, out newlyCreadted);

if (!newlyCreadted)
{
    return;
}

var builder = WebApplication.CreateBuilder(args);

// Adding gRPC.
//builder.Services.AddGrpc();
//builder.Services.AddCors(o =>
//    o.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//        .AllowAnyHeader()
//        .AllowAnyMethod()
//        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
//    }));
// Adding our EF context service with connetion to SQL server.
builder.Services.AddDbContext<MessageDbContext>();
builder.Services.AddTransient<IDecryptionService, RSADecryptionService>();

var app = builder.Build();
app.UseRouting();

//app.UseGrpcWeb();
//app.UseCors();

//Adding our Decryption service who then calls Decryptor to decrypt the message
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGrpcService<DecryptorService>()
//        .EnableGrpcWeb()
//        .RequireCors("AllowAll");
//});


app.MapGet("/", () => {
    var result = Results.Ok();
    return Results.Ok(new CommonStatusCode(){ Status = "I'm teapot", Code = 3303 });
});
app.MapPost("/", (GetMessage gmessage, IDecryptionService ids, MessageDbContext context) => {
    var message = context.Messages.FirstOrDefault(m => m.ID.ToString() == gmessage.Id);
    if (message == null) return null;

    return ids.Decrypt(message.EncryptedText, gmessage.Key);
});
app.Run(async (context) => await context.Response.WriteAsync("Server Started"));
//Creating DB if it's not exist.
using (MessageDbContext dbcontext = new MessageDbContext())
{
    dbcontext.Database.Migrate();
}
app.Run();

//bool IsSingleInstance()
//{
//    Mutex m = new Mutex(false, AppDomain.CurrentDomain.FriendlyName);

//    GC.KeepAlive(m);

//    try
//    {
//        return m.WaitOne(0, false);
//    }
//    catch(AbandonedMutexException)
//    {
//        m.ReleaseMutex();
//        return m.WaitOne(0,false);
//    }
//}
