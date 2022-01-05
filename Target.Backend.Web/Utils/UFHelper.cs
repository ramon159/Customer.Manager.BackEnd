using System.Collections.Generic;

namespace Target.Backend.Web.Utils
{
    public static class UFHelper
    {
        /// <summary>
        /// Lista com todas as Unidades Federais
        /// </summary>
        public static List<string> UFs = new List<string> { "RO", "AC", "AM", "RR", "PA", "AP", "TO", "MA", "PI", "CE", "RN", "PB", "PE", "AL", "SE", "BA", "MG", "ES", "RJ", "SP", "PR", "SC", "RS", "MS", "MT", "GO", "DF" };
        /// <summary>
        /// Verifica se a UF é válida
        /// </summary>
        public static bool ValidaUF(string uf)
        {
            uf = uf.ToUpper();
            if (uf.Length != 2) return false;
            if (!UFs.Contains(uf)) return false;
            return true;
        }
    }
}
