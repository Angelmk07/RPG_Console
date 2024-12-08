using System;
using System.Xml.Linq;

namespace RPG
{
    public class Knight : Player
    {
        private bool upperBlock;
        private int potions = 5;

        public Knight(string name) : base(name)
        {
            dexterity = 5;
            physicalPower = 19;
            maxHp = 120;
        currentHp = maxHp;
        }

        public override int Attack()
        {
            return physicalPower + dexterity / 2;
        }

        public override void Block()
        {
            Console.WriteLine("Выберите направление для блока (1 - Вверх, 2 - Влево, 3 - Вправо):");
            if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(Direction), choice) && choice != 0)
            {
                blockDirection = (Direction)choice;
                Console.WriteLine($"{_name} ставит блок в направлении: {blockDirection}");
            }
            else
            {
                Console.WriteLine("Некорректный выбор направления.");
                Block();
            }
        }

        public void UpYourBlock()
        {
            Console.WriteLine($"{_name} шит. все удары выше головы автоматически парируются");
            upperBlock = true;
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
            upperBlock = false;
            blockDirection = 0;
        }

        public override void HandleAttack(Direction attackDirection, int damage)
        {
            if (blockDirection == attackDirection)
            {
                AddRage();
                Console.WriteLine($"{_name} мастерски отражает атаку");
                Console.WriteLine($"шкала ярости {rage}/100");
            }
            else if (upperBlock && attackDirection == Direction.Up)
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
    }
}