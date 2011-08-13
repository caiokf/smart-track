using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using FubuMVC.WebForms;
using SmartTrack.Model.Measures;
using SmartTrack.Model.Repositories;

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

        public FubuContinuation CreateMeasurePost(CreateMeasureInputModel input)
        {
            repository.SaveEvent(new MeasureCreated { Measure = input.Name, Unit = input.Unit }, user);
            
            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
        }

        public EditMeasureViewModel EditMeasure(EditThisMeasureInputModel input)
        {
            var measure = user.Measures.Single(x => x.Name == input.OriginalName);
            
            return new EditMeasureViewModel { Name = measure.Name, Unit = measure.Unit };
        }

        public FubuContinuation EditMeasurePost(EditMeasureInputModel input)
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

        public FubuContinuation AddSingleMeasure(AddMeasureInputModel input)
        {
            repository.SaveEvent(new MeasureAdded
            {
                Date = DateTime.Now, 
                Measure = input.Name, 
                Unit = input.Unit, 
                Value = input.Value
            }, user);
            
            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
        } 
    }

    public class EditMeasureViewModel
    {
        public string Name { get; set; }
        public string Unit { get; set; }
    }

    public class EditMeasureInputModel 
    {
        public string OriginalName { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }

    public class EditThisMeasureInputModel
    {
        [QueryString] public string OriginalName { get; set; }
    }

    public class CreateMeasureInputModel
    {
        public string Name { get; set; }
        public string Unit { get; set; }
    }

    public class AddMeasureInputModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
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