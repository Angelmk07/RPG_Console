using System;
using System.Xml.Linq;

namespace RPG
{
    public class Ranger : Player
    {
        private int potions = 5;
        private Direction dodgeDirection;
        public Ranger(string name) : base(name)
        {
            dexterity = 25;
            physicalPower = 12;
            maxHp = 85;
        currentHp = maxHp;
        }

        public override int Attack()
        {
            Console.WriteLine($"{_name} делает выпад копьем, нанося урон {dexterity + physicalPower / 2}.");
            return physicalPower + (int)(dexterity / 3);
        }

        public void RangeAttack()
        {
            Console.WriteLine($"{_name} делает выстрел, нанося урон {dexterity + physicalPower/2}.");
        }

        public void dodge()
        {
            Console.WriteLine("Выберите уторону отшага (1 - Вперед, 2 - Влево, 3 - Вправо):");
            if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(Direction), choice) && choice != 0)
            {
                dodgeDirection = (Direction)choice;
                Console.WriteLine($"{_name} делает рывок в сторону: {dodgeDirection}");
            }
            else
            {
                Console.WriteLine("Некорректный выбор направления.");
                dodge();
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
        public override void GetHit(int val)
        {
            currentHp -= val;
            if (currentHp < 0)
            {
                Console.WriteLine("Вы мертвы");
                isAlive = false;
            }
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

        public override void reset()
        {
            blockDirection = 0;
            dodgeDirection = 0;
        }
        public override void HandleAttack(Direction attackDirection, int damage)
        {
            if (blockDirection == attackDirection)
            {
                AddRage();
                Console.WriteLine($"{_name} мастерски отражает атаку");
                Console.WriteLine($"шкала ярости {rage}/100");
            }
            else if (dodgeDirection!=0&&dodgeDirection!= attackDirection)
            {
                Console.WriteLine($"{_name} мастерски уклонился");
            }
            else
            {
                GetHit(damage);
                Console.WriteLine($"{_name} получил урон {damage}");
            }
        }

    }
}
