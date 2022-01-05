using System.ComponentModel.DataAnnotations;
using Target.Backend.Web.Utils;

namespace Target.Backend.Web.Attributes
{
    /// <summary>
    /// Validação customizada para Unidades Federais
    /// </summary>
    public class UFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            return UFHelper.ValidaUF(value.ToString());
        }
    }
}
