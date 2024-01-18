using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Data;
using System;
using System.Reflection.PortableExecutable;

namespace Final_Project___Dungons_of_Equavar
{
    public class Enemy
    {
        static Random rngFactor = new Random();

        bool useMagic;
        Stats stats;
        Texture2D enemyTexture;
        Rectangle enemyLocation;
        SpriteFont enemyFont;

        private bool movingDown;
        private int i;

        public Enemy(Stats stats, Texture2D texture, Rectangle location, bool useMagic, SpriteFont enemyFont)
        {
            this.stats = stats;
            this.enemyTexture = texture;
            this.enemyLocation = location;
            this.useMagic = useMagic;
            Weakness = -1;
            Resist = -1;
            Immune = -1;
            this.enemyFont = enemyFont;
            movingDown = true;
        }

        public Enemy(Stats stats, Texture2D texture, Rectangle location, bool useMagic, int weakness, int resist, int immune, SpriteFont enemyFont)
        {
            this.stats = stats;
            this.enemyTexture = texture;
            this.enemyLocation = location;
            this.useMagic = useMagic;
            Weakness = weakness;
            Resist = resist;
            Immune = immune;
            this.enemyFont = enemyFont;
            movingDown = true;
        }

        public bool Turn(Player attackTarget)
        {
            bool turn = false;

            if (movingDown)
            {
                if (i < 22)
                {
                    enemyLocation.Y += 2;
                }
                else
                {
                    movingDown = false;
                    i = 0;
                }
            }
            else if (i < 22)
            {
                enemyLocation.Y -= 2;
            }
            else
            {
                movingDown = true; i = 0;
                turn = true;
                Damage(attackTarget);
            }
            i++;

            return turn;
        }
        

        private void Damage(Player attackTarget)
        {
            float atk, def, dmg;
            if (useMagic)
            {
                atk = stats.MagicAttack;
                def = attackTarget.Stats.MagicDefense;
            }
            else
            {
                atk = stats.Attack;
                def = attackTarget.Stats.Defense;
            }

            if (atk >= def)
            {
                dmg = (((atk * 2 - def) * (rngFactor.Next(90, 111) / 100f)));
            }
            else
            {
                dmg = (((atk * atk / def) * (rngFactor.Next(90, 111) / 100f)));
            }
            attackTarget.TakeDmg(dmg);

        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(enemyTexture, enemyLocation, Color.White);
            sprite.DrawString(enemyFont, $"{stats.Health.ToString("0.0")}/{stats.MaxHealth}", new Vector2(enemyLocation.X + 125, enemyLocation.Y - 20), Color.White);
        }


        public float PhysicalDef { get { return stats.Defense; } }
        public float MagicDef { get { return stats.MagicDefense; } }
        public int Weakness { get; private set; }
        public int Resist { get; private set; }
        public int Immune { get; private set; }

        public void TakeDmg(float dmg)
        {
            float health = stats.Health - dmg;
            if (health < 0)
            { 
                health = 0;
            }
            stats.Health = health;

        }



    }
}
