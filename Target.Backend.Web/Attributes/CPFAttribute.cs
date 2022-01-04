using System.ComponentModel.DataAnnotations;
using Target.Backend.Web.Utils;

namespace Target.Backend.Web.Attributes
{
    // <summary>
    /// Validação customizada para CPF
    /// </summary>
    public class CPFAttribute : ValidationAttribute
    {
        public CPFAttribute() { }
        public string GetErrorMessage() =>
        $"CPF inválido .";
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            bool valido = CPFHelper.ValidaCPF(value.ToString());
            return valido;
        }
    }
}
