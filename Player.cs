using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project___Dungons_of_Equavar
{
    internal class Player
    {
        string name;
        int currentAttack;
        int level, exp;
        Stats stats;
        // sowwy

        Texture2D playerTexture;
        Rectangle iconLocation;
        Attack[] attacks;
        SpriteFont statText;

        public Player(string name, float maxhealth, float maxMana, float attack, float magicAttack, float defense, float magicDefense, float speed, Texture2D playerIcon, Rectangle iconRect, Attack[] attacks, SpriteFont font)
        {
            this.name = name;
            //Stats
            this.stats = new Stats(maxhealth, maxhealth, maxMana, maxMana, attack, defense, magicAttack, magicDefense, speed);
            //Icon Tex and Attacks
            this.playerTexture = playerIcon;
            this.attacks = attacks;
            this.statText = font;
            this.iconLocation = iconRect;
            currentAttack = -1;
            level = 1; exp = 0;
        }

        public Stats Stats { get { return stats; } set { stats = value; } }


        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(playerTexture, iconLocation, Color.White);
            sprite.DrawString(statText, $"HP: {stats.Health}/{stats.MaxHealth}", new Vector2(iconLocation.X + 60, iconLocation.Y), Color.White );
            sprite.DrawString(statText, $"MP: {stats.Mana}/{stats.MaxMana}", new Vector2(iconLocation.X + 60, iconLocation.Y + 30), Color.White);
            sprite.DrawString(statText, name, new Vector2(iconLocation.X, iconLocation.Bottom + 5), Color.White);

            foreach (Attack attack in attacks)
            {
                attack.DrawIcon(sprite);
            }

            if (currentAttack != -1)
            {
                attacks[currentAttack].DrawAnimation(sprite);
            }

        }

        public bool Turn(MouseState mouse)
        {
            bool didAttack = false;

            for (int i = 0; i < attacks.Length; i++)
            {
                if (attacks[i].rectangle.Contains(mouse.X, mouse.Y))
                {
                    didAttack = true;
                    currentAttack = i;
                    float mana = stats.Mana - attacks[i].UseMana();
                    stats.Mana = mana;
                }
            }
            return didAttack;
        }

        public void DamageCalc(Enemy enemy)
        {
            float dmg = attacks[currentAttack].AttackDmg(stats.Attack, stats.MagicAttack, enemy);
            enemy.TakeDmg(dmg);
            currentAttack = -1;
        }

        public void TakeDmg(float dmg)
        {
            float health = stats.Health - dmg;
            stats.Health = health;
            
        }

        public bool GainExp(int exp)
        {
            this.exp += exp;
            return exp > 200 * level;
        }


    }
}
