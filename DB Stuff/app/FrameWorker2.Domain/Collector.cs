using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpLite.Domain;

namespace MovieDatabase.Domain
{
    public class Collector : Entity
    {
        public Collector()
        {
            //This just prevents the possibility of a null list exception
            Movies = new List<Movie>();
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual IList<Movie> Movies { get; protected set; }
    }
}