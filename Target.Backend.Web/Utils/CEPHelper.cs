namespace Target.Backend.Web.Utils
{
    public static class CEPHelper
    {
        /// <summary>
        /// Verifica se um CEP é válido
        /// </summary>
        public static bool ValidaCep(string cep)
        {
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
            }
            return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }
    }
}
