using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

namespace AspectMVVM
{
    [Serializable]
    [ProvideAspectRole(StandardRoles.Persistence)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.DataBinding)]
    public class LazyLoad : LocationInterceptionAspect
    {
        public Type Type { get; set; }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            args.ProceedGetValue();
            if (args.Value == null)
            {
                args.Value = Activator.CreateInstance(Type ?? args.Location.PropertyInfo.PropertyType);
                args.ProceedSetValue();
            }
        }
    }
}
