using System.ComponentModel.DataAnnotations;
using Target.Backend.Web.Utils;

namespace Target.Backend.Web.Attributes
{
    /// <summary>
    /// Validação customizada para CEP
    /// </summary>
    public class CEPAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            return CEPHelper.ValidaCep(value.ToString());
        }
    }
}
