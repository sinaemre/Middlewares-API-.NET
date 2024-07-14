namespace Middlewares_API.Middlewares;

public class AuthorizationMiddleware
{
    // Bir sonraki middleware'i temsil eden RequestDelegate türünde bir alan tanımlıyoruz.
    private readonly RequestDelegate _next;

    // Middleware'in constructor'ı, bir sonraki middleware'i parametre olarak alır.
    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next; // Bu parametreyi _next alanına atıyoruz.
    }

    // Middleware'in ana işlevi olan InvokeAsync metodu, HttpContext'i parametre olarak alır.
    public async Task InvokeAsync(HttpContext context)
    {
        
        if (context.Request.Path.StartsWithSegments("/api/users/login"))
        {
            await _next(context);
            return;
        }

        // Kullanıcının kimliğinin doğrulanıp doğrulanmadığını kontrol ediyoruz.
        if (!context.User.Identity.IsAuthenticated)
        {
            // Eğer kullanıcı kimliği doğrulanmamışsa, 401 Unauthorized durum kodunu döndürüyoruz.
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return; // İşlemi sonlandırıyoruz ve sonraki middleware'e geçmiyoruz.
        }

        // Özel yetkilendirme mantığını burada ekleyebilirsiniz.
        // Örneğin, belirli roller veya talepler için yetkilendirme kontrolü yapabilirsiniz.

        // Bir sonraki middleware'e geçiyoruz.
        await _next(context);
    }
}