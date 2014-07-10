using Rhyous.ServiceManager.Aspects;

#if DEBUG
[assembly: MethodTraceAspect(AttributePriority = 0)]
namespace Rhyous.ServiceManager { }
#endif