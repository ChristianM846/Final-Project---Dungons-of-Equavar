using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project___Dungons_of_Equavar
{
    //The Player Class is used when creating any character such as Kalstar, Scorpious or Seraphina,
    public class Player
    {
        string name;
        int currentAttack;
        int level, exp;
        Stats stats;

        Texture2D playerTexture;
        Rectangle iconLocation;
        Attack[] attacks;
        SpriteFont statText;

        public Player(string name, Stats stat, Texture2D playerIcon, Rectangle iconRect, Attack[] attacks, SpriteFont font)
        {
            this.name = name;
            //Stats
            this.stats = stat;
            //Icon Tex and Attacks
            this.playerTexture = playerIcon;
            this.attacks = attacks;
            this.statText = font;
            this.iconLocation = iconRect;
            currentAttack = -1;
            level = 1; exp = 0;
        }

        public Stats Stats { get { return stats; } set { stats = value; } }

        /// <summary>
        /// Draws all the stuff player needs to draw
        /// </summary>
        /// <param name="sprite"></param>
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(playerTexture, iconLocation, Color.White);
            sprite.DrawString(statText, $"HP: {stats.Health.ToString("0.0")}/{stats.MaxHealth}", new Vector2(iconLocation.X + 80, iconLocation.Y), Color.White );
            sprite.DrawString(statText, $"MP: {stats.Mana}/{stats.MaxMana}", new Vector2(iconLocation.X + 80, iconLocation.Y + 20), Color.White);
            sprite.DrawString(statText, name, new Vector2(iconLocation.X, iconLocation.Bottom + 5), Color.White);
            sprite.DrawString(statText, $"Level: {level}", new Vector2(iconLocation.X + 190, iconLocation.Y), Color.White);
            sprite.DrawString(statText, $"Exp: {exp}/{level*200}", new Vector2(iconLocation.X + 190, iconLocation.Y + 20), Color.White);

            foreach (Attack attack in attacks)
            {
                attack.DrawIcon(sprite);
            }

            if (currentAttack != -1)
            {
                attacks[currentAttack].DrawAnimation(sprite);
            }

        }
        /// <summary>
        /// Passed when a mouseclick is detected on the player's turn
        /// </summary>
        /// <param name="mouse"></param>
        /// <returns>true if they clicked an attack, false otherwise</returns>
        public bool Turn(MouseState mouse)
        {
            bool didAttack = false;

            for (int i = 0; i < attacks.Length; i++)
            {
                if (attacks[i].rectangle.Contains(mouse.X, mouse.Y) && stats.Mana >= attacks[i].UseMana())
                {
                    didAttack = true;
                    currentAttack = i;
                    float mana = stats.Mana - attacks[i].UseMana();
                    stats.Mana = mana;
                }
            }
            return didAttack;
        }
        /// <summary>
        /// deals dmg to enemy called after Turn returns true and a short delay
        /// </summary>
        /// <param name="enemy">enemy you are attacking</param>
        public void DamageCalc(Enemy enemy)
        {
            attacks[currentAttack].AttackDmg(stats, enemy);
            currentAttack = -1;
        }
        /// <summary>
        /// Takes damage based on what is passed (-dmg = healing)
        /// </summary>
        /// <param name="dmg"></param>
        public void TakeDmg(float dmg)
        {
            float health = stats.Health - dmg;
            if (health < 0)
                health = 0;
            stats.Health = health;
            
        }
        /// <summary>
        /// Called after a battle to increase exp and check for level ups
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>true if leveled up</returns>
        public bool GainExp(int exp)
        {
            this.exp += exp;
            if (this.exp >= 200 * level)
            {
                this.exp -= 200 * level;
                level++;
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
