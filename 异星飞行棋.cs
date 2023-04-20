using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Formats.Asn1.AsnWriter;

namespace LessonMai//传说中又惊险又刺激的飞行棋之控制台二维版
{
    public struct GamePlayer//结构体，区分是玩家还是Ai、玩家名字、当前走过的部署
    {
        public bool ai;
        public string name;
        public int steps;
        public int gold;
    }
    public struct PropsProps//游戏道具构造体，可以在游戏主函数中
    {
        public int[] 折跃门;
        public int[] 陨石;
        public int[] 占卜师;
        public int[] 商店;
        public int[] 太空掠食者;
    }
    public enum E_specialEffect
    {
        没钱,
        奸商,
        曲率引擎,
        舰载武器,
    }
    public enum E_PropsProps//游戏场景枚举
    {
        折跃门,
        陨石,
        占卜师,
        商店,
        太空掠食者,
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
        hotSeat,//热座模式，即单设备的多人本地对战
        babyBus,//宝宝巴士模式，即默认的人机对战模式
        intellectualEquipmentCrisis,//智械危机模式，高难度人机对战
        superRich,//富豪模式，双方阵营开局999金币，商店刷新数量X10
        cultivatingImmortals,//修仙模式，占卜师数量X10
        bloodyHero,//喋血英豪模式，踩到太空掠食者不获得赏金，会再次投掷骰子
        assassinCreed,//踩到对手后，可以将其击退六格
        portal,//折跃门规则修改，走进折跃门后，将会随机出现在另一个折跃门
        meteorShower,//流星雨模式，陨石数量X2
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
            const int Height = 29;
            return Height;
        }
        public static int GetWindowsWidth()//宽度接口
        {
            const int Width = 160;
            return Width;
        }
        public static void SetWindows()//默认调用接口并设置
        {
            Console.CursorVisible = false;
            int Height = GetWindowsHeight();
            int Width = GetWindowsWidth();
            Console.SetWindowSize(Width + 1, Height + 1);
            Console.SetBufferSize(Width + 2, Height + 2);//将缓冲区设置的比窗口更大一些避免exe出错
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
            PlotBroadcast();//开始剧情
            E_GameScene e_GameScene = E_GameScene.mainMenu;//调用枚举
            Scene(ref e_GameScene);

        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void Scene(ref E_GameScene e_GameScene)
        {
            while (true)
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
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void GameScene(ref E_GameScene e_GameScene)
        {
            Console.Clear();
            PrintingMap();//印刷场景
            PrintingRules();
            int LevelData = FightMap(true);
            #region 声明会用到的变量
            int progress = 0;
            int steps = 0;
            bool firsTime = true;
            bool Manual = false;
            int action = 1;
            string ActorName = "";
            bool gameRuns = true;
            int gold = 0;
            #endregion
            //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
            #region 声明玩家和电脑的结构体
            GamePlayer Player;
            Player.steps = 0;
            Player.name = "★";
            Player.ai = false;
            Player.gold = 3;//开局有3金币**************************************************************************************
            GamePlayer AlienStar;
            AlienStar.steps = 0;
            AlienStar.name = "●";
            AlienStar.ai = true;
            AlienStar.gold = 3;//开局有3金币**************************************************************************************
            #endregion
            //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
            #region 声明场景道具，初始化它们的参数，枚举参数的数量代表着道具的数量
            PropsProps propsProps;
            propsProps.折跃门 = new int[LevelData / 30];//折跃门**************************************************************************************
            propsProps.陨石 = new int[LevelData / 6];//陨石**************************************************************************************
            propsProps.占卜师 = new int[LevelData / 120];//占卜师**************************************************************************************
            propsProps.商店 = new int[LevelData / 60];//商店******************************************************************************************
            propsProps.太空掠食者 = new int[LevelData / 20];//太空掠食者********************************************************************************
            #endregion
            #region 将道具的坐标储存进propsPlacement，为他们编号
            int[][] propsPlacement = { propsProps.折跃门, propsProps.陨石, propsProps.占卜师, propsProps.商店, propsProps.太空掠食者 };
            #endregion
            #region 将编好号的道具列表传入方法，将其随机化
            propsPlacement = PropsPlacement(LevelData, propsPlacement);//生成随机道具参数
            #endregion
            //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
            PropMap(propsPlacement);//地图印刷
            //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
            #region 投掷骰子并前进的逻辑
            Random random = new Random();
            int[] printCoordinates;
            bool[,] specialEffectBool = { { false , false },
                                          { false , false } };//商店的两种效果
            while (gameRuns)
            {
                PrintingMap();//印刷场景
                FightMap();//场景印刷
                PropMap(propsPlacement);//地图印刷
                #region 印刷终点图标
                printCoordinates = CoordinateSystemConversion(LevelData - 1);
                Console.SetCursorPosition(printCoordinates[0], printCoordinates[1]);
                WriteLineColorOnce("■", false);
                #endregion
                #region 开始游戏前打印金币数量和其他提示
                Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 6);
                Console.Write("人类阵营的金币数量是：");
                WriteLineColorOnce(Player.gold.ToString(), false, ConsoleColor.DarkYellow);
                Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 7);
                Console.Write("异星阵营当前的金币数量是：");
                WriteLineColorOnce(AlienStar.gold.ToString(), false, ConsoleColor.DarkYellow);
                Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 4);
                Console.Write("？表示对应玩家和道具产生交互的起点");
                Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 5);
                Console.Write("！表示对应玩家和道具产生交互的终点");
                #endregion
                action++;//循环开始时，进行行动
                action = action == 3 ? 1 : action;
                if (action == 1)//行动为单数时，是玩家的行动。行动为双数时，是电脑的行动
                {
                    ActorName = Player.name;
                    progress = Player.steps;
                    Manual = Player.ai;
                    gold = Player.gold;
                }
                else
                {
                    ActorName = AlienStar.name;
                    progress = AlienStar.steps;
                    Manual = AlienStar.ai;
                    gold = AlienStar.gold;
                }
                #region 存在优化空间的代码，应该有更好的印刷方式
                printCoordinates = CoordinateSystemConversion(Player.steps);
                Console.SetCursorPosition(printCoordinates[0], printCoordinates[1]);
                WriteLineColorOnce(Player.name, false);

