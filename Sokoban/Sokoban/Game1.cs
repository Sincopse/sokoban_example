using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Sokoban
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private int nRows = 0;
        private int nColumns = 0;
        private char[,] level;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            LoadLevel("level1.txt");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "Sample Text", new Vector2(320, 200), Color.Green);
            _spriteBatch.DrawString(font, $"Numero de Linhas = {nRows} | Numero de Colunas = {nColumns}", new Vector2(10, 10), Color.Black);
            _spriteBatch.DrawString(font, "dwqwqwe", new Vector2(10, 20), Color.Black);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        void LoadLevel(string levelFile)
        {
            string[] rows = File.ReadAllLines($"Content/{levelFile}");
            nRows = rows.Length;
            nColumns = rows[0].Length;

            level = new char[nColumns, nRows];

            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nColumns; j++)
                {
                    level[i,j] = rows[j][i];
                }
            }
        }
    }
}