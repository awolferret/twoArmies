
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            BattleField battleField = new BattleField();
            battleField.Fight();
        }
    }

    class BattleField 
    {   
        public void Fight()
        {
            Squad firstSquad = new Squad();
            List <Warrior> firstWarriors = firstSquad.CreateSquad();
            Squad secondSquad = new Squad();
            List<Warrior> secondWarriors = secondSquad.CreateSquad();
            ShowFightersList(firstWarriors);

            while (firstWarriors.Count > 0 && secondWarriors.Count > 0)
            {
                Random random = new Random();
                int firstIndex = random.Next(1, firstWarriors.Count);
                Warrior firstFighter = firstWarriors[firstIndex - 1];
                int secondIndex = random.Next(1, secondWarriors.Count);
                Warrior secondFighter = secondWarriors[secondIndex - 1];

                while (firstFighter.Health > 0 && secondFighter.Health > 0)
                {
                    firstFighter.GetRandom();
                    firstFighter.UseAbility();
                    firstFighter.TakeDamage(secondFighter.Damage);
                    secondFighter.GetRandom();
                    secondFighter.UseAbility();
                    secondFighter.TakeDamage(firstFighter.Damage);
                    firstFighter.ShowInfo();
                    secondFighter.ShowInfo();
                    DeleteDeadFighter(firstWarriors, firstIndex);
                    DeleteDeadFighter(secondWarriors, secondIndex);
                }
            }

            CheckWin(firstWarriors, secondWarriors);
        }

        private void ShowFightersList(List<Warrior> warriors)
        { 
            for (int i = 0; i < warriors.Count; i++)
            {
                Console.Write(i + 1 + " ");
                warriors[i].ShowInfo();
            }
        }

        private void CheckWin(List<Warrior> firstWarriors, List<Warrior> secondWarriors)
        {
            if (firstWarriors.Count <= 0)
            {
                Console.WriteLine("Победа второй команды");
            }
            else if (secondWarriors.Count <= 0)
            {
                Console.WriteLine("Победа первой команды");
            }
            else
            {
                Console.WriteLine("Ничья");
            }
        }
        private void DeleteDeadFighter(List<Warrior> list, int index)
        {
            if (list[index - 1].Health <= 0)
            {
                Console.WriteLine($"{list[index - 1].Name} погиб");
                list.RemoveAt(index - 1);
            }
        }
    }

    class Squad
    {
        public Squad()
        {
            List<Warrior> firstWarriors = CreateSquad();
            List<Warrior> secondWarriors = CreateSquad();
        }

        public List<Warrior> CreateSquad()
        {
            List<Warrior> list = new List<Warrior>();
            AddToList(list);
            return list;
        }

        private void AddToList(List<Warrior> list)
        {
            list.Add(new Rogue("Rogue", 100, 50, 10));
            list.Add(new Cliric("Cliric", 200, 40, 25));
            list.Add(new Paladin("Paladin", 150, 45, 25));
            list.Add(new Ninja("Ninja", 80, 55, 5));
            list.Add(new Huntsman("Huntsman", 120, 50, 10));
        }
    }

    class Warrior
    {
        public string Name { get; private set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        private int _armor;

        public Warrior(string name, int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            _armor = armor;
        }

        public virtual void UseAbility()
        {

        }

        public int GetRandom()
        {
            Random random = new Random();
            int minRandomChance = 1;
            int maxRandomChance = 11;
            int randomChance = random.Next(minRandomChance, maxRandomChance);
            return randomChance;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage - _armor;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}, {Health} Здоровья, {Damage} Урона, {_armor} Брони");
        }
    }

    class Rogue : Warrior
    {
        public Rogue(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {

        }

        public override void UseAbility()
        {
            int count = 0;
            int strike = 3;
            int multiplayer = 2;
            count++;

            if (count == strike)
            {
                Console.WriteLine("Разбойник нанес двойной урон");
                Damage *= multiplayer;
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

        public override void TakeDamage(int damage)
        {
            int randomChance = GetRandom();
            int firstThird = 3;

            if (randomChance < firstThird)
            {
                Console.WriteLine("Ниндзя увернулся от урона");
            }
            else
            {
                base.TakeDamage(damage);
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