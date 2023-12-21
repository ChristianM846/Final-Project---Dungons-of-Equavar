using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project___Dungons_of_Equavar
{
    public class Stats
    {
        private float maxHealth, health, maxMana, mana, attack, magicAttack, defense, magicDefense, speed;
        public float Health { get { return health; } set { health = value; } }
        public float MaxHealth { get { return maxHealth ; } set { maxHealth = value; } }
        public float Mana { get { return mana; } set { mana = value; } }
        public float MaxMana { get { return maxMana; } set { maxMana = value; } }
        public float Attack { get { return attack * StatMultiplyer; } set { attack = value; } }
        public float Defense { get { return defense * StatMultiplyer; } set { defense = value; } }
        public float MagicAttack { get { return magicAttack * StatMultiplyer; } set { magicAttack = value; } }
        public float MagicDefense { get { return magicDefense * StatMultiplyer; } set { magicDefense = value; } }
        public float Speed { get { return speed * StatMultiplyer; } set { speed = value; } }
        public float StatMultiplyer { get; set; }
        public Stats(float health, float maxHealth, float mana, float maxMana, float attack, float defense, float magicAttack, float magicDefense, float speed)
        {
            StatMultiplyer = 1;
        }

    }
}
