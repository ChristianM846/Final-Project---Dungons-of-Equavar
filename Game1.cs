using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project___Dungons_of_Equavar
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState;

        Texture2D menuTexture;
        Texture2D startButtonTexture;
        Texture2D battleTexture;
        Texture2D backgroundTexture;

        Rectangle menuRect;
        Rectangle startButtonRect;

        Player kalstar;
        Player scorpious;
        


        enum Screen
        {
            Menu,
            Intro,
            CharacterSelesct,
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
            menuRect = new Rectangle(0, 0, 900, 640);
            startButtonRect = new Rectangle(300, 500, 300, 50);



            base.Initialize();





        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            menuTexture = Content.Load<Texture2D>("MenuScreen");
            startButtonTexture = Content.Load<Texture2D>("rectangle");
            battleTexture = Content.Load<Texture2D>("BattleBackground");
            backgroundTexture = Content.Load<Texture2D>("rectangle");


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();






            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            if (screen == Screen.Menu)
            {
                _spriteBatch.Draw(menuTexture, menuRect, Color.White);
                _spriteBatch.Draw(startButtonTexture, startButtonRect, Color.LightGray);

            }




            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}