using System.Net;

public class ErrorHandlingMiddleware
{
    // Bir sonraki middleware'i temsil eden RequestDelegate türünde bir alan tanımlıyoruz.
    private readonly RequestDelegate _next;
    // Loglama için ILogger türünde bir alan tanımlıyoruz.
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    // Middleware'in constructor'ı, bir sonraki middleware'i ve logger'ı parametre olarak alır.
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next; // Bu parametreyi _next alanına atıyoruz.
        _logger = logger; // Logger parametresini _logger alanına atıyoruz.
    }

    // Middleware'in ana işlevi olan InvokeAsync metodu, HttpContext'i parametre olarak alır.
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Bir sonraki middleware'e geçiyoruz ve onun sonucunu bekliyoruz.
            await _next(context);
        }
        catch (Exception ex)
        {
            // Eğer bir istisna yakalanırsa, bu istisnayı logluyoruz.
            _logger.LogError(ex, "An unhandled exception occurred.");
            // Hata durumunu yönetmek için özel bir metod çağırıyoruz.
            await HandleExceptionAsync(context, ex);
        }
    }

    // İstisna durumunu yöneten statik metod.
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // HTTP yanıt durum kodunu 500 (Internal Server Error) olarak ayarlıyoruz.
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        // Yanıt içeriği türünü JSON olarak ayarlıyoruz.
        context.Response.ContentType = "application/json";
        // İstisna mesajını içeren bir JSON yanıt oluşturuyoruz ve bunu yazıyoruz.
        return context.Response.WriteAsync(new { error = exception.Message }.ToString());
    }
}