using System;
using PostSharp.Aspects;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [PersistOnSetAspect(AttributeExclude = true)]
    public class PersistOnSetAspect : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            if (args.Value == args.GetCurrentValue())
                return;
            var persistentOjbect = args.Instance as IPersist;
            // Ignore sets during load
            if (persistentOjbect != null && persistentOjbect.IsLoaded)
                persistentOjbect.Save();

            args.ProceedSetValue();
        }
    }

    public interface IPersist
    {
        void Load();
        void Save();
        bool IsLoaded { get; }
    }
}