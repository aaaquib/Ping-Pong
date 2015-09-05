using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Pong
{

    /// This is a game component that implements IUpdateable.
    public class Scorecard : Microsoft.Xna.Framework.GameComponent
    {
        public bool win1;
        public bool win2;
        Color player1color;
        Color player2color;
        Rectangle rectangle;
        Rectangle rectangle2;
        Texture2D player1;
        Texture2D player2;
        float timer=0;
        SpriteFont font;
        SpriteBatch spriteBatch;

        public int score1, score2;
        public bool startTimer;

        public Scorecard(Game game)
            : base(game)
        {
            // Construct any child components here
            rectangle = new Rectangle(0, 0, 40, 40);
            rectangle2 = new Rectangle(0, 0, 40, 40);
            startTimer = false;
            Reset();
        }

        public void Reset()
        {
            score1 = 0;
            score2 = 0;
        }

        
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        public override void Initialize()
        {
            // Add your initialization code here
            rectangle.Location = new Point((int)Game.Window.ClientBounds.Width / 2 - 200, (int)10);
            rectangle2.Location = new Point((int)Game.Window.ClientBounds.Width / 2 + 200, (int)10);
            base.Initialize();
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("ScoreFont");
            player1 = Game.Content.Load<Texture2D>("player1");
            player2 = Game.Content.Load<Texture2D>("player2");
        }
        /// Allows the game component to update itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            switch (score1)
            {
                case 0: player2color = Color.White; break;
                case 1: player2color = Color.PeachPuff; break;
                case 2: player2color = Color.LightSalmon; break;
                case 3: player2color = Color.Coral; break;
                case 4: player2color = Color.OrangeRed; break;
                case 5: player2color = Color.DarkRed; break;
            }
            switch (score2)
            {
                case 0: player1color = Color.White; break;
                case 1: player1color = Color.PeachPuff; break;
                case 2: player1color = Color.LightSalmon; break;
                case 3: player1color = Color.Coral; break;
                case 4: player1color = Color.OrangeRed; break;
                case 5: player1color = Color.DarkRed; break;
            }

            if (score1 == 5) player1color = Color.LawnGreen;
            if (score2 == 5) player2color = Color.LawnGreen;
            if (startTimer == false)
            {
                timer = 0;
            }
            if (startTimer == true)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            //if (Math.IEEERemainder((double)gameTime.ElapsedGameTime.TotalMilliseconds, (double)1000) == 0)
            //{
            //    sec = sec++;

            //    if (sec == 60)
            //    {
            //        min = min++; sec = 0;
            //    }
            //    if ( visible == false)
            //    {
            //        sec = min = 0;
            //    }
            //}
            base.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
            // Draw the strings

            spriteBatch.DrawString(font, timer.ToString("0.00"), new Vector2(Game.Window.ClientBounds.Width / 2-20, 20), Color.White,
                       0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, Convert.ToString(score1), new Vector2(300, 20), Color.White,
                0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(font, Convert.ToString(score2), new Vector2(Game.Window.ClientBounds.Width - 300, 20), Color.White,
                0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(player1, rectangle, player1color);
            spriteBatch.Draw(player2, rectangle2, player2color);

            if (win1 == true)
            {
                spriteBatch.DrawString(font, "  Game Over", new Vector2(Game.Window.ClientBounds.Width/2 - 120, Game.Window.ClientBounds.Height/2-25), Color.White,
                   0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, " Player 1 WINS!", new Vector2(Game.Window.ClientBounds.Width / 2 - 150, Game.Window.ClientBounds.Height / 2), Color.Yellow,
                   0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, "Press 'Esc' for Menu", new Vector2(Game.Window.ClientBounds.Width / 2 - 170, Game.Window.ClientBounds.Height / 2 + 25), Color.White,
                   0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);
                //if (Keyboard.GetState().IsKeyDown(Keys.Space))
                //{
                //    win1 = false;
                //}
            }
            if (win2 == true)
            {
                spriteBatch.DrawString(font, "  Game Over", new Vector2(Game.Window.ClientBounds.Width / 2 - 120, Game.Window.ClientBounds.Height / 2 - 25), Color.White,
                   0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, " Player 2 WINS!", new Vector2(Game.Window.ClientBounds.Width / 2 - 150, Game.Window.ClientBounds.Height / 2), Color.Yellow,
                   0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, "Press 'Esc' for Menu", new Vector2(Game.Window.ClientBounds.Width / 2 - 170, Game.Window.ClientBounds.Height / 2 + 25), Color.White,
                   0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);
                //if (Keyboard.GetState().IsKeyDown(Keys.Space))
                //{
                //    win2 = false;
                //}
            }

            spriteBatch.End();
        }
    }
}
