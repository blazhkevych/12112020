// delegate, action, func, Predicate
using System;
using System.Threading;

namespace _12112020
{
    public delegate double MathMethod(double a, double b); //створює обєкт в якому можна зберігати ссилку на методи

    class MyMath
    {
        public double Sum(double a, double b)
        {
            Console.WriteLine('+');
            return a + b;
        }
        public double Sub(double a, double b)
        {
            Console.WriteLine('-');
            return a - b;
        }
        public static double Mult(double a, double b)
        {
            Console.WriteLine('*');
            return a * b;
        }
        public static double Div(double a, double b)
        {
            Console.WriteLine('/');
            return a / b;
        }
    }

    class Program
    {
        static void Test1()
        {
            //10+5 = 15
            Console.WriteLine("Enter string");
            string str = Console.ReadLine();
            char Sign = ' ';
            try
            {
                if (str.Contains('+')) Sign = '+';
                else if (str.Contains('-')) Sign = '-';
                else if (str.Contains('*')) Sign = '*';
                else if (str.Contains('/')) Sign = '/';
                else Sign = ' ';
                string[] numbers = str.Split(Sign, StringSplitOptions.RemoveEmptyEntries);
                double a = double.Parse(numbers[0]);
                double b = double.Parse(numbers[1]);

                MathMethod mathod = null;
                MyMath algebra = new MyMath();

                switch (Sign)
                {
                    case '+':
                        mathod = new MathMethod(algebra.Sum); // старий запис
                        //Console.WriteLine(a+b);
                        break;
                    case '-':
                        mathod = algebra.Sub;
                        break;
                    case '*':
                        mathod = MyMath.Mult;
                        break;
                    case '/':
                        mathod = MyMath.Div;
                        break;
                }
                //Console.WriteLine(mathod(a, b));
                Console.WriteLine(mathod?.Invoke(a, b));

                Console.WriteLine("-------------------------------------------------");
                MathMethod math = null;
                math = MyMath.Mult; // ДОДАЄМО МЕТОДИ
                math += MyMath.Div;
                math += algebra.Sub;
                math += algebra.Sum;
                Console.WriteLine(math?.Invoke(a, b));
                math -= algebra.Sum; // видаляємо методи
                foreach (MathMethod fun in math.GetInvocationList())
                {
                    Console.WriteLine(fun(a, b));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        delegate void ShowMethod(double a);
        delegate void Show();

        static void Test2()
        {
            //10+5 = 15
            Console.WriteLine("Enter string");
            string str = Console.ReadLine();
            char Sign = ' ';
            try
            {
                if (str.Contains('+')) Sign = '+';
                else if (str.Contains('-')) Sign = '-';
                else if (str.Contains('*')) Sign = '*';
                else if (str.Contains('/')) Sign = '/';
                else Sign = ' ';
                string[] numbers = str.Split(Sign, StringSplitOptions.RemoveEmptyEntries);
                double a = double.Parse(numbers[0]);
                double b = double.Parse(numbers[1]);

                MathMethod mathod = null;

                switch (Sign)
                {
                    case '+':
                        mathod = delegate (double a, double b) { return a + b; };
                        break;
                    case '-':
                        mathod = delegate (double a, double b) { return a - b; };
                        break;
                    case '*':
                        mathod = delegate (double a, double b) { return a * b; };
                        break;
                    case '/':
                        mathod = delegate (double a, double b) { return a / b; };
                        break;
                }
                //Console.WriteLine(mathod(a, b));
                Console.WriteLine(mathod?.Invoke(a, b));

                ShowMethod sm = delegate (double d)
                {
                    Console.WriteLine($"d={d}");
                };

                Show show = delegate { Console.WriteLine($"Hello"); };
                show += delegate { Console.WriteLine($"By"); };

                sm(159.36);
                show();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        static void Test3()
        {
            //10+5 = 15
            Console.WriteLine("Enter string");
            string str = Console.ReadLine();
            char Sign = ' ';
            try
            {
                if (str.Contains('+')) Sign = '+';
                else if (str.Contains('-')) Sign = '-';
                else if (str.Contains('*')) Sign = '*';
                else if (str.Contains('/')) Sign = '/';
                else Sign = ' ';
                string[] numbers = str.Split(Sign, StringSplitOptions.RemoveEmptyEntries);
                double av = double.Parse(numbers[0]);
                double bv = double.Parse(numbers[1]);

                MathMethod mathod = null;

                switch (Sign)
                {
                    case '+':
                        mathod = (a, b) => a + b;
                        break;
                    case '-':
                        mathod = (a, b) => a - b;
                        break;
                    case '*':
                        mathod = (a, b) => a * b;
                        break;
                    case '/':
                        mathod = (a, b) => a / b;
                        break;
                }
                //Console.WriteLine(mathod(a, b));
                Console.WriteLine(mathod?.Invoke(av, bv));

                ShowMethod sm = d => Console.WriteLine($"d={d}");

                Show show = () => Console.WriteLine($"Hello");

                show += () => Console.WriteLine($"By");

                sm(159.36);
                show();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        static void Test4()
        {

            Action show;
            show = () => Console.WriteLine(DateTime.Now.Day);
            show += () => Console.WriteLine(DateTime.Now.Month);
            show();

            Action<int> showOne;
            showOne = (d) => Console.WriteLine(d * d);
            showOne(123);


            Action<int, double> showTwo;
            showTwo = (x, y) => Console.WriteLine(x * y);
            showTwo(10, 5.96);
        }

        static void Test5()
        {

            Func<int> sq;
            sq = () => 10 * 10;
            Console.WriteLine(sq());

            Func<int, int> cube;
            cube = i => i * i * i;
            Console.WriteLine(cube(5));
            cube = i => (int)Math.Pow(i, 3);
            //int Cube(int) { x* x*x}

            Func<int, double> cubeD;
            cubeD = i => Math.Pow(i, 3);

            Func<double, int, double> mult = (x, y) => x * y;
            Console.WriteLine(mult(10.2, 2));

        }

        static void Test6()
        {
            Predicate<int> sq;
            sq = x => x > 0;
            Console.WriteLine(sq(10));

            Func<int, bool> neg = (x) => x < 0;
            Console.WriteLine(neg(5));

            Console.WriteLine("------------------------");

        }

        static int CountPos(int[] arr)
        {
            int k = 0;
            foreach (var elem in arr)
                if (elem > 0) k++;
            return k;
        }

        static int CountNeg(int[] arr)
        {
            int k = 0;
            foreach (var elem in arr)
                if (elem > 0) k++;
            return k;
        }

        static int Count(int[] arr, Predicate<int> Test)
        {
            int k = 0;
            foreach (var elem in arr)
                if (Test(elem)) k++;
            return k;
        }

        static void Test7()
        {
            int[] mas = { 10, -10, 20, 30, -50, 60, 70, 0, 13, 19 };
            Console.WriteLine("+ " + CountPos(mas));
            Console.WriteLine("+ " + Count(mas, x => x > 0));
            Console.WriteLine("- " + CountNeg(mas));
            Console.WriteLine("- " + Count(mas, x => x < 0));
            Console.WriteLine("0 " + Count(mas, x => x == 0));
        }

        static void Main(string[] args)
        {
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            //Test6();
            Test7();
            //Console.WriteLine("Hello World!");
        }
    }
}
