using System.Text.RegularExpressions;

namespace MottuRental.Infra.CrossCutting.Commons.Extensions;

public static class StringExtensions
{
    public static string RemoveNonNumeric(this string str) => Regex.Replace(str, @"\D", "");
    public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);
    public static string RemoveSpecialCharacters(this string str) => Regex.Replace(str, "[^a-zA-Z0-9]", "");
    public static bool IsEquals(this string x, string y) => string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
    public static string DbStringFormat(this string value, string host, string user, string secret) => value.Replace("{Host}", host).Replace("{User}", user).Replace("{Secret}", secret);
    
    public static bool IsValidCnh(this string cnh)
    {
        if (!IsNullOrWhiteSpace(cnh) && cnh.Length.Equals(11))
        {
            cnh.RemoveNonNumeric();

            int[] digits = Array.ConvertAll(cnh.ToCharArray(), c => (int)char.GetNumericValue(c));

            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += digits[i] * (10 - i);

            int verifier = sum % 11;
            if (verifier >= 10) verifier = 0;
            return digits[9] == verifier;
        }

        return false;
    }

    public static bool IsValidCNPJ(this string cnpj)
    {
        if (!IsNullOrWhiteSpace(cnpj) && cnpj.Length.Equals(14))
        {
            cnpj.RemoveNonNumeric();

            int[] multiplier1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplier2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

            string tempCNPJ = cnpj[..12];
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCNPJ[i].ToString()) * multiplier1[i];

            int remainder = (sum % 11);

            remainder = remainder < 2 ? 0 : 11 - remainder;

            string checkDigit = remainder.ToString();

            tempCNPJ = $"{tempCNPJ}{checkDigit}";

            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCNPJ[i].ToString()) * multiplier2[i];

            remainder = (sum % 11);

            remainder = remainder < 2 ? 0 : 11 - remainder;

            return cnpj.EndsWith($"{checkDigit}{remainder}");
        }

        return false;
    }
}