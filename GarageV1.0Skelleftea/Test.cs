using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageV1._0Skelleftea
{
    class Test<T> : IEnumerable<T> where T : Vehicle
    {
        List<T> test = new List<T>();


        public IEnumerator<T> GetEnumerator()
        {
            foreach (T t in test)
                yield return t;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /*IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }*/

        
    }
}
