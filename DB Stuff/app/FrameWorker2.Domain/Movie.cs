using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpLite.Domain;

namespace MovieDatabase.Domain
{
    public class Movie : Entity
    {
        public Movie()
        {
            //This just prevents the possibility of a null list exception.
            Collectors = new List<Collector>();
            Directors = new List<Director>();
            Genres = new List<Genre>();
            Writers = new List<Writer>();
        }

        public virtual string Title { get; set; }
        public virtual string TagLine { get; set; }

        public virtual IList<Collector> Collectors { get; protected set; }
        public virtual IList<Director> Directors { get; protected set; }
        public virtual IList<Genre> Genres { get; protected set; }
        public virtual IList<Writer> Writers { get; protected set; }
    }
}