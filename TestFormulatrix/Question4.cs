using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TestFormulatrix
{
    /*
     Question :
        The following method will find the intersection (duplicates) of two given sets.
        What would be the effect on performance in these two cases:
                        bagA                            bagB
            - Case 1	Has LARGE number of elements	Has SMALL number of elements
            - Case 2	Has SMALL number of elements	Has LARGE number of elements

     Answer :
        - Case 1 will be long loop bacause bagA has a Much elements to compare agains bagB
        - Case 1 will be short loop bacause bagA has a small elements to compare agains bagB
    */
    class Question4
    {
        public static IList intersect(IList bagA, IList bagB)
        {
            IList result = new ArrayList();

            foreach (object o in bagA)
            {
                if (bagB.Contains(o))
                    result.Add(o);
            }

            return result;
        }

        public static IList intersect_join(IEnumerable<object> bagA, IEnumerable<object> bagB)
        {
            return bagA.Join(bagB, e => e, e => e, (a, b) => a)
                .GroupBy(g => g)
                .Select(s => s.Key)
                .ToList();
        }

        public static IEnumerable<object> intersect_unbuffered(IList bagA, IList bagB)
        {
            foreach (object o in bagA)
            {
                if (bagB.Contains(o))
                    yield return o;
            }
        }
    }
}
