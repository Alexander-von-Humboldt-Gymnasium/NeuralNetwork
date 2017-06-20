using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkMG.Expandable
{
    public struct VectorN<T> where T : struct,
          IComparable,
          IComparable<T>,
          IConvertible,
          IEquatable<T>,
          IFormattable
    {
        public int NumberRows { get; private set; }
        public T[] Vector { get; private set; }


        public VectorN(int rows)
            :this()
        {
            NumberRows = rows;
            Vector = new T[NumberRows];
        }

        public void Insert(int row, T value)
        {
            Vector[row] = value;
        }


    }
}
