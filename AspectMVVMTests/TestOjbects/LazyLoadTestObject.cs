using System.Collections.Generic;
using AspectMVVM;

namespace AspectMVVMTests.TestOjbects
{
    public class LazyLoadTestObject
    {
        [LazyLoad]
        public List<int> Numbers { get; set; }
    }

    public class LazyLoadChangeDefaultValueObject
    {
        [LazyLoad(true)]
        public bool TrueFalse { get; set; }
    }

    public class LazyLoadPropertyCtorNotEmtpyObject
    {
        [LazyLoad]
        public ParamRequredClass Name { get; set; }

        public class ParamRequredClass
        {
            ParamRequredClass(string inString)
            {
                Name = inString;
            }

            public string Name { get; set; }
        }
    }
}
