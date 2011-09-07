using System.Collections.Generic;
using FubuValidation;

namespace SmartTrack.Web.Http.Output
{
    public class JsonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public IEnumerable<ValidationError> Errors { get { return errors; } }

        private readonly IList<ValidationError> errors = new List<ValidationError>();

        public void RegisterErrors(IEnumerable<ValidationError> newErrors)
        {
            this.errors.Fill(newErrors);
            Success = false;
        }

        public void RegisterError(ValidationError error)
        {
            RegisterErrors(new[] { error });
        }
    }
}