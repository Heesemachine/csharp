using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public abstract class TLinearSystemEquation
    {
        private int size;
        protected Double[,] data;
        protected Double[] resultVector;

        public TLinearSystemEquation(int aSize)
        {
            this.size = aSize;
            data = new Double[aSize, aSize];
            resultVector = new Double[aSize];
        }

        public TLinearSystemEquation(Double[,] aData)
        {
            //TODO Check all 2 dimenstion are equal
            this.size = aData.GetLength(0);
            data = new Double[this.size, this.size];
            for (int row = 0; row < this.size; row++)
            {
                for (int col = 0; col < this.size; col++)
                {
                    this.data[row, col] = aData[row, col];
                }
            }
        }

        public override string ToString()
        {
            String S = "";
            for (int row = 0; row < this.size; row++)
            {
                for (int col = 0; col < this.size; col++)
                {
                    S = S + data[row, col] + " * x" + (col + 1) + (col == this.size - 1 ? " = " : " + ");
                }
                if (resultVector != null)
                {
                    S = S + "=" + resultVector[row];
                }
                else
                {
                    S = S + " 0 ";
                }
                S = S + "\n";
            }
            return S;
        }

        public void printMatrix()
        {
            for (int row = 0; row < this.size; row++)
            {
                String S = "";
                for (int col = 0; col < this.size; col++)
                {
                    S = S + data[row, col] + " * x" + (col + 1) + (col < this.size - 1 ? " + " : "");
                }
                if (resultVector != null)
                {
                    Console.WriteLine(S + " = " + resultVector[row]);
                }
            }
            Console.WriteLine("Det = " + this.getDet());
            Double[] result = this.solve();
            Console.WriteLine("Result = [");
            for (int x = 0; x < result.Length; x++)
            {
                Console.WriteLine("\tx" + (x + 1) + " = " + result[x]);
            }
            Console.WriteLine("]");
        }

        public void fillData(Double[,] data, Double[] resultVector)
        {
            for (int row = 0; row < this.size; row++)
            {
                for (int col = 0; col < this.size; col++)
                {
                    this.data[row, col] = data[row, col];
                }
            }
            for (int i = 0; i < this.size; i++)
            {
                this.resultVector[i] = resultVector[i];
            }
        }

        public abstract Double[] solve();
        public abstract Double getDet();


        public Boolean checkVector(Double[] results)
        {
            for (int row = 0; row < this.size; row++)
            {
                Double rowResult = 0;
                for (int col = 0; col < this.size; col++)
                {
                    Double r = data[row, col] * results[col];
                    rowResult = rowResult + r;
                    Console.Write(data[row, col] + " * " + results[col] + "=" + r + "; ");
                }
                Console.WriteLine(" => " + resultVector[row] + " vs " + rowResult);
                if (rowResult != resultVector[row])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class T2DMatrix : TLinearSystemEquation
    {

        public T2DMatrix() : base(2) { }
        public T2DMatrix(Double[,] aData) : base(aData) { }

        public override Double[] solve()
        {
            Console.WriteLine(this.resultVector[1]);
            Double[,] x1m_data = new Double[2, 2] {
      { this.resultVector[0], this.data[0,1] },
      { this.resultVector[1], this.data[1,1] },
    };

            Double[,] x2m_data = new Double[2, 2] {
      { this.data[0,0], this.resultVector[0] },
      { this.data[1,0], this.resultVector[1] },
    };


            T2DMatrix x1m = new T2DMatrix(x1m_data);
            T2DMatrix x2m = new T2DMatrix(x2m_data);

            Double det = this.getDet();
            Double x1_det = x1m.getDet();
            Double x2_det = x2m.getDet();

            Console.WriteLine("============");
            Console.WriteLine(x1m);
            Console.WriteLine(x2m);
            Console.WriteLine("============");

            Console.WriteLine("x1_det = " + x1_det);
            Console.WriteLine("x2_det = " + x2_det);

            return new Double[2] { x1_det / det, x2_det / det };
        }

        public override Double getDet()
        {
            return this.data[0, 0] * this.data[1, 1] - this.data[0, 1] * this.data[1, 0];
        }
    }

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello World");

            T2DMatrix m = new T2DMatrix();
            m.fillData(new Double[,] {
      {3, 4},
      {6, 7}
    }, new Double[] { 5, 8 });

            m.printMatrix();

            Console.WriteLine(m.checkVector(new Double[] { -1, 2 }));
        }
    }
}
