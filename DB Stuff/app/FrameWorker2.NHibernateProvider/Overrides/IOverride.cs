using NHibernate.Mapping.ByCode;

namespace MovieDatabase.NHibernateProvider.Overrides
{
    internal interface IOverride
    {
        void Override(ModelMapper mapper);
    }
}
