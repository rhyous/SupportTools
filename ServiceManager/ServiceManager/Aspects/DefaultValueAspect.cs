using System;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [DefaultValueAspect(AttributeExclude = true)]
    [MulticastAttributeUsage(MulticastTargets.Property)]
    public class DefaultValueAspect : LocationInterceptionAspect
    {
        public Object DefaultValue { get; set; }

        public Object EmptyValue { get; set; }

        private bool _FirstAccess = true;

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            if (_FirstAccess)
            {
                _FirstAccess = false;
                object value = args.GetCurrentValue();
                if (value.Equals(EmptyValue) && args.Value.Equals(EmptyValue) && !args.Value.Equals(DefaultValue))
                {
                    args.Value = DefaultValue;
                    args.ProceedSetValue();
                }
            }
            args.ProceedGetValue();
        }
    }
}