                printCoordinates = CoordinateSystemConversion(AlienStar.steps);
                Console.SetCursorPosition(printCoordinates[0], printCoordinates[1]);
                WriteLineColorOnce(AlienStar.name, false, ConsoleColor.DarkGreen);
                if (Player.steps == AlienStar.steps)
                {
                    printCoordinates = CoordinateSystemConversion(AlienStar.steps);
                    Console.SetCursorPosition(printCoordinates[0], printCoordinates[1]);
                    WriteLineColorOnce("★", false, ConsoleColor.DarkGreen);
                }
                #endregion
                if (firsTime)
                {
                    Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 2);
                    Console.Write("按下回车键开始丢骰子");
                    firsTime = false;
                }
                else
                {
                    if (!Manual)
                    {
                        Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 3);
                        Console.Write("按下回车键开始丢骰子，长按可自动游戏");
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 3);
                        Console.Write("敌人的回合等待丢骰子，请耐心等待敌人");
                    }
                }
                if (!Manual)
                {
                    Console.ReadLine();
                }
                for (int i = 0; i < 10; i++)
                {
                    steps = random.Next(1, 7);
                    Thread.Sleep(50);
                    Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1);
                    Console.Write("本回合骰子的点数是:{0}", steps);
                }
                Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1);
                Console.Write("本回合骰子的点数是:");
                WriteLineColorOnce(steps.ToString(), false);
                if (!specialEffectBool[action - 1, 1])//本回合没有被冻结
                {
                    if (action == 1 && steps == 6)
                    {
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] - 1);
                        WriteLineColorOnce("！发射导弹", false);
                        action = action == 1 ? 2 : 1;
                        specialEffectBool[action - 1, 1] = true;
                        action = action == 1 ? 2 : 1;
                    }
                    if (action == 2 && steps < 3)
                    {
                        steps = steps + 1;
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] - 1);
                        WriteLineColorOnce("！发动引擎", false, ConsoleColor.DarkGreen);
                    }
                    if (specialEffectBool[action - 1, 0] == true)//获得了曲率引擎
                    {
                        if (gold >= 1)
                        {
                            progress = progress + 1;
                            gold--;
                        }
                    }
                    progress = progress + steps;//行动结束，所在位置
                    int[] gameSettlement = new int[3];
                    gameSettlement = GameSettlement(action, progress, gold, propsPlacement);
                    progress = gameSettlement[0];
                    gold = gameSettlement[1];
                    int specialEffect = gameSettlement[2];
                    switch (specialEffect)
                    {
                        case (int)E_specialEffect.曲率引擎:
                            specialEffectBool[action - 1, 0] = true;
                            break;
                        case (int)E_specialEffect.舰载武器:
                            action = action == 1 ? 2 : 1;
                            specialEffectBool[action - 1, 1] = true;
                            action = action == 1 ? 2 : 1;
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                else
                {
                    specialEffectBool[action - 1, 1] = false;
                }
                #region 行动结束后，进入结算流程
                if (action == 1)//结算结束时，结算的数值进行保存
                {
                    Player.gold = gold;
                    Player.steps = progress;
                }
                else
                {
                    AlienStar.gold = gold;
                    AlienStar.steps = progress;
                }
                if (progress >= LevelData)
                {
                    gameRuns = false;
                    Console.Clear();
                    if (Player.steps >= progress)
                    {
                        WriteLineColorOnce("人类胜利了！");
                    }
                    else
                    {
                        WriteLineColorOnce("惨败！少侠请重新来过", true, ConsoleColor.DarkGreen);
                    }
                    Thread.Sleep(10000);
                    Environment.Exit(0);//正常退出
                }
            }
            #endregion
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static int[] GameSettlement(int action, int progress, int gold, int[][] propsPlacement)//将玩家数据和场景道具数据导入结算页
        {
            int specialEffect = 0;
            if (action == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Random random = new Random();
            int propType = 0;
            bool generateInteraction = false;
            int[] printCoordinates;
            for (int I = 0; I < propsPlacement.Length; I++)
            {
                for (int i = 0; i < propsPlacement[I].Length; i++)//遍历每一个道具的位置，查找重叠的
                {
                    if (propsPlacement[I][i] == progress)
                    {
                        propType = I;//找到重叠的后，进行标记
                        generateInteraction = true;
                    }
                }
            }//由于是循环，会覆盖数据，所以最终到处的是最后印刷进游戏的道具（道具坐标可能存在重叠）所以不用担心触发和可视化错误
            if (generateInteraction)//如果确实产生了交互，才会进行效果的判断
            {
                Console.SetCursorPosition(Windows.GetWindowsWidth() - Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 5);
                switch (propType)
                {
                    case (int)E_PropsProps.折跃门:
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write("？");
                        int Fold = random.Next(0, 21);
                        progress = progress - 10 + Fold;
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write("!闪");
                        //随机出现在前后十格范围内
                        break;
                    case (int)E_PropsProps.陨石:
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write("？");
                        progress = progress - 1;
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write("!撞");
                        break;
                    case (int)E_PropsProps.占卜师:
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write("？");
                        int[] divination = Divination(progress);
                        progress = divination[0];
                        string divinationWrite = "占卜失败";
                        switch (divination[1])
                        {
                            case 1://!占卜结果为：VII战车-正位
                                divinationWrite = "!占卜结果为：VII战车-正位";
                                break;
                            case 2://!占卜结果为：VII战车-逆位
                                divinationWrite = "!占卜结果为：VII战车-逆位";
                                break;
                            case 3://!占卜结果为：X命运之轮-正位
                                divinationWrite = "!占卜结果为：X命运之轮-正位";
                                break;
                            case 4://!占卜结果为：X命运之轮-逆位
                                divinationWrite = "!占卜结果为：X命运之轮-逆位";
                                break;
                            case 5://!占卜结果为：XV恶魔-正位
                                divinationWrite = "!占卜结果为：XV恶魔-正位";
                                break;
                            case 6://!占卜结果为：XV恶魔-逆位
                                divinationWrite = "!占卜结果为：XV恶魔-逆位";
                                break;
                            case 7://!占卜结果为：XXI世界-正位
                                divinationWrite = "!占卜结果为：XXI世界-正位";
                                break;
                            case 8://!占卜结果为：XXI世界-逆位
                                divinationWrite = "!占卜结果为：XXI世界-逆位";
                                break;
                        }
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] - 1);
                        Console.Write(divinationWrite);
                        //随机神秘效果
                        break;
                    case (int)E_PropsProps.商店:
                        int[] shop;
                        printCoordinates = CoordinateSystemConversion(progress);
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write("？");
                        shop = Shop(gold, progress);
                        progress = shop[0];
                        gold = shop[1];
                        specialEffect = shop[2];
                        string specialEffectStr = "";
                        switch (specialEffect)
                        {
                            case (int)E_specialEffect.没钱:
                                specialEffectStr = "!没钱";
                                break;
                            case (int)E_specialEffect.奸商:
                                specialEffectStr = "!奸商";
                                break;
                            case (int)E_specialEffect.曲率引擎:
                                specialEffectStr = "!曲率引擎:有金币时，每回合扣一金币，加一移速";
                                break;
                            case (int)E_specialEffect.舰载武器:
                                specialEffectStr = "!舰载武器:对手跳过一回合";
                                break;
                            default:
                                break;
                        }
                        printCoordinates = CoordinateSystemConversion(progress);
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write(specialEffectStr);
                        //扣除三金币，随机获得神秘效果
                        break;
                    case (int)E_PropsProps.太空掠食者:
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write("？");
                        gold = ++gold;
                        printCoordinates = CoordinateSystemConversion(progress);
                        Console.SetCursorPosition(printCoordinates[0], printCoordinates[1] + 1);
                        Console.Write("!赏");
                        //获得一金币
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            progress = progress < 0 ? 0 : progress;//数值校验，避免低于最低格
            int[] gameSettlement = new int[3];
            gameSettlement[0] = progress;
            gameSettlement[1] = gold;
            gameSettlement[2] = specialEffect;
            return gameSettlement;
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static int[] Shop(int gold, int progress)
        {
            int[] shop = new int[3];
            Random random = new Random();
            int commodityResult = 0;
            if (gold >= 3)
            {
                gold = gold - 3;
                commodityResult = random.Next(1, 4);
                switch (commodityResult)
                {
                    case (int)E_specialEffect.奸商://!商品结果为：!奸商
                        break;
                    case (int)E_specialEffect.曲率引擎://!商品结果为：!曲率引擎
                        progress = progress + 3;
                        break;
                    case (int)E_specialEffect.舰载武器://!商品结果为：!舰载武器
                        break;
                }
            }
            shop[0] = progress;
            shop[1] = gold;
            shop[2] = commodityResult;
            return shop;
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static int[] Divination(int progress)
        {
            int[] divination = new int[2];
            Random random = new Random();
            int divinationResult = random.Next(1, 7);
            switch (divinationResult)
            {
                case 1://!占卜结果为：VII战车-正位
                    progress = progress + 7;
                    break;
                case 2://!占卜结果为：VII战车-逆位
                    progress = progress - 7;
                    break;
                case 3://!占卜结果为：X命运之轮-正位
                    progress = progress + random.Next(1, 11);
                    break;
                case 4://!占卜结果为：X命运之轮-逆位
                    progress = progress - random.Next(1, 11);
                    break;
                case 5://!占卜结果为：XV恶魔-正位
                    progress = progress - 15;
                    break;
                case 6://!占卜结果为：XV恶魔-逆位
                    progress = progress + 15;
                    break;
                case 7://!占卜结果为：XXI世界-正位
                    progress = progress % 3 == 0 ? progress * 2 : progress;
                    break;
                case 8://!占卜结果为：XXI世界-逆位
                    progress = progress % 3 == 0 ? progress / 2 : progress;
                    break;
            }
            divination[0] = progress;
            divination[1] = divinationResult;
            return divination;
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void PropMap(int[][] propsPlacement)
        {
            int[] printCoordinates;
            for (int I = 0; I < propsPlacement.Length; I++)
            {
                for (int i = 0; i < propsPlacement[I].Length; i++)
                {
                    printCoordinates = CoordinateSystemConversion(propsPlacement[I][i]);
                    Console.SetCursorPosition(printCoordinates[0], printCoordinates[1]);
                    switch (I)
                    {
                        case (int)E_PropsProps.折跃门:
                            WriteLineColorOnce("门", false, ConsoleColor.Blue);
                            break;
                        case (int)E_PropsProps.陨石:
                            WriteLineColorOnce("陨", false, ConsoleColor.White);
                            break;
                        case (int)E_PropsProps.占卜师:
                            WriteLineColorOnce("占", false, ConsoleColor.Yellow);
                            break;
                        case (int)E_PropsProps.商店:
                            WriteLineColorOnce("商", false, ConsoleColor.Yellow);
                            break;
                        case (int)E_PropsProps.太空掠食者:
                            WriteLineColorOnce("掠", false, ConsoleColor.Red);
                            break;
                    }
                }
            }
        }
        static int[][] PropsPlacement(int LevelData, params int[][] propsIncoming)
        {
            Random random = new Random();
            for (int I = 0; I < propsIncoming.Length; I++)
            {
                for (int i = 0; i < propsIncoming[I].Length; i++)
                {
                    propsIncoming[I][i] = random.Next(LevelData);
                }
            }
            return propsIncoming;//生成随机地图
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
                "传说中又惊险又刺激的飞行棋",
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

        static void PrintingMap()//墙体打印
        {
            for (int Printing = 0; Printing < Windows.GetWindowsWidth() - 1; ++Printing)
            {
                if (Printing % 2 == 0)
                {
                    Console.SetCursorPosition(Printing, 0);
                    Console.Write("■");
                    Console.SetCursorPosition(Printing, Windows.GetWindowsHeight() - 1);//减1是因为打印是从第0行开始算第1行，如果不减一会溢出崩溃
                    Console.Write("■");
                    Console.SetCursorPosition(Printing, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3));//除以三分之一，印刷场景
                    Console.Write("■");
                }
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
        static int FightMap(bool firstPrint)//重载，true即是初次打印战斗地图，会记录关卡的进度，关卡坐标系;不输入则不会记录，但是会更新游戏内容
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            int record = 0;
            int PrintingWidth = 0;
            int PrintingHeight = 2;
            bool continuousPrinting = true;
            while (continuousPrinting)
            {
                #region 是否达到印刷页面上限？
                if (PrintingHeight > Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) - 7)
                {
                    continuousPrinting = false;
                }
                #endregion
                ++PrintingHeight;
                #region 打印顺行的逻辑
                for (PrintingWidth = 6; PrintingWidth < Windows.GetWindowsWidth() - 6; PrintingWidth++)
                {
                    if (PrintingWidth % 2 == 0)
                    {
                        Console.SetCursorPosition(PrintingWidth, PrintingHeight);
                        Console.Write("□");
                        record++;
                    }
                }
                #endregion
                #region 打印右侧衔接
                ++PrintingHeight;
                Console.SetCursorPosition(PrintingWidth - 2, PrintingHeight);
                Console.Write("□");
                record++;
                #endregion
                ++PrintingHeight;
                #region 打印逆行的逻辑
                for (; PrintingWidth > 6; PrintingWidth--)
                {
                    if (PrintingWidth % 2 == 0)
                    {
                        Console.SetCursorPosition(PrintingWidth - 2, PrintingHeight);
                        Console.Write("□");
                        record++;
                    }
                }
                #endregion
                #region 打印左侧衔接
                ++PrintingHeight;
                Console.SetCursorPosition(PrintingWidth, PrintingHeight);
                Console.Write("□");
                record++;
                #endregion
            }
            int LevelData = record;//关卡的进度上限
            Console.ForegroundColor = ConsoleColor.White;
            return LevelData;//关卡的进度上限
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void FightMap()//重载，true即是初次打印战斗地图，会记录关卡的进度，关卡坐标系;不写则不会记录，但是会更新游戏内容
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            int PrintingWidth = 0;
            int PrintingHeight = 2;
            bool continuousPrinting = true;
            while (continuousPrinting)
            {
                #region 是否达到印刷页面上限？
                if (PrintingHeight > Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) - 7)
                {
                    continuousPrinting = false;
                }
                #endregion
                ++PrintingHeight;
                #region 打印顺行的逻辑
                for (PrintingWidth = 6; PrintingWidth < Windows.GetWindowsWidth() - 6; PrintingWidth++)
                {
                    if (PrintingWidth % 2 == 0)
                    {
                        Console.SetCursorPosition(PrintingWidth, PrintingHeight);
                        Console.Write("□");
                    }
                }
                #endregion
                #region 打印右侧衔接
                ++PrintingHeight;
                Console.SetCursorPosition(PrintingWidth - 2, PrintingHeight);
                Console.Write("□");
                #endregion
                ++PrintingHeight;
                #region 打印逆行的逻辑
                for (; PrintingWidth > 6; PrintingWidth--)
                {
                    if (PrintingWidth % 2 == 0)
                    {
                        Console.SetCursorPosition(PrintingWidth - 2, PrintingHeight);
                        Console.Write("□");
                    }
                }
                #endregion
                #region 打印左侧衔接
                ++PrintingHeight;
                Console.SetCursorPosition(PrintingWidth, PrintingHeight);
                Console.Write("□");
                #endregion
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static int[] CoordinateSystemConversion(int recordprogress)//输入关卡进度，输出显示坐标
        {
            int[] printCoordinates = new int[2];
            int record = 0;
            int PrintingWidth = 0;
            int PrintingHeight = 2;
            bool continuousPrinting = true;
            while (continuousPrinting)
            {
                #region 是否达到印刷页面上限？
                if (PrintingHeight > Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) - 7)
                {
                    continuousPrinting = false;
                }
                #endregion
                ++PrintingHeight;
                #region 打印顺行的逻辑
                for (PrintingWidth = 6; PrintingWidth < Windows.GetWindowsWidth() - 6; PrintingWidth++)
                {
                    if (PrintingWidth % 2 == 0)
                    {
                        if (recordprogress == record)
                        {
                            printCoordinates[0] = PrintingWidth;
                            printCoordinates[1] = PrintingHeight;
                            return printCoordinates;
                        }
                        record++;
                    }
                }
                #endregion
                #region 打印右侧衔接
                ++PrintingHeight;
                if (recordprogress == record)
                {
                    printCoordinates[0] = PrintingWidth - 2;
                    printCoordinates[1] = PrintingHeight;
                    return printCoordinates;
                }
                record++;
                #endregion
                ++PrintingHeight;
                #region 打印逆行的逻辑
                for (; PrintingWidth > 6; PrintingWidth--)
                {
                    if (PrintingWidth % 2 == 0)
                    {
                        if (recordprogress == record)
                        {
                            printCoordinates[0] = PrintingWidth - 2;
                            printCoordinates[1] = PrintingHeight;
                            return printCoordinates;
                        }
                        record++;
                    }
                }
                #endregion
                #region 打印左侧衔接
                ++PrintingHeight;
                if (recordprogress == record)
                {
                    printCoordinates[0] = PrintingWidth;
                    printCoordinates[1] = PrintingHeight;
                    return printCoordinates;
                }
                record++;
                #endregion
            }
            return printCoordinates;
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void PrintingRules()//印刷游戏规则
        {
            string[] PlayerRules =
                {
                "★表示人类阵营的星舰:",
                "战机根据每点骰子移动一格",
                "骰子点数为六时，发动空空导弹攻击前方",
                "空空导弹：击落的敌人下回合将无法移动",
                };
            string[] AiRules =
                {
                "●表示异星阵营的飞碟:",
                "飞碟根据每点骰子移动一格",
                "骰子点数为三以下，发动反物质引擎",
                "反物质引擎：本回合移动格子数加一",
                };
            string[] interactiveProps =
                {
                "■表示双方位置重叠",
                "■表示终点，先到达终点即可胜利",
                "折-闪|折跃门：踩到随机前往前后十格内",
                "陨-撞|陨石：踩到后退一格，不会连撞",
                "占-卜|占卜师：随机获得强力神秘效果",
                "商-买|商店：扣除三赏金随机获得神秘效果",
                "掠-赏|太空掠食者：踩到即击败获得一赏金",
                };
            for (int i = 0; i < PlayerRules.Length; ++i)
            {
                Console.SetCursorPosition(2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1 + i);
                //2是因为字符占两格，加1是避免覆盖到了划分格子的方块，加i是每次+1行打印
                Console.Write(PlayerRules[i]);
            }
            for (int i = 0; i < AiRules.Length; ++i)
            {
                Console.SetCursorPosition(Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1 + i);
                //2是因为字符占两格，加1是避免覆盖到了划分格子的方块，加i是每次+1行打印
                Console.Write(AiRules[i]);
            }
            for (int i = 0; i < interactiveProps.Length; ++i)
            {
                Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1 + i);
                Console.Write(interactiveProps[i]);
            }
            Console.SetCursorPosition(2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1);
            WriteLineColorOnce("★", false);
            Console.SetCursorPosition(Windows.GetWindowsWidth() / 4, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1);
            WriteLineColorOnce("●", false, ConsoleColor.DarkGreen);
            Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 1);
            WriteLineColorOnce("★", false, ConsoleColor.DarkGreen);
            Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 2);
            WriteLineColorOnce("■", false);
            Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 3);
            WriteLineColorOnce("门", false, ConsoleColor.Blue);
            Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 4);
            WriteLineColorOnce("陨", false, ConsoleColor.White);
            Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 5);
            WriteLineColorOnce("占", false, ConsoleColor.Yellow);
            Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 6);
            WriteLineColorOnce("商", false, ConsoleColor.Yellow);
            Console.SetCursorPosition(Windows.GetWindowsWidth() / 2, Windows.GetWindowsHeight() - (Windows.GetWindowsHeight() / 3) + 7);
            WriteLineColorOnce("掠", false, ConsoleColor.Red);
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
        static void PlotBroadcast()//开始的剧情播报，开发过程中为了不每次都看会跳过这个
        {
            string[] Plot =
                {
                "终末日危机爆发",
                "世界摇摇欲坠而你是被选中的人",
                "来拯救一切吧！和邪恶异星人来一场又惊险又刺激的",
                "太空飞行棋",
                "这场比赛的结果将决定人类的",
                "命运"
                };
            for (int i = 0; i < Plot.Length; ++i)
            {
                WriteLineColorOnce(Plot[i]);
                Thread.Sleep(750);
                Console.Clear();
            }
        }
        //—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线—————— ฅ՞• •՞ฅ ——————华丽分割线
    }
}
