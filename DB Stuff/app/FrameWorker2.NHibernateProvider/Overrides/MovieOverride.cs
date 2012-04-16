using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using NHibernate.Mapping.ByCode;

namespace MovieDatabase.NHibernateProvider.Overrides
{
    internal class MovieOverride : IOverride
    {
        public void Override(ModelMapper mapper)
        {
            mapper.Class<Movie>(map =>
               map.Bag(x => x.Collectors,
                   bag =>
                   {
                       bag.Key(key =>
                       {
                           key.Column("MovieFk");
                       });
                       bag.Table("Movies_Collectors");
                       bag.Cascade(Cascade.None);
                   },
                   collectionRelation =>
                       collectionRelation.ManyToMany(m => m.Column("CollectorFk"))));

            mapper.Class<Movie>(map =>
               map.Bag(x => x.Directors,
                   bag =>
                   {
                       bag.Key(key =>
                       {
                           key.Column("MovieFk");
                       });
                       bag.Table("Movies_Directors");
                       bag.Cascade(Cascade.None);
                   },
                   collectionRelation =>
                       collectionRelation.ManyToMany(m => m.Column("DirectorFk"))));

            mapper.Class<Movie>(map =>
               map.Bag(x => x.Writers,
                   bag =>
                   {
                       bag.Key(key =>
                       {
                           key.Column("MovieFk");
                       });
                       bag.Table("Movies_Writers");
                       bag.Cascade(Cascade.None);
                   },
                   collectionRelation =>
                       collectionRelation.ManyToMany(m => m.Column("WriterFk"))));

            mapper.Class<Movie>(map =>
               map.Bag(x => x.Genres,
                   bag =>
                   {
                       bag.Key(key =>
                       {
                           key.Column("MovieFk");
                       });
                       bag.Table("Movies_Genres");
                       bag.Cascade(Cascade.None);
                   },
                   collectionRelation =>
                       collectionRelation.ManyToMany(m => m.Column("GenreFk"))));
        }
    }
}
