namespace BossFight;

public class Fight
{
    public GameCharacter hero;
    public GameCharacter boss;
    private List<GameCharacter> ListOfFighters { get; set; }
    public Fight()
    {
        hero = new GameCharacter("Hero", 100, 20, 40, 0);
        boss = new GameCharacter("Boss", 400, 30, 10, 1);
        Console.WriteLine("Give a name to your hero: ");
        hero.CharacterName = Console.ReadLine();
        Console.WriteLine($"Game is beginning between {hero.CharacterName} and {boss.CharacterName}");
        ListOfFighters = new List<GameCharacter>();
        ListOfFighters.Add(hero);
        ListOfFighters.Add(boss);
        while (hero.Health > 0 && boss.Health > 0)
        { 
            Random rnd = new Random();
            int id = rnd.Next(0, 2);
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            Console.WriteLine(" ");
            CommenceFight(id, hero, boss);
        }
        CheckWinner(hero.Health, boss.Health);
    }
    static void CheckWinner(int herosHealth, int bosssHealth)
    {
        if (herosHealth <= 0)
        {
            Console.WriteLine("Boss is the winner.");
        }
        if (bosssHealth <= 0)
        {
            Console.WriteLine("Hero is the winner.");
        }
    }
    public void CommenceFight(int attckersId, GameCharacter hero, GameCharacter boss)
    {
        Random rnd = new Random();
        int bossStrength = rnd.Next(0, 31);
        boss.Strength = bossStrength;
        if (attckersId == 0)
        {
            Console.WriteLine($"This is {hero.CharacterName}'s turn.");
            PrintMenu();
            var usersChoice = Console.ReadLine();
            if (usersChoice == "1" && hero.Stamina > 0)
            {
                boss.Health = Int32.Max(0, boss.Health - hero.Strength);
                hero.Stamina = Int32.Max(0, hero.Stamina - 10);
                Console.WriteLine($"{hero.CharacterName} has attacked {boss.CharacterName} with {hero.Strength} damage, enemy has {boss.Health} health left.");
            }
            else if (usersChoice == "0" || hero.Stamina <= 0)
            {
                Console.WriteLine($"{hero.CharacterName} has {hero.Stamina} stamina and will now recharge.");
                if (hero.Stamina <= 0)
                {
                    hero.Stamina = hero.Stamina + 10;
                }
                else
                {
                    hero.Stamina = hero.Stamina + 5;
                }
                Recharge(hero);
            }
        }
        else if(attckersId == 1)
        {
            if (boss.Stamina > 0)
            {
                hero.Health = Int32.Max(0,hero.Health - boss.Strength);
                boss.Stamina = Int32.Max(0, boss.Stamina - 10);
                Console.WriteLine($"{boss.CharacterName} has attacked {hero.CharacterName} with {boss.Strength} damage, hero has {hero.Health} health left.");
            }
            else
            {
                Console.WriteLine($"{boss.CharacterName} tried to attack but must recharge now to fight.");
                boss.Stamina = boss.Stamina + 10;
                Recharge(boss);
            }
        }
    }

    public void PrintMenu()
    {
        Console.WriteLine("Press 1 to fight");
        Console.WriteLine("Press 2 to recharge");
    }
    
    public void Recharge(GameCharacter fighter)
    {
        Console.WriteLine($"{fighter.CharacterName} is now recharging.");
    }
    
}