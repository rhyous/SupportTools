using System;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Aspects.Dependencies;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [DefaultValueAspect(AttributeExclude = true)]
    [MulticastAttributeUsage(MulticastTargets.Property)]
    [ProvideAspectRole(StandardRoles.Persistence)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.DataBinding)]
    public class DefaultValueAspect : LocationInterceptionAspect
    {
        public Object DefaultValue { get; set; }

        /// <summary>
        /// Empty value is used 
        /// </summary>
        public Object EmptyValue { get; set; }

        private bool _FirstAccess = true;

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            if (_FirstAccess)
            {
                _FirstAccess = false;
                var value = args.GetCurrentValue();
                if (value.Equals(EmptyValue) || args.Value.Equals(EmptyValue) || !args.Value.Equals(DefaultValue))
                {
                    args.Value = DefaultValue;
                    args.ProceedSetValue();
                }
            }
            args.ProceedGetValue();
        }
    }
}
