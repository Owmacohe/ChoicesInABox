public class Entity
{
    public string Name { get; }
    public float Health { get; set; }
    public float Speed { get; set; }
    public float Armour { get; set; }
    public float Damage { get; set; }

    public Entity(string name, float health, float speed, float armour, float damage)
    {
        Name = name;
        
        Health = health;
        Speed = speed;
        Armour = armour;
        Damage = damage;
    }
}