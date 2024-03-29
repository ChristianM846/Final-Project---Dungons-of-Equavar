﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project___Dungons_of_Equavar
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        bool didAttack, battling, fullHeal, healedAfterBattle;
        int introTextY, turnCounter;
        float timer, battleNum;

        MouseState mouseState, pastState;

        Texture2D menuTexture;
        Texture2D startButtonTexture;
        Texture2D battleTexture;
        Texture2D backgroundTexture;
        Texture2D textBoxTexture;
        Texture2D kalstarPortrait;
        Texture2D scorpiusPortrait;
        Texture2D seraphinaPortrait;

        Rectangle startButtonRect;
        Rectangle backgroundRect;
        Rectangle battleBackgroundRect;
        Rectangle textBoxrect;
        Rectangle kalstarPortraitRect;
        Rectangle scorpiusPortraitRect;
        Rectangle seraphinaPortraitRect;

        SpriteFont menuFont;
        SpriteFont titleFont;
        SpriteFont introFont;
        SpriteFont statFont;
        SpriteFont extraTextFont;
        SpriteFont toSkipFont;
        SpriteFont enemyFont;

        SoundEffect introTheme;
        SoundEffectInstance introThemeInstance;
        SoundEffect battleTheme;
        SoundEffectInstance battleThemeInstance;

        Player kalstar;
        Player scorpius;
        Enemy goblin;


        enum Screen
        {
            Menu,
            Intro,
            CharacterSelect,
            BeforeFirstBattle,
            Battle,
            Between,
            GameOver
        }
        Screen screen;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            screen = Screen.Menu;
            backgroundRect = new Rectangle(0, 0, 900, 640);
            battleBackgroundRect = new Rectangle(0, 0, 900, 440);
            startButtonRect = new Rectangle(300, 300, 300, 50);
            textBoxrect = new Rectangle(0, 440, 900, 200);
            kalstarPortraitRect = new Rectangle(200, 150, 125, 125);
            scorpiusPortraitRect = new Rectangle(600, 150, 125, 125);
            seraphinaPortraitRect = new Rectangle(400, 150, 125, 125);
            turnCounter = 0;
            battleNum = 0;
            fullHeal = false;
            healedAfterBattle = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Menu
            menuTexture = Content.Load<Texture2D>("MenuScreen");
            startButtonTexture = Content.Load<Texture2D>("rectangle");
            menuFont = Content.Load<SpriteFont>("MenuText");
            titleFont = Content.Load<SpriteFont>("TitleCard");

            //Intro
            backgroundTexture = Content.Load<Texture2D>("rectangle");
            introFont = Content.Load<SpriteFont>("Intro");
            toSkipFont = Content.Load<SpriteFont>("Tips");
            introTextY = _graphics.PreferredBackBufferHeight;

            // Character Selection
            textBoxTexture = Content.Load<Texture2D>("TextBox");
            extraTextFont = Content.Load<SpriteFont>("ExtraText");
            kalstarPortrait = Content.Load<Texture2D>("KalstarPortrait");
            scorpiusPortrait = Content.Load<Texture2D>("ScorpiusPortrait");
            seraphinaPortrait = Content.Load<Texture2D>("SeraphinaPortrait");

            // Battle
            battleTexture = Content.Load<Texture2D>("BattleBackground");
            statFont = Content.Load<SpriteFont>("Stats");
            enemyFont = Content.Load<SpriteFont>("EnemyFont");

            //Sound Effects
            introTheme = Content.Load<SoundEffect>("IntroTheme");
            introThemeInstance = introTheme.CreateInstance();
            battleTheme = Content.Load<SoundEffect>("BattleTheme");
            battleThemeInstance = battleTheme.CreateInstance();

            Rectangle enemyLocation = new(260, 90, 300, 300);

            Stats kalstarStats = new Stats(100, 70, 10, 2, 13, 8, 5);
            Attack[] kalstarAttacks = new Attack[6]
            {
                new Attack(Content.Load<Texture2D>("HammerStrikeIcon"), Content.Load<Texture2D>("StaffWack_HammerStrikeEffect"), new Rectangle(120, 490, 65, 65), new Rectangle(330, 110, 200, 200), 0, false, false, 5, 0),
                new Attack(Content.Load<Texture2D>("DivineStrikeIcon"), Content.Load<Texture2D>("StaffWack_HammerStrikeEffect"), new Rectangle(200, 490, 65, 65), new Rectangle(330, 110, 200, 200), 10, false, true, 8, 2),
                new Attack(Content.Load<Texture2D>("LayOnHandsIcon"), Content.Load<Texture2D>("LayOnHandsIcon"), new Rectangle(280, 490, 65, 65), new Rectangle(330, 110, 200, 200), 10, false, false, 0, 2),
                new Attack(Content.Load<Texture2D>("ShieldOfFaithIcon"), Content.Load<Texture2D>("ShieldOfFaithIcon"), new Rectangle(120, 560, 65, 65), new Rectangle(330, 110, 200, 200), 5, false, false, 0, 2),
                new Attack(Content.Load<Texture2D>("RaiseTheDeadIcon"), Content.Load<Texture2D>("RaiseTheDeadIcon"), new Rectangle(200, 560, 65, 65), new Rectangle(330, 110, 200, 200), 30, false, false, 0, 2),
                new Attack(Content.Load<Texture2D>("RallyIcon"), Content.Load<Texture2D>("RallyIcon"), new Rectangle(280, 560, 65, 65), new Rectangle(330, 110, 200, 200), 5, false, false, 0, 2)

            };
            kalstarAttacks[2].AddBuff(0, false, 1); // Will be healing
            kalstarAttacks[3].AddBuff(7, false, 1.5f);
            kalstarAttacks[4].AddBuff(0, false, 1); // Will be revival
            kalstarAttacks[5].AddBuff(1, false, 1.5f);
            kalstar = new Player("Kalstar", kalstarStats, kalstarPortrait, new Rectangle(20, 450, 75, 75), kalstarAttacks, statFont);

            Stats scorpiusStats = new Stats(60, 100, 3, 15, 5, 15, 10);
            Attack[] scorpiusAttacks = new Attack[6]
            {
                new Attack(Content.Load<Texture2D>("StaffWackIcon"), Content.Load<Texture2D>("StaffWack_HammerStrikeEffect"), new Rectangle(520, 490, 65, 65), new Rectangle(330, 110, 200, 200), 0, false, false, 2, 0),
                new Attack(Content.Load<Texture2D>("FireBoltIcon"), Content.Load<Texture2D>("FireBoltEffect"), new Rectangle(600, 490, 65, 65), new Rectangle(280, 90, 300, 300), 5, true, true, 6, 3),
                new Attack(Content.Load<Texture2D>("IceShardIcon"), Content.Load<Texture2D>("IceShardEffect"), new Rectangle(680, 490, 65, 65), new Rectangle(330, 140, 200, 200), 5, true, true, 5, 4),
                new Attack(Content.Load<Texture2D>("LightningBoltIcon"), Content.Load<Texture2D>("LightningBoltEffect"), new Rectangle(520, 560, 65, 65), new Rectangle(300, 90, 300, 300), 5, true, true, 6, 5),
                new Attack(Content.Load<Texture2D>("MeltIcon"), Content.Load<Texture2D>("MeltIcon"), new Rectangle(600, 560, 65, 65), new Rectangle(330, 110, 200, 200), 15, true, true, 0, 0),
                new Attack(Content.Load<Texture2D>("WeakenIcon"), Content.Load<Texture2D>("WeakenIcon"), new Rectangle(680, 560, 65, 65), new Rectangle(330, 110, 200, 200), 15,true, true,  0, 0)

            };
            scorpiusAttacks[4].AddBuff(0, true, 0.5f);
            scorpiusAttacks[5].AddBuff(5, true, 0.75f);
            scorpius = new Player("Scorpius", scorpiusStats, scorpiusPortrait, new Rectangle(420, 450, 75, 75), scorpiusAttacks, statFont);

            Stats goblinStats = new Stats(200, 0, 9, 0, 9, 3, 6);
            goblin = new Enemy(goblinStats, Content.Load<Texture2D>("GoblinSprite"), enemyLocation, false, statFont);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();

            if (screen == Screen.Menu)
            {
                if (introThemeInstance.State == SoundState.Stopped)
                {
                    introThemeInstance.Play();
                }



                if (mouseState.LeftButton == ButtonState.Pressed && startButtonRect.Contains(mouseState.Position))
                {
                    screen = Screen.Intro;
                }

            }
            else if (screen == Screen.Intro)
            {



                if (mouseState.LeftButton == ButtonState.Pressed && pastState.LeftButton != ButtonState.Pressed || introTextY <= -2250)
                {
                    introThemeInstance.Stop();
                    screen = Screen.CharacterSelect;
                }
                introTextY -= 1;
            }
            else if (screen == Screen.CharacterSelect)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && pastState.LeftButton != ButtonState.Pressed)
                {
                    screen = Screen.BeforeFirstBattle;
                }
            }
            else if (screen == Screen.BeforeFirstBattle)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && pastState.LeftButton != ButtonState.Pressed)
                {
                    battleThemeInstance.Play();
                    screen = Screen.Battle;
                    battling = true;
                }
            }
            else if (screen == Screen.Battle)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (turnCounter == 0 && (mouseState.LeftButton == ButtonState.Pressed || didAttack))
                {
                    if (kalstar.Stats.Health == 0)
                    {
                        turnCounter++;
                    }
                    else if (didAttack)
                    {
                        if (timer > 2)
                        {
                            kalstar.DamageCalc(goblin);
                            didAttack = false;
                            turnCounter++;
                        }
                    }
                    else if (kalstar.Turn(mouseState))
                    {
                        didAttack = true;
                        timer = 0;
                    }
                }
                else if (turnCounter == 1 && (mouseState.LeftButton == ButtonState.Pressed || didAttack || goblin.Stats.Health == 0))
                {
                    if (goblin.Stats.Health == 0)
                    {
                        if (battling)
                        {
                            if (kalstar.GainExp(50))
                            {
                                kalstar.Stats.MaxHealth += 10;
                                kalstar.Stats.MaxMana += 5;
                                kalstar.Stats.Attack += 1;
                                kalstar.Stats.MagicAttack += 0.5f;
                                kalstar.Stats.Defense += 1.5f;
                                kalstar.Stats.MagicDefense += 1;
                            }

                            if (scorpius.GainExp(50))
                            {
                                scorpius.Stats.MaxHealth += 5;
                                scorpius.Stats.MaxMana += 10;
                                scorpius.Stats.Attack += 0.5f;
                                scorpius.Stats.MagicAttack += 1;
                                scorpius.Stats.Defense += 0.5f;
                                scorpius.Stats.MagicDefense += 2;
                            }
                            battling = false;
                            battleNum++;
                        }

                        battleThemeInstance.Stop();



                        if (timer > 5)
                            screen = Screen.Between;
                    }
                    else
                    {
                        if (scorpius.Stats.Health == 0)
                        {
                            turnCounter++;
                        }
                        else if (didAttack)
                        {
                            if (timer > 2)
                            {
                                scorpius.DamageCalc(goblin);
                                didAttack = false;

                                if (scorpius.Stats.Health != 0)
                                {
                                    scorpius.Stats.Mana += 2;

                                    if (scorpius.Stats.Mana > scorpius.Stats.MaxMana)
                                    {
                                        scorpius.Stats.Mana = scorpius.Stats.MaxMana;
                                    }
                                }

                                turnCounter++;
                            }
                        }
                        else if (scorpius.Turn(mouseState))
                        {
                            didAttack = true;
                            timer = 0;
                        }
                    }
                }
                else if (turnCounter == 2)
                {
                    bool gobTurn = false;
                    if (goblin.Stats.Health == 0)
                    {
                        if (battling)
                        {
                            if (kalstar.GainExp(50))
                            {
                                kalstar.Stats.MaxHealth += 10;
                                kalstar.Stats.MaxMana += 5;
                                kalstar.Stats.Attack += 1;
                                kalstar.Stats.MagicAttack += 0.5f;
                                kalstar.Stats.Defense += 1.5f;
                                kalstar.Stats.MagicDefense += 1;
                            }

                            if (scorpius.GainExp(50))
                            {
                                scorpius.Stats.MaxHealth += 5;
                                scorpius.Stats.MaxMana += 10;
                                scorpius.Stats.Attack += 0.5f;
                                scorpius.Stats.MagicAttack += 1;
                                scorpius.Stats.Defense += 0.5f;
                                scorpius.Stats.MagicDefense += 2;
                            }

                            battling = false;
                            battleNum++;
                        }

                        battleThemeInstance.Stop();
                        if (timer > 5)
                            screen = Screen.Between;
                    }
                    else
                    {
                        if (kalstar.Stats.Health > 0)
                        {
                            gobTurn = goblin.Turn(kalstar);
                        }
                        else if (kalstar.Stats.Health == 0 && scorpius.Stats.Health > 0)
                        {
                            gobTurn = goblin.Turn(scorpius);
                        }

                        if (kalstar.Stats.Health > 0 || scorpius.Stats.Health > 0)
                        {
                            if (gobTurn)
                                turnCounter = 0;
                        }
                        else
                        {
                            battling = false;

                            if (timer > 5)
                            {
                                battleThemeInstance.Stop();
                                screen = Screen.GameOver;
                            }
                        }
                    }
                }
            }
            else if (screen == Screen.Between)
            {

                if (battleNum / 5 == 1 || battleNum / 5 == 2 || battleNum / 5 == 3 || battleNum / 5 == 4 || battleNum / 5 == 5 && healedAfterBattle == false)
                {
                    kalstar.Stats.Health = kalstar.Stats.MaxHealth;
                    kalstar.Stats.Mana = kalstar.Stats.MaxMana;
                    scorpius.Stats.Health = scorpius.Stats.MaxHealth;
                    scorpius.Stats.Mana = scorpius.Stats.MaxMana;
                    fullHeal = true;
                    healedAfterBattle = true;
                }
                else if (healedAfterBattle == false)
                {
                    kalstar.Stats.Health += (kalstar.Stats.MaxHealth / 4);
                    kalstar.Stats.Mana += (int)kalstar.Stats.MaxMana / 4;
                    scorpius.Stats.Health += scorpius.Stats.MaxHealth / 4;
                    scorpius.Stats.Mana += (int)scorpius.Stats.MaxMana / 4;

                    fullHeal = false;
                    healedAfterBattle = true;
                }

                if (mouseState.LeftButton == ButtonState.Pressed && pastState.LeftButton != ButtonState.Pressed)
                {

                    kalstar.Stats.AttackMultiplyer = 1;
                    kalstar.Stats.DefenseMultiplyer = 1;
                    kalstar.Stats.MagicAttackMultiplyer = 1;
                    kalstar.Stats.MagicDefenseMultiplyer = 1;
                    kalstar.Stats.SpeedMultiplyer = 1;

                    scorpius.Stats.AttackMultiplyer = 1;
                    scorpius.Stats.DefenseMultiplyer = 1;
                    scorpius.Stats.MagicAttackMultiplyer = 1;
                    scorpius.Stats.MagicDefenseMultiplyer = 1;
                    scorpius.Stats.SpeedMultiplyer = 1;

                    goblin.Stats.AttackMultiplyer = 1;
                    goblin.Stats.DefenseMultiplyer = 1;
                    goblin.Stats.MagicAttackMultiplyer = 1;
                    goblin.Stats.MagicDefenseMultiplyer = 1;
                    goblin.Stats.SpeedMultiplyer = 1;


                    goblin.Stats.Health = goblin.Stats.MaxHealth;
                    turnCounter = 0;
                    battling = true;
                    healedAfterBattle = false;
                    screen = Screen.Battle;
                    battleThemeInstance.Play();
                }


            }

            if (kalstar.Stats.Health > kalstar.Stats.MaxHealth)
            {
                kalstar.Stats.Health = kalstar.Stats.MaxHealth;
            }

            if (kalstar.Stats.Mana > kalstar.Stats.MaxMana)
            {
                kalstar.Stats.Mana = kalstar.Stats.MaxMana;
            }

            if (scorpius.Stats.Health > scorpius.Stats.MaxHealth)
            {
                scorpius.Stats.Health = scorpius.Stats.MaxHealth;
            }

            if (scorpius.Stats.Mana > scorpius.Stats.MaxMana)
            {
                scorpius.Stats.Mana = scorpius.Stats.MaxMana;
            }

            pastState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            if (screen == Screen.Menu)
            {
                _spriteBatch.Draw(menuTexture, backgroundRect, Color.White);
                _spriteBatch.Draw(startButtonTexture, startButtonRect, Color.LightGray);
                _spriteBatch.DrawString(titleFont, "Dungeons Of Equavar", new Vector2(135, 100), Color.Yellow);
                _spriteBatch.DrawString(menuFont, "Start Game", new Vector2(350, 310), Color.Black);


            }
            else if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.Black);
                _spriteBatch.DrawString(introFont, "The thriving world of Equavar,", new Vector2(185, introTextY), Color.White);
                _spriteBatch.DrawString(introFont, "a world filled with magic, and", new Vector2(195, introTextY + 50), Color.White);
                _spriteBatch.DrawString(introFont, "races of every kind,", new Vector2(280, introTextY + 100), Color.White);
                _spriteBatch.DrawString(introFont, "the diverse humans, the elegant elves,", new Vector2(120, introTextY + 150), Color.White);
                _spriteBatch.DrawString(introFont, "the fearsome dragonkin, the sturdy dwarves,", new Vector2(70, introTextY + 200), Color.White);
                _spriteBatch.DrawString(introFont, "the raging orcs, the tricky demons,", new Vector2(170, introTextY + 250), Color.White);
                _spriteBatch.DrawString(introFont, "and so many more.", new Vector2(295, introTextY + 300), Color.White);
                _spriteBatch.DrawString(introFont, "However, among the thriving and", new Vector2(160, introTextY + 450), Color.White);
                _spriteBatch.DrawString(introFont, "peaceful creatures, there are times", new Vector2(155, introTextY + 500), Color.White);
                _spriteBatch.DrawString(introFont, "of war and conflict.", new Vector2(285, introTextY + 550), Color.White);
                _spriteBatch.DrawString(introFont, "The ongoing war between the,", new Vector2(180, introTextY + 700), Color.White);
                _spriteBatch.DrawString(introFont, "material plane and the nine hells", new Vector2(165, introTextY + 750), Color.White);
                _spriteBatch.DrawString(introFont, "has raged for years, as the evil", new Vector2(175, introTextY + 800), Color.White);
                _spriteBatch.DrawString(introFont, "queen of the hells, Sinestra,", new Vector2(200, introTextY + 850), Color.White);
                _spriteBatch.DrawString(introFont, "strives for control of the surface.", new Vector2(165, introTextY + 900), Color.White);
                _spriteBatch.DrawString(introFont, "Amidst the hordes of monsters, heroes stand,", new Vector2(65, introTextY + 1050), Color.White);
                _spriteBatch.DrawString(introFont, "swinging sword, launching arrows,", new Vector2(170, introTextY + 1100), Color.White);
                _spriteBatch.DrawString(introFont, "and casting spells to defend their homes.", new Vector2(100, introTextY + 1150), Color.White);
                _spriteBatch.DrawString(introFont, "But the conflict rages on, Sinetra", new Vector2(165, introTextY + 1300), Color.White);
                _spriteBatch.DrawString(introFont, "has created a new super weapon,", new Vector2(160, introTextY + 1350), Color.White);
                _spriteBatch.DrawString(introFont, "an artifcat capable of draining,", new Vector2(185, introTextY + 1400), Color.White);
                _spriteBatch.DrawString(introFont, "the natural magic within a warrior", new Vector2(165, introTextY + 1450), Color.White);
                _spriteBatch.DrawString(introFont, "and transfer it to herself.", new Vector2(230, introTextY + 1500), Color.White);
                _spriteBatch.DrawString(introFont, "In the test run of this new weapon,", new Vector2(165, introTextY + 1650), Color.White);
                _spriteBatch.DrawString(introFont, "Sinstra has captured a small group", new Vector2(155, introTextY + 1700), Color.White);
                _spriteBatch.DrawString(introFont, "of warriors, drained them of their magic", new Vector2(120, introTextY + 1750), Color.White);
                _spriteBatch.DrawString(introFont, "and has left them to rot in the dungeons.", new Vector2(115, introTextY + 1800), Color.White);
                _spriteBatch.DrawString(introFont, "However, with such an imperfect device,", new Vector2(95, introTextY + 1950), Color.White);
                _spriteBatch.DrawString(introFont, "a small peice of the warriors' magic remains,", new Vector2(65, introTextY + 2000), Color.White);
                _spriteBatch.DrawString(introFont, "giving them the perfect chance", new Vector2(175, introTextY + 2050), Color.White);
                _spriteBatch.DrawString(introFont, "to fight their way out,", new Vector2(270, introTextY + 2100), Color.White);
                _spriteBatch.DrawString(introFont, "and perhaps, destory the artifact", new Vector2(170, introTextY + 2150), Color.White);
                _spriteBatch.DrawString(introFont, "before Sinestra can use it again.", new Vector2(170, introTextY + 2200), Color.White);
                _spriteBatch.DrawString(toSkipFont, "Click to Skip", new Vector2(800, 600), Color.White);

            }
            else if (screen == Screen.CharacterSelect)
            {
                _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.Black);
                _spriteBatch.Draw(textBoxTexture, textBoxrect, Color.White);
                _spriteBatch.Draw(kalstarPortrait, kalstarPortraitRect, Color.White);
                _spriteBatch.Draw(scorpiusPortrait, scorpiusPortraitRect, Color.White);
                _spriteBatch.Draw(seraphinaPortrait, seraphinaPortraitRect, Color.Gray);
                // All Text here should start at 30 and be no less than 20 pixels off the top or bottom
                _spriteBatch.DrawString(extraTextFont, "Okay, so this game is still in early development, and I had to focus on the combat first, so character", new Vector2(30, 460), Color.White);
                _spriteBatch.DrawString(extraTextFont, "selection needs to come last. Normally you would be able to choose one of the three characters as the", new Vector2(30, 480), Color.White);
                _spriteBatch.DrawString(extraTextFont, "main character, and one other as your companion. But for now, you'll just have Kalstar as your main", new Vector2(30, 500), Color.White);
                _spriteBatch.DrawString(extraTextFont, "character (The dragonkin paladin), and Scorpius as your companion (The human wizard). Cool? Cool!", new Vector2(30, 520), Color.White);
                _spriteBatch.DrawString(toSkipFont, "Click to Continue", new Vector2(760, 600), Color.White);
            }
            else if (screen == Screen.BeforeFirstBattle)
            {
                _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.Black);
                _spriteBatch.Draw(textBoxTexture, textBoxrect, Color.White);
                _spriteBatch.DrawString(extraTextFont, "Normally right here would be a small decription, maybe a small animation showing how the captured", new Vector2(30, 460), Color.White);
                _spriteBatch.DrawString(extraTextFont, "heroes broke out of their cell and got ready to try and find Sinestra, but that comes after", new Vector2(30, 480), Color.White);
                _spriteBatch.DrawString(extraTextFont, "I've finished the combat system. So for now, onto the combat", new Vector2(30, 500), Color.White);
                _spriteBatch.DrawString(toSkipFont, "Click to Continue", new Vector2(760, 600), Color.White);
            }
            else if (screen == Screen.Battle)
            {
                _spriteBatch.Draw(battleTexture, battleBackgroundRect, Color.White);
                _spriteBatch.Draw(textBoxTexture, textBoxrect, Color.White);
                goblin.Draw(_spriteBatch);
                kalstar.Draw(_spriteBatch);
                scorpius.Draw(_spriteBatch);


            }
            else if (screen == Screen.Between)
            {
                if (fullHeal)
                {
                    _spriteBatch.DrawString(introFont, $"You have defeated {battleNum} enemies. Your party \n had time for a long rest, \n fully recovering their health and mana.", new Vector2(75, 50), Color.White);
                    _spriteBatch.DrawString(toSkipFont, "Click to Continue", new Vector2(760, 600), Color.White);
                }
                else
                {
                    _spriteBatch.DrawString(introFont, $"You have defeated {battleNum} enemies. Your party \n had time for a short rest, \n partially recovering their health and mana.", new Vector2(75, 50), Color.White);
                    _spriteBatch.DrawString(toSkipFont, "Click to Continue", new Vector2(760, 600), Color.White);
                }

            }
            else if (screen == Screen.GameOver)
            {
                _spriteBatch.DrawString(titleFont, "GAME OVER", new Vector2(300, 300), Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}