using paramLesson;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace paramLesson
{
    internal class Program
    {
        struct Role
        {
            public string name;
            public int attack;
            public int attackRandom;
            public int health;
            public Role(string name, int attack, int attackRandom, int health)
            {
                this.name = name;
                this.attack = attack;
                this.attackRandom = attackRandom;
                this.health = health;
            }
        }
        static void Battle(int RoleTeamANumber, int RoleTeamBNumber, string RoleTeamA, string RoleTeamB)
        {
            Console.WriteLine("本轮是{0}只{1}对战{2}只{3}!", RoleTeamANumber, RoleTeamA, RoleTeamBNumber, RoleTeamB);
            Role altman = new Role("奥特曼", 125, 200, 1000);
            Role monster = new Role("怪兽", 15, 10, 200);
            int TeamAHealth = 0;
            int TeamBHealth = 0;
            int currentTeamAHealth = 0;
            int currentTeamBHealth = 0;
            int TeamA_Attack = 0;
            int TeamB_Attack = 0;
            int TeamA_AttackRandom = 0;
            int TeamB_AttackRandom = 0;
            Random r = new Random();
            for (int i = 0; i < 2; ++i)
            {
                string TeamID;
                if (i == 0)
                {
                    TeamID = RoleTeamA;
                }
                else
                {
                    TeamID = RoleTeamB;
                }

                switch (TeamID)
                {
                    case "怪兽":
                        if (i == 0)
                        {
                            TeamAHealth = monster.health;
                            TeamA_Attack = monster.attack;
                            TeamA_AttackRandom = monster.attackRandom;
                        }
                        else
                        {
                            TeamBHealth = monster.health;
                            TeamB_Attack = monster.attack;
                            TeamB_AttackRandom = monster.attackRandom;
                        }
                        break;
                    case "奥特曼":
                        if (i == 0)
                        {
                            TeamAHealth = altman.health;
                            TeamA_Attack = altman.attack;
                            TeamA_AttackRandom = altman.attackRandom;
                        }
                        else
                        {
                            TeamBHealth = altman.health;
                            TeamB_Attack = altman.attack;
                            TeamB_AttackRandom = altman.attackRandom;
                        }
                        break;
                    default:
                        Console.WriteLine("参数错误，无法战斗");
                        return;
                }
            }
            currentTeamAHealth = TeamAHealth;
            currentTeamBHealth = TeamBHealth;
            while (RoleTeamANumber > 0 && RoleTeamBNumber > 0)
            {
                int currentTeamB_AttackRandom = r.Next(TeamB_AttackRandom);
                currentTeamAHealth = currentTeamAHealth - TeamB_Attack - currentTeamB_AttackRandom > 0 ? currentTeamAHealth - TeamB_Attack - currentTeamB_AttackRandom : 0;
                Console.WriteLine("{0}攻击了{1}，造成{2}的伤害，现在{1}剩余{3}的生命值！", RoleTeamB, RoleTeamA, TeamB_Attack + currentTeamB_AttackRandom, currentTeamAHealth);
                if (currentTeamAHealth == 0)
                {
                    currentTeamAHealth = TeamAHealth;
                    RoleTeamANumber = --RoleTeamANumber;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n1只{0}死亡！现在{0}还剩余{1}只！\n", RoleTeamA, RoleTeamANumber);
                    Console.ForegroundColor = ConsoleColor.White;
                    if (RoleTeamANumber == 0) { break; }
                }

                int currentTeamA_AttackRandom = r.Next(TeamA_AttackRandom);
                currentTeamBHealth = currentTeamBHealth - TeamA_Attack - currentTeamA_AttackRandom > 0 ? currentTeamBHealth - TeamA_Attack - currentTeamA_AttackRandom : 0;
                Console.WriteLine("{0}攻击了{1}，造成{2}的伤害，现在{1}剩余{3}的生命值！", RoleTeamA, RoleTeamB, TeamA_Attack + currentTeamA_AttackRandom, currentTeamBHealth);
                if (currentTeamBHealth == 0)
                {
                    currentTeamBHealth = TeamBHealth;
                    RoleTeamBNumber = --RoleTeamBNumber;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n1只{0}死亡！现在{0}还剩余{1}只！\n", RoleTeamB, RoleTeamBNumber);
                    Console.ForegroundColor = ConsoleColor.White;
                    if (RoleTeamBNumber == 0) { break; }
                }
            }
            if (RoleTeamANumber > RoleTeamBNumber)
            {
                Console.WriteLine("游戏结束！{0}击败光了{1}！{0}胜利！", RoleTeamA, RoleTeamB);
            }
            else
            {
                Console.WriteLine("游戏结束！{0}击败光了{1}！{0}胜利！", RoleTeamB, RoleTeamA);
            }
        }
        static void Main(string[] args)
        {
            Battle(5, 179, "奥特曼", "怪兽");
        }
    }
}
