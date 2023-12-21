using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Project___Dungons_of_Equavar
{
    public class Enemy
    {
        float maxHealth, health, attack, MagicAtk, defense, magicDef, speed;
        Stats stats;
        Texture2D enemyTexture;
        Rectangle enemyLocation;






        public float PhysicalDef { get { return stats.Defense; } }
        public float MagicDef { get { return stats.MagicDefense; } }
        public int Weakness { get; private set; }
        public int Resist { get; private set; }
        public int Immune { get; private set; }

        public void TakeDmg(float dmg)
        {
            health -= dmg;
        }



    }
}
