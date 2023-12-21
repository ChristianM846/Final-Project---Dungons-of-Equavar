using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project___Dungons_of_Equavar
{
    internal class Player
    {
        int currentAttack;
        float maxHealth, health, maxMana, mana, attack, magicAttack, defense, magicDefense, speed;
        string name;

        Texture2D playerTexture;
        Rectangle iconLocation;
        Attack[] attacks;
        SpriteFont statText;

        public Player(string name, float maxhealth, float maxMana, float attack, float magicAttack, float defense, float magicDefense, float speed, Texture2D playerIcon, Rectangle iconRect, Attack[] attacks, SpriteFont font)
        {
            this.name = name;
            //Stats
            this.maxHealth = maxhealth;
            this.health = maxhealth;
            this.maxMana = maxMana;
            this.mana = maxMana;
            this.attack = attack;
            this.magicAttack = magicAttack;
            this.defense = defense;
            this.magicDefense = magicDefense;
            this.speed = speed;
            //Icon Tex and Attacks
            this.playerTexture = playerIcon;
            this.attacks = attacks;
            this.statText = font;
            this.iconLocation = iconRect;
            currentAttack = -1;
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(playerTexture, iconLocation, Color.White);
            sprite.DrawString(statText, $"HP: {health}/{maxHealth}", new Vector2(iconLocation.X + 60, iconLocation.Y), Color.White );
            sprite.DrawString(statText, $"MP: {mana}/{maxMana}", new Vector2(iconLocation.X + 60, iconLocation.Y + 30), Color.White);
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
                    mana -= attacks[i].UseMana();
                }
            }
            return didAttack;
        }

        public void DamageCalc(Enemy enemy)
        {
            float dmg = attacks[currentAttack].AttackDmg(attack, magicAttack, enemy);
            enemy.TakeDmg(dmg);
            currentAttack = -1;
        }

        public void TakeDmg(float dmg)
        {
            health -= dmg;
        }

    }
}
