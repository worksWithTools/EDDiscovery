using EDDMobileImpl.ViewModels;
using EDMobileLibrary.Services;
using System.Linq;

namespace EDDMobileImpl.Services
{
    public class PropertyNotifier<TSource, TTarget> where TSource : class
                                                where TTarget : IPropertyChangedNotifier
    {
        public static void NotifyAllChanges(TSource source, TTarget target)
        {
            var sourceProps = source.GetType().GetProperties();
            var targetProps = target.GetType().GetProperties();

            var updateProps = from p in sourceProps
                              join t in targetProps on p.Name.ToLower() equals t.Name.ToLower()
                              where p.PropertyType == t.PropertyType
                              select p.Name;

            foreach(var prop in updateProps)
                target.OnPropertyChanged(prop);
        }
    }
}