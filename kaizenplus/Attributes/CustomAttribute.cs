using System;
using System.ComponentModel.DataAnnotations;

namespace kaizenplus.Attributes
{
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var number = Convert.ToInt64(value);
            return number >= 1;
        }
    }
}