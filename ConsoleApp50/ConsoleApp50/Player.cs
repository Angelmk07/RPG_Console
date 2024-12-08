using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPG
{
    public abstract class Player
    {
        public Player(string name) 
        {
            _name = name;
        }
        public bool IsAlive => isAlive;
        public int Rage => rage;
        protected string _name;
        protected bool isAlive = true;
        protected int rage;
        protected string? SpecificAtributeName;
        protected int? SpecificAtribute = null;
        protected int dexterity;
        protected int physicalPower;
        protected int maxHp;
        protected int currentHp;
        protected int ragearmor;

        protected Direction blockDirection;
        public abstract int Attack();
        public abstract void Heal();
        public abstract void Block();
        public virtual void AddRage()
        {
            rage += 25;
        }
        public abstract void GetHit(int val);
        public abstract void reset();
        public abstract void HandleAttack(Direction attackDirection, int damage);
        public virtual void Info()
        {
            Console.WriteLine($"Hp {currentHp}, защита {ragearmor}");
        }
        public virtual void riot()
        {
            Console.WriteLine("Буйство");
            ragearmor = maxHp;
            currentHp = Math.Min(maxHp, currentHp + 25);
            rage = 0;
            Console.WriteLine($"Текущее HP: {currentHp}");
            Console.WriteLine($"зашита : {ragearmor}");
        }
        public virtual void UpgradeStats()
        {
            Console.WriteLine("Доступные характеристики для прокачки:");
            Console.WriteLine("1. Ловкость (Dexterity)");
            Console.WriteLine("2. Сила (Physical Power)");
            Console.WriteLine("3. Максимальное здоровье (Max HP)");
            Console.WriteLine($"4. {SpecificAtributeName}");
            Console.WriteLine("Выберите характеристику для повышения:");
            bool isPass = false;
            while (!isPass)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        dexterity++;
                        Console.WriteLine("Ловкость увеличена.");
                        isPass = true;
                        break;
                    case "2":
                        physicalPower++;
                        Console.WriteLine("Сила увеличена.");
                        isPass = true;
                        break;
                    case "3":
                        maxHp++;
                        Console.WriteLine("Максимальное здоровье увеличено.");
                        isPass = true;
                        break;
                    case "4":
                        if (SpecificAtribute != null)
                        {
                            SpecificAtribute++;
                        }
                        Console.WriteLine($"{SpecificAtributeName} увеличено.");
                        isPass = true;
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор, попробуйте снова.");
                        continue;
                }
            }
        }
    }

    public enum Direction
    {
        None = 0,
        Up = 1,
        Left = 2,
        Right = 3
    }
}
