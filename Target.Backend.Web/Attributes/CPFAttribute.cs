﻿using System.ComponentModel.DataAnnotations;
using Target.Backend.Web.Utils;

namespace Target.Backend.Web.Attributes
{
    // <summary>
    /// Validação customizada para CPF
    /// </summary>
    public class CPFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            return CPFHelper.ValidaCPF(value.ToString()); 
        }
    }
}
