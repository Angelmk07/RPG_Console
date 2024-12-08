using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Enemy
    {
        private int damage;
        private int hp;
        private bool isdead = false;
        public int Damage => damage;
        public int Hp => hp;
        public bool Isdead => isdead;
        private Random _random = new Random();
        public Enemy ()
        {
            this.hp = _random.Next(60,151);
            this.damage = _random.Next(15,30);
        }
        public void Gethit(int hit)
        {
            hp -= hit;
            if (hp < 0)
            {
                isdead = true;
            }
        }
        public Direction ChooseAttackDirection()
        {
            return (Direction)_random.Next(1, 4); 
        }
    }
}
