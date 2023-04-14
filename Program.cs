using System.Runtime.Intrinsics.X86;

namespace paramLesson
{
    internal class Program
    {
        static int inputLesson()
        {
            int input = 0;//初始化默认输入参数，input后面会用于保存用户输入的数字
            while (true)//循环检测用户输入按键
            {
                try//输入内容转化为文字，然后将文字转化为数字可能是无法执行通过的，所以用try来判断输入是否合法
                {
                    input = int.Parse(Console.ReadLine().ToString());//输入内容转化为文字，然后将文字转化为数字，再将数字保存进input
                    break;//输入合法的情况下，用户输入的内容通过了，关闭循环
                }
                catch
                {
                    Console.WriteLine("输入错误，请输入数字");//输入内容无法被识别为数字时，显示报错文字
                }
            }
            return input;//将保存的值传输出去，让阶乘计算器好接收到用户想要计算的参数
        }//输入数字
        static bool EnterContinue()//判断用户输入内容，并将结果输出为布尔
        {
            Console.WriteLine("是否还要输入?\"ENTER\"继续输入  \"ESC\"不继续输入");//文字提示，告诉用户输入哪些按键有哪些结果
            while (true)//循环
            {
                Char readKey = Console.ReadKey(true).KeyChar;//每次循环时保存玩家的输入的单次按键
                switch (readKey)//将输入按键和我们的预设按键比对
                {
                    case (Char)ConsoleKey.Enter://如果是输入了回车，即Enter
                        Console.WriteLine("输入数字求阶乘");//提示玩家可以输入数字
                        return true;//跳转出函数，并返回true的布尔值，告诉外部玩家选择的结果
                    case (Char)ConsoleKey.Escape://如果是输入了退出，即Esc
                        Console.WriteLine("已结束输入");//提示玩家输入结束，无法再继续输入，将进入阶乘计算器了
                        return false;//跳转出函数，并返回false的布尔值，告诉外部玩家选择的结果
                    default://输入了其他内容
                        //不执行任何东西
                        break;//通过break跳出按键比对，但是并没有成功跳出循环，所以会继续监听玩家的按键
                }
            }
        }//是否继续输入
        static void mainLesson()//阶乘计算器，在没有参数传入时，会提示玩家可以输入参数，如果阶乘计算器是挂载了数组传入的，那么便不会进入这一段逻辑
        {
            int numberValu = 0;//声明变量，这会是input数组的编号，input会保存将用于阶乘的数
            int[] input = new int[numberValu + 1];//初始化input数组，numberValu + 1代表了input数组储存空间的上限，由于数组是0开始排序的，所以上限是0+1开始
            Console.WriteLine("输入数字求阶乘");//打印文字，提示用户初始化已完成，可以输入数据了
            input[numberValu] = inputLesson();//调用输入数字，让数字填充进input数组
            while (true)//循环，用户若不选择不需要输入更多数字，则持续循环
            {
                if (EnterContinue())//判断，如果需要输入更多数字，则调用是否继续输入
                {
                    ++numberValu;//input即将拥有的空位上限
                    int[] Storage = input;//声明一个临时储存的数组Storage来保存input的数据
                    input = new int[numberValu + 1];//将input初始化为新的堆，即将input扩大，但数据丢失了
                    for (int i = 0; i < Storage.Length; i++)//通过遍历，将Storage的数据保存进input
                    {
                        input[i] = Storage[i]; 
                    }
                    input[numberValu] = inputLesson();//调用输入数字
                }
                else
                {
                    mainLesson(input);//判断，如果不需要输入更多数字，则调用阶乘计算器
                    break;//阶乘计算器使用完毕后，终止循环让程序结束
                }
            }
        }
        static void mainLesson(int[] input)//阶乘计算器本身，这是一个重载函数，如果用户传入了的数组，则会直接调用阶乘计算器，而不需要输入
        {
            int[] storage = new int[input.Length];//声明一个临时储存的数组Storage，临时数组需要和input一样长，但是不能是input本身
            for (int i = 0; i < input.Length; i++)
            {
                storage[i] = input[i];//遍历input，并将其的数据传入临时储存的数组Storage当中，因为Storage会用来储存阶乘结果，所以不能等于input，因为堆的特性我们不能直接等于
            }

            for (int Value = 0; Value != storage.Length; Value++)//开始计算阶乘，我们开始从0遍历Storage，每个参数都会参与一次计算
            {
                int factorial = storage[Value];//将Storage中储存的当次遍历数据，保存进临时变量factorial中，因为堆的特性，我们不能直接让storage保存计算过程，这会污染默认值
                for (int i = 1; i < storage[Value]; i++)//遍历当次数据的大小，从最小的正数1开始，我们计算正数的阶乘
                {
                    factorial = factorial * i;//每次遍历时进行一次相乘，等遍历完毕后，临时变量factorial即代表了input的阶乘结果
                }

                storage[Value] = factorial;//将最终结果从factorial保存进Storage中，这样计算便结束了

                Console.WriteLine("{0}的阶乘是{1}", input[Value], storage[Value]);//打印本次阶乘的结果
            }

            int sum = 0;//声明临时变量factorial，这将会用于保存阶乘的结果
            for (int Value = 0; Value != storage.Length; Value++)//遍历阶乘的结果
            {
                sum = sum + storage[Value];//每次遍历时将阶乘的结果保存进factorial
                Console.Write(input[Value] + " ");//每次遍历时，打印被计算的原始数值,使用空格分割数字
            }
            Console.WriteLine("阶乘的总和是{0}", sum);//打印阶乘的结果
        }
        static void Main(string[] args)
        {
            mainLesson();//调用递归求阶乘
        }
    }
}