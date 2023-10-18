using System.Collections.Generic;

namespace Mechanics.SystemMechanicPatterns
{
    public class EntitiesPattern : SystemMechanicPattern
    {
        public Dictionary<string, Entity> Entities { get; set; }
        
        public EntitiesPattern(MechanicManager manager) : base(manager)
        {
            Entities = new Dictionary<string, Entity>();
        }

        public Entity CreateEntity(string name, float health, float speed, float armour, float damage)
        {
            Entity temp = new Entity(name, health, speed, armour, damage);
            Entities.Add(name, temp);

            return temp;
        }

        public Entity RemoveEntity(string name)
        {
            Entity temp = Entities[name];
            Entities.Remove(name);
            
            return temp;
        }
    }
}