using System;
using System.Collections.Generic;
using System.Text;

namespace TestFormulatrix
{
    /*
     Questions :
        Write the content of the method below that counts the maximum number of levels in a given tree. 
        Please notice that this is NOT counting the TOTAL number of nodes, but counting the DEPTH.
     */

    class Question3
    {
        public class Node
        {
            public Node right;
            public Node left;
        }

        public static int calculateMaxDepth(Node n)
        {
            // your code here
            return GetDepth(0, n);
        }

        private static int GetDepth(int counter, Node n)
        {
            if (n == null) return counter;

            counter++;
            int left = GetDepth(counter, n.left);
            int right = GetDepth(counter, n.right);

            if (left > right)
                counter = left;
            else
                counter = right;
            return counter;
        }
    }
}
