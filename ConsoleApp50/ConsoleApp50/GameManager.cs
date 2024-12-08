using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class GameManager
    {
        private Player _player;
        private Enemy? enemy;
        private int ActionsPerRound = 2;
        private int counter = 0;
        private bool game = true;
        public GameManager(Player player)
        {
            _player = player;
        }
        public void StartGame()
        {
            enemy = new Enemy();
            while (_player.IsAlive&& game)
            {
                Console.WriteLine("\nВаши действия:");
                Console.WriteLine("1. Атаковать");
                if (_player is Mag)
                    Console.WriteLine("2. Использовать магическую атаку");
                Console.WriteLine("3. Заблокировать");
                Console.WriteLine("4. Использовать зелье");
                if (_player is Mag)
                    Console.WriteLine("5. Востановление");
                if (_player.Rage >= 100)
                {
                    Console.WriteLine("6. Ярость");
                }
                if(_player is Ranger)
                {
                    Console.WriteLine("7. Уворот");
                }
                
                Console.WriteLine("8. Буйство");
                Console.WriteLine("0. Выйти из игры");
                Console.Write("\nВыберите действие: ");
                _player.Info();
                Console.WriteLine($"Hp врага {enemy.Hp}");
                
                Round();
   
                
                _player.reset();
                ActionsPerRound--;
                if (enemy.Isdead)
                {
                    ActionsPerRound = 2;
                    counter++;
                    if (counter == 15)
                    {
                        Console.WriteLine("Вы дошли до конца");
                        Console.WriteLine("Победа");
                        break;
                    }
                    Console.WriteLine("враг убит");
                    Console.WriteLine("\nВаши действия:");
                    Console.WriteLine("1. Отдохнуть и прокочать характеристики");
                    Console.WriteLine("2. Использовать зелье");
                    AfterRound();

                    enemy = new Enemy();
                }
                else
                {
                    if (ActionsPerRound == 0)
                    {

                        ActionsPerRound = 2;
                    }
                }

                
            }
        }
        
        void AfterRound()
        {
            bool isPass = false;
            while (!isPass)
            {
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _player.UpgradeStats();
                        isPass = true;
                        break;

                    case "2":
                        _player.Heal();
                        isPass = false;
                        break;

                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        continue;
                }
            }
               
        }
        void Round()
        {
            bool isPass = false;
            while (!isPass)
            {
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        game = false;
                        isPass = true;
                        break;
                    case "1":
                        enemy?.Gethit(_player.Attack());
                        Console.WriteLine($"Вы нанесли {_player.Attack()} урона.");
                        isPass = true;
                        break;

                    case "2":
                        if (_player is Mag mag)
                        {
                            enemy?.Gethit(mag.AttackMagic());
                            Console.WriteLine($"Вы нанесли {mag.AttackMagic()} урона магией.");
                            isPass = true;
                        }
                            
                        else
                            Console.WriteLine("Этот персонаж не может использовать магическую атаку.");
                        
                        break;

                    case "3":
                        _player.Block();
                        isPass = true;
                        break;

                    case "4":
                        _player.Heal();
                        isPass = true;
                        break;

                    case "5":
                        if (_player is Mag buffableMag)
                        {
                            buffableMag.Buff();
                            Console.WriteLine("Вы использовали баф, восстановив ману.");
                            isPass = true;
                        }
                        else
                            Console.WriteLine("Этот персонаж не может использовать востановление.");
                        break;

                    case "6":
                        if (_player.Rage >= 100)
                        {
                            _player.riot();
                            isPass = true;
                        }
                        else
                        {
                            Console.WriteLine($"Ярость не накопленна{_player.Rage}");
                        }
                        break;

                    case "7":
                        if (_player is Ranger ranger)
                        {
                            Console.WriteLine($"Делавет уворот.");
                            ranger.dodge();
                        
                            isPass=true;
                        }
                        else
                            Console.WriteLine("Этот персонаж не может уворачиватся.");
                        break;

                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        break;
                }
            }
           
        }
    }
}
