using AngularJSDemo.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace AngularJSDemo.MvcWebApi.Models.ViewModels
{
    public class DirectorModel
    {
        public DirectorModel()
        {
            this.Movies = new HashSet<Movie>();
        }

        public Director Director { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}