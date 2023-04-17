using System;
using System.Drawing;
using System.Reflection;

namespace LessonMai//传说中又惊险又刺激的飞行棋之控制台二维版
{
    enum E_GameScene//游戏场景枚举
    {
        lowerLimit,
        mainMenu,
        mainGame,
        upperLimit,
    }
    enum E_MainMenu//主菜单枚举
    {
        lowerLimit,
        startGame,
        quitGame,
        upperLimit,
    }
    public class Windows
    //窗体的类
    {
        public static int GetWindowsHeight()//高度接口
        {
            int Height = 20;
            return Height;
        }
        public static int GetWindowsWidth()//宽度接口
        {
            int Width = 60;
            return Width;
        }
        public static void SetWindows()//默认调用接口并设置
        {
            Console.CursorVisible = false;
            int Height = GetWindowsHeight();
            int Width = GetWindowsWidth();
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);
        }
    }
    class Snake
    {
        static void Scene(ref E_GameScene e_GameScene)
        {
            switch (e_GameScene)
            {
                case E_GameScene.mainMenu:
                    mainMenu(ref e_GameScene);
                    break;
                case E_GameScene.mainGame:
                    GameScene(ref e_GameScene);
                    break;
            }
        }
        #region 场景居中打印
        static void inTheMiddle(string writeLine)//场景居中打印
        {
            int Height = Windows.GetWindowsHeight();
            int Width = Windows.GetWindowsWidth();
            Console.SetCursorPosition(Width / 2 - writeLine.Length, Height / 2);
        }
        static void WriteLineColorOnce(string writeLine)//默认不写则是居中,红色印刷，结束后初始化回白色
        {
            inTheMiddle(writeLine); 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(writeLine);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void WriteLineColorOnce(string writeLine, bool middle)//默认不写则是红色印刷，结束后初始化回白色
        {
            if (middle) { inTheMiddle(writeLine); }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(writeLine);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void WriteLineColorOnce(string writeLine,bool middle, ConsoleColor colorStart)//重载，可填写印刷颜色，结束后初始化回白色
        {
            if (middle) { inTheMiddle(writeLine); }
            Console.ForegroundColor = colorStart;
            Console.WriteLine(writeLine);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void WriteLineColorOnce(string writeLine, bool middle, ConsoleColor colorStart, ConsoleColor colorEnd)//重载，可填写印刷颜色，结束后可填写初始化颜色
        {
            if (middle) { inTheMiddle(writeLine); }
            Console.ForegroundColor = colorStart;
            Console.WriteLine(writeLine);
            Console.ForegroundColor = colorEnd;
        }
        #endregion
        static E_GameScene mainMenu(ref E_GameScene e_GameScene)//主菜单逻辑
        {
            int Height = Windows.GetWindowsHeight();
            int Width = Windows.GetWindowsWidth();
            string[] text = { "传说中又惊险又刺激的飞行棋之控制台二维版", "开始游戏", "退出游戏" };
            E_MainMenu mainMenu = E_MainMenu.startGame;
            for (mainMenu = 0; ((int)mainMenu) < text.Length; ++mainMenu)
            {
                Console.SetCursorPosition(Width / 2 - text[((int)mainMenu)].Length, Height / 2 + ((int)mainMenu) * 2);
                Console.Write(text[((int)mainMenu)]);
            }
            mainMenu = E_MainMenu.startGame;
            //进入主菜单时，打印主菜单文字，打印完毕后重新初始化

            Console.SetCursorPosition(Width / 2 - text[((int)mainMenu)].Length, Height / 2 + ((int)mainMenu) * 2);
            WriteLineColorOnce(text[((int)mainMenu)], false);
            //进入主菜单时，标红以高亮默认选项

            while (true)
            {
                Char Input = Console.ReadKey(true).KeyChar;
                Console.SetCursorPosition(Width / 2 - text[((int)mainMenu)].Length, Height / 2 + ((int)mainMenu) * 2);
                WriteLineColorOnce(text[((int)mainMenu)], false,ConsoleColor.White);
                switch (Input)
                {
                    case 'W':
                    case 'w':
                        mainMenu = (int)mainMenu - 1 > (int)(E_MainMenu.lowerLimit) ? (E_MainMenu)((int)mainMenu - 1) : mainMenu;
                        break;
                    case 'S':
                    case 's':
                        mainMenu = (int)mainMenu + 1 < (int)(E_MainMenu.upperLimit) ? (E_MainMenu)((int)mainMenu + 1) : mainMenu;
                        break;
                    case (Char)ConsoleKey.Enter://当按下的是Enter时，改变场景状态逻辑，根据当前选择的选项，跳转进入对应的游戏
                        switch (mainMenu)
                        {
                            case E_MainMenu.startGame://这是正常进入游戏的选项
                                return e_GameScene = E_GameScene.mainGame;
                            case E_MainMenu.quitGame:
                                Console.WriteLine("\nExit(0)：退出成功");//这是正常退出的选项
                                Environment.Exit(0);//正常退出
                                break;
                            default:
                                Console.WriteLine("\nExit(1)：错误退出，选择到了不存在的选项");//选择到了不存在的选项时的情况
                                Environment.Exit(0);//错误退出
                                break;
                        }
                        break;
                }
                //通过循环，将玩家的当前选项高亮
                Console.SetCursorPosition(Width / 2 - text[((int)mainMenu)].Length, Height / 2 + ((int)mainMenu) * 2);
                WriteLineColorOnce(text[((int)mainMenu)], false);
            }
            //按键监听，将选项高亮
        }
        static void GameScene(ref E_GameScene e_GameScene)
        {
            Console.Clear();
            WriteLineColorOnce("成功开始游戏！");//调用居中单次标红印刷代码
            Console.ReadLine();
        }
        static void Main(string[] args)//游戏主函数
        {
            Windows.SetWindows();//进入游戏时，设置窗口
            WriteLineColorOnce("来自某位不知名摸鱼圣手");//调用居中单次标红印刷代码
            Thread.Sleep(1000);
            Console.Clear();
            E_GameScene e_GameScene = E_GameScene.mainMenu;//调用枚举
            while(true)//循环，调用用于控制检测游戏进入场景的逻辑
            {
                Scene(ref e_GameScene);
            }

        }
    }
}
