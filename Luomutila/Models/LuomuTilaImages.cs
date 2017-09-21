using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuomuTila.Models { 
    public class LuomuTilaImages {

        public string Fileimg { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IEnumerable<string> Videos { get; set; }
    }
}