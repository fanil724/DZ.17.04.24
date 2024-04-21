using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();




app.Map("/postuser", async (context) =>
{
    var form = context.Request.Form;
    string name = form["name"];
    string age = form["age"];
    string message = $"Имя: {name}, Взораст: {age}";
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync("<p>Добро пожаловать!!!</p>" +
        $"<p>{message}</p><br>" +
        "<a href=\"https://localhost:7135/pic1\">canwitcher</a><br>" +
        "<a href=\"https://localhost:7135/pic2\">witcher</a><br>" +
        "<a href=\"https://localhost:7135/pic3\">city</a><br> " +
        "<a href=\"https://localhost:7135/pic4\">rock</a>");

});
app.Map("/pic1", (context) => getPicture("canwitcher", context));
app.Map("/pic2", (context) => getPicture("witcher", context));
app.Map("/pic3", (context) => getPicture("city", context));
app.Map("/pic4", (context) => getPicture("rock", context));
app.Map("/", async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("html/form.html");
});


app.Run();

async Task getPicture(string name, HttpContext context)
{
    var fileProfider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
    var fileInfo = fileProfider.GetFileInfo($"IMAGE/{name}.jpg");
    context.Response.Headers.ContentDisposition = $"attachment; filename={name}.jpg";
    await context.Response.SendFileAsync(fileInfo);
}