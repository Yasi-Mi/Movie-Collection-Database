using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using NHibernate.Mapping.ByCode;

namespace MovieDatabase.NHibernateProvider.Overrides
{
    internal class CollectorOverride : IOverride
    {
        public void Override(ModelMapper mapper)
        {
            mapper.Class<Collector>(map =>
               map.Bag(x => x.Movies,
                   bag =>
                   {
                       bag.Key(key =>
                       {
                           key.Column("CollectorFk");
                       });
                       bag.Table("Movies_Collectors");
                       bag.Cascade(Cascade.None);
                   },
                   collectionRelation =>
                       collectionRelation.ManyToMany(m => m.Column("MovieFk"))));
        }
    }
}
