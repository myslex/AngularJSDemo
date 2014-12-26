using AngularJSDemo.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSDemo.MvcWebApi.Models.ViewModels
{
    public class MovieModel
    {
        public MovieModel() { }

        public Movie Movie { get; set; }
        public virtual Director Director { get; set; }
    }
}