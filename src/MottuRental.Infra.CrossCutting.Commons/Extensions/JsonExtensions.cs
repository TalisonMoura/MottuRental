using System.Text;
using System.Text.Json;

namespace MottuRental.Infra.CrossCutting.Commons.Extensions;

public static class JsonExtensions
{
    public static string ToJson(this object obj) => JsonSerializer.Serialize(obj);
    public static T ToObject<T>(this string obj) => JsonSerializer.Deserialize<T>(obj);
}