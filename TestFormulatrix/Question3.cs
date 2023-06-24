using System.Collections.Generic;

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

        public static int CalculateMaxDepthNoRecursive(Node n)
        {
            int count = 0;

            Stack<Node> nodes = new Stack<Node>();
            Node node;

            nodes.Push(n);

            while (nodes.Count > 0)
            {
                node = nodes.Pop();
                count++;

                if (node.left != null)
                    nodes.Push(node.left);

                if (node.right != null)
                    nodes.Push(node.right);
            }
            count--;

            return count;
        }
    }
}
