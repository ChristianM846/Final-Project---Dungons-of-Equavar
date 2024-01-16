using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Final_Project___Dungons_of_Equavar

{
    //A class that holds all the attacks information
    public class Attack
    {
        static Random rngFactor = new Random();

        Texture2D attackIcon;
        Texture2D attackAnimation;
        Rectangle iconPosition;
        Rectangle animationPosition;

        // 0 = Bludgeoning, 1 = Piercing, 2 = Radiant, 3 = Fire, 4 = Ice, 5 = Lightning
        int dmgType;
        float basePower, manaUsage;


        private bool UseMagicAtk { get; set; }
        private bool IsMagicDmg { get; set; }
        /// <summary>
        /// Creates a new attack
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="animation"></param>
        /// <param name="iconLoc"></param>
        /// <param name="animPos"></param>
        /// <param name="manaUsage"></param>
        /// <param name="basePower"></param>
        /// <param name="damageType">0 = Bludgeoning, 1 = Piercing, 2 = Radiant, 3 = Fire, 4 = Ice, 5 = Lightning</param>
        public Attack(Texture2D icon, Texture2D animation, Rectangle iconLoc, Rectangle animPos, float manaUsage, float basePower, int damageType)
        {
            this.attackIcon = icon;
            this.attackAnimation = animation;
            this.iconPosition = iconLoc;
            this.animationPosition = animPos;
        }
        /// <summary>
        /// How much mana the attack uses
        /// </summary>
        /// <returns></returns>
        public float UseMana()
        {
            return manaUsage;
        }

        public void DrawIcon(SpriteBatch sprite)
        {
            sprite.Draw(attackIcon, iconPosition, Color.White);
        }
        public void DrawAnimation(SpriteBatch sprite)
        {
            sprite.Draw(attackAnimation, animationPosition, Color.White);
        }
        /// <summary>
        /// Calculates how much damage will be dealt.
        /// </summary>
        /// <param name="physicalAtk"></param>
        /// <param name="magicAtk"></param>
        /// <param name="enemy"></param>
        /// <returns>The amount of damage to deal to enemy</returns>
        public float AttackDmg(float physicalAtk, float magicAtk, Enemy enemy)
        {
            float atk, def;
            if (UseMagicAtk)
                atk = magicAtk;
            else
                atk = physicalAtk;

            if (IsMagicDmg)
                def = enemy.MagicDef;
            else
                def = enemy.PhysicalDef;

            float dmg = 0, F = 1;
            if (enemy.Weakness == dmgType)
                F = 2;
            else if (enemy.Resist == dmgType)
                F = 0.5f;
            else if (enemy.Immune == dmgType)
                F = 0;

            if (atk >= def && F != 0)
            {
                dmg = (((atk * 2 - def) * (1 + (basePower / 100))) * (rngFactor.Next(90, 111) / 100)) * F;
            }
            else
            {
                dmg = (((atk*atk/def) * (1 + (basePower / 100))) * (rngFactor.Next(90, 111) / 100)) *F;
            }

            return dmg;
        }
        /// <summary>
        /// Returns the location of icon
        /// </summary>
        public Rectangle rectangle { get { return iconPosition; } }

    }
}
