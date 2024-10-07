using Microsoft.EntityFrameworkCore;
using ServidorPlatosMauiAPI.DBcontext;
using ServidorPlatosMauiAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opcion => opcion.UseSqlite(builder.Configuration.GetConnectionString("ConexionSQL")));

// Add services to the container.

var app = builder.Build();

app.MapGet("api/plato", async(ApplicationDbContext context) =>
{
    var elementos = await context.Platos.ToListAsync();
    return Results.Ok(elementos);
});

app.MapPost("api/plato",async(ApplicationDbContext context,Platos plato) =>
{
    var elementos= await context.Platos.AddAsync(plato);
    await context.SaveChangesAsync();
    return Results.Created($"api/plato/{plato.Id}",plato); 
});

app.MapPut("api/plato/{id}", async (ApplicationDbContext context,int id, Platos plato) =>
{
    var platoModelo = await context.Platos.FirstOrDefaultAsync(plato => plato.Id == id);
    if (platoModelo == null)
    {
        return Results.NotFound();
    }
    platoModelo.Nombre=plato.Nombre;
    platoModelo.Costo=plato.Costo;
    platoModelo.Ingredientes=plato.Ingredientes;
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/plato/{id}", async (ApplicationDbContext context,int id) =>
{
    var platoModelo = await context.Platos.FirstOrDefaultAsync(plato => plato.Id == id);
    if (platoModelo == null)
    {
        return Results.NotFound();
    }
    context.Platos.Remove(platoModelo);
    await context.SaveChangesAsync();
    return Results.NoContent();
});




app.Run();


