using System;
using PostSharp.Aspects;

namespace AspectMVVM
{
    [Serializable]
    public class LazyLoad : LocationInterceptionAspect
    {
        private readonly object _DefaultValue;
        private bool _IsSet;

        public LazyLoad()
        {
            AspectPriority = 1;
        }

        public LazyLoad(object inDefaultValue = null)
        {
            AspectPriority = 1;
            _DefaultValue = inDefaultValue;
        }

        public LazyLoad(Type type)
        {
            AspectPriority = 1;
            _DefaultValue = Activator.CreateInstance(type);
        }

        public Type Type { get; set; }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            args.ProceedGetValue();
            if (args.Value == null || !_IsSet)
            {
                args.Value = _DefaultValue ?? Activator.CreateInstance(Type ?? args.Location.PropertyInfo.PropertyType);
                _IsSet = true;
                args.ProceedSetValue();
            }
        }
    }
}
