using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Combat : MechanicPattern
{
    bool enemiesCanAttack;
    Exploration exploration;
    Entities entities;
    
    public override void Initialize(params object[] args)
    {
        enemiesCanAttack = (bool)args[0];
        
        entities = GetComponent<Entities>();
        exploration = GetComponent<Exploration>();
        
        MechanicManager.AddToControls("Space", "attack");
    }

    void FixedUpdate()
    {
        if (!Paused && enemiesCanAttack)
        {
            for (int i = 0; i < entities.EntitiesList.Count; i++)
            {
                if (entities.EntitiesList[i] != null)
                {
                    Entity temp = entities.EntitiesList[i].GetComponent<Entity>();
                
                    if (temp.Autonomous && Random.value <= 0.01f) temp.Attack();   
                }
            }
        }
    }

    public override void KeyDown(string keyName)
    {
        if (!Paused && keyName == "space")
            exploration.Player.Attack();
    }
}