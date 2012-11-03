using System;
using PostSharp.Aspects;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [LazyPropertyAspect(AttributeExclude = true)]
    public class LazyPropertyAspect : LocationInterceptionAspect
    {
        public Type Type { get; set; }

        public Object DefaultValue { get; set; }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            args.ProceedGetValue();
            if (args.Value == null)
            {
                args.Value = DefaultValue ?? Activator.CreateInstance(Type ?? args.Location.PropertyInfo.PropertyType);
                args.ProceedSetValue();
            }
        }
    }
}
