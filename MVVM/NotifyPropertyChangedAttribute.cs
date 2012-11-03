using System;
using System.ComponentModel;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;
using PostSharp.Reflection;

namespace Rhyous.MVVM
{

    /// <summary>
    /// Aspect that, when applied on a class, fully implements the interface 
    /// <see cref="INotifyPropertyChanged"/> into that class, and overrides all properties to
    /// that they raise the event <see cref="INotifyPropertyChanged.PropertyChanged"/>.
    /// </summary>
    [Serializable]
    [IntroduceInterface(typeof(INotifyPropertyChanged), OverrideAction = InterfaceOverrideAction.Ignore)]
    [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Strict)]
    public sealed class NotifyPropertyChangedAttribute : InstanceLevelAspect, INotifyPropertyChanged
    {
        /// <summary>
        /// Field bound at runtime to a delegate of the method <c>OnPropertyChanged</c>.
        /// </summary>
        [ImportMember("NotifyPropertyChanged", IsRequired = false)]
        public Action<string> NotifyPropertyChangedMethod;

        /// <summary>
        /// Method introduced in the target type (unless it is already present);
        /// raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [IntroduceMember(Visibility = Visibility.Family, IsVirtual = true, OverrideAction = MemberOverrideAction.Ignore)]
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this.Instance, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Method introduced in the target type (unless it is already present);
        /// Untested
        /// </summary>
        [IntroduceMember(Visibility = Visibility.Public, IsVirtual = true, OverrideAction = MemberOverrideAction.Ignore)]
        public void NotifyPropertyChangedAll()
        {
            foreach (PropertyInfo pi in this.Instance.GetType().GetProperties(BindingFlags.Public))
            {
                NotifyPropertyChanged(pi.Name);
            }
        }

        /// <summary>
        /// Event introduced in the target type and fails if it is already there;
        /// raised whenever a property has changed.
        /// </summary>
        [IntroduceMember(OverrideAction = MemberOverrideAction.Fail)]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method intercepting any call to a property setter.
        /// </summary>
        /// <param name="args">Aspect arguments.</param>
        [OnLocationSetValueAdvice, MulticastPointcut(Targets = MulticastTargets.Property, Attributes = MulticastAttributes.Instance)]
        public void OnPropertySet(LocationInterceptionArgs args)
        {
            // Don't go further if the new value is equal to the old one.
            // (Possibly use object.Equals here).
            if (args.Value == args.GetCurrentValue())
                return;

            // Actually sets the value.
            args.ProceedSetValue();

            // Invoke method OnPropertyChanged (our, the base one, or the overridden one).
            NotifyPropertyChangedMethod.Invoke(args.Location.Name);
        }
    }
}