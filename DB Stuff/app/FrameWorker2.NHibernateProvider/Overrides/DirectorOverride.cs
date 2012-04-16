using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using NHibernate.Mapping.ByCode;

namespace MovieDatabase.NHibernateProvider.Overrides
{
    internal class DirectorOverride : IOverride
    {
        public void Override(ModelMapper mapper)
        {
            mapper.Class<Director>(map =>
               map.Bag(x => x.Movies,
                   bag =>
                   {
                       bag.Key(key =>
                       {
                           key.Column("DirectorFk");
                       });
                       bag.Table("Movies_Directors");
                       bag.Cascade(Cascade.None);
                   },
                   collectionRelation =>
                       collectionRelation.ManyToMany(m => m.Column("MovieFk"))));
        }
    }
}
