using ClientWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientWeb.ViewModel
{
    public class FilmViewModel
    {
        public Client client { get; set; }
        public Film film { get; set; }
    }
}