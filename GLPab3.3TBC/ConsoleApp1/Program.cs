using System;

namespace TurnBasedCombat
{
    public class GameManager
    {
        private static readonly Random random = new Random();

        public void RunGame()
        {
            Console.Write("Enter your name, hero: ");
            string playerName = Console.ReadLine();
            Player player = CreatePlayer(playerName);

            int monstersDefeated = 0;

            while (player.IsAlive())
            {
                Monster monster = CreateMonster();
                PrintColoredMessage($"A wild {monster.Name} appears!", ConsoleColor.Yellow);

                DoBattle(player, monster);

                if (player.IsAlive())
                {
                    monstersDefeated++;
                }
            }

            PrintColoredMessage($"\nGame Over! You defeated {monstersDefeated} monsters.", ConsoleColor.Red);
        }

        private Player CreatePlayer(string name)
        {
            return new Player(name, 100, 15); // Example values
        }

        private Monster CreateMonster()
        {
            int maxHP = random.Next(30, 60); // Random HP between 30 and 60
            int attackPower = random.Next(5, 15); // Random attack power between 5 and 15
            return new Monster("Monster", maxHP, attackPower);
        }

        private void DoBattle(Player player, Monster monster)
        {
            while (player.IsAlive() && monster.IsAlive())
            {
                PrintColoredMessage($"\n{player.Name} HP: {player.CurrentHP}/{player.MaxHP}", ConsoleColor.DarkBlue);
                PrintColoredMessage($"{monster.Name} HP: {monster.CurrentHP}/{monster.MaxHP}", ConsoleColor.DarkRed);

                PlayerSelectAndDoMove(player, monster);

                if (monster.IsAlive())
                {
                    PrintColoredMessage($"{monster.Name} attacks {player.Name}!", ConsoleColor.Red);
                    player.TakeDamage(monster);
                }
            }

            if (!monster.IsAlive())
            {
                PrintColoredMessage($"{monster.Name} has been slain!", ConsoleColor.Green);
            }
        }

        private void PlayerSelectAndDoMove(Player player, Monster monster)
        {
            Console.Write("What do you want to do? (attack/heal): ");
            string action = Console.ReadLine()?.ToLower();

            while (action != "attack" && action != "heal")
            {
                PrintColoredMessage("Invalid action. Please type 'attack' or 'heal':", ConsoleColor.Yellow);
                action = Console.ReadLine()?.ToLower();
            }

            if (action == "attack")
            {
                PrintColoredMessage($"{player.Name} attacks {monster.Name}!", ConsoleColor.Cyan);
                monster.TakeDamage(player);
            }
            else if (action == "heal")
            {
                player.Heal();
            }
        }

        private void PrintColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    // Player and Monster classes remain unchanged, except for slight additions for color
    public class Player : Entity
    {
        public Player(string name, int maxHP, int attackPower)
            : base(name, maxHP, attackPower)
        {
        }

        public void Heal()
        {
            int healAmount = 20; // Example heal value
            CurrentHP += healAmount;
            if (CurrentHP > MaxHP)
                CurrentHP = MaxHP;

            PrintColoredMessage($"{Name} heals for {healAmount} HP! Current HP: {CurrentHP}/{MaxHP}", ConsoleColor.Green);
        }

        private void PrintColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    public class Monster : Entity
    {
        private static readonly Random random = new Random();

        public Monster(string name, int maxHP, int attackPower)
            : base(name, maxHP, attackPower)
        {
        }

        public override void TakeDamage(Entity attacker)
        {
            base.TakeDamage(attacker);

            if (random.Next(100) < 30) // Counterattack chance (e.g., 30%)
            {
                PrintColoredMessage($"{Name} counterattacks!", ConsoleColor.Red);
                attacker.TakeDamage(this);
            }
        }

        private void PrintColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    public abstract class Entity
    {
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int AttackPower { get; set; }

        public Entity(string name, int maxHP, int attackPower)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            AttackPower = attackPower;
        }

        public virtual void TakeDamage(Entity attacker)
        {
            int damage = attacker.AttackPower;
            CurrentHP -= damage;
            if (CurrentHP < 0)
                CurrentHP = 0;

            PrintColoredMessage($"{Name} takes {damage} damage! Current HP: {CurrentHP}/{MaxHP}", ConsoleColor.Red);
        }

        public bool IsAlive()
        {
            return CurrentHP > 0;
        }

        private void PrintColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.RunGame();
        }
    }
}