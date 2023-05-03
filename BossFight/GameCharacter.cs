namespace BossFight;

public class GameCharacter
{
    public string CharacterName { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Stamina { get; set; }
    public int Id { get; set; }
    public List<GameCharacter> gameCharacters { get; set; }

    public GameCharacter(string characterName, int health, int strength, int stamina, int id)
    {
        CharacterName = characterName;
        Health = health;
        Strength = strength;
        Stamina = stamina;
        Id = id;
    }
    
    public GameCharacter(int health, int stamina)
    {
        Health = health;
        Stamina = stamina;
    }
    
    public GameCharacter(int strength)
    {
        Strength = strength;
    }
    

}