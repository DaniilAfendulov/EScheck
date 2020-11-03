using System;

namespace LEScheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double[], double>[] funcs = LinearEquations(new double[][] 
            {
                new double[]{ 1, 1, 1, 0, 0, 0 },
                new double[]{ 1, 3, 0, 1, 0, 0 },
                new double[]{ 1, 0, 0, 0, 1, 0 },
                new double[]{ 1, 2, 0, 0, 0, 1 }
            });
            //    new Func<double[], double>[]
            //{
            //    /*(x)=>   x[0]+x[1]+x[2],
            //    (x)=>   x[0]+3*x[1]+x[3],
            //    (x)=>   x[0]+x[4],
            //    (x)=>   x[0]+2*x[1]+x[5],*/
            //    /*LinearEquation(new double[]{ 1,1,1,0,0,0}),
            //    LinearEquation(new double[]{ 1,3,0,1,0,0}),
            //    LinearEquation(new double[]{ 1,0,0,0,1,0}),
            //    LinearEquation(new double[]{ 1,2,0,0,0,1})*/
            //};


            // свободные переменные
            double[] fv = new double[] { 1, 2 };

            // базисные переменные выраженные через свободные
            double[] bv = new double[]
            {
                BasicVar(new double[] { 1,-1,1},    fv),
                BasicVar(new double[] { 2,2,-3},    fv),
                BasicVar(new double[] { 4,1,0},         fv),
                BasicVar(new double[] { 4,-1,2},     fv)
            };

            // подстановка переменных 
            double[] variables = new double[] { bv[2],bv[0],fv[0],bv[1],fv[1],bv[3] };

            // ответный столбец
            double[] ans = new double[] { 5,9,4,8};

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

        /// <summary>
        /// Method of creation Linear Equation by a coefficient array
        /// </summary>
        /// <param name="coef">a coefficient array</param>
        /// <returns></returns>
        static Func<double[],double> LinearEquation(double[] coef)
        {
            return (x) =>
            {
                double ans = 0;
                for (int i = 0; i < coef.Length; i++)
                {
                    ans += coef[i] * x[i];
                }
                return ans;
            };
        }

        /// <summary>
        /// Method of creation Linear Equation System by a coefficient matrix
        /// </summary>
        /// <param name="coef">a coefficient matrix</param>
        /// <returns></returns>
        static Func<double[], double>[] LinearEquations(double[][] coef)
        {
            Func<double[], double>[] funcs = new Func<double[], double>[coef.Length];
            for (int i = 0; i < funcs.Length; i++)
            {
                funcs[i] = LinearEquation(coef[i]);
            }
            return funcs;
        }
    }
}
