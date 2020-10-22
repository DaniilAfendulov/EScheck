using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEScheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, double, double, double, double>[] funcs = new Func<double, double, double, double, double>[]
            {
                (x1,x2,x3,x4)=>7 * x1 - 4 * x2 - 4 * x3 + x4,
                (x1,x2,x3,x4)=>5 * x1 - 4 * x3 - x4,
                (x1,x2,x3,x4)=>-1 * x1 + 2 * x2 - x4
            };

            // базисные переменные выраженные через свободные
            double bv1(double x1, double x2) => (-5.0 ) - (-5.0 / 2 * x1) - (3.0 / 2 * x2);
            double bv2(double x1, double x2) => (-3.0 ) - (-2.0 * x1) - (1.0  * x2);

            // свободные переменные
            double fv1 = 1;
            double fv2 = 2;

            // подстановка переменных 
            double[] variables = new double[] { bv2(fv1, fv2), fv1, bv1(fv1, fv2), fv2 };

            // ответный столбец
            double[] ans = new double[] { -1,5,3 };

            LESCheck(funcs, variables, ans);
            Console.ReadLine();
        }

        static void LESCheck(Func<double, double, double, double, double>[] funcs, double[] roots, double[] ans)
        {
            for (int i = 0; i < funcs.Length; i++)
            {
                Console.WriteLine(funcs[i](roots[0], roots[1], roots[2], roots[3]) + "\t" + ans[i]);
            }
        }
    }
}
