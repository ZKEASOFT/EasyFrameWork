using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;

namespace Easy.ViewPort.Validator
{
    public class RequiredValidator : ValidatorBase
    {
        public RequiredValidator()
        {
            this.BaseErrorMessage = "{0}是必填的！";
        }
        public override bool Validate(object value)
        {
            if (value == null || value.ToString().IsNullOrEmpty()) return false;
            else return true;
        }
    }
}
