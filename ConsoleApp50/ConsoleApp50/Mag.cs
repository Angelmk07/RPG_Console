using System;
using System.Xml.Linq;

namespace RPG
{
    public class Mag : Player
    {
        private int knowledge = 25;
        private int magicCost = 10;
        private int maxMana = 50;
        private int mana;
        private int potions = 5;
        
        public Mag(string name) : base(name)
        {
            SpecificAtributeName = "знание";  
            SpecificAtribute = knowledge;
            dexterity = 10;
            maxHp = 75;
            physicalPower = 5;
            currentHp = maxHp;
            mana = maxMana;
        }

        public override int Attack()
        {
            return physicalPower + knowledge / 10;
        }

        public int AttackMagic()
        {
            if (mana >= magicCost)
            {
                mana -= magicCost;
                Console.WriteLine($"{_name} запускает магический осколок, нанося урон {knowledge + magicCost * 2}.");
                return knowledge + magicCost * 2;
            }
            return 0; 
        }

        public override void Block()
        {
            Console.WriteLine("Выберите направление для блока (1 - Вверх, 2 - Влево, 3 - Вправо):");
            if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(Direction), choice) && choice != 0)
            {
                blockDirection = (Direction)choice;
                Console.WriteLine($"{_name} ставит магический блок в направлении: {blockDirection}");
            }
            else
            {
                Console.WriteLine("Некорректный выбор направления.");
                Block();
            }
        }

        public void Buff()
        {
            mana = Math.Min(maxMana, mana+knowledge);

        }

        public override void GetHit(int val)
        {
            currentHp -= val;
            if (currentHp < 0)
            {
                Console.WriteLine("Вы мертвы");
                isAlive = false;
            }
        }

        public override void Heal()
        {
            if (potions > 0)
            {
                currentHp = Math.Min(maxHp, currentHp + 20);
                Console.WriteLine($"{_name} восстановил здоровье. Текущее HP: {currentHp}");
                potions--;
            }
            else
            {
                Console.WriteLine("Зелий больше нет");
            }
        }

        public override void reset()
        {
            blockDirection = 0;
        }


        public override void HandleAttack(Direction attackDirection, int damage)
        {
            if(blockDirection == attackDirection)
            {
                AddRage();
                Console.WriteLine($"{_name} мастерски отражает атаку");
                Console.WriteLine($"шкала ярости {rage}/100");
            }
            else
            {
                GetHit(damage);
                Console.WriteLine($"{_name} получил урон {damage}");
            }
        }
        public override void Info()
        {
            base.Info();
            Console.WriteLine($"значение маны {mana}");
        }

    }
}
