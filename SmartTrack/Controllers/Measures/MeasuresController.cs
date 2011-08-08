using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Continuations;
using FubuMVC.WebForms;
using SmartTrack.Model.Measures;
using SmartTrack.Model.Repositories;

namespace SmartTrack.Web.Controllers.Measures
{
    public class AllMeasures : FubuPage<MeasuresViewModel> { }

    public class MeasuresController
    {
        private readonly User user;
        private readonly Repository repository;

        public MeasuresController(User loggedUser, Repository repository)
        {
            user = loggedUser;
            this.repository = repository;
        }

        public MeasuresViewModel AllMeasures()
        {
            var viewmodel = new MeasuresViewModel();
            
            user.Measures.ToList().ForEach(x => viewmodel.Measures.Add(x.Name));

            return new MeasuresViewModel();
        }

        public FubuContinuation AddSingleMeasure(AddMeasureInputModel input)
        {
            var e = new MeasureAdded {Date = DateTime.Now, Measure = input.Name, Unit = input.Unit, Value = input.Value};
            repository.SaveEvent(e, user);
            
            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
        } 
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
            Measures = new List<string>();
        }
        public List<string> Measures { get; set; }
    }
}