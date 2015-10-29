using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOP_Lab1
{
    class my_matrix
    {
        private float[,] matr;

        private uint size;

        public my_matrix(uint s, float[,] m)
        {
            matr = new float[s, s];
            size = s;
            matr = m;
        }

        public my_matrix(uint s)
        {
            matr = new float[s, s];
            size = s;
        }

        public my_matrix(StreamReader reader)
        {
            string line = reader.ReadLine();
            size = uint.Parse(line);
            matr = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                line = reader.ReadLine();
                uint j = 0;
                var items = line.Split();
                foreach (var item in items)
                {
                    matr[i, j++] = float.Parse(item);
                }
            }
        }

        private static void chek_size(my_matrix a, my_matrix b){
            if (a.size == b.size)
            {
                return;
            }
            throw new ArgumentException("Must be equal");
        }

        public float Norm()
        {
            float result = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result += matr[i, j] * matr[i, j];
                }
            }
            return (float)Math.Sqrt(result);
        }

        public float Normi()
        {
            float[] tmp = new float[size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tmp[i] += matr[i, j];
                }  
            }
                return tmp.Max();
        }

        public float Normj()
        {
            float[] tmp = new float[size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tmp[j] += matr[i, j];
                }  
            }
            return tmp.Max();
        }

        public static my_matrix operator +(my_matrix a, my_matrix b)
        {
            chek_size(a, b);
            my_matrix result = new my_matrix(a.size);
            for (int i = 0; i < a.size; i++)
            {
                for (int j = 0; j < a.size; j++)
                {
                    result.matr[i, j] = a.matr[i, j] + b.matr[i, j];
                }
            }
                return result;
        }

        public static my_matrix operator -(my_matrix a, my_matrix b)
        {
            chek_size(a, b);
            my_matrix result = new my_matrix(a.size);
            for (int i = 0; i < a.size; i++)
            {
                for (int j = 0; j < a.size; j++)
                {
                    result.matr[i, j] = a.matr[i, j] - b.matr[i, j];
                }
            }
            return result;
        }

        public static my_matrix operator *(my_matrix a, float b)
        {
            my_matrix result = new my_matrix(a.size);
            for (int i = 0; i < a.size; i++)
            {
                for (int j = 0; j < a.size; j++)
                {
                    result.matr[i, j] = a.matr[i, j] * b;
                }
            }
            return result;
        }

        public static my_matrix operator *(my_matrix a, my_matrix b)
        {
            chek_size(a, b);
            my_matrix result = new my_matrix(a.size);
            for (int i = 0; i < a.size; i++)
            {
                for (int j = 0; j < a.size; j++)
                {
                    for (int k = 0; k < a.size; k++)
                    {
                        result.matr[i, j] += a.matr[i, k] * b.matr[k, j];
                    }
                }
            }
            return result;
        }

        public static my_vector operator *(my_matrix a, my_vector b)
        {
            my_vector result = new my_vector(a.size);
            for (int i = 0; i < a.size; i++)
            {
                for (int j = 0; j < a.size; j++)
                {
                        result.coordinat[i] += a.matr[i, j] * b.coordinat[j];
                }
            }
            return result;
        }

        public void pow(uint power)
        {
            my_matrix tmp = new my_matrix(size);
            tmp = this;
            for (int i = 0; i < power - 1; i++)
            {
                tmp = tmp * this;
            }
            this.matr = tmp.matr;
        }

        public my_matrix Transposed()
        {
            my_matrix temp = new my_matrix(this.size);
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    temp.matr[i, j] = this.matr[j, i];
                }
            }
            return temp;
        }

        public void print_matrix()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(string.Format("{0} ", matr[i, j]));
                }
                Console.Write("\n");
            }
            Console.WriteLine("_____________");
        }

        public override string ToString()
        {
            string strMatrix = "";
            for (int i = 0; i < this.size; i++)
            {
                strMatrix += "(";
                for (int j = 0; j < this.size; j++)
                {
                    if (j == this.size - 1)
                        strMatrix += matr[i, j].ToString() + ")";
                    else strMatrix += matr[i, j].ToString() + " ";
                }
                strMatrix += "\n";
            }

            return strMatrix;
        }
    }

    class my_vector
    {
        public float[] coordinat;

        public my_vector(uint elements) //constructor that takes number of coordinats
        {
            coordinat = new float[elements];
        }

        public my_vector(float[] vector) //constructor
        {
            coordinat = vector;
        }

        public my_vector(StreamReader reader)
        {
            string line = reader.ReadLine();
            uint size = uint.Parse(line);
            coordinat = new float[size];
            line = reader.ReadLine();
            var items = line.Split();
            uint j = 0;
            foreach (var item in items)
            {
                coordinat[j++] = float.Parse(item);
            }
        }

        public void print_vector()
        {
            Console.Write(string.Join(" " , coordinat));
            Console.WriteLine("\n_____________");
        }

        public float Norm
        {
            get { return (float)Math.Sqrt(this.coordinat.Sum(c => c * c)); }
        }

        public float ManhattanNorm
        {
            get { return this.coordinat.Sum(x => Math.Abs(x)); }
        }

        public float MaximumNorm
        {
            get { return this.coordinat.Max(); }
        }

        public static my_vector operator +(my_vector a, my_vector b)
        {
            check_lenght(a, b);
            return new my_vector(a.coordinat.Zip(b.coordinat, (x, y) => x + y).ToArray());
        }

        public static my_vector operator -(my_vector a, my_vector b)
        {
            check_lenght(a, b);
            return new my_vector(a.coordinat.Zip(b.coordinat, (x, y) => x - y).ToArray()); // Zip - merges 2 array's and by using predicate
        }

        public static my_vector operator *(my_vector a, float b)
        {
            return new my_vector(a.coordinat.Select(x => x * b).ToArray()); // Select - go through array, using predicate to each member
        }

        public static float operator *(my_vector a, my_vector b)
        {
            check_lenght(a, b);
            return (a.coordinat.Zip(b.coordinat, (x, y) => x * y).ToArray().Sum());
        }

        public uint lenght
        {
            get { return (uint)this.coordinat.Length; }
        }

        private static void check_lenght(my_vector a, my_vector b)
        {
            if (a.lenght == b.lenght)
            {
                return;
            }
            throw new ArgumentException("Vector's lenght must be equal");
        }

        public override string ToString()
        {
            string strVector = "";
            strVector += "(";
            for (int i = 0; i < this.lenght; i++)
            {
                if (i == this.lenght - 1)
                    strVector += coordinat[i].ToString() + ")";
                else strVector += coordinat[i].ToString() + ",";
            }

            return strVector;
        }
    }

    class logger
    {
        private StreamWriter stream;

        public logger(StreamWriter stream)
        {
            this.stream = stream;
        }

        public void Close()
        {
            stream.Close();
        }

        public void Log(string actionType, string comment, bool ok)
        {
            DateTime dateTime = DateTime.Now;
            stream.WriteLine(
            "[{0}] [{1}] [{2}] [{3}] [{4}]",
            dateTime.ToShortDateString(),
            dateTime.ToLongTimeString(),
            actionType,
            comment,
            ok ? " OK " : "FAIL");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            logger l = new logger(new StreamWriter("output.log"));
            try
            {
                FileStream file = new FileStream("OOP_Lab1.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);
                l.Log("Attaching to input file", "OOP_Lab1.txt", true);

                l.Log("Reading stuff from", "OOP_Lab1.txt", true);
                my_matrix A = new my_matrix(reader);
                l.Log("Loaded A", string.Format("{0}", A), true);
                my_matrix B = new my_matrix(reader);
                l.Log("Loaded B", string.Format("{0}", B), true);
                my_matrix C = new my_matrix(reader);
                l.Log("Loaded C", string.Format("{0}", C), true);
                my_matrix D = new my_matrix(reader);
                l.Log("Loaded D", string.Format("{0}", D), true);
                my_vector X = new my_vector(reader);
                l.Log("Loaded X", string.Format("{0}", X), true);
                my_vector Y = new my_vector(reader);
                l.Log("Loaded Y", string.Format("{0}", Y), true);

                reader.Close();

                my_vector total = (A + B).Transposed() * X + (C - D) * Y;
                l.Log("Calculating answer", string.Format("({0} + {1})^t * {4} + ({2} - {3}) * {5}", A,B,C,D,X,Y), true);

                l.Log("Result", string.Format("{0}", total), true);

                l.Log("EuclidNorm", string.Format("{0}", total.Norm), true);
                l.Log("ManhattanNorm", string.Format("{0}", total.ManhattanNorm), true);
                l.Log("MaximumNorm", string.Format("{0}", total.MaximumNorm), true);

                l.Log("Closing program", "", true);
            }
            catch (Exception error)
            {
                l.Log("Fail", error.Message, false);
            }
            try
            {
                FileStream file = new FileStream("OOP_Lab1_Fail.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);
                l.Log("Attaching to input file", "OOP_Lab1_Fail.txt", true);

                l.Log("Reading stuff from", "OOP_Lab1_Fail.txt", true);
                my_matrix M = new my_matrix(reader);
                l.Log("Loaded A", string.Format("{0}", M), true);
                my_vector N = new my_vector(reader);
                l.Log("Loaded X", string.Format("{0}", N), true);
            }
            catch (Exception error)
            {
                l.Log("Fail", error.Message, false);
            }
            try
            {
                FileStream file = new FileStream("OOP_Lab1_Fail_Size.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);
                l.Log("Attaching to input file", "OOP_Lab1_Fail_Size.txt", true);

                l.Log("Reading stuff from", "OOP_Lab1_Fail.txt", true);
                my_matrix U = new my_matrix(reader);
                l.Log("Loaded U", string.Format("{0}", U), true);
                my_matrix V = new my_matrix(reader);
                l.Log("Loaded V", string.Format("{0}", V), true);

                U = U + V;

                l.Log("Adding matrix of different sizes", string.Format("{0} + {1}", U, V), true);

                l.Log("Closing program", "", true);
            }
            catch (Exception error)
            {
                l.Log("Fail", error.Message, false);
            }

            l.Log("Closing program", "", true);
            l.Close();
        }
    }   
}
