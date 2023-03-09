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
        private int nRows = 0;
        private int nColumns = 0;
        private char[,] level;

        private Texture2D player, dot, box, wall;
        private SpriteFont font;

        private Player sokoban;

        int tileSize = 64;

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
            player = Content.Load<Texture2D>("Character4");
            dot = Content.Load<Texture2D>("EndPoint_Blue");
            box = Content.Load<Texture2D>("Crate_Brown");
            wall = Content.Load<Texture2D>("Wall_Brown");

            _graphics.PreferredBackBufferHeight = tileSize * level.GetLength(1); //definição da altura
            _graphics.PreferredBackBufferWidth = tileSize * level.GetLength(0); //definição da largura
            _graphics.ApplyChanges(); //aplica a atualização da janela

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
            Rectangle position = new Rectangle(0, 0, tileSize, tileSize); //calculo do retangulo a depender do tileSize
            for (int x = 0; x < level.GetLength(0); x++) //pega a primeira dimensão
            {
                for (int y = 0; y < level.GetLength(1); y++) //pega a segunda dimensão
                {
                    position.X = x * tileSize; // define o position
                    position.Y = y * tileSize; // define o position
                    switch (level[x, y])
                    {
                        //case 'Y':
                        //    _spriteBatch.Draw(player, position, Color.White);
                        //    break;
                        case '#':
                            _spriteBatch.Draw(box, position, Color.White);
                            break;
                        case '.':
                            _spriteBatch.Draw(dot, position, Color.White);
                            break;
                        case 'X':
                            _spriteBatch.Draw(wall, position, Color.White);
                            break;
                    }
                }
            }

            position.X = sokoban.Position.X * tileSize; //posição do Player
            position.Y = sokoban.Position.Y * tileSize; //posição do Player
            _spriteBatch.Draw(player, position, Color.White); //desenha o Player

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

            for (int x = 0; x < nColumns; x++)
            {
                for (int y = 0; y < nRows; y++)
                {
                    if (rows[y][x] == 'Y')
                    {
                        sokoban = new Player(x, y);
                        level[x, y] = ' '; // put a blank instead of the sokoban 'Y'
                    }
                    else
                    {
                        level[x, y] = rows[y][x];
                    }
                }
            }
        }
    }
}