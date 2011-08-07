using FubuMVC.WebForms;
using SmartTrack.Model.Measures;

namespace SmartTrack.Web.Controllers.Measures
{
    public class AllMeasures : FubuPage<MeasuresViewModel> { }

    public class MeasuresController
    {
        private readonly User user;

        public MeasuresController(User loggedUser)
        {
            user = loggedUser;
        }

        public MeasuresViewModel AllMeasures()
        {

            return new MeasuresViewModel();
        } 
    }

    public class MeasuresViewModel
    {

    }
}