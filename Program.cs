using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
#region 巨幅画布
//'  ████████▄     ▄████████    ▄████████    ▄█   ▄█▄    ▄████████  ▄██████▄  ███    █▄   ▄█        ▄█   ▄█   ▄█          ▄█    █▄    ████████▄          ▄████████    ▄████████   ▄▄▄▄███▄▄▄▄      ▄████████    ▄█   ▄█▄    ▄████████ 
//'  ███   ▀███   ███    ███   ███    ███   ███ ▄███▀   ███    ███ ███    ███ ███    ███ ███       ███  ███  ███         ███    ███   ███   ▀███        ███    ███   ███    ███ ▄██▀▀▀███▀▀▀██▄   ███    ███   ███ ▄███▀   ███    ███ 
//'  ███    ███   ███    ███   ███    ███   ███▐██▀     ███    █▀  ███    ███ ███    ███ ███       ███▌ ███▌ ███▌        ███    ███   ███    ███        ███    ███   ███    █▀  ███   ███   ███   ███    ███   ███▐██▀     ███    █▀  
//'  ███    ███   ███    ███  ▄███▄▄▄▄██▀  ▄█████▀      ███        ███    ███ ███    ███ ███       ███▌ ███▌ ███▌       ▄███▄▄▄▄███▄▄ ███    ███       ▄███▄▄▄▄██▀  ▄███▄▄▄     ███   ███   ███   ███    ███  ▄█████▀     ▄███▄▄▄     
//'  ███    ███ ▀███████████ ▀▀███▀▀▀▀▀   ▀▀█████▄    ▀███████████ ███    ███ ███    ███ ███       ███▌ ███▌ ███▌      ▀▀███▀▀▀▀███▀  ███    ███      ▀▀███▀▀▀▀▀   ▀▀███▀▀▀     ███   ███   ███ ▀███████████ ▀▀█████▄    ▀▀███▀▀▀     
//'  ███    ███   ███    ███ ▀███████████   ███▐██▄            ███ ███    ███ ███    ███ ███       ███  ███  ███         ███    ███   ███    ███      ▀███████████   ███    █▄  ███   ███   ███   ███    ███   ███▐██▄     ███    █▄  
//'  ███   ▄███   ███    ███   ███    ███   ███ ▀███▄    ▄█    ███ ███    ███ ███    ███ ███▌    ▄ ███  ███  ███         ███    ███   ███   ▄███        ███    ███   ███    ███ ███   ███   ███   ███    ███   ███ ▀███▄   ███    ███ 
//'  ████████▀    ███    █▀    ███    ███   ███   ▀█▀  ▄████████▀   ▀██████▀  ████████▀  █████▄▄██ █▀   █▀   █▀          ███    █▀    ████████▀         ███    ███   ██████████  ▀█   ███   █▀    ███    █▀    ███   ▀█▀   ██████████ 
//'                            ███    ███   ▀                                            ▀                                                              ███    ███                                             ▀                      
//                                            33  33
//                                           33    33
//                                           33333333
//                                          33333333333
//                                       33 3 3  3333 3 33
//                                       3333333333  3    33
//                                      333333   333   333 33     33
//                                      3333 3333333333   333   3333
//                                      33333333 3333 33333333 33333
//                                      33333 33 3333333333333333333
//                                     333333333333333333  333333333
//                                    3 33333 33333333333   33333333
//                                   33333333333333333333  333333333
//                                    333333333333333333333333333333
//                                   33 3333333333 3 33333333333333
//                                 33333333 333 33 3333333333333333
//                              3 333333333333333333333333333333333
//                              333  333 33333333 3333333333333333
//                            333 33333333333333333333333333333333
//                          33   3 333333333333333333333333333333
//                        33     3 3333333  33333333333333333333
//                     33       33333333         333333333333333
//                   33         33333333         3333333  333
//                 33            33333333        3 33333   33
//              33                3333333         333333   333
//            33                   333333           3  3    33
//          33                      33333         3 33333   333
//        3                          3333         3333333    33
//                                    3333         33333      3
//                                  33333333333    33333
//                              333333333333333333 33333
//                            3333333333333333333333333
//                          3333333333333333333333333 333
//                          33333333333333333333333333333
//                         333333333333333333333333333333333
#endregion
namespace Ds3Remake
{
    enum E_Carreer
    {
        骑士,
        佣兵,
        战士,
        传令者,
        小偷,
        刺客,
        魔法师,
        咒术师,
        圣职,
        一无所有者,
    }
    //职业系统
    enum E_Gender
    {
        男,
        女,
    }
    //性别系统
    class MainMenu
    {
        static void Main(string[] args)
        {
            #region 游戏中只出现一次的提示
            bool JionMainMenuScene4 = false;
            #endregion
            #region 声明窗口范围
            int windowsHeight = 32;
            int windowsWidth = 128;
            #endregion
            #region 游戏初始化信息 常量
            const float gameVersion = 0.04f;
            const float gameMode = 0.01f;
            const string dlc1Name = "ASHES OF ARIANDEL";
            const string dlc2Name = "THE RINGED CITY";
            const bool online = false;
            const string onlinetrue = "在线模式";
            const string onlineFalse = "脱机模式";
            const string onlineStr = online == true ? onlinetrue : onlineFalse;
            const Char enter = (char)ConsoleKey.Enter;
            //声明常量，enter = Enter，检测是否输入的是回车
            //创建常量
            #endregion
            #region 字符画打印机，打印前的初始化
            //int numberLine = 0;
            //bool printerStarts = true;
            //string printContent = "";//打印的内容
            //string printPerLine = "";//每行打印的内容，自动读取，在这里设置是无效的
            //int widthPerRow = 0;//每行的宽度，自动读取，在这里设置是无效的
            //int pictureHeight = 0;//字符画的行数，后续功能需求：字符画行数自动识别的程序
            string pictureTitle = "";
            #endregion
            #region 弹窗，初始化
            string logInStr = "";
            int logInInt = 2;
            bool logInBool = false;
            Console.ForegroundColor = ConsoleColor.White;
            int popWindow = 0;
            int logInLineInt = 6;//登陆描述有几行？
            #endregion
            #region 声明加载菜单变量
            //int mainSwitch = 0;//控制标题
            int pictureSwitch = 0;
            int colorEggSwitch = 0;
            bool mainWhile = true;
            string mainTitle = "";
            string colorEggTitle = "";
            int loadProgress = 0;
            int pictureRows = - 5;//字符画位置
            int mainRows = pictureRows + 8;//标题位置
            int mainloadingRows = mainRows + 2;//进度条位置
            int colorEggRows = mainloadingRows + 2;//提示语位置
            #endregion
            #region 初始化屏幕
            Console.CursorVisible = false;
#pragma warning disable CA1416 // 验证平台兼容性
            Console.SetWindowSize(windowsWidth, windowsHeight);
#pragma warning restore CA1416 // 验证平台兼容性
            //视窗的宽和高
#pragma warning disable CA1416 // 验证平台兼容性
            Console.SetBufferSize(windowsWidth, windowsHeight);
#pragma warning restore CA1416 // 验证平台兼容性
            //设置视窗的宽和高，仅支持windows平台
            Console.BackgroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Thread.Sleep(1000);
            Console.Clear();
            Thread.Sleep(1000);
            #endregion
            #region 主菜单系统的准备工作
            bool keyEnterBool = false;
            //检测用户是否输入了回车
            #region 重要参数 选单文本行数，每次新增后需要调整这个值；
            int mainMenuLineReal = 0;
            int mainMenuLine = mainMenuLineReal + 1;
            #endregion
            string mainMenuStr = "占位符";
            //每行文字的内容
            bool ready = false;
            //初始化执行
            Char keyChar = '\0';
            //初始化监听的玩家按键
            Console.CursorVisible = false;
            //隐藏光标
            #endregion
            int mainMenuScene = 0;//主菜单初始化
            bool closeTheGame = false;//游戏初始化
            while (mainWhile == true)
            {
                Thread.Sleep(1500);
                #region 标题页代码
                //Console.ForegroundColor = ConsoleColor.Black;
                //Console.SetCursorPosition((windowsWidth - mainTitle.Length ) / 2, windowsHeight / 2 + mainRows);
                //Console.WriteLine(mainTitle);
                //Console.ForegroundColor = ConsoleColor.White;
                //switch (mainSwitch)
                //{
                //    case 0:
                //        mainTitle = "Only AtariPong";
                //        mainSwitch++;
                //        break;
                //    case 1:
                //        mainTitle = "@ 1974 Bandai Namco";
                //        mainSwitch++;
                //        break;
                //    case 2:
                //        mainTitle = "From Software";
                //        mainSwitch++;
                //        break;
                //    case 3:
                //        mainTitle = "DARK SOULS III REMAKE";
                //        mainSwitch++;
                //        break;
                //    case 4:
                //        mainTitle = "HidetakaMiyazaki";
                //        mainSwitch++;
                //        break;
                //    default:
                //        break;
                //}
                //Console.SetCursorPosition((windowsWidth - mainTitle.Length) / 2, windowsHeight / 2 + mainRows);
                //Console.WriteLine(mainTitle);
                #endregion
                #region 字符画代码
                Console.ForegroundColor = ConsoleColor.White;//字符画颜色
                switch (pictureSwitch)
                {
                    case 0:
                        #region 字符画雅达利
                        int lineAtarl = -8;
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("              ||| ||| |||               ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("              ||| ||| |||               ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("             |||  |||  |||              ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("            ||||  |||  ||||             ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("           ||||   |||   ||||            ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("         |||||    |||    ||||           ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("     |||||||      |||     |||||||       ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("    ||||||        |||       ||||||      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("    |||           |||            ||     ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("                                        ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("      ||| |||||||  ||    ||||    |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("      |||    |    |||    |   ||  |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("      | ||   |    | ||   |   ||  |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("     || ||   |   ||   |  | ||    |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("     ||||||  |   ||||||  | ||    |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("    ||   ||  |  ||   ||  |  ||   |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("    |     |  |  |     || |   ||  |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureSwitch++;
                        #endregion
                        Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Black;//字符画颜色
                        #region 字符画雅达利
                        lineAtarl = -8;
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("               || ||| ||                ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("              ||| ||| |||               ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("              ||| ||| |||               ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("             |||  |||  |||              ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("            ||||  |||  ||||             ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("           ||||   |||   ||||            ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("         |||||    |||    ||||           ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("     |||||||      |||     |||||||       ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("    ||||||        |||       ||||||      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("    |||           |||            ||     ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("                                        ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("      ||| |||||||  ||    ||||    |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("      |||    |    |||    |   ||  |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("      | ||   |    | ||   |   ||  |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("     || ||   |   ||   |  | ||    |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("     ||||||  |   ||||||  | ||    |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("    ||   ||  |  ||   ||  |  ||   |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("    |     |  |  |     || |   ||  |      ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + lineAtarl);
                        lineAtarl++;
                        Console.WriteLine(pictureTitle);
                        #endregion
                        Console.ForegroundColor = ConsoleColor.White;//字符画颜色
                        #region 字符画万代南宫梦
                        pictureTitle = ("██████╗  █████╗ ███╗   ██╗██████╗  █████╗ ██╗      ███╗   ██╗ █████╗ ███╗   ███╗ ██████╗ ██████╗ ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██╔══██╗██╔══██╗████╗  ██║██╔══██╗██╔══██╗██║      ████╗  ██║██╔══██╗████╗ ████║██╔════╝██╔═══██╗ ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 1);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██████╔╝███████║██╔██╗ ██║██║  ██║███████║██║█████╗██╔██╗ ██║███████║██╔████╔██║██║     ██║   ██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 2);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██╔══██╗██╔══██║██║╚██╗██║██║  ██║██╔══██║██║╚════╝██║╚██╗██║██╔══██║██║╚██╔╝██║██║     ██║   ██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 3);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██████╔╝██║  ██║██║ ╚████║██████╔╝██║  ██║██║      ██║ ╚████║██║  ██║██║ ╚═╝ ██║╚██████╗╚██████╔╝");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 4);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝ ╚═╝  ╚═╝╚═╝      ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝     ╚═╝ ╚═════╝ ╚═════╝ ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 5);
                        Console.WriteLine(pictureTitle);
                        #endregion
                        break;
                    case 1:
                        pictureSwitch++;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Black;//字符画颜色
                        #region 字符画万代南宫梦
                        pictureTitle = ("██████╗  █████╗ ███╗   ██╗██████╗  █████╗ ██╗      ███╗   ██╗ █████╗ ███╗   ███╗ ██████╗ ██████╗ ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██╔══██╗██╔══██╗████╗  ██║██╔══██╗██╔══██╗██║      ████╗  ██║██╔══██╗████╗ ████║██╔════╝██╔═══██╗ ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 1);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██████╔╝███████║██╔██╗ ██║██║  ██║███████║██║█████╗██╔██╗ ██║███████║██╔████╔██║██║     ██║   ██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 2);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██╔══██╗██╔══██║██║╚██╗██║██║  ██║██╔══██║██║╚════╝██║╚██╗██║██╔══██║██║╚██╔╝██║██║     ██║   ██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 3);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██████╔╝██║  ██║██║ ╚████║██████╔╝██║  ██║██║      ██║ ╚████║██║  ██║██║ ╚═╝ ██║╚██████╗╚██████╔╝");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 4);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝ ╚═╝  ╚═╝╚═╝      ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝     ╚═╝ ╚═════╝ ╚═════╝ ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 5);
                        Console.WriteLine(pictureTitle);
                        #endregion
                        Console.ForegroundColor = ConsoleColor.White;//字符画颜色
                        #region 字符画From Software
                        pictureTitle = ("From Software");
                        #region From Software
                        pictureTitle = ("███████╗██████╗  ██████╗ ███╗   ███╗    ███████╗ ██████╗ ███████╗████████╗██╗    ██╗ █████╗ ██████╗ ███████╗");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██╔════╝██╔══██╗██╔═══██╗████╗ ████║    ██╔════╝██╔═══██╗██╔════╝╚══██╔══╝██║    ██║██╔══██╗██╔══██╗██╔════╝");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 1);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("█████╗  ██████╔╝██║   ██║██╔████╔██║    ███████╗██║   ██║█████╗     ██║   ██║ █╗ ██║███████║██████╔╝█████╗  ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 2);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██╔══╝  ██╔══██╗██║   ██║██║╚██╔╝██║    ╚════██║██║   ██║██╔══╝     ██║   ██║███╗██║██╔══██║██╔══██╗██╔══╝  ");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 3);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██║     ██║  ██║╚██████╔╝██║ ╚═╝ ██║    ███████║╚██████╔╝██║        ██║   ╚███╔███╔╝██║  ██║██║  ██║███████╗");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 4);
                        Console.WriteLine(pictureTitle);
                        #endregion
                        pictureSwitch++;
                        break;
                    #endregion
                    case 3:
                        #region 字符画雅达利
                        pictureTitle = ("");
                        pictureSwitch++;
                        break;
                    #endregion
                    case 4:
                        #region 字符画雅达利
                        pictureTitle = ("");
                        pictureSwitch++;
                        break;
                    #endregion
                    default:
                        break;
                }
                #endregion
                #region 彩蛋页代码
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(windowsWidth / 2 - colorEggTitle.Length, windowsHeight / 2 + colorEggRows);
                Console.Write(colorEggTitle);
                Console.ForegroundColor = ConsoleColor.White;
                switch (colorEggSwitch)
                {
                    case 0:
                        colorEggTitle = "正在角落布置怪物";
                        colorEggSwitch++;
                        break;
                    case 1:
                        colorEggTitle = "前有巨大宝箱";
                        colorEggSwitch++;
                        break;
                    case 2:
                        colorEggTitle = "正在将口口剑强化至二十二";
                        colorEggSwitch++;
                        break;
                    case 3:
                        colorEggTitle = "正在赞美太阳";
                        colorEggSwitch++;
                        break;
                }
                Console.SetCursorPosition(windowsWidth / 2 - colorEggTitle.Length, windowsHeight / 2 + colorEggRows);
                Console.Write(colorEggTitle);
                #endregion
                #region 进度条代码
                string mainloading = "■\0■\0■\0■\0■\0";
                int loadUpper = mainloading.Length * 3;
                Console.SetCursorPosition(windowsWidth / 2 - loadUpper + loadProgress * (mainloading.Length), windowsHeight / 2 + mainloadingRows);
                Console.WriteLine(mainloading);
                loadProgress++;
                mainWhile = loadProgress > 4 ? false : true;
                # endregion
            }
            Thread.Sleep(1000);
            while (closeTheGame == false)//没有退出游戏
            {
                switch (mainMenuScene)//主菜单系统
                {
                    #region 菜单_00主菜单首页
                    case 0:
                        #region 加载完成
                        Console.Clear();
                        #region DarkSoul3字符画
                        pictureTitle = ("██████╗  █████╗ ██████╗ ██╗  ██╗    ███████╗ ██████╗ ██╗   ██╗██╗     ███████╗    ██╗██╗██╗");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 1);
                        Console.WriteLine(pictureTitle);
                        Thread.Sleep(200);
                        pictureTitle = ("██╔══██╗██╔══██╗██╔══██╗██║ ██╔╝    ██╔════╝██╔═══██╗██║   ██║██║     ██╔════╝    ██║██║██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 2);
                        Console.WriteLine(pictureTitle);
                        Thread.Sleep(200);
                        pictureTitle = ("██║  ██║███████║██████╔╝█████╔╝     ███████╗██║   ██║██║   ██║██║     ███████╗    ██║██║██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 3);
                        Console.WriteLine(pictureTitle);
                        Thread.Sleep(200);
                        pictureTitle = ("██║  ██║██╔══██║██╔══██╗██╔═██╗     ╚════██║██║   ██║██║   ██║██║     ╚════██║    ██║██║██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 4);
                        Console.WriteLine(pictureTitle);
                        Thread.Sleep(200);
                        pictureTitle = ("██████╔╝██║  ██║██║  ██║██║  ██╗    ███████║╚██████╔╝╚██████╔╝███████╗███████║    ██║██║██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 5);
                        Console.WriteLine(pictureTitle);
                        Thread.Sleep(200);
                        pictureTitle = ("╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝    ╚══════╝ ╚═════╝  ╚═════╝ ╚══════╝╚══════╝    ╚═╝╚═╝╚═╝");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 6);
                        Console.WriteLine(pictureTitle);
                        Thread.Sleep(200);
                        mainTitle = "请按任一按钮";
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(windowsWidth / 2 - mainTitle.Length, windowsHeight / 2 + mainRows + 5);
                        Console.WriteLine(mainTitle);
                        #endregion
                        Thread.Sleep(1000);
                        Console.ReadKey(true);
                        Console.ReadKey(true);
                        Console.Clear();
                        #region DarkSoul3字符画
                        pictureTitle = ("██████╗  █████╗ ██████╗ ██╗  ██╗    ███████╗ ██████╗ ██╗   ██╗██╗     ███████╗    ██╗██╗██╗");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 1);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██╔══██╗██╔══██╗██╔══██╗██║ ██╔╝    ██╔════╝██╔═══██╗██║   ██║██║     ██╔════╝    ██║██║██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 2);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██║  ██║███████║██████╔╝█████╔╝     ███████╗██║   ██║██║   ██║██║     ███████╗    ██║██║██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 3);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██║  ██║██╔══██║██╔══██╗██╔═██╗     ╚════██║██║   ██║██║   ██║██║     ╚════██║    ██║██║██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 4);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("██████╔╝██║  ██║██║  ██║██║  ██╗    ███████║╚██████╔╝╚██████╔╝███████╗███████║    ██║██║██║");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 5);
                        Console.WriteLine(pictureTitle);
                        pictureTitle = ("╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝    ╚══════╝ ╚═════╝  ╚═════╝ ╚══════╝╚══════╝    ╚═╝╚═╝╚═╝");
                        Console.SetCursorPosition((windowsWidth - pictureTitle.Length) / 2, windowsHeight / 2 + pictureRows + 6);
                        Console.WriteLine(pictureTitle);
                        #endregion
                        #endregion
                        #region 游戏基本信息提示
                        Console.SetCursorPosition(0, 1);
                        Console.WriteLine(onlineStr + "\n" + "游戏版本" + gameVersion + "\n" + "规则模式" + gameMode + "\n" + dlc1Name + "\n" + dlc2Name);
                        #endregion
                        #region 操作文字提示
                        Console.SetCursorPosition(windowsWidth - 14, windowsHeight - 4);
                        Console.WriteLine("W - 选择上一项");
                        Console.SetCursorPosition(windowsWidth - 14, windowsHeight - 3);
                        Console.WriteLine("S - 选择下一项");
                        Console.SetCursorPosition(windowsWidth - 14, windowsHeight - 2);
                        Console.WriteLine("E\\ENTER - 确认");
                        Console.SetCursorPosition(windowsWidth - 14, windowsHeight - 1);
                        Console.WriteLine("Q - 返回");
                        #endregion
                        #region 主菜单系统
                        keyEnterBool = false;
                        ready = false;
                        //———————————————————————————————————————————
                        //———————————————————————————————————————————
                        //———————————————————————————————————————————
                        #region 重要参数 选单文本行数，每次新增后需要调整这个值；
                        mainMenuLineReal = 7;
                        mainMenuLine = mainMenuLineReal + 1;
                        #endregion
                        //———————————————————————————————————————————
                        //———————————————————————————————————————————
                        //———————————————————————————————————————————
                        #region 主菜单系统执行
                        while (keyEnterBool == false)//选中选项后退出主菜单系统
                        {
                            #region 初始化印刷位置和颜色
                            Console.SetCursorPosition(windowsWidth / 2 - mainMenuStr.Length, windowsHeight / 2 + mainMenuLine + windowsHeight / 8 - 1);
                            Console.ForegroundColor = ConsoleColor.White;
                            #endregion
                            #region 倒叙初始化执行 刷新主菜单 执行成功后会开启每次监听
                            if (ready == false)
                            {
                                mainMenuLine--;
                                ready = mainMenuLine == 0 ? true : false;
                                mainMenuLine = mainMenuLine == 0 ? 1 : mainMenuLine;
                            }
                            #endregion
                            else
                            {
                                #region 按键监听 选择想要进入的页面
                                keyChar = Console.ReadKey(true).KeyChar;
                                //监听用户输入按键
                                Console.Write(mainMenuStr);
                                switch (keyChar)
                                {
                                    case 'W':
                                    case 'w':
                                        mainMenuLine--;
                                        mainMenuLine = mainMenuLine < 1 ? 1 : mainMenuLine;
                                        break;
                                    case 'S':
                                    case 's':
                                        mainMenuLine++;
                                        mainMenuLine = mainMenuLine > mainMenuLineReal ? mainMenuLineReal : mainMenuLine;
                                        break;
                                    case 'E':
                                    case 'e':
                                    case enter:
                                        {
                                            switch (mainMenuLine)
                                            {
                                                case 1://确认的是继续游戏，直接加载
                                                    Console.Clear();
                                                    mainMenuStr = "暂无存档系统";
                                                    Console.SetCursorPosition(windowsWidth / 2 - mainMenuStr.Length, windowsHeight / 2 - 1);
                                                    Console.WriteLine(mainMenuStr);
                                                    mainMenuStr = "按任一按钮返回主菜单";
                                                    Console.SetCursorPosition(windowsWidth / 2 - mainMenuStr.Length, windowsHeight / 2 + 1);
                                                    Console.WriteLine(mainMenuStr);
                                                    Thread.Sleep(1000);
                                                    Console.ReadKey(true);
                                                    Console.Clear();
                                                    keyChar = ' ';
                                                    logInBool = true;
                                                    keyEnterBool = true;
                                                    mainMenuScene = 0;
                                                    closeTheGame = false;
                                                    mainMenuScene = 0;
                                                    break;
                                                case 5://确认的是登陆，进行加载
                                                    #region 登陆系统
                                                    logInStr = "";
                                                    logInInt = 2;
                                                    logInBool = false;
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    popWindow = 0;
                                                    logInLineInt = 6;//登陆描述有几行？
                                                    //使用前进行初始化
                                                    #region 制作弹窗
                                                    while (popWindow != logInLineInt + 2)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Black;
                                                        logInStr = "----------------------------------------------";
                                                        Console.SetCursorPosition((windowsWidth - logInStr.Length) / 2, windowsHeight / 2 - logInLineInt / 2 + popWindow);
                                                        switch (popWindow)
                                                        {
                                                            case 0:
                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                break;
                                                            case 7:
                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                        Console.WriteLine(logInStr);
                                                        popWindow++;
                                                    }
                                                    #endregion
                                                    #region 报告连接状态
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    logInStr = "正在确认互联网链接状态";
                                                    Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2);
                                                    Console.WriteLine(logInStr);
                                                    Thread.Sleep(1000);
                                                    Console.ForegroundColor = ConsoleColor.Black;
                                                    Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2);
                                                    Console.WriteLine(logInStr);
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    logInStr = "连接失败";
                                                    Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2);
                                                    Console.WriteLine(logInStr);
                                                    Thread.Sleep(1000);
                                                    Console.ForegroundColor = ConsoleColor.Black;
                                                    Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2);
                                                    Console.WriteLine(logInStr);
                                                    #endregion
                                                    while (logInBool == false)
                                                    {
                                                        switch (logInInt)
                                                        {
                                                            case 0:
                                                                logInStr = "占位符";
                                                                break;
                                                            case 1:
                                                                logInStr = "连接失败";
                                                                break;
                                                            case 2:
                                                                logInStr = "将以脱机模式开始游玩";
                                                                break;
                                                            case 3:
                                                                logInStr = "如果要使用互联网功能";
                                                                break;
                                                            case 4:
                                                                logInStr = "请在确定联机环境后";
                                                                break;
                                                            case 5:
                                                                logInStr = "选择标题选单中的登陆";
                                                                break;
                                                            case 6:
                                                                logInStr = "确定";
                                                                break;
                                                            case 7:
                                                                Thread.Sleep(1500);
                                                                Console.ReadKey(true);
                                                                Console.Clear();
                                                                logInBool = true;
                                                                keyEnterBool = true;
                                                                mainMenuScene = 0;
                                                                closeTheGame = false;
                                                                break;
                                                        }
                                                        Console.ForegroundColor = logInInt == logInLineInt ? ConsoleColor.Red : ConsoleColor.White;
                                                        Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2 - logInLineInt / 2 + logInInt - 1);
                                                        if(logInInt == 6)
                                                        {
                                                            Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2 - logInLineInt / 2 + logInInt);
                                                        }
                                                        Console.WriteLine(logInStr);
                                                        logInInt++;
                                                    }
                                                    #endregion
                                                    break;
                                                case 6://确认的是结束游戏，所以关闭控制台
                                                    Environment.Exit(0);
                                                    break;
                                                default://确认的不是弹窗，也不是直接进入游戏，跳转进其他的菜单页
                                                    keyEnterBool = true;
                                                    mainMenuScene = mainMenuLine;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 'Q':
                                    case 'q':
                                        keyEnterBool = true;
                                        mainMenuScene = 0;
                                        break;
                                    default:
                                        break;
                                }//根据按键操作返回结果的逻辑
                                #endregion
                            }
                            #region 主菜单每行内容填写 修改后需在准备工作修改行上限
                            switch (mainMenuLine)
                            {
                                case 0:
                                    mainMenuStr = "返回值，自身，占位符";//是刷新当前页
                                    break;
                                case 1:
                                    mainMenuStr = "继续游戏";//是直接进入
                                    break;
                                case 2:
                                    mainMenuStr = "加载游戏";//是切换其他页面
                                    break;
                                case 3:
                                    mainMenuStr = "开始游戏";//是切换其他页面
                                    break;
                                case 4:
                                    mainMenuStr = "系统设定";//是切换其他页面
                                    break;
                                case 5:
                                    mainMenuStr = "登陆";//是弹窗
                                    break;
                                case 6:
                                    mainMenuStr = "结束游戏";//是直接关闭
                                    break;
                                case 7:
                                    mainMenuStr = "联系我们";//是切换其他页面
                                    break;
                                default:
                                    mainMenuStr = "";//其他情况
                                    break;
                            }
                            #endregion
                            #region 选中项刷新 显示高亮
                            Console.SetCursorPosition(windowsWidth / 2 - mainMenuStr.Length, windowsHeight / 2 + mainMenuLine + windowsHeight / 8 - 1);
                            Console.ForegroundColor = ready == true ? ConsoleColor.Red : ConsoleColor.White;//当前是否处于初始化阶段？如果是，那么当前选中的行颜色也是白色的
                            Console.Write(mainMenuStr);//印刷当前选中项
                            #endregion
                        }
                        #region 监听输入后清空页面
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        #endregion
                        #region 监听输入后输出的结果
                        #endregion
                        #endregion
                        #endregion
                        break;
                    #endregion
                    #region 菜单_02加载游戏
                    case 2:
                        Console.Clear();
                        mainMenuStr = "暂无存档系统";
                        Console.SetCursorPosition(windowsWidth / 2 - mainMenuStr.Length, windowsHeight / 2 - 1);
                        Console.WriteLine(mainMenuStr);
                        mainMenuStr = "按任一按钮返回主菜单";
                        Console.SetCursorPosition(windowsWidth / 2 - mainMenuStr.Length, windowsHeight / 2 + 1);
                        Console.WriteLine(mainMenuStr);
                        Thread.Sleep(1000);
                        Console.ReadKey(true);
                        Console.Clear();
                        keyChar = ' ';
                        mainMenuScene = 0;
                        break;
                    #endregion
                    #region 菜单_03开始游戏
                    case 3:
                        #region 启动初始化设置
                        Console.CursorVisible = false;
                        //基础设置隐藏光标

                        int printLin = 1;
                        //非标题栏打印位置
                        #endregion
                        #region 全属性声明为变量
                        int level = 10;
                        int vitality = 10;
                        int concentratedForce = 10;
                        int persistence = 10;
                        int physicalStrength = 10;
                        int strength = 10;
                        int Agile = 10;
                        int intelligence = 10;
                        int belief = 10;
                        int luck = 10;
                        #endregion
                        #region 初始化性别选择
                        bool confirmGender = true;
                        E_Gender gender = E_Gender.男;
                        //创建变量

                        Console.WriteLine("选择性别\n男\n女");
                        //打印默认文字
                        #endregion
                        #region 性别选择
                        while (confirmGender)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(0, (int)gender + printLin);
                            Console.WriteLine(gender);
                            //当前选项高亮
                            char userinput = Console.ReadKey(true).KeyChar;
                            //按键监听
                            Console.SetCursorPosition(0, (int)gender + printLin);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(gender);
                            //前选择清除
                            switch (userinput)
                            {
                                case 'W':
                                case 'w':
                                    gender = E_Gender.男;
                                    break;
                                case 'S':
                                case 's':
                                    gender = E_Gender.女;
                                    break;
                                case enter:
                                    confirmGender = false;
                                    break;
                            }
                        }
                        #endregion
                        #region 页面结束后清空屏幕
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        #endregion
                        #region 初始化职业选择
                        Console.WriteLine("选择职业");
                        bool confirmCarreer = true;
                        E_Carreer carreer = E_Carreer.骑士;
                        while (carreer <= E_Carreer.一无所有者)
                        {
                            Console.WriteLine(carreer);
                            carreer = (E_Carreer)(int)++carreer;
                        }
                        //初始化职业选择印刷
                        carreer = E_Carreer.骑士;
                        //初始化职业选择
                        #endregion
                        #region 职业选择
                        while (confirmCarreer)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(0, (int)carreer + printLin);
                            Console.WriteLine(carreer);
                            //当前选项高亮
                            char userinput = Console.ReadKey(true).KeyChar;
                            //按键监听
                            Console.SetCursorPosition(0, (int)carreer + printLin);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(carreer);
                            //前选择清除
                            switch (userinput)
                            {
                                case 'W':
                                case 'w':
                                    carreer = carreer == E_Carreer.骑士 ? carreer : (E_Carreer)(int)--carreer;
                                    break;
                                case 'S':
                                case 's':
                                    carreer = carreer == E_Carreer.一无所有者 ? carreer : (E_Carreer)(int)++carreer;
                                    break;
                                case enter:
                                    confirmCarreer = false;
                                    break;
                            }
                        }
                        #endregion
                        #region 页面结束后清空屏幕
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        #endregion
                        #region 打印当前选择性别和职业
                        switch (carreer)
                        {
                            case E_Carreer.骑士:
                                level -= 1;
                                vitality += 2;
                                concentratedForce += 0;
                                persistence += 1;
                                physicalStrength += 5;
                                strength += 3;
                                Agile += 2;
                                intelligence += 0;
                                belief -= 1;
                                luck -= 3;
                                break;
                            case E_Carreer.佣兵:
                                level -= 2;
                                vitality += 1;
                                concentratedForce += 2;
                                persistence += 1;
                                physicalStrength += 0;
                                strength += 0;
                                Agile += 6;
                                intelligence += 0;
                                belief -= 2;
                                luck = luck - 1;
                                break;
                            case E_Carreer.战士:
                                level -= 1;
                                vitality += 4;
                                concentratedForce -= 4;
                                persistence += 2;
                                physicalStrength += 1;
                                strength += 6;
                                Agile += 1;
                                intelligence += 0;
                                belief -= 1;
                                luck += 1;
                                break;
                            case E_Carreer.传令者:
                                level += 4;
                                vitality += 2;
                                concentratedForce += 0;
                                persistence -= 1;
                                physicalStrength += 2;
                                strength += 2;
                                Agile += 1;
                                intelligence += 0;
                                belief += 8;
                                luck += 1;
                                break;
                            case E_Carreer.小偷:
                                level += 5;
                                vitality += 0;
                                concentratedForce += 1;
                                persistence += 0;
                                physicalStrength -= 1;
                                strength = strength - 1;
                                Agile += 3;
                                intelligence += 0;
                                belief -= 2;
                                luck += 4;
                                break;
                            case E_Carreer.刺客:
                                level += 8;
                                vitality += 0;
                                concentratedForce += 4;
                                persistence += 1;
                                physicalStrength += 0;
                                strength += 0;
                                Agile += 4;
                                intelligence += 1;
                                belief += 7;
                                luck += 0;
                                break;
                            case E_Carreer.魔法师:
                                level += 1;
                                vitality -= 1;
                                concentratedForce += 6;
                                persistence -= 1;
                                physicalStrength -= 3;
                                strength = strength - 3;
                                Agile += 2;
                                intelligence += 6;
                                belief += 2;
                                luck += 2;
                                break;
                            case E_Carreer.咒术师:
                                level += 5;
                                vitality += 1;
                                concentratedForce += 2;
                                persistence += 0;
                                physicalStrength -= 2;
                                strength += 2;
                                Agile += 6;
                                intelligence += 4;
                                belief += 4;
                                luck = luck - 3;
                                break;
                            case E_Carreer.圣职:
                                level += 3;
                                vitality += 0;
                                concentratedForce += 4;
                                persistence -= 1;
                                physicalStrength -= 3;
                                strength += 2;
                                Agile -= 2;
                                intelligence += 2;
                                belief += 10;
                                luck += 3;
                                break;
                            case E_Carreer.一无所有者:
                                level -= 9;
                                break;
                            default:
                                break;
                        }
                        Console.WriteLine("选择完成\n性别:" + gender + "\n职业:" + carreer);
                        Console.WriteLine("等级 = " + level + "\n生命力 = " + vitality + "\n集中力 = " + concentratedForce + "\n持久力 = " + persistence + "\n体力 = " + physicalStrength + "\n力气 = " + strength + "\n敏捷 = " + Agile + "\n智力 = " + intelligence + "\n信仰 = " + belief + "\n运气 = " + luck);
                        #endregion
                        Console.ReadLine();
                        //防止自动切入循环，输入监听后继续
                        Console.Clear();
                        break;
                    #endregion
                    #region 菜单_04系统设定
                    case 4:
                        Console.Clear();
                        #region 制作弹窗
                        popWindow = 0;
                        while (popWindow != logInLineInt + 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            logInStr = "----------------------------------------------";
                            Console.SetCursorPosition((windowsWidth - logInStr.Length) / 2, windowsHeight / 2 - logInLineInt / 2 + popWindow);
                            switch (popWindow)
                            {
                                case 0:
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                case 7:
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                default:
                                    break;
                            }
                            Console.WriteLine(logInStr);
                            popWindow++;
                        }
                        #endregion
                        #region 首次进入提示
                        if (JionMainMenuScene4 == false)
                        {
                            JionMainMenuScene4 = true;
                            logInStr = "请谨慎使用";
                            Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2 - 1);
                            Console.WriteLine(logInStr);
                            logInStr = "可能会导致游戏出现严重错误甚至崩溃";
                            Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2);
                            Console.WriteLine(logInStr);
                            logInStr = "确认";
                            Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2 + 3);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(logInStr);
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(1000);
                            Console.ReadKey(true);
                            Console.Clear();
                        }
                        #endregion
                        #region 操作文字提示
                        Console.SetCursorPosition(windowsWidth - 14, windowsHeight - 4);
                        Console.WriteLine("W - 选择上一项");
                        Console.SetCursorPosition(windowsWidth - 14, windowsHeight - 3);
                        Console.WriteLine("S - 选择下一项");
                        Console.SetCursorPosition(windowsWidth - 14, windowsHeight - 2);
                        Console.WriteLine("E\\ENTER - 确认");
                        Console.SetCursorPosition(windowsWidth - 14, windowsHeight - 1);
                        Console.WriteLine("Q - 返回");
                        #endregion
                        //———————————————————————————————————————————
                        //———————————————————————————————————————————
                        #region 重要参数 选单文本行数，每次新增后需要调整这个值；
                        mainMenuLineReal = 7;
                        mainMenuLine = mainMenuLineReal + 1;
                        #endregion
                        //———————————————————————————————————————————
                        //———————————————————————————————————————————
                        #region 倒叙初始化执行 刷新系统设定菜单 执行成功后会开启每次监听
                        mainMenuStr = "";
                        keyEnterBool = false;
                        ready = false;
                        #endregion
                        #region 系统设置菜单系统执行
                        while (keyEnterBool == false)//选中选项后退出主菜单系统
                        {
                            #region 初始化印刷位置和颜色
                            Console.SetCursorPosition(windowsWidth / 2 - mainMenuStr.Length, windowsHeight / 2 + mainMenuLine + windowsHeight / 8 - 1);
                            Console.ForegroundColor = ConsoleColor.White;
                            #endregion
                            #region 倒叙初始化执行 刷新主菜单 执行成功后会开启每次监听
                            if (ready == false)
                            {
                                mainMenuLine--;
                                ready = mainMenuLine == 0 ? true : false;
                                mainMenuLine = mainMenuLine == 0 ? 1 : mainMenuLine;
                            }
                            #endregion
                            else
                            {
                                #region 按键监听 选择想要进入的页面
                                keyChar = Console.ReadKey(true).KeyChar;
                                //监听用户输入按键
                                Console.Write(mainMenuStr);
                                switch (keyChar)
                                {
                                    case 'W':
                                    case 'w':
                                        mainMenuLine--;
                                        mainMenuLine = mainMenuLine < 1 ? 1 : mainMenuLine;
                                        break;
                                    case 'S':
                                    case 's':
                                        mainMenuLine++;
                                        mainMenuLine = mainMenuLine > mainMenuLineReal ? mainMenuLineReal : mainMenuLine;
                                        break;
                                    case 'E':
                                    case 'e':
                                    case enter:
                                        #region 制作弹窗
                                        popWindow = 0;
                                        while (popWindow != logInLineInt + 2)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            logInStr = "----------------------------------------------";
                                            Console.SetCursorPosition((windowsWidth - logInStr.Length) / 2, windowsHeight / 2 - logInLineInt / 2 + popWindow);
                                            switch (popWindow)
                                            {
                                                case 0:
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    break;
                                                case 7:
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    break;
                                                default:
                                                    break;
                                            }
                                            Console.WriteLine(logInStr);
                                            popWindow++;
                                        }
                                        #endregion
                                        logInStr = "当前功能未开发";
                                        Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2 - 1);
                                        Console.WriteLine(logInStr);
                                        logInStr = "敬请期待";
                                        Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2);
                                        Console.WriteLine(logInStr);
                                        logInStr = "确认";
                                        Console.SetCursorPosition(windowsWidth / 2 - logInStr.Length, windowsHeight / 2 + 2);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(logInStr);
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Thread.Sleep(1000);
                                        Console.ReadKey(true);
                                        Console.Clear();
                                        keyEnterBool = true;
                                        mainMenuScene = 4;
                                        break;
                                    case 'Q':
                                    case 'q':
                                        Console.Clear();
                                        keyEnterBool = true;
                                        mainMenuScene = 0;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #region 主菜单每行内容填写 修改后需在准备工作修改行上限
                            switch (mainMenuLine)
                            {
                                case 0:
                                    mainMenuStr = "返回值，自身，占位符";//是回到首页
                                    break;
                                case 1:
                                    mainMenuStr = "游戏选项";//是直接进入
                                    break;
                                case 2:
                                    mainMenuStr = "镜头选项";//是切换其他页面
                                    break;
                                case 3:
                                    mainMenuStr = "显示及声音设定";//是切换其他页面
                                    break;
                                case 4:
                                    mainMenuStr = "亮度设定";//是切换其他页面
                                    break;
                                case 5:
                                    mainMenuStr = "联机相关设定";//是弹窗
                                    break;
                                case 6:
                                    mainMenuStr = "操控装置设定";//是直接关闭
                                    break;
                                case 7:
                                    mainMenuStr = "画面设定";//是切换其他页面
                                    break;
                                default:
                                    mainMenuStr = "";//其他情况
                                    break;
                            }
                            #endregion
                            #region 选中项刷新 显示高亮
                            Console.SetCursorPosition(windowsWidth / 2 - mainMenuStr.Length, windowsHeight / 2 + mainMenuLine + windowsHeight / 8 - 1);
                            Console.ForegroundColor = ready == true ? ConsoleColor.Red : ConsoleColor.White;//当前是否处于初始化阶段？如果是，那么当前选中的行颜色也是白色的
                            Console.Write(mainMenuStr);//印刷当前选中项
                            #endregion
                        }
                        break;
                            #endregion
                        }
                #endregion
                #endregion
            }
        }
    }
}