using paramLesson;
using System;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace paramLesson
{
    internal class Program
    {
        static void WriteRandomArr(int Max)//只填入随机数上限即可打印
        {
            Random rnd = new Random();
            int rndMax = Max + 1;
            int rndArr;
            int[] arr = new int[Max];
            for (int i = 0; i < Max; i++)
            {
                rndArr = 0;
                int a;
                while (rndArr == 0 && arr[Max - 1] == 0)
                {
                    rndArr = rnd.Next(rndMax);
                    for (a = 0; a < Max; a++)
                    {
                        rndArr = rndArr == arr[a] ? 0 : rndArr;
                    }
                }
                arr[i] = rndArr;
                Console.Write(arr[i] + " ");
            }
        }
        static void WriteRandomArr(out int[]arr ,int Max)//重载，如果挂载了数组，将把随机结果填入其中
        {
            Random rnd = new Random();
            int rndMax = Max + 1;
            int rndArr;
            arr = new int[Max];
            for (int i = 0; i < Max; i++)
            {
                rndArr = 0;
                int a;
                while (rndArr == 0 && arr[Max - 1] == 0)
                {
                    rndArr = rnd.Next(rndMax);
                    for (a = 0; a < Max; a++)
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
            int[] arr;
            WriteRandomArr(out arr, 10);
            Console.WriteLine();

            for (int i = 0; i < arr.Length; ++i)//如果挂载了数组，那么可以再打印一次
            {
                Console.Write(arr[i] + " ");
            }

        }
    }
}
