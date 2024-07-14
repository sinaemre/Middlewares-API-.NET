
using System.Diagnostics;

namespace Middlewares_API.Middlewares;

public class PerformanceMonitoringMiddleware
{
    // Bir sonraki middleware'i temsil eden RequestDelegate türünde bir alan tanımlıyoruz.
    private readonly RequestDelegate _next;
    // Loglama için ILogger türünde bir alan tanımlıyoruz.
    private readonly ILogger<PerformanceMonitoringMiddleware> _logger;
    // Performans ölçümü için Stopwatch türünde bir alan tanımlıyoruz.
    private readonly Stopwatch _stopwatch;

    // Middleware'in constructor'ı, bir sonraki middleware'i, logger'ı ve stopwatch'ı başlatır.
    public PerformanceMonitoringMiddleware(RequestDelegate next, ILogger<PerformanceMonitoringMiddleware> logger)
    {
        _next = next; // Bu parametreyi _next alanına atıyoruz.
        _logger = logger; // Logger parametresini _logger alanına atıyoruz.
        _stopwatch = new Stopwatch(); // Stopwatch nesnesini başlatıyoruz.
    }

    // Middleware'in ana işlevi olan InvokeAsync metodu, HttpContext'i parametre olarak alır.
    public async Task InvokeAsync(HttpContext context)
    {
        // İsteğin işlenme süresini ölçmeye başlıyoruz.a
        _stopwatch.Start();
        
        // Bir sonraki middleware'e geçiyoruz ve onun sonucunu bekliyoruz.
        await _next(context);
        
        // İsteğin işlenme süresini ölçmeyi durduruyoruz.
        _stopwatch.Stop();

        // Geçen süreyi milisaniye cinsinden alıyoruz.
        var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        
        // İsteğin HTTP metodunu ve yolunu, ayrıca geçen süreyi içeren bir bilgi mesajı logluyoruz.
        _logger.LogInformation($"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMilliseconds} ms");
    }
}