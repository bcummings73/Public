using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Utilities
{

    // helper static utils used for  IEnumerable data in this project
    public static class IEmumerationUtilities
    {

        // flatten the employee tree

        public static IEnumerable<Employee> Traverse(this Employee rootNode)
        {
            var stack = new Stack<Employee>();
            stack.Push(rootNode);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                yield return currentNode;
                foreach (var childNode in currentNode.DirectReports ?? Enumerable.Empty<Employee>())
                    stack.Push(childNode);
            }
        }
    }
}
