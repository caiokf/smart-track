using System;
using System.Html;
using SmartTrack.Scripts.Libraries;
using jQueryApi;

namespace SmartTrack.Scripts
{
	public static class SetupAjaxValidation
	{
		static SetupAjaxValidation()
		{
		    jQuery.OnDocumentReady(delegate
	        {
		        jQuery
		            .Select(".form-ajax-validate-and-submit")
		            .Each(delegate(int index, Element e)
		            {
		                new AjaxFormValidation(e).SetupValidation();
		            });
	        });
		}
	}

	public class AjaxFormValidation
	{
		private readonly jQueryObject form;

		public AjaxFormValidation(Element f)
		{
			form = jQuery.FromElement(f);
		}

		public void SetupValidation()
		{
            ValidateOptions options = new ValidateOptions();
            options.ErrorElement = "span";
            options.ErrorPlacement = delegate(jQueryObject error, jQueryObject element) 
            { 
                error.PrependTo(element.Parent());
            };
            options.SubmitHandler = Submit;

			form.Plugin<JQueryValidationPlugin>().Validate(options);
		}

	    private bool Submit(Element formElement)
	    {
	        AjaxSubmitOptions ajaxSubmitOptions = new AjaxSubmitOptions();
	        ajaxSubmitOptions.Url = this.form.GetAttribute("action");
	        ajaxSubmitOptions.Type = "POST";
	        ajaxSubmitOptions.DataType = "json";
	        ajaxSubmitOptions.Success = ShowValidationErrors;
	        ajaxSubmitOptions.Error = delegate { this.form.Submit(); };

	        this.form.Plugin<FormsPluginObject>().AjaxSubmit(ajaxSubmitOptions);

	        return true;
	    }

	    private void ShowValidationErrors(JsonResponse response)
		{
			if (response.Success)
			{
				form.Submit();
			}
			else
			{
				ShowValidationSummary(response);
                ShowValidationFields(response);
			}
		}

	    private void ShowValidationSummary(JsonResponse response)
	    {
	        form.Find(".ui-error-summary")
	            .FadeIn(EffectDuration.Slow, delegate
	            {
	                jQueryObject list = form.Find(".ui-state-error > ul").Html("");
	                jQuery.Each(response.Errors, delegate(int index, ValidationError value)
	                {
	                    list.Append("<li>" + value.Message + "</li>");
	                });
	            });
	    }
        
        private void ShowValidationFields(JsonResponse response)
        {
            form.Find(".ui-error-field")
                .Each(delegate(int i, Element e)
                {
                    jQueryObject field = jQuery.FromElement(e);
                    field.FadeIn(EffectDuration.Slow, delegate
                    {
                        jQueryObject fieldToValidate = jQuery.Select("#" + field.GetAttribute("id").Replace("validation-for-", "")); 
                        jQueryObject list = field.Find("ul").Html("");
                        Array errors = Linq
                            .From(response.Errors)
                            .Where(delegate(object x) { return ((ValidationError)x).Field == fieldToValidate.GetAttribute("name"); })
                            .ToArray();
                        
                        jQuery.Each(errors, delegate(int index, object value)
                        {
                            list.Append("<li>" + ((ValidationError) value).Message + "</li>");
                        });
                    });
                });
        }
	}
}