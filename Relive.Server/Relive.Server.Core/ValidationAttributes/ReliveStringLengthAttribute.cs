using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relive.Server.Core.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ReliveStringLengthAttribute: ValidationAttribute
    {
        // Attribute Properties
        public string Min { get; set; } 
        public string Max { get; set; }
        public string Equal { get; set; }

        // Methods
        public override bool IsValid(object value)
        {
            string valueString = (string) value;
            if (valueString == null)
                return true;
            if (Equal != null && valueString.Length != int.Parse(Equal))
            {
                ErrorMessage = $"Length must be equal to {Equal} characters";
                return false;
            }
            if (Min != null && valueString.Length < int.Parse(Min))
            {
                ErrorMessage = $"Length must be atleast {Min} characters";
                return false;
            }
            if (Max != null && valueString.Length > int.Parse(Max))
            {
                ErrorMessage = $"Length must not exceed {Max} characters";
            }
            return true;
        }
    }
}
