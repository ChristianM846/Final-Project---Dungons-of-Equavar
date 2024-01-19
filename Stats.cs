using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___Dungons_of_Equavar
{
    //Holds all stats in a single class so it is more readable and organized
    //All stats can be accessed via Stats.<insert stat name here>
    public class Stats
    {
        private float maxHealth, health, maxMana, mana, attack, magicAttack, defense, magicDefense, speed;
        public float Health { get { return health; } set { health = value; } }
        public float MaxHealth { get { return maxHealth ; } set { maxHealth = value; } }
        public float Mana { get { return mana; } set { mana = value; } }
        public float MaxMana { get { return maxMana; } set { maxMana = value; } }
        public float Attack { get { return attack * AttackMultiplyer; } set { attack = value; } }
        public float Defense { get { return defense * DefenseMultiplyer; } set { defense = value; } }
        public float MagicAttack { get { return magicAttack * MagicAttackMultiplyer; } set { magicAttack = value; } }
        public float MagicDefense { get { return magicDefense * MagicDefenseMultiplyer; } set { magicDefense = value; } }
        public float Speed { get { return speed * SpeedMultiplyer; } set { speed = value; } }
        public float AttackMultiplyer { get; set; }
        public float DefenseMultiplyer { get; set; }
        public float MagicAttackMultiplyer { get; set; }
        public float MagicDefenseMultiplyer { get; set; }
        public float SpeedMultiplyer { get; set; }
        public Stats(float maxHealth, float maxMana, float attack,  float magicAttack,float defense, float magicDefense, float speed)
        {
            AttackMultiplyer = 1;
            DefenseMultiplyer = 1;
            MagicAttackMultiplyer = 1;
            MagicDefenseMultiplyer = 1;
            SpeedMultiplyer = 1;
            this.health = maxHealth;
            this.maxHealth = maxHealth;
            this.mana = maxMana;
            this.maxMana = maxMana;
            this.attack = attack;
            this.defense = defense;
            this.magicAttack = magicAttack;
            this.magicDefense = magicDefense;
            this.speed = speed;
        }

    }
}
