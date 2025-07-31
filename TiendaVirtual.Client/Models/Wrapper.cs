using System.Text.Json.Serialization;
namespace TiendaVirtual.Client.Models
{
    public class Wrapper<T>
    {
        [JsonPropertyName("$values")]
        public List<T>? values {get; set;}
    }
}
