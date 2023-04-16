using paramLesson;
using System;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace paramLesson
{
    internal class Program
    {
        //*****************************************************************
        #region 冒泡排序 核心逻辑
        static void BubblingSort(int[] arr, bool inverted)
        {
            for (int Sort = 0; Sort < arr.Length; ++Sort)
            {
                //bool isSort = false;//每次循环开始时，进行记录是否有循环 特殊情景下可以用于优化
                for (int i = 0; i < arr.Length; ++i)
                {
                    //isSort = true;//如果本轮进行了循环，则记录有
                    if (inverted)
                    {
                        if (i < arr.Length - 1 && arr[i] > arr[i + 1])
                        {
                            int temp = arr[i];
                            arr[i] = arr[i + 1];
                            arr[i + 1] = temp;
                            SortVisualizationSystem(arr, inverted);//调用可视化模块
                        }
                    }
                    else
                    {
                        if (i < arr.Length - 1 && arr[i] < arr[i + 1])
                        {
                            int temp = arr[i];
                            arr[i] = arr[i + 1];
                            arr[i + 1] = temp;
                            SortVisualizationSystem(arr, inverted);//调用可视化模块
                        }
                    }
                }
                //if (!isSort) { break; }//出现了无需循环的情况，则直接结束程序
            }
        }
        #endregion
        //*****************************************************************
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int[] arr;
            int[] storage;
            WriteRandomArr(out arr, 100);

            #region 储存随机结果
            storage = new int[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                storage[i] = arr[i];
            }
            #endregion
            //*****************************************************************
            //*****************************************************************
            BubblingSort(arr, true);
            //调用冒泡排序，true为正序，false倒序
            //*****************************************************************
            //*****************************************************************

            #region 打印随机前数据
            Console.WriteLine("\n本次参与排序的随机数是:");
            for (int i = 0; i < arr.Length; ++i)//如果挂载了数组，那么可以选择手动打印
            {
                Console.Write(storage[i] + " ");
            }
            #endregion
        }
        static void WriteRandomArr(out int[] arr, int Max)//重载，如果挂载了数组，将把随机结果填入其中
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
        static void SortVisualizationSystem(int[] arr, bool inverted)
        {
            #region 打印排序情况
            Console.SetCursorPosition(0, 0);
            for (int visualization = 0; visualization < arr.Length; ++visualization)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (inverted)
                {
                    if (arr[visualization] == visualization + 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                else
                {
                    if (arr[visualization] == arr.Length - visualization)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                Console.Write(" {0} ", arr[visualization]);
            }
            Console.ForegroundColor = ConsoleColor.White;
            #endregion
        }
    }
}