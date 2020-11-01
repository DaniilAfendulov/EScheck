using System;

namespace LEScheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double[],double>[] funcs = new Func<double[], double>[]
            {
                (x)=>   x[0]     +x[1]   -x[2]   -x[3],
                (x)=>   x[0]   -2*x[1]   -x[2]           -x[4],
                (x)=> 2*x[0]     -x[1]           +x[3]   +x[4]
            };


            // свободные переменные
            double fv1 = 1;
            double fv2 = 2;

            // базисные переменные выраженные через свободные
            double bv1 = (2.0) - (1.0 * fv1) - (-2.0 * fv2);
            double bv2 = (2.0) - (-2.0 * fv1) - (1.0 * fv2);
            double bv3 = (5.0) - (1.0 * fv1) - (1.0 * fv2);
            // подстановка переменных 
            double[] variables = new double[] { fv1,fv2,bv2,bv1,bv3};

            // ответный столбец
            double[] ans = new double[] { -4,-7, 7};

            LESCheck(funcs, variables, ans);
            Console.ReadLine();
        }

        static void LESCheck(Func<double[], double>[] funcs, double[] roots, double[] ans)
        {
            for (int i = 0; i < funcs.Length; i++)
            {
                Console.WriteLine(funcs[i](roots) + "\t" + ans[i]);
            }
        }
    }
}
