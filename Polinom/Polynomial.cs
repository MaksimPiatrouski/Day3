using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public sealed class Polynomial
    {
        private double[] array;

        public Polynomial(params double[] array)
        {
            this.array = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                this.array[i] = array[i];
            }
        }

        public double[] Array
        {
            get { return array; }
        }

        public override string ToString()
        {
            StringBuilder strB = new StringBuilder();
            string str;
            for (int i = Array.Length - 1; i >= 0; i--)
            {
                if (Array[i] != 0)
                {
                    if (Array[i] >= 0)
                        strB.Append("+");
                    if (i == 0)
                        strB.Append(String.Format("{0}", Array[i]));
                    else if (i == 1)
                        strB.Append(String.Format("{0}x", Array[i]));
                    else
                        strB.Append(String.Format("{0}x^" + i, Array[i]));
                }
            }
            str = strB.ToString();
            if (str.ElementAt(0).ToString() == "+")
                str = str.Remove(0, 1);
            if (str.Length != 0)
                return str;
            else
                return "0";
        }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Polynomial)obj);
        }

        public bool Equals(Polynomial p)
        {
            if (ReferenceEquals(null, p)) return false;
            if (ReferenceEquals(this, p)) return true;
            if (this.array.Length != p.array.Length)
                return false;
            for (var i = 0; i < this.array.Length; i++)
            {
                if (!this.array[i].Equals(p.array[i]))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = Array.Length;
            for (int i = 0; i < Array.Length; i++)
            {
                hashCode = unchecked(hashCode * 314159 + (int)Math.Round(Array[i], 0));
            }
            return hashCode;
        }

        public static bool operator ==(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, p2)) return true;
            if (ReferenceEquals(p1, null)) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Polynomial p1, Polynomial p2)
        {
            if (p1 != p2) return true;
            return false;
        }

        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null)) throw new ArgumentNullException();
            if (ReferenceEquals(p2, null)) throw new ArgumentNullException();

            double[] k1 = (double[])p1.Array.Clone();
            double[] k2 = (double[])p2.Array.Clone();
            double[] result;

            if (k1.Length >= k2.Length)
            {
                result = k1;
                for (int i = 0; i < k2.Length; i++)
                {
                    result[i] += k2[i];
                }
            }
            else
            {
                result = k2;
                for (int i = 0; i < k1.Length; i++)
                {
                    result[i] += k1[i];
                }
            }
            return new Polynomial(result);
        }

        public static Polynomial operator +(Polynomial p1, double x)
        {
            if (ReferenceEquals(p1, null)) throw new ArgumentNullException();

            double[] result = (double[])p1.Array.Clone();
            result[0] += x;
            return new Polynomial(result);
        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            return p1 + (p2 * (-1));
        }

        public static Polynomial operator -(Polynomial p1, double x)
        {
            return p1 + (x * (-1));
        }

        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            double[] k1 = (double[])p1.Array.Clone();
            double[] k2 = (double[])p2.Array.Clone();
            double[] result;
            if (k1.Length >= k2.Length)
            {
                result = new double[k1.Length + k2.Length];
                for (int i = 0; i < k1.Length; i++)
                {
                    for (int j = 0; j < k2.Length; j++)
                    {
                        result[i + j] += k1[i] * k2[j];
                    }
                }
            }
            else
            {
                result = new double[k2.Length * k2.Length + k1.Length];
                for (int i = 0; i < k1.Length; i++)
                {
                    for (int j = 0; j < k2.Length; j++)
                    {
                        result[i * k2.Length + j] = k1[i] * k2[j];
                    }
                }
            }
            return new Polynomial(result);
        }

        public static Polynomial operator *(Polynomial p1, double x)
        {
            double[] result = (double[])p1.Array.Clone();
            for (int i = 0; i < result.Length; i++)
            {
                result[i] *= x;
            }
            return new Polynomial(result);
        }
    }
}