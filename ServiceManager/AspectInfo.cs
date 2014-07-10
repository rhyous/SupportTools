
using Common.Aspects;

#if DEBUG
[assembly: MethodTraceAspect(AttributePriority = 0)]
namespace Rhyous { }
#endif