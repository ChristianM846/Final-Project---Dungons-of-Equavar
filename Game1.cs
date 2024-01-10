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

        int introTextY;

        MouseState mouseState, pastState;

        Texture2D menuTexture;
        Texture2D startButtonTexture;
        Texture2D battleTexture;
        Texture2D backgroundTexture;

        Rectangle startButtonRect;
        Rectangle backgroundRect;

        SpriteFont menuFont;
        SpriteFont titleFont;
        SpriteFont introFont;
        SpriteFont statFont;

        SoundEffect introTheme;
        SoundEffectInstance introThemeInstance;

        Player kalstar;
        Player scorpious;
        


        enum Screen
        {
            Menu,
            Intro,
            CharacterSelect,
            BeforeFirstBattle,
            Battle,
            Between
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
            startButtonRect = new Rectangle(300, 300, 300, 50);
            


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
            introTextY = _graphics.PreferredBackBufferHeight;

            // Battle
            battleTexture = Content.Load<Texture2D>("BattleBackground");




            //Sound Effects
            introTheme = Content.Load<SoundEffect>("IntroTheme");
            introThemeInstance = introTheme.CreateInstance();
            


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
                

                
                if (mouseState.LeftButton == ButtonState.Pressed && pastState.LeftButton != ButtonState.Pressed)
                {
                    introThemeInstance.Stop();
                    screen = Screen.CharacterSelect;
                }
                introTextY -= 1;
            }
            else if (screen == Screen.CharacterSelect)
            {



            }




            pastState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
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
                _spriteBatch.DrawString(introFont, "The thriving world of Equavar,", new Vector2(175, introTextY), Color.White);
                _spriteBatch.DrawString(introFont, "a world filled with magic, and", new Vector2(195, introTextY + 50), Color.White);
                _spriteBatch.DrawString(introFont, "races of every kind,", new Vector2(280, introTextY + 100), Color.White);
                _spriteBatch.DrawString(introFont, "the diverse humans, the elegant elves,", new Vector2(120, introTextY + 150), Color.White);
                _spriteBatch.DrawString(introFont, "the fearsome dragonkin, the sturdy dwarves,", new Vector2(70, introTextY + 200), Color.White);
                _spriteBatch.DrawString(introFont, "the raging orcs, the tricky demons,", new Vector2(170, introTextY + 250), Color.White);
                _spriteBatch.DrawString(introFont, "and so many more", new Vector2(295, introTextY + 300), Color.White);
                _spriteBatch.DrawString(introFont, "However, among the thriving and", new Vector2(175, introTextY + 450), Color.White);
                _spriteBatch.DrawString(introFont, "peacful creatures, there are times", new Vector2(170, introTextY + 500), Color.White);
                _spriteBatch.DrawString(introFont, "of war and conflict,", new Vector2(280, introTextY + 550), Color.White);
                _spriteBatch.DrawString(introFont, "The ongoing war between the,", new Vector2(175, introTextY + 700), Color.White);
                _spriteBatch.DrawString(introFont, "material plane and the nine hells", new Vector2(175, introTextY + 750), Color.White);
                _spriteBatch.DrawString(introFont, "has raged for years, as the evil", new Vector2(175, introTextY + 800), Color.White);
                _spriteBatch.DrawString(introFont, "queen of the hells, ", new Vector2(175, introTextY + 850), Color.White);
                _spriteBatch.DrawString(introFont, "The thriving country of Equavar,", new Vector2(175, introTextY + 900), Color.White);
                _spriteBatch.DrawString(introFont, "The thriving country of Equavar,", new Vector2(175, introTextY + 950), Color.White);
                _spriteBatch.DrawString(introFont, "The thriving country of Equavar,", new Vector2(175, introTextY + 1000), Color.White);

            }
            else if (screen == Screen.CharacterSelect)
            {

            }
            else if (screen == Screen.BeforeFirstBattle)
            {




            }
            else if (screen == Screen.Battle)
            {


            }
            else if (screen == Screen.Between)
            {


            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}