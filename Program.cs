using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello, World!");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
app.MapGet("/dbtest", async () =>
{
    try
    {
        using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        var command = new MySqlCommand("SHOW DATABASES;", connection);
        using var reader = await command.ExecuteReaderAsync();

        var databases = new List<string>();
        while (await reader.ReadAsync())
        {
            databases.Add(reader.GetString(0));
        }

        return Results.Ok(new { message = "Connected to MySQL!", databases });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error: {ex.Message}");
    }
});

app.Run("http://0.0.0.0:5158");

