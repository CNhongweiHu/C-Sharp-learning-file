using paramLesson;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace paramLesson
{
    internal class Program
    {
        static void WriteRandomArr(int ranMax)
        {
            Random rnd = new Random();
            int rndMax = ranMax + 1;
            int rndArr;
            int[] arr = new int[rndMax];
            for (int i = 0; i < rndMax; i++)
            {
                rndArr = 0;
                int a;
                while (rndArr == 0)
                {
                    rndArr = rnd.Next(rndMax);
                    for (a = 0; a < rndMax; a++)
                    {
                        rndArr = rndArr == arr[a] ? 0 : rndArr;
                    }
                }
                arr[i] = rndArr;
                Console.Write(arr[i] + " ");
            }
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            WriteRandomArr(10);
        }
    }
}