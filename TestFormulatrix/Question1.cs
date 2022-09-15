using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestFormulatrix
{
    /*
     * QUESTIONS:
        -	What is wrong with the above code?
        -	How would you fix it (without using the “*” or “/” operator)?
        -	As part of our development process we test all methods at a code level. Which input values would you use to do the testing?
        -	After you fix the above code, what other error conditions should you look for?
        -	For later discussion: What else worries you as you fix this problem
       Answer:
        - Problem when the x below zero
        - In code multiply_fix
     */
    class Question1
    {
        ///
        /// multiplies x times y
        ///
        public static int multiply(int x, int y)
        {
            int total = 0;

            while (x > 0)
            {
                total += y;
                x--;
            }

            return total;
        }

        public static int multiply_fix(int x, int y)
        {
            int z;
            bool isNeg = false;
            if (x < 0)
            {
                isNeg = true;
                z = -x;
            }
            else z = x;
            int total = 0;

            while (z > 0)
            {
                total += y;
                z--;
            }

            return (isNeg ? -total : total);
        }

        public static int multiply_simplyfy(int x, int y)
        {
            return Enumerable.Repeat(x, y).Sum();
        }

        public static int multiply_normal(int x, int y)
        {
            return x * y;
        }
    }
}
