using System;

namespace EScheck
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
            // equivalent records:
            //Func<double[], double>[] funcs = new Func<double[], double>[]
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
            double[] fv = new double[] { 1, 2 };    // x1,x3


            // базисные переменные выраженные через свободные
            double[] bv = BasicVars(fv, new double[][]
            {
                new double[]{ 5, 1, 1 },    // x2
                new double[]{ -6, -2, -3 }, // x4
                new double[]{ 4, 1, 0 },    // x5
                new double[]{ -2, -1, -2 }  // x6
            });

            // подстановка переменных 
            // x1, x2, x3, x4, x5 *Главное порядок!!!
            double[] variables = new double[] { fv[0],  bv[0], fv[1], bv[1], bv[2], bv[3]};

            // ответный столбец
            double[] ans = new double[] { 5,9,4,8};

            ESCheck(funcs, variables, ans);
            Console.ReadLine();
        }
        /// <summary>
        /// Inserts roots in the functions and show result and expected answer in Console
        /// </summary>
        /// <param name="funcs">equation system or set of functions</param>
        /// <param name="roots">roots of equation system</param>
        /// <param name="ans">expected answer</param>
        static void ESCheck(Func<double[], double>[] funcs, double[] roots, double[] ans)
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

        /// <summary>
        /// Computing an array of basic variables by free variables and basic vars coefficient
        /// </summary>
        /// <param name="fv">free variables</param>
        /// <param name="coef">basic variables coefficient</param>
        /// <returns></returns>
        static double[] BasicVars(double[] fv, double[][] coef)
        {
            double[] bv = new double[coef.Length];
            for (int i = 0; i < bv.Length; i++)
            {
                bv[i] = BasicVar(coef[i], fv);
            }
            return bv;
        }
    }
}
