using System;

namespace LEScheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double[],double>[] funcs = new Func<double[], double>[]
            {
                (x)=>   5*x[0]     +2*x[1]   +x[2],
                (x)=>   6*x[0]   +12*x[1]   +x[3]        
            };


            // свободные переменные
            double[] fv = new double[] { 1, 2 };

            // базисные переменные выраженные через свободные
            double[] bv = new double[]
            {
                BasicVar(new double[] {4,1.0/5,2.0/5 }, fv),
                BasicVar(new double[] { 48, -6.0 / 5, 48.0 / 5 }, fv)       
            };

            // подстановка переменных 
            double[] variables = new double[] { bv[0],fv[1],fv[0],bv[1]};

            // ответный столбец
            double[] ans = new double[] { 20,72};

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

        /// <summary>
        /// Считает значение базисной переменной по коэффициентам 
        /// перед свободными переменными и самим этим переменным. 
        /// </summary>
        /// <param name="coef">коэффициенты перед свободными переменными.(Первый эл-свободный коэф)</param>
        /// <param name="fv">значения свободных переменных</param>
        /// <returns></returns>
        static double BasicVar(double[] coef, double[] fv)
        {
            double ans = coef[0];
            for (int i = 1; i < coef.Length; i++)
            {
                ans -= coef[i]*fv[i-1];
            }
            return ans;
        }
    }
}
