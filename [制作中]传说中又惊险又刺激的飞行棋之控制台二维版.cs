using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;

namespace LessonMai//传说中又惊险又刺激的飞行棋之控制台二维版
{
    struct GamePlayer//结构体，区分是玩家还是Ai、玩家名字、当前走过的部署
    {
        bool ai;
        string name;
        int steps;
    }
    //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
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
    enum E_GameMod//游戏模式枚举
    {
        lowerLimit,
        HotSeat,//热座模式，即单设备的多人本地对战
        BabyBus,//宝宝巴士模式，即简单人机对战
        IntellectualEquipmentCrisis,//智械危机模式，即普通人机对战
        upperLimit,
    }
    enum E_LevelData
    {
        Height,
        Width,
    }
    //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
    public class Windows
    //窗体的类
    {
        public static int GetWindowsHeight()//高度接口
        {
            int Height = 24;
            return Height;
        }
        public static int GetWindowsWidth()//宽度接口
        {
            int Width = 80;
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
    //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
    class Snake
    {
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void Main(string[] args)//游戏主函数
        {
            Windows.SetWindows();//进入游戏时，设置窗口
            //PlotBroadcast();//开始剧情
            E_GameScene e_GameScene = E_GameScene.mainMenu;//调用枚举
            while (true)//循环，调用用于控制检测游戏进入场景的逻辑
            {
                Scene(ref e_GameScene);
            }

        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
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
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void GameScene(ref E_GameScene e_GameScene)
        {
            Console.Clear();
            PrintingMap();//印刷场景
            PrintingRules();
            int[] LevelData = FightMap(true);
            int playerProgress = 0;
            int[] playerCoordinate = CoordinateSystemConversion(playerProgress);
            Console.SetCursorPosition(playerCoordinate[0], playerCoordinate[1]);
            WriteLineColorOnce("★", false);
            Console.ReadKey(true);
        }

        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
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
        static void WriteLineColorOnce(string writeLine, bool middle, ConsoleColor colorStart)//重载，可填写印刷颜色，结束后初始化回白色
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
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static E_GameScene mainMenu(ref E_GameScene e_GameScene)//主菜单逻辑
        {
            int Height = Windows.GetWindowsHeight();
            int Width = Windows.GetWindowsWidth();
            string[] text =
                {
                "传说中又惊险又刺激的飞行棋之控制台二维版",
                "开始游戏",
                "退出游戏"
                };
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
                WriteLineColorOnce(text[((int)mainMenu)], false, ConsoleColor.White);
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
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void PlotBroadcast()//开始的剧情播报，开发过程中为了不每次都看会跳过这个
        {
            string[] Plot =
                {
                "二零二三年四月十七日",
                "一场危机爆发",
                "世界摇摇欲坠",
                "你是被选中的人",
                "来拯救一切吧",
                "和邪恶的敌人来一场",
                "又惊险又刺激的",
                "飞行棋",
                "这场比赛的结果将决定",
                "人类的",
                "命运"
                };
            for (int i = 0; i < Plot.Length; ++i)
            {
                WriteLineColorOnce(Plot[i]);
                Thread.Sleep(1250);
                Console.Clear();
            }
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线

        static void PrintingMap()//墙体打印
        {
            for (int Printing = 0; Printing < Windows.GetWindowsWidth() - 1; ++Printing)
            {
                Console.SetCursorPosition(Printing, 0);
                Console.Write("■");
                Console.SetCursorPosition(Printing, Windows.GetWindowsHeight() - 1);//减1是因为打印是从第0行开始算第1行，如果不减一会溢出崩溃
                Console.Write("■");
                Console.SetCursorPosition(Printing, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3));//除以三分之一，印刷场景
                Console.Write("■");
                if (Printing < Windows.GetWindowsHeight())
                {
                    Console.SetCursorPosition(0, Printing);
                    Console.Write("■");
                    Console.SetCursorPosition(Windows.GetWindowsWidth() - 2, Printing);//减2是因为中文字符占两格
                    Console.Write("■");
                }
            }
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static int[] FightMap(bool firstPrint)//重载，输入布尔值即是初次打印战斗地图，会记录关卡的进度，关卡坐标系
        {
            int record = 0;
            int LineWidth = 0;
            for (int PrintingWidth = 0; PrintingWidth < Windows.GetWindowsWidth() - 1; ++PrintingWidth)
            {
                if (PrintingWidth > 4 && PrintingWidth < Windows.GetWindowsWidth() - 6)
                {
                    LineWidth++;
                    for (int PrintingHeight = 0; PrintingHeight < Windows.GetWindowsHeight() - Windows.GetWindowsHeight() / 3; ++PrintingHeight)
                    {
                        if (PrintingHeight % 4 == 0 && PrintingHeight != 0 && PrintingWidth % 2 == 0)
                        {
                            Console.SetCursorPosition(PrintingWidth, PrintingHeight);
                            Console.WriteLine("□");
                            ++record;
                        }
                    }
                }
            }
            int[] LevelData = new int[2];//关卡坐标系
            LevelData[(int)E_LevelData.Width] = LineWidth;//关卡的宽度
            LevelData[(int)E_LevelData.Height] = record / LineWidth;//关卡的高度
            return LevelData;//到处宽、高后，两个数据相乘，即是游戏的地图的路径上限
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static int[] CoordinateSystemConversion(int progress)//输入关卡进度，输出显示坐标
        {
            int record = 0;
            int LineWidth = 0;
            int[] LevelData = new int[2];
            int[] WindowCoordinate = new int[2];
            while (true)
            {
                for (int PrintingWidth = 0; PrintingWidth < Windows.GetWindowsWidth() - 1; ++PrintingWidth)
                {
                    if (PrintingWidth > 4 && PrintingWidth < Windows.GetWindowsWidth() - 6)
                    {
                        LineWidth++;
                        for (int PrintingHeight = 0; PrintingHeight < Windows.GetWindowsHeight() - Windows.GetWindowsHeight() / 3; ++PrintingHeight)
                        {
                            if (PrintingHeight % 4 == 0 && PrintingHeight != 0 && PrintingWidth % 2 == 0)
                            {
                                if (progress == record)
                                {
                                    WindowCoordinate[0] = PrintingWidth;
                                    WindowCoordinate[1] = PrintingHeight;
                                    return WindowCoordinate;
                                }
                                ++record;//遍历地图，只要行径的步数相同，坐标则是相同的
                            }
                        }
                    }
                }
            }
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void PrintingRules()//印刷游戏规则
        {
            string[] PlayerRules =
                {
                "★表示人类阵营的战机:",
                "战机会根据骰子点数移动",
                "骰子点数为六对前方敌人发动攻击",
                "空空导弹-击落的敌人两回合将无法移动",
                "☆表示人类阵营的基地，进入可补给",
                "补给：立刻多骰一次骰子",
                };
            string[] AiRules =
{
                "●表示异星阵营的飞碟:",
                "飞碟会根据骰子点数移动",
                "如果骰子点数为三以下，移动加一",
                "隐身外形-百分之五十概率摆脱导弹",
                "☆表示人类阵营的基地，可踩毁基地",
                "不稳定折跃：每回合随机前进或后退两格",
                };
            for (int i = 0; i < PlayerRules.Length; ++i)
            {
                Console.SetCursorPosition(2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1 + i);
                //2是因为字符占两格，加1是避免覆盖到了划分格子的方块，加i是每次+1行打印
                Console.Write(PlayerRules[i]);
            }
            for (int i = 0; i < AiRules.Length; ++i)
            {
                Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1 + i);
                //2是因为字符占两格，加1是避免覆盖到了划分格子的方块，加i是每次+1行打印
                Console.Write(AiRules[i]);
            }
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
    }
}
