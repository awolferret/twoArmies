
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Warrior warrior = new Warrior("Default", 1, 1, 1);
            warrior.Fight();
        }
    }

    class Warrior
    {
        protected string Name;
        protected int Health;
        protected int Damage;
        protected int Armor;

        public Warrior(string name, int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public void Fight()
        {
            List<Warrior> firstWarriors = new List<Warrior>();
            AddToList(firstWarriors);
            List<Warrior> secondWarriors = new List<Warrior>();
            AddToList(secondWarriors);
            Random random = new Random();

            for (int i = 0; i < firstWarriors.Count; i++)
            {
                Console.Write(i + 1 + " ");
                firstWarriors[i].ShowInfo();
            }

            while (firstWarriors.Count > 0 && secondWarriors.Count > 0)
            {
                int firstIndex = random.Next(1, firstWarriors.Count);
                Warrior firstFighter = firstWarriors[firstIndex-1];
                int secondIndex = random.Next(1, secondWarriors.Count);
                Warrior secondFighter = secondWarriors[secondIndex-1];

                while (firstFighter.Health > 0 && secondFighter.Health > 0)
                {
                    firstFighter.GetRandom();
                    firstFighter.UseAbility();
                    firstFighter.TakeDamage(secondFighter.Damage, ref secondFighter.Health);
                    secondFighter.GetRandom();
                    secondFighter.UseAbility();
                    secondFighter.TakeDamage(firstFighter.Damage, ref firstFighter.Health);
                    firstFighter.ShowInfo();
                    secondFighter.ShowInfo();
                    CheckDeath(firstWarriors, firstIndex, firstFighter.Health);
                    CheckDeath(secondWarriors, secondIndex, secondFighter.Health);
                }
            }
            CheckWin(firstWarriors, secondWarriors);
        }

        private void AddToList(List<Warrior> list)
        {
            list.Add(new Rogue("Rogue", 100, 50, 10));
            list.Add(new Cliric("Cliric", 200, 40, 25));
            list.Add(new Paladin("Paladin", 150, 45, 25));
            list.Add(new Ninja("Ninja", 80, 55, 5));
            list.Add(new Huntsman("Huntsman", 120, 50, 10));
        }

        protected virtual void TakeDamage(int damage,ref int health)
        {
            health -= damage - Armor;
        }

        public int GetRandom()
        {
            Random random = new Random();
            int minRandomChance = 1;
            int maxRandomChance = 11;
            int randomChance = random.Next(minRandomChance, maxRandomChance);
            return randomChance;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}, {Health} Здоровья, {Damage} Урона, {Armor} Брони");
        }

        public void CheckDeath(List<Warrior> list, int index,int health)
        {
            if (health <= 0)
            {
                Console.WriteLine($"{list[index - 1].Name} погиб");
                list.RemoveAt(index-1);
            }
        }

        public void CheckWin(List<Warrior> FirstWarriors, List<Warrior> SecondWarriors)
        {
            if (FirstWarriors.Count <= 0)
            {
                Console.WriteLine("Победа второй команды");
            }
            else if (SecondWarriors.Count <= 0)
            {
                Console.WriteLine("Победа первой команды");
            }
            else
            {
                Console.WriteLine("Ничья");
            }
        }

        public virtual void UseAbility()
        {

        }
    }

    class Rogue : Warrior
    {
        public Rogue(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {

        }

        public void DealDoubleDamage()
        {
            int count = 0;
            int strike = 3;
            int multiplayer = 2;
            count++;

            if (count == strike)
            {
                Console.WriteLine("Разбойник нанес двойной урон");
                Damage *= multiplayer;
                count = 0;
            }
        }
    }

    class Cliric : Warrior
    {
        public Cliric(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {

        }

        public override void UseAbility()
        {
            int maxHealth = 200;
            int firstThird = 3;
            int healthAmplifier = 50;

            if (Health < maxHealth)
            {
                int randomChance = GetRandom();

                if (randomChance < firstThird)
                {
                    Console.WriteLine("Клирик исцелил себя");
                    Health += healthAmplifier;
                }
            }
        }
    }

    class Paladin : Warrior
    {
        public Paladin(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {

        }

        public override void UseAbility()
        {
            int randomChance = GetRandom();
            int half = 5;
            int amplifier = 50;

            if (randomChance < half)
            {
                Console.WriteLine("Паладин получил баф ХП");
                Health += amplifier;
            }
            else if (randomChance == half)
            {
                Console.WriteLine("Паладин получил баф урона и ХП");
                Health += amplifier;
                Damage += amplifier;
            }
            else
            {
                Console.WriteLine("Паладин получил баф урона");
                Damage += amplifier;
            }
        }
    }

    class Ninja : Warrior
    {
        public Ninja(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {

        }

        protected override void TakeDamage(int damage,ref int health)
        {
            int randomChance = GetRandom();
            int firstThird = 3;

            if (randomChance < firstThird)
            {
                Console.WriteLine("Ниндзя увернулся от урона");
            }
            else
            {
                base.TakeDamage(damage,ref health);
            }
        }
    }

    class Huntsman : Warrior
    {
        public Huntsman(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {

        }

        public override void UseAbility()
        {
            int randomChance = GetRandom();
            int firstThird = 3;
            int dogDamage = 10;

            if (randomChance < firstThird)
            {
                Console.WriteLine("Охотник призвал на помощь собаку");
                Damage += dogDamage;
            }
        }
    }
}