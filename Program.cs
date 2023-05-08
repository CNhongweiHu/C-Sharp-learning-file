using System;
using System.Runtime.CompilerServices;

namespace Lesson
{
    public class Tool
    {
        public int x;
        public int y;
        public int z;
        Tool()
        {
        }
        public Tool(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static Tool operator +(Tool a, Tool b)
        {
            Tool c = new Tool();
            c.x = (a.x + b.x);
            c.y = (a.y + b.y);
            c.z = (a.z + b.z);
            return c;
        }
        public static Tool operator -(Tool a, Tool b)
        {
            Tool c = new Tool();
            c.x = (a.x - b.x);
            c.y = (a.y - b.y);
            c.z = (a.z - b.z);
            return c;
        }
        public static Tool operator *(int value , Tool a)
        {
            Tool c = new Tool();
            c.x = (a.x * value);
            c.y = (a.y * value);
            c.z = (a.z * value);
            return c;
        }
    }

    class MainExpansion
    {
        static void Main(string[] args)
        {
            Tool a = new Tool(1,2,3);
            Tool b = new Tool(1,2,3);
            Tool c;
            c = a + b;
            Console.WriteLine("{0}、{1}、{2}",c.x, c.y, c.z);
            c = a - b;
            Console.WriteLine("{0}、{1}、{2}", c.x, c.y, c.z);
            c = 5 * a;
            Console.WriteLine("{0}、{1}、{2}", c.x, c.y, c.z);
        }
    }
}