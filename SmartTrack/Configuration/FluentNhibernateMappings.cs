using FluentNHibernate.Mapping;
using SmartTrack.Model.Measures;

namespace SmartTrack.Web.Configuration
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Password);
            Map(x => x.Email);
        }
    }
}