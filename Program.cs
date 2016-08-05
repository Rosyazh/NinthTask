using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Practice: 
Take List of Animals from task 3.2 as data source and perform queries of different types on that data source.
*/

namespace NinthTask
{
    interface IWalk
    {
        void FoodSearch(bool _search);
    }

    interface IFly
    {
        void FoodSearch(bool _search);
    }

    interface ISwim
    {
        void FoodSearch(bool _search);
    }

    class Animal
    {
        public string Species { get; set; }
        public int Energy { get; set; }

        public void Eat()
        {
            Random rand = new Random();
            int upEnergy = rand.Next(1, 51);
            Energy += upEnergy;
            Console.WriteLine("Received energy: " + upEnergy);
        }

        public void FoundFood()
        {
            bool[] findFood = new bool[2];
            findFood[0] = true;
            findFood[1] = false;
            Random rand = new Random();
            bool foodFounded = findFood[rand.Next(0, 2)];
            if (foodFounded)
            {
                Eat();
                Console.WriteLine("Current energy: " + Energy);
            }
            else
                Console.WriteLine("Food not found.");
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Species, Energy);
        }
    }

    class Dog : Animal, IWalk
    {
        public void FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search on the land.");
                FoundFood();
            }
        }
    }

    class Crocodile : Animal, IWalk, ISwim
    {
        public void FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search on the land.");
                FoundFood();
            }
        }

        void ISwim.FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search in the water.");
                FoundFood();
            }
        }
    }

    class Bat : Animal, IWalk, IFly
    {
        void IWalk.FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search on the land.");
                FoundFood();
            }
        }

        void IFly.FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search on the air.");
                FoundFood();
            }
        }
    }

    class Crucian : Animal, ISwim
    {
        public void FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search in the water.");
                FoundFood();
            }
        }
    }

    class Goose : Animal, IWalk, ISwim, IFly
    {
        void IWalk.FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search on the land.");
                FoundFood();
            }
        }

        void IFly.FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search on the air.");
                FoundFood();
            }
        }

        void ISwim.FoodSearch(bool _search)
        {
            if (_search)
            {
                Console.WriteLine("Search in the water.");
                FoundFood();
            }
        }
    }

    class Group<TValue>
    {
        public int position;
        public TValue[] data = new TValue[10];

        public void Push(TValue obj)
        {
            data[position++] = obj;
        }

        public void Print()
        {
            Console.WriteLine("\nThe animal data:\n");
            foreach (TValue value in data)
            {
                if (value != null)
                    Console.WriteLine(value.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dog d1 = new Dog { Species = "Labrador Retriever" };
            d1.FoodSearch(true);

            Crocodile c1 = new Crocodile { Species = "Nile crocodile" };
            c1.FoodSearch(true);
            ((ISwim)c1).FoodSearch(true);

            Bat b1 = new Bat { Species = "Desert long-eared bat" };
            ((IWalk)b1).FoodSearch(true);
            ((IFly)b1).FoodSearch(true);

            Crucian k1 = new Crucian { Species = "Gold crucian" };
            k1.FoodSearch(true);

            Goose g1 = new Goose { Species = "Grey goose" };
            ((ISwim)g1).FoodSearch(true);
            ((IFly)g1).FoodSearch(true);
            ((IWalk)g1).FoodSearch(true);

            Group<Animal> group = new Group<Animal>();

            group.Push(d1);
            group.Push(c1);
            group.Push(b1);
            group.Push(k1);
            group.Push(g1);
            //group.Print();

            Console.WriteLine("\nLINQ queries №1\n");

            var gr1 = group.data.Where(n => n != null && n.Species.Length >= 15).OrderBy(n=>n.Species.Length);
            foreach (Animal animal in gr1)
                Console.WriteLine(animal.Species);

            Console.WriteLine("\nLINQ queries №2\n");

            var gr2 = from animal in @group.data
                      where animal != null && animal.Species.StartsWith("G")
                      orderby animal.Species descending
                      select animal;
            foreach (Animal animal in gr2)
                Console.WriteLine(animal.Species);

            Console.WriteLine("\nLINQ queries №3\n");

            var gr4 = (from animal in @group.data where animal != null && animal.Energy >= 20 select animal).Count();
            Console.WriteLine(gr4);

            Console.WriteLine("\nLINQ queries №4\n");

            var gr3 = from animal in @group.data
                      where animal != null
                      let species = "Animal: " + animal.Species
                      let energy = "Energy: " + animal.Energy
                      select new
                      {
                          Species = species,
                          Energy = energy
                      };

            foreach (var animal in gr3)
                Console.WriteLine("{0} {1}", animal.Species.PadRight(35), animal.Energy);
        }
    }
}
