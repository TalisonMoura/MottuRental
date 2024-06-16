using System.Text;
using System.Text.Json;

namespace MottuRental.Infra.CrossCutting.Commons.Extensions;

public static class JsonExtensions
{
    public static string ToJson(this object obj) => JsonSerializer.Serialize(obj);
    public static T ToObject<T>(this string obj) => JsonSerializer.Deserialize<T>(obj);
    public static byte[] ToByte(this object obj) => Encoding.UTF8.GetBytes(obj.ToJson());
    public static string GetStringFromByte(this byte[] obj) => Encoding.UTF8.GetString(obj);
}