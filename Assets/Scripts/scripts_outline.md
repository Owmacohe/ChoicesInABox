# Mechanics



## Player

- Modification of population
- Free exploration
- Multiple ways to finish a discussion
- Multiple ways to complete a task/quest
- Multiple ways to win a combat
- Unbounded creation / art-making
- Story choices matter
- Skill trees / character upgrading
- Purchasing/upgrading items
- Item/pickup use
- Can win
- Can lose
- Inventory
- Localized exploration



## System
- Advanced NPC logic
- Game physics
- Random terrain/layout generation
- Generative simulation/evolution
- Runtime terrain/layout modification
- Random events
- Random item/reward generation
- Enemy combat/movement AI
- Effect/weapon damage variance



# Full list

- `MechanicPattern` (interface that implements `FixedUpdate`, `Click`, `Move`, and `Confirm` methods, and access to the `MechanicManager`, `PlayerInputController`, `EntityManager`)
  - `PlayerMechanic`
    - `PopulationModification` (ability to create and destroy entities)
    - `Exploration` (ability to navigate the environment)
      - **Free** (can explore anywhere)
      - **Localized** (can explore within bounds)
      - **None** (can’t move at all)
    - `MultipleDiscussionSolutions` (dialogue trees with multiple end points)
    - `MultipleTaskSolutions` (tasks to complete with multiple solutions)
    - `MultipleCombatSolutions` (combats with multiple win conditions)
    - `UnboundedCreation` (completely modular game components)
    - `MeaningfulStoryChoices` (dialogue decisions are saved and acted upon later)
    - `PlayerUpgrading` (knowledge unlocks or upgrades to player stats)
    - `ItemUse` (consuming or actualizing items)
      - Item upgrading (ability to combine or upgrade existing items)
    - `CanWin` (some possible game win state)
    - `CanLose` (some possible game loss state)
    - `Inventory` (a persistent list of items or knowledge)
  - `SystemMechanic`
    - `Entities` (some number of NPCs)
    - `AdvancedNPCReactions` (NPCs with changing or non-headlong motives)
      - **Dialogue** (own motives and reactive thought to the player)
      - **Movement** (non-direct or even adaptive pathing)
      - **Combat** (unique strategies and defences)
    - `Physics` (a random, realistic, and unchangeable world physics system)
    - `RandomEnvironmentGeneration` (an environment which is pre-generated randomly)
    - `EnvironmentModification` (modifications to the environment at runtime)
    - `GenerativeEvolution` (modifications/iterations to the entities at runtime)
    - `RandomEvents` (random entity/item spawns, conversations, population modifications, environment modifications, etc.)
    - `RandomItemGeneration` (randomized item generation, rather than at planned times or types)
    - `DamageVariance` (some deviation regarding damage received or dealt)
- `MechanicManager` (initializes the `MechanicPattern`s, and calls their other methods as needed)
- `PlayerInputController` (gets player input, reacts, and passes some on to the `MechanicManager`)
- `SceneSwitcher` (static scene changing and quitting methods)
- `Entity` (with **Health**, **Speed**, **Armour**, **Damage**, etc.)