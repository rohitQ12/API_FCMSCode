using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace GlobalApi.GlobalClasses
{
    public class ModuleValidation: ValidationAttribute
    {
		private ADO_Configrations ado_Configurations;

		public ModuleValidation()
        {
			ado_Configurations = new ADO_Configrations();
		}
		protected override ValidationResult IsValid(object value,
			ValidationContext validationContext)
		{
			using (IDbConnection con = ado_Configurations.connection())
			{
				string Table_name = validationContext.ObjectType.Name;

				string Column_name = validationContext.DisplayName;

				string query = $"select * from {Table_name} where {Column_name}='{value}'";

				var result = con.Query(query);

                if (result.Count() == 0)
                {
					return ValidationResult.Success;
				}

				return new ValidationResult($"{value} Already Exists");
			}

		}
	}
    
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Code { get; set; }

        public string Message { get; }

        public ValidationError(string field, int code, string message)
        {
            Field = field != string.Empty ? field : null;
            Code = code != 0 ? code : 55;
            Message = message;
        }
    }
    public class ValidationResultModel
    {
        public string Message { get; }

        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, 0, x.ErrorMessage)))
                    .ToList();
        }
    }
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity; //change the http status code to 422.
        }
    }
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //context.Result = new BadRequestObjectResult(context.ModelState);
                context.Result=new BadRequestResult();
            }
            //context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
