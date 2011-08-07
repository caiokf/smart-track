using FubuMVC.WebForms;

namespace SmartTrack.Web.Controllers.Measures
{
    public class AllMeasures : FubuPage<MeasuresViewModel> { }

    public class MeasuresController
    {
        public MeasuresViewModel AllMeasures()
        {
            return new MeasuresViewModel();
        } 
    }

    public class MeasuresViewModel
    {

    }
}