using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAntiforgery(options => options.SuppressXFrameOptionsHeader = true); ;

builder.Services.AddRazorPages().AddRazorPagesOptions(x
    => x.Conventions
    .ConfigureFilter(new IgnoreAntiforgeryTokenAttribute())).AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();


app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";


 //Новая страница
 //Создай формачку блог и полностью отображайте
 //в ней свою информацию Имя фамилия дата рождения прфессия
 //школа и тд используйте стили
 //Загрузить дз в гитхаю

    // если обращение идет по адресу "/postuser", получаем данные формы
    if (context.Request.Path == "/postuser")
    {
        var form = context.Request.Form;

        string FIO = form["FIO"];
        string age = form["age"];
        string work = form["work"];
        string birthdayDate = form["birthdayDate"];

        await context.Response.WriteAsync($"<div><p>FIO:{FIO} </p> " +
                                          $"<p>Age: {age} </p></div>" +
                                          $"<p>BirthdayDate: {birthdayDate} </p></div>" +
                                          $"<p>Work: {work} </p></div>");
    }

    else
    {
        await context.Response.SendFileAsync("Pages/Shared/Index.cshtml");
        //await context.Response.SendFileAsync("html/cshtml.css");
    }
});

app.Run();

