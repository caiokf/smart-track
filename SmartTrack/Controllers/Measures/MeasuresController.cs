using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using FubuMVC.WebForms;
using SmartTrack.Model.Measures;
using SmartTrack.Model.Repositories;
using SmartTrack.Web.Http.Output;

namespace SmartTrack.Web.Controllers.Measures
{
    public class AllMeasures : FubuPage<MeasuresViewModel> { }
    public class CreateMeasure : FubuPage<EditMeasureViewModel> { }
    public class EditMeasure : FubuPage<EditMeasureViewModel> { }

    public class MeasuresController
    {
        private readonly User user;
        private readonly Repository repository;

        public MeasuresController(User loggedUser, Repository repository)
        {
            user = loggedUser;
            this.repository = repository;
        }

        public EditMeasureViewModel CreateMeasure()
        {
            return new EditMeasureViewModel();
        }

        public FubuContinuation CreateMeasurePost(CreateMeasureInput input)
        {
            repository.SaveEvent(new MeasureCreated { Measure = input.Name, Unit = input.Unit }, user);
            
            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
        }

        public EditMeasureViewModel EditMeasure(EditMeasureRequest request)
        {
            var measure = user.Measures.Single(x => x.Name == request.OriginalName);
            
            return new EditMeasureViewModel { Name = measure.Name, Unit = measure.Unit };
        }

        public FubuContinuation EditMeasurePost(EditMeasureInput input)
        {
            repository.SaveEvent(new MeasureEdited { OldMeasure = input.OriginalName, NewMeasure = input.Name, Unit = input.Unit }, user);
            
            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
        }

        public MeasuresViewModel AllMeasures()
        {
            var viewmodel = new MeasuresViewModel();
            
            user.Measures.ToList().ForEach(x => viewmodel.Measures.Add(x));

            return viewmodel;
        }

        public FubuContinuation AddSingleMeasure(AddMeasureInput input)
        {
            repository.SaveEvent(new MeasureAdded
            {
                Date = DateTime.Now, 
                Measure = input.Name, 
                Value = input.Value
            }, user);
            
            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
        }

        public JsonResponse SaveTodaysMeasurements(SaveTodaysMeasurementsInput input)
        {
            return new JsonResponse {Success = true};
        }
    }

    public class EditMeasureViewModel
    {
        public string Name { get; set; }
        public string Unit { get; set; }
    }

    public class EditMeasureInput 
    {
        public string OriginalName { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }

    public class EditMeasureRequest
    {
        [QueryString] public string OriginalName { get; set; }
    }

    public class CreateMeasureInput
    {
        public string Name { get; set; }
        public string Unit { get; set; }
    }

    public class SaveTodaysMeasurementsInput
    {
        public SaveSingleTodaysMeasurementInput[] Measurements { get; set; }

        public class SaveSingleTodaysMeasurementInput
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }

    public class AddMeasureInput
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class MeasuresViewModel
    {
        public MeasuresViewModel()
        {
            Measures = new List<Measure>();
        }
        public List<Measure> Measures { get; set; }
    }
}