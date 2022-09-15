using System;
using System.Collections.Generic;
using System.Text;

namespace TestFormulatrix
{
    /*
     Questions :
        A Formulatrix number is defined as a positive integer greater than 1 that is divisible only by 1 and itself. 
        When the number is divided by any other positive integers, it would have remainders.

        For example:
            7 is a Formulatrix number since it could be divided by 1 and 7 only.
            9 is NOT a Formulatrix number since it could be divided by 3.


     Please write a method to print out ALL the Formulatrix numbers between 2 and 100.

     Answer :
        - Just realized, that Formulatrix number means Prime Number :(
     */
    class Question5
    {
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
