namespace MottuRental.Infra.CrossCutting.Commons.Extensions;

public static class DateTimeExtensions
{
    public static bool HasFullAge(this DateTime birthDate)
        => DateTime.UtcNow.AddYears(-18).Date >= birthDate.Date;
}