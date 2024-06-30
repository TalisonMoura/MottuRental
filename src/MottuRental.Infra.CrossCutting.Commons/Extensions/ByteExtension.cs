using System.Text;

namespace MottuRental.Infra.CrossCutting.Commons.Extensions;

public static class ByteExtension
{
    public static byte[] ImageToByte(this string image) => File.ReadAllBytes(image);
    public static byte[] ToJsonByte(this object obj) => Encoding.UTF8.GetBytes(obj.ToJson());
    public static string GetStringFromByte(this byte[] obj) => Encoding.UTF8.GetString(obj);
}
