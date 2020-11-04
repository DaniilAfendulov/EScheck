using System;
using System.Collections.Generic;

namespace EScheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double[], double>[] funcs = LinearEquations(new double[][]
            {
                new double[]{ 1, 1, 2, -1 },
                new double[]{ 2, 1, -3, 1 },
                new double[]{ 1, 1, 1, 1 }
            });
            // equivalent records:
            //Func<double[], double>[] funcs = new Func<double[], double>[]
            //{
            //    /*(x)=>   x[0]+x[1]+2*x[2]-x[3],
            //    (x)=>   2*x[0]+x[1]-3*x[2]+x[3],
            //    (x)=>   x[0]+x[1]+x[2]+x[3]*/
            //  /*LinearEquation(new double[]{ 1, 1, 2, -1 }),
            //    LinearEquation(new double[]{ 2, 1, -3, 1 }),
            //    LinearEquation(new double[]{ 1, 1, 1, 1 })*/
            //};


            // свободные переменные
            double[] fv = new double[] { 8 };    // x1

            double[][] jor = new double[][]
            {
                new double[]{ 2,1,1,2,-1},      // x4
                new double[]{ 6,2,1,-3,1},      // x2
                new double[]{ 7,1,1,1,1},       // x3
                new double[]{ 0,-2,-1,1,1}      // f
            };

            // Шаги жорданова исключения
            // Условие вторым параметром убирает ненужные элементы
            // В данном случае это столбцы
            List<int> jExec = new List<int>();
            Console.WriteLine("Изначальная таблица");
            Print(jor);
            Print(jor = JordanStep(jor, 2, 3), IsJOneOf(new int[] { 3 }));
            Print(jor = JordanStep(jor, 0, 1), IsJOneOf(new int[] { 3, 1 }));
            Print(jor = JordanStep(jor, 1, 2), IsJOneOf(new int[] { 3, 1, 2 }));
            Print(jor = JordanStep(jor, 1, 4), IsJOneOf(new int[] { 3, 1, 2 }));
            Print(jor = JordanStep(jor, 0,4),  IsJOneOf(new int[] { 3, 1, 2 }));

            Console.WriteLine("введенная таблица:");
            jor = new double[][]
            {
                new double[]{ 37.0/8,7.0/8},       // x4
                new double[]{ 17.0/8,3.0/8},       // x2
                new double[]{ 1.0/4,-1.0/4},       // x3
                new double[]{ 9.0/4,-5.0/4}        // f
            };
            Print(jor) ;

            // базисные переменные выраженные через свободные
            double[] bv = BasicVars(fv, jor);

            // подстановка переменных 
            // x1, x2, x3, x4, x5 *Главное порядок!!!
            double[] variables = new double[] { fv[0], bv[1], bv[2], bv[0] };

            // ответный столбец
            double[] ans = new double[] { 2,6,7 };

            Console.WriteLine("Результат проверки");
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
                ans -= coef[i] * fv[i - 1];
            }
            return ans;
        }

        /// <summary>
        /// Method of creation Linear Equation by a coefficient array
        /// </summary>
        /// <param name="coef">a coefficient array</param>
        /// <returns></returns>
        static Func<double[], double> LinearEquation(double[] coef)
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

        /// <summary>
        /// Make Jordan step
        /// </summary>
        /// <param name="coef"></param>
        /// <param name="ind1"></param>
        /// <param name="ind2"></param>
        /// <returns></returns>
        static double[][] JordanStep(double[][] coef, int ind1, int ind2)
        {
            double[][] ans = new double[coef.Length][];
            for (int i = 0; i < coef.Length; i++)
            {
                ans[i] = new double[coef[i].Length];
                for (int j = 0; j < coef[i].Length; j++)
                {
                    if (i == ind1 && j == ind2)
                    {
                        ans[i][j] = 1.0 / coef[ind1][ind2];
                        continue;
                    }
                    if (i == ind1)
                    {
                        ans[i][j] = coef[i][j] / coef[ind1][ind2];
                        continue;
                    }
                    if (j == ind2)
                    {
                        ans[i][j] = -coef[i][j] / coef[ind1][ind2];
                        continue;
                    }
                    ans[i][j] = coef[i][j] - (coef[i][ind2] * coef[ind1][j]) / coef[ind1][ind2];
                }
            }
            return ans;
        }

        /// <summary>
        /// Выводит в консоль значения массивов(разделяя табом и строками),
        /// исключая элементы удовлетв условию для индексов
        /// </summary>
        /// <param name="arr">массив, который нужно вывести</param>
        /// <param name="exep">условие для индексов</param>
        static void Print(double[][] arr, Func<int, int, bool> exep)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (exep(i,j))
                    {
                        continue;
                    }
                    Console.Write(Math.Round(arr[i][j],4)+"\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void Print(double[][] arr) => Print(arr, (i, j) => false);

        static Func<int,int,bool> IsJOneOf(int[] nums)
        {
            return (i, j) =>
            {
                foreach (var num in nums)
                {
                    if (num == j)
                    {
                        return true;
                    }
                }
                return false;
            };
        }
    }
}
