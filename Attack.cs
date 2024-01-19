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
        // Attack = 0, Defense = 1, Magic Attack = 2, Magic Defense = 3, Speed = 4, All = 5, BothAttacks = 6, BothDefense = 7
        int buffType;
        float buffAmount;
        float basePower, manaUsage;


        private bool UseMagicAtk { get; set; }
        private bool IsMagicDmg { get; set; }
        private bool IsDeBuff { get; set; }

        
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
        public Attack(Texture2D icon, Texture2D animation, Rectangle iconLoc, Rectangle animPos, float manaUsage, bool useMagicAttack, bool dealsMagic, float basePower, int damageType)
        {
            this.attackIcon = icon;
            this.attackAnimation = animation;
            this.iconPosition = iconLoc;
            this.animationPosition = animPos;
            this.manaUsage = manaUsage;
            this.UseMagicAtk = useMagicAttack;
            this.IsMagicDmg = dealsMagic;
            this.basePower = basePower;
            this.dmgType = damageType;
            buffType = -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffStat">Attack = 0, Defense = 1, Magic Attack = 2, Magic Defense = 3, Speed = 4, All = 5, BothAttacks = 6, BothDefense = 7</param>
        /// <param name="debuff"></param>
        /// <param name="buffAmount"></param>
        public void AddBuff(int buffStat, bool debuff, float buffAmount)
        {
            this.buffType = buffStat;
            this.IsDeBuff = debuff;
            this.buffAmount = buffAmount;
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
        /// <param name="playerstats"></param>
        /// <param name="enemy"></param>
        /// <returns>The amount of damage to deal to enemy</returns>
        public void AttackDmg(Stats playerstats, Enemy enemy)
        {
            if (buffType != -1)
            {
                if (IsDeBuff)
                {
                    if (buffType == 0)
                    {
                        enemy.Stats.AttackMultiplyer *= buffAmount;
                    }
                    else if (buffType == 1)
                    {
                        enemy.Stats.DefenseMultiplyer *= buffAmount;
                    }
                    else if (buffType == 2)
                    {
                        enemy.Stats.MagicAttackMultiplyer *= buffAmount;
                    }
                    else if (buffType == 3)
                    {
                        enemy.Stats.MagicDefenseMultiplyer *= buffAmount;
                    }
                    else if (buffType == 4)
                    {
                        enemy.Stats.SpeedMultiplyer *= buffAmount;
                    }
                    else if (buffType == 5)
                    {
                        enemy.Stats.AttackMultiplyer *= buffAmount;
                        enemy.Stats.DefenseMultiplyer *= buffAmount;
                        enemy.Stats.MagicAttackMultiplyer *= buffAmount;
                        enemy.Stats.MagicDefenseMultiplyer *= buffAmount;
                        enemy.Stats.SpeedMultiplyer *= buffAmount;
                    }
                    else if (buffType == 6)
                    {
                        enemy.Stats.AttackMultiplyer *= buffAmount;
                        enemy.Stats.MagicAttackMultiplyer *= buffAmount;
                    }
                    else if (buffType == 7)
                    {
                        enemy.Stats.DefenseMultiplyer *= buffAmount;
                        enemy.Stats.MagicDefenseMultiplyer *= buffAmount;
                    }
                }
                else
                {
                    if (buffType == 0)
                    {
                        playerstats.AttackMultiplyer *= buffAmount;
                    }
                    else if (buffType == 1)
                    {
                        playerstats.DefenseMultiplyer *= buffAmount;
                    }
                    else if (buffType == 2)
                    {
                        playerstats.MagicAttackMultiplyer *= buffAmount;
                    }
                    else if (buffType == 3)
                    {
                        playerstats.MagicDefenseMultiplyer *= buffAmount;
                    }
                    else if (buffType == 4)
                    {
                        playerstats.SpeedMultiplyer *= buffAmount;
                    }
                    else if (buffType == 5)
                    {
                        playerstats.AttackMultiplyer *= buffAmount;
                        playerstats.DefenseMultiplyer *= buffAmount;
                        playerstats.MagicAttackMultiplyer *= buffAmount;
                        playerstats.MagicDefenseMultiplyer *= buffAmount;
                        playerstats.SpeedMultiplyer *= buffAmount;
                    }
                }
                
            }
            else
            {
                float atk, def;
                if (UseMagicAtk)
                    atk = playerstats.MagicAttack;
                else
                    atk = playerstats.Attack;

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
                    dmg = (((atk * 2 - def) * (1 + (basePower / 100f))) * (rngFactor.Next(90, 111) / 100f)) * F;
                }
                else
                {
                    dmg = (((atk * atk / def) * (1 + (basePower / 100f))) * (rngFactor.Next(90, 111) / 100f)) * F;
                }

                enemy.TakeDmg(dmg);
            }
            
        }
        /// <summary>
        /// Returns the location of icon
        /// </summary>
        public Rectangle rectangle { get { return iconPosition; } }

    }
}
