using paramLesson;
using System;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace paramLesson
{
    internal class Program
    {
        //*****************************************************************
        #region 选择排序 核心逻辑
        static void SelectSort(int[] arr)
        {
            int temp = arr[0];//初始化temp为第一个数字
            Console.WriteLine(arr.Length);
            for (int Sort = 0; Sort < arr.Length; ++Sort)//排序完成部分记录
            {
                for (int i = 0; i < arr.Length - Sort; ++i)//遍历未排序完成的那部分表格
                {
                    temp = temp > arr[i] ? temp : arr[i];//temp储存未排序完成的那部分的最大值
                }
                Console.WriteLine();
                for (int i = 0; i < arr.Length - Sort; ++i)//遍历未排序完成的那部分表格
                {
                    if (arr[i] == temp)//如果表格未排序完成的最大值是arr[i]
                    {
                        arr[i] = arr[arr.Length - Sort - 1];//将其替换为未排序完成的那部分中的最后一行
                        arr[arr.Length - Sort - 1] = temp;//将Temp填入未排序完成的那部分中的最后一行
                        break;
                    }
                }
                temp = arr[0];//将参数Temp初始化为第一行
                #endregion
                //*****************************************************************
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < arr.Length; ++i)//排序过程可视化
                {
                    if (arr[i] == Sort || arr[i] == i + 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(arr[i] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int[] arr;
            int[] storage;
            WriteRandomArr(out arr, 1000);

            #region 储存随机结果
            storage = new int[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                storage[i] = arr[i];
            }
            #endregion
            //*****************************************************************
            SelectSort(arr);
            //调用选择排序
            //*****************************************************************

            #region 打印随机前数据
            Console.WriteLine("\n本次参与排序的随机数是:");
            for (int i = 0; i < arr.Length; ++i)//如果挂载了数组，那么可以选择手动打印
            {
                Console.Write(storage[i] + " ");
            }
            #endregion
            #region 打印随机后数据
            Console.WriteLine("\n本次排序的结果是:");
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < arr.Length; ++i)//如果挂载了数组，那么可以选择手动打印
            {
                Console.Write(arr[i] + " ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            #endregion
        }
        static void WriteRandomArr(out int[] arr, int Max)//将随机结果填入其中
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
    }
}