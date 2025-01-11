using Microsoft.AspNetCore.Mvc;

namespace ConvertifyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController : ControllerBase
    {
        [HttpGet("longitud")]
        public IActionResult ConvertirLongitud(
            [FromQuery] double cantidad,
            [FromQuery] string unidadOrigen,
            [FromQuery] string unidadDestino)
        {
            if (cantidad <= 0)
            {
                return BadRequest(new { error = "La cantidad debe ser un número positivo." });
            }

            var unidadesValidas = new[] { "kilometro", "hectometro", "decametro", "metro", "decimetro", "centimetro", "milimetro" };
            if (!unidadesValidas.Contains(unidadOrigen.ToLower()) || !unidadesValidas.Contains(unidadDestino.ToLower()))
            {
                return BadRequest(new { error = "Las unidades deben ser 'kilometro', 'hectometro', 'decametro', 'metro', 'decimetro', 'centimetro', o 'milimetro'." });
            }

            // Conversión de unidades
            double resultado = ConvertirLng(cantidad, unidadOrigen.ToLower(), unidadDestino.ToLower());

            return Ok(new
            {
                cantidad,
                unidadOrigen,
                unidadDestino,
                resultado
            });
        }

        private double ConvertirLng(double cantidad, string unidadOrigen, string unidadDestino)
        {
            double cantidadEnMetros = unidadOrigen switch
            {
                "kilometro" => cantidad * 1000,
                "hectometro" => cantidad * 100,
                "decametro" => cantidad * 10,
                "metro" => cantidad,
                "decimetro" => cantidad * 0.1,
                "centimetro" => cantidad * 0.01,
                "milimetro" => cantidad * 0.001,
                _ => throw new ArgumentException("Unidad de origen no válida")
            };

            return unidadDestino switch
            {
                "kilometro" => cantidadEnMetros / 1000,
                "hectometro" => cantidadEnMetros / 100,
                "decametro" => cantidadEnMetros / 10,
                "metro" => cantidadEnMetros,
                "decimetro" => cantidadEnMetros / 0.1,
                "centimetro" => cantidadEnMetros / 0.01,
                "milimetro" => cantidadEnMetros / 0.001,
                _ => throw new ArgumentException("Unidad de destino no válida")
            };
        }
    }
}
