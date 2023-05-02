using System;
namespace lesson
{
    class Print
    {
        static void Main(string[] args)
        {
            IntArray intArray = new IntArray();

            intArray.Add(100);
            Console.WriteLine(intArray[0] + "\n");

            intArray.Add(200);
            Console.WriteLine(intArray[5] + "\n");

            intArray[2] = 300;
            Console.WriteLine(intArray[2] + "\n");

            intArray[1] = 300;
            Console.WriteLine(intArray[1] + "\n");
        }
    }
    class IntArray
    {
        int[] array;
        int limit;
        int plan;
        //初始化
        public IntArray()
        {
            limit = 5;
            plan = 0;
            array = new int[limit];
        }
        //增
        public void Add(int input)
        {
            if (plan == limit)
            {
                int[] array2 = new int[limit + 5];
                for (int i = 0; i < plan; i++)
                {
                    array2[i] = array[i];
                }
                array = array2;
            }
            array[plan] = input;
            plan++;
        }
        //删
        public void Del(int input)
        {
            if (input <= plan)
            {
                for (int i = input; i < plan - 1; i++)
                {
                    array[i] = array[i + 1];
                }
                plan--;
            }
            else
            {
                Console.WriteLine("删除了不存在的数据");
            }
        }
        public int this[int index]
        {
            //查
            get
            {
                if (index > plan - 1 ||
                    index < 0)
                {
                    Console.WriteLine("读取了不存在的数据");
                    return 0;
                }
                return array[index];
            }
            //改
            set
            {
                if (index > plan - 1 ||
                    index < 0)
                {
                    Console.WriteLine("设置了不存在的数据");
                }
                array[index] = value;
            }
        }
    }
}