var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("map1", async (context) =>
    {
       await context.Response.WriteAsync("map1 route");
    });

    endpoints.MapGet("mapget", async (context) =>
    {
        await context.Response.WriteAsync("Using MapGet method");
    });

    endpoints.MapPost("mappost", async (context) =>
    {
        await context.Response.WriteAsync("MapPost used");
    });

    //RouteParameters
    endpoints.Map("files/{filename}.{ext}", async context =>
    {
        RouteValueDictionary dict = context.Request.RouteValues;
        string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
        string? filename2 = Convert.ToString(dict["filename"]);
        string? ext = Convert.ToString(context.Request.RouteValues["ext"]);

        await context.Response.WriteAsync($"File path is {filename2}.{ext}");

    });

    //DefaultRouteParameters
    endpoints.Map("product/{id=1}", async context =>
    { 
        int? id = Convert.ToInt32(context.Request.RouteValues["id"]);

        await context.Response.WriteAsync($"ID is {id}");

    });

    //RouteConstraints
    endpoints.Map("dailyreport/{date:datetime}", async context =>
    {
        DateTime? date = Convert.ToDateTime(context.Request.RouteValues["date"]);
        await context.Response.WriteAsync($"Today's date is {date}");
    });

});

app.Use(async (context, next) => 
{
    await context.Response.WriteAsync("buddy\n");
    await next(context);
});

app.Run(async (context) =>
{
   await context.Response.WriteAsync("run executed jhaha");
});

app.Run();
