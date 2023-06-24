using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TestFormulatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();

            var sw = Stopwatch.StartNew();
            int num;

            // Question 1
            var q1_1 = Question1.multiply(-3, 2);
            Console.WriteLine("Question1: Original " + q1_1.ToString());
            var q1_2 = Question1.multiply_fix(-3, 2);
            Console.WriteLine("Question1: Fix " + q1_1.ToString());
            var q1_3 = Question1.multiply_simplyfy(-3, 2);
            Console.WriteLine("Question1: Simplyfy " + q1_1.ToString());
            var q1_4 = Question1.multiply_normal(-3, 2);
            Console.WriteLine("Question1: Normal " + q1_1.ToString());

            Console.WriteLine();
            Console.WriteLine();

            // Question 2
            var q2_q1 = Question2.Q1();
            Console.WriteLine();
            Console.WriteLine("Question2: Join both table by ID");
            Console.WriteLine("Name                Name");
            foreach (var row in q2_q1)
            {
                Console.WriteLine($"{row.Item1.PadRight(20, ' ')}{row.Item2}");
            }
            var q2_q2 = Question2.Q2();
            Console.WriteLine();
            Console.WriteLine("Question2: Left Join both table by ID");
            Console.WriteLine("Name                Name");
            foreach (var row in q2_q2)
            {
                Console.WriteLine($"{row.Item1.PadRight(20, ' ')}{row.Item2 ?? "<null>"}");
            }
            var q2_q3 = Question2.Q3();
            Console.WriteLine();
            Console.WriteLine("Question2: Select both table by ID");
            Console.WriteLine("Name                Name");
            foreach (var row in q2_q3)
            {
                Console.WriteLine($"{row.Item1.PadRight(20, ' ')}{row.Item2}");
            }

            Console.WriteLine();
            Console.WriteLine();

            // Question 3
            Question3.Node node = new Question3.Node
            {
                left = new Question3.Node(),
                right = new Question3.Node
                {
                    right = new Question3.Node()
                }
            };
            var q3_1 = Question3.calculateMaxDepth(node);
            Console.WriteLine("Question3: Using Recursive " + q3_1.ToString());


            var q3_2 = Question3.CalculateMaxDepthNoRecursive(node);
            Console.WriteLine("Question3: Using Non Recursive " + q3_2.ToString());

            Console.WriteLine();
            Console.WriteLine();

            // Question 4
            List<object> bagALarge = new List<object>();
            List<object> bagA = new List<object>();
            List<object> bagBLarge = new List<object>();
            List<object> bagB = new List<object>();
            int shortBag = 50;
            int largeBag = 10000;
            int numBag = 0;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            for (num = 1; num < largeBag; num++)
            {
                bagALarge.Add(num);
                while (bagBLarge.Contains(numBag))
                {
                    numBag = rnd.Next(largeBag);
                }
                bagBLarge.Add(numBag);
            }
            for (num = 1; num < shortBag; num++)
            {
                bagA.Add(num);
                while (bagB.Contains(numBag))
                {
                    numBag = rnd.Next(num, shortBag);
                }
                bagB.Add(rnd.Next(num, shortBag));
            }
            // avoiding initialize list contains
            sw.Reset();
            sw.Start();
            var q4_1 = Question4.intersect(bagALarge, bagB);
            sw.Stop();
            Console.WriteLine($"Question4: Case 1 Original {sw.Elapsed.TotalMilliseconds} ms");
            sw.Reset();
            sw.Start();
            var q4_2 = Question4.intersect_join(bagALarge, bagB);
            sw.Stop();
            Console.WriteLine($"Question4: Case 1 Join {sw.Elapsed.TotalMilliseconds} ms");
            sw.Reset();
            sw.Start();
            var q4_3 = Question4.intersect_unbuffered(bagALarge, bagB).ToList();
            sw.Stop();
            Console.WriteLine($"Question4: Case 1 Unbuffered {sw.Elapsed.TotalMilliseconds} ms");
            sw.Reset();
            sw.Start();
            var q4_4 = Question4.intersect(bagA, bagBLarge);
            sw.Stop();
            Console.WriteLine($"Question4: Case 2 Original {sw.Elapsed.TotalMilliseconds} ms");
            sw.Reset();
            sw.Start();
            var q4_5 = Question4.intersect_join(bagA, bagBLarge);
            sw.Stop();
            Console.WriteLine($"Question4: Case 2 Join {sw.Elapsed.TotalMilliseconds} ms");
            sw.Reset();
            sw.Start();
            var q4_6 = Question4.intersect_unbuffered(bagA, bagBLarge).ToList();
            sw.Stop();
            Console.WriteLine($"Question4: Case 2 Unbuffered {sw.Elapsed.TotalMilliseconds} ms");
            sw.Reset();
            sw.Start();

            Console.WriteLine();
            Console.WriteLine();

            // Question 5
            int formulatrixMin = 2,
                formulatrixMax = 100;
            Console.WriteLine($"Question5: Calculate Formulatrix/Prime Number between {formulatrixMin} - {formulatrixMax}");
            Console.Write($"Question5: Number is ");
            for (num = formulatrixMin; num <= formulatrixMax; num++)
            {
                if (Question5.IsPrime(num))
                    Console.Write($"{num} ");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press enter to exit !");

            Console.ReadLine();
        }
    }
}
