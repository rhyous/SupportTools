using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

namespace AspectMVVM
{
    [Serializable]
    [ProvideAspectRole(StandardRoles.DataBinding)]
    public class NotifyPropertyChanged : LocationInterceptionAspect
    {
        public NotifyPropertyChanged()
            : this(true, null)
        {
        }

        public NotifyPropertyChanged(params string[] inProperty)
            : this(true, inProperty)
        {
        }

        public NotifyPropertyChanged(bool inNotifyCurrentProperty, params string[] inProperty)
        {
            _NotifyCurrentProperty = inNotifyCurrentProperty;
            _OtherProperties = inProperty;
        }

        private readonly string[] _OtherProperties;
        private readonly bool _NotifyCurrentProperty;

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            // Do nothing if the property value doesn't change
            if (args.Value == args.GetCurrentValue())
                return;

            args.ProceedSetValue();

            var npc = args.Instance as INotifyPropertyChangedWithMethod;
            if (npc != null)
            {

                // Notify for current property
                if (_NotifyCurrentProperty)
                    npc.NotifyPropertyChanged(args.Location.PropertyInfo.Name);

                // Notify for other properties
                if (_OtherProperties != null)
                {
                    foreach (string otherProperty in _OtherProperties)
                    {
                        npc.NotifyPropertyChanged(otherProperty);
                    }
                }
            }
        }
    }
}
