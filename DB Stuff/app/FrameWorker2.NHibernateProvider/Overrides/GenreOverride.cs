using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using NHibernate.Mapping.ByCode;

namespace MovieDatabase.NHibernateProvider.Overrides
{
    internal class GenreOverride : IOverride
    {
        public void Override(ModelMapper mapper)
        {
            mapper.Class<Genre>(map =>
               map.Bag(x => x.Movies,
                   bag =>
                   {
                       bag.Key(key =>
                       {
                           key.Column("GenreFk");
                       });
                       bag.Table("Movies_Genres");
                       bag.Cascade(Cascade.None);
                   },
                   collectionRelation =>
                       collectionRelation.ManyToMany(m => m.Column("MovieFk"))));
        }
    }
}
