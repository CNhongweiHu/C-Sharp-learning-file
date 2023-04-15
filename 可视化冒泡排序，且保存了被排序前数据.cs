using paramLesson;
using System;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace paramLesson
{
    internal class Program
    {
        //*****************************************************************
        //*****************************************************************
        #region 冒泡排序核心逻辑
        static void BubblingSort(int[] arr)
        {
            for (int Sort = 0; Sort < arr.Length; ++Sort)
            {
                for (int i = 0; i < arr.Length; ++i)
                {
                    if (i < arr.Length - 1 && arr[i] > arr[i + 1])
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        SortVisualizationSystem(arr);//调用可视化模块
                    }
                }
            }
        }
        #endregion
        //*****************************************************************
        //*****************************************************************
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int[] arr;
            int[] storage;
            WriteRandomArr(out arr, 100);

            #region 储存随机结果
            storage = new int[arr.Length];
            for (int i = 0;i < arr.Length; ++i)
            {
                storage[i] = arr[i];
            }
            #endregion

            BubblingSort(arr);//调用冒泡排序

            #region 打印随机前数据
            Console.WriteLine("\n本次参与排序的随机数是:");
            for (int i = 0; i < arr.Length; ++i)//如果挂载了数组，那么可以选择手动打印
            {
                Console.Write(storage[i] + " ");
            }
            #endregion
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
            }
        }
        static void SortVisualizationSystem(int[]arr)
        {
            #region 打印排序情况
            Console.SetCursorPosition(0, 0);
            for (int visualization = 0; visualization < arr.Length; ++visualization)
            {
                if (arr[visualization] == visualization + 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(" {0} ", arr[visualization]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            #endregion
        }
    }
}
