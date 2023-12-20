using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Project___Dungons_of_Equavar
{
    internal class Player
    {
        float maxHelath, health, maxMana, mana, attack, magicAttack, defense, magicDefense, speed;
        string name;

        Texture2D playerTexture;
        Texture2D[] attackIconTextures;
        Texture2D[] attackAnimationTextures;
        Rectangle[] attackRectangles;
        SpriteFont statText;

        public Player(string name, float maxhealth, float maxMana, float attack, float magicAttack, float defense, float magicDefense, float speed, Texture2D playerIcon)
        {
            this.name = name;
            this.maxHelath = maxhealth;
            this.health = maxhealth;
            this.maxMana = maxMana;
            this.mana = maxMana;
            this.attack = attack;
            this.magicAttack = magicAttack;
            this.defense = defense;
            this.magicDefense = magicDefense;
            this.speed = speed;

            
        }







    }
}
