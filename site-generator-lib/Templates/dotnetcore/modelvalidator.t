using DotNetCore.Validation;
using FluentValidation;

namespace DotNetCoreArchitecture.Model
{
    public class {{Name}}ModelValidator<T> : Validator<T> where T : {{Name}}Model
    {
        protected {{Name}}ModelValidator()
        {
     
        }
    }
}