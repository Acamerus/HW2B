using System;

string[] pettingZoo = 
{
    "alpacas", "capybaras", "chickens", "ducks", "emus", "geese", 
    "goats", "iguanas", "kangaroos", "lemurs", "llamas", "macaws", 
    "ostriches", "pigs", "ponies", "rabbits", "sheep", "tortoises",
};

// RandomizeAnimals();
RandomizeAnimals();

// string[,] group = AssignGroup(); 

Console.WriteLine("School A");
Console.WriteLine("School B");
Console.WriteLine("School C");

// PrintGroup(group);

void RandomizeAnimals() 
{
    Random random = new Random();

    for (int i = 0; i < pettingZoo.Length; i++) 
    {
        int r = random.Next(i, pettingZoo.Length);

        string temp = pettingZoo[i];
        pettingZoo[i] = pettingZoo[r];
        pettingZoo[r] = temp;
    }
}

// foreach(string animal in pettingZoo) Test***
// {
//     Console.WriteLine(animal);
// }
