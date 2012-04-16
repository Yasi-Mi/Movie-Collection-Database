using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using NHibernate.Mapping.ByCode;

namespace MovieDatabase.NHibernateProvider.Overrides
{
    internal class WriterOverride : IOverride
    {
        public void Override(ModelMapper mapper)
        {
            mapper.Class<Writer>(map =>
               map.Bag(x => x.Movies,
                   bag =>
                   {
                       bag.Key(key =>
                       {
                           key.Column("WriterFk");
                       });
                       bag.Table("Movies_Writers");
                       bag.Cascade(Cascade.None);
                   },
                   collectionRelation =>
                       collectionRelation.ManyToMany(m => m.Column("MovieFk"))));
        }
    }
}
