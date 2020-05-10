using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CompanyEmployees.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if(!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
            var providedValue = bindingContext.ValueProvider
                                .GetValue(bindingContext.ModelName)
                                .ToString();

            //If this is null, return to the controller as there is error handling for null;
            if(string.IsNullOrEmpty(providedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            //Use refelction to get type of Model (Guid)
            var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            
            //Get a converter for generic type
            var converter = TypeDescriptor.GetConverter(genericType);

            //Remove empty entries, trim and create object array
            var objectArray = providedValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(x => converter.ConvertFromString(x.Trim()))
                                    .ToArray();

            //Copy the oject array to the guid array
            var guidArray = Array.CreateInstance(genericType, objectArray.Length); objectArray.CopyTo(guidArray, 0); 
            
            //Bind the Guid array to the bindingContext
            bindingContext.Model = guidArray; 
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model); 
            
            return Task.CompletedTask;
        }
    }
}
