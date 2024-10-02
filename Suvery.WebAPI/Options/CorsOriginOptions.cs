namespace Suvery.WebAPI.Options
{
    public class CorsOriginOptions
    {
        public bool AcceptCorsEnable { get; set; } = true;
        public string AllowCorsOrigin { get; set; } = "*";
        public string AllowAnyMethod { get; set; } = "*";
        public string AllowAnyHeader { get; set; } = "*";
    }
}