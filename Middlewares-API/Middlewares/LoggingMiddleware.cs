namespace Middlewares_API.Middlewares;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
public class LoggingMiddleware
{
    // Bir sonraki middleware'i temsil eden RequestDelegate türünde bir alan tanımlıyoruz.
    private readonly RequestDelegate _next;
    // Loglama için ILogger türünde bir alan tanımlıyoruz.
    private readonly ILogger<LoggingMiddleware> _logger;

    // Middleware'in constructor'ı, bir sonraki middleware'i ve logger'ı parametre olarak alır.
    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next; // Bu parametreyi _next alanına atıyoruz.
        _logger = logger; // Logger parametresini _logger alanına atıyoruz.
    }

    // Middleware'in ana işlevi olan InvokeAsync metodu, HttpContext'i parametre olarak alır.
    public async Task InvokeAsync(HttpContext context)
    {
        // İstek işlenmeye başlandığında bir bilgi mesajı logluyoruz.
        _logger.LogInformation("Handling request: " + context.Request.Path);
        
        // Bir sonraki middleware'e geçiyoruz ve onun sonucunu bekliyoruz.
        await _next(context);
        
        // İstek işlendiğinde bir bilgi mesajı logluyoruz.
        _logger.LogInformation("Finished handling request.");
    }
}