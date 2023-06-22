namespace BossFight;

public class Fight
{
    public GameCharacter hero;
    public GameCharacter boss;
    private List<GameCharacter> ListOfFighters { get; set; }
    public Fight()
    {
        hero = new GameCharacter("Hero", 100, 20, 40, 0, 0);
        boss = new GameCharacter("Boss", 400, 30, 10, 1);
        var items = new Item[]
        {
            new Item("StaminaPotion", "Beer", 1, 1),
            new Item("HealthPotion", "Bandage", 2, 1),
            new Item("StrengthPotion", "Hammer", 3, 1),        
            new Item("StaminaPotion", "Power Bar", 4, 1),
            new Item("HealthPotion", "Blood", 5, 1),
            new Item("StrengthPotion", "Axe", 6, 1),        
            new Item("StaminaPotion", "Hoka sneakers", 7, 1),
            new Item("HealthPotion", "Antibiotics", 8, 1),
            new Item("StrengthPotion", "Thors hammer", 9, 1),
            new Item("HealthPotion", "Paracetamol", 10, 1)
        };
        Console.WriteLine("Give a name to your hero: ");
        hero.CharacterName = Console.ReadLine();
        Console.WriteLine($"Game is beginning between {hero.CharacterName} and {boss.CharacterName}");
        ListOfFighters = new List<GameCharacter>();
        ListOfFighters.Add(hero);
        ListOfFighters.Add(boss);
        int roundCount = 1;
        while (hero.Health > 0 && boss.Health > 0)
        {
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine($"Round: {roundCount}");
            Console.WriteLine();
            Console.WriteLine($"{hero.CharacterName}'s current health: {hero.Health}");
            Console.WriteLine($"{hero.CharacterName}'s current stamina: {hero.Stamina}");
            Console.WriteLine();
            Console.WriteLine($"Boss's current health: {boss.Health}");
            Console.WriteLine($"Boss's current stamina: {boss.Stamina}");
            Console.WriteLine();
            Random rnd = new Random();
            int id = rnd.Next(0, 2);
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            Console.WriteLine(" ");
            items = CommenceFight(id, hero, boss, roundCount, items);
            roundCount++;
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
    public Item[] CommenceFight(int attckersId, GameCharacter hero, GameCharacter boss, int roundCount, Item[] items)
    {
        Random rnd = new Random();
        int bossStrength = rnd.Next(0, 31);
        boss.Strength = bossStrength;
        if (attckersId == 0)
        {
            Console.WriteLine($"This is {hero.CharacterName}'s turn.");
            
            bool isHerosRoundFinished = false;
            bool isItemChosen = false;

            while (!isHerosRoundFinished)
            {

                PrintMenu(roundCount, isItemChosen);

                var usersChoice = Console.ReadLine();

                if (usersChoice == "1" && hero.Stamina > 0)
                {
                    if (hero.TemporaryStrength != 0)
                    {
                        boss.Health = Int32.Max(0, boss.Health - hero.TemporaryStrength);
                    }
                    else
                    {
                        boss.Health = Int32.Max(0, boss.Health - hero.Strength);
                    }

                    hero.Stamina = Int32.Max(0, hero.Stamina - 10);
                    Console.WriteLine(
                        $"{hero.CharacterName} has attacked {boss.CharacterName} with {(hero.TemporaryStrength == 0 ? hero.Strength : hero.TemporaryStrength)} damage, enemy has {boss.Health} health left.");
                    hero.TemporaryStrength = 0;
                    isHerosRoundFinished = true;
                    break;
                }
            
                else if (usersChoice == "3")
                {
                    if (roundCount % 3 != 0)
                    {
                        Console.WriteLine("Choose an item: ");
                        foreach (var item in items)
                        {
                            Console.WriteLine($"Press {item.Id} to use {item.ItemName}");
                        }

                        var userItemIdchoice = Convert.ToInt32(Console.ReadLine());
                        items = ConsumeItem(hero, items, userItemIdchoice);
                        isItemChosen = true;
                    }
                    else
                    {
                        var myRandom = new Random();
                        var dropChance = myRandom.Next(0, 2);
                        if (dropChance == 0)
                        {
                            Console.WriteLine("No dropped item in this round.");
                        }
                        else
                        {
                            if (hero.Health <= 30)
                            {
                                var healthItems = items.Where(i => i.ItemType == "HealthPotion").ToArray();
                                var healtItemindex = myRandom.Next(0, healthItems.Length);
                                Console.WriteLine(
                                    $"{healthItems[healtItemindex].ItemName} has been dropped. Press enter to consume it.");
                                Console.ReadLine();
                                hero.Health = 100;
                                items = items.Where(i => i.ItemName != healthItems[healtItemindex].ItemName).ToArray();

                            }
                            else
                            {
                                var droppedItemIndex = myRandom.Next(0, items.Length);
                                Console.WriteLine(
                                    $"{items[droppedItemIndex].ItemName} has been dropped. Press enter to consume it.");
                                Console.ReadLine();
                                items = ConsumeItem(hero, items, items[droppedItemIndex].Id);

                            }

                        }

                        isItemChosen = true;
                    }

                    isHerosRoundFinished = false;
                }
                else if (usersChoice == "2" || hero.Stamina <= 0)
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
                    isHerosRoundFinished = true;
                    break;
                }
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

        return items;
    }

    private static Item[] ConsumeItem(GameCharacter hero, Item[] items, int userItemIdchoice)
    {
        Item? userItemPicked = items.FirstOrDefault(i => i.Id == userItemIdchoice);
        if (userItemPicked?.ItemType == "StaminaPotion")
        {
            if (hero.Stamina <= 0)
            {
                hero.Stamina = hero.Stamina + 10;
                Console.WriteLine($"{hero.CharacterName} gained 10 stamina.");
            }
            else
            {
                hero.Stamina = hero.Stamina + 5;
                Console.WriteLine($"{hero.CharacterName} gained 5 stamina.");
            }
        }
        else if (userItemPicked.ItemType == "HealthPotion")
        {
            hero.Health = 100;
            Console.WriteLine($"{hero.CharacterName} gained full health.");
        }
        else if (userItemPicked.ItemType == "StrengthPotion")
        {
            hero.TemporaryStrength = 30;
            Console.WriteLine($"{hero.CharacterName} gained 30 temporary strength.");
        }

        items = items.Where(i => i.ItemName != userItemPicked.ItemName).ToArray();

        return items;
    }

    public void PrintMenu(int roundCount, bool isItemChosen)
    {
        Console.WriteLine("Press 1 to fight");
        Console.WriteLine("Press 2 to recharge");
        if (!isItemChosen)
        {
            if (roundCount % 3 != 0)
            {
                Console.WriteLine("Press 3 to choose an item");
            }
            else if (roundCount % 3 == 0)
            {
                Console.WriteLine("Press 3 to see the dropped item");
            }
        }
    }   
    
    
    public void Recharge(GameCharacter fighter)
    {
        Console.WriteLine($"{fighter.CharacterName} is now recharging.");
    }
    
}