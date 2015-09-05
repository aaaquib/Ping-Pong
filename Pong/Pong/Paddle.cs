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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Paddle : Microsoft.Xna.Framework.GameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;
       
        const int size = 75;
        public Rectangle rectangle;
        
        
        Vector2 position; // The paddle's position
        float y;

        const float initialSpeedFactor = 400;
        float speedFactor;

        Rectangle field;
        

        public Paddle(Game game,double x)
            : base(game)
        {
            rectangle = new Rectangle(0, 0, size/2, size);
            
            // set the (initial) speed factor
            speedFactor = initialSpeedFactor;

            // store a rectangle representing the field 
            // (to determine initial position and out-of-field situations)
            field = Game.Window.ClientBounds;
            field.Location = new Point(0, 0);
            y = (float)x;
            // set the initial position and velocity
            ResetPosition(y);
            // TODO: Construct any child components here
        }

        public void ResetPosition(float x)
        {
            // place the paddle in the middle
            position.X = (float)field.Width / x;
            position.Y = (float)field.Height / 2.0f - size / 2; 
        }
            
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("paddle");
            
        }
        public void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //check if the paddle is outside the window
            if (!rectangle.Intersects(field))
                ResetPosition(y);
          
            rectangle.Location = new Point((int)(position.X - size / 2.0f), (int)(position.Y - size / 2.0f));
            
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            // draw the paddle
            if (texture != null)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                spriteBatch.Draw(texture, rectangle, Color.Red);
                
                spriteBatch.End();
            }
        }
        public void MoveUp(GameTime gametime)
        {
            position.Y -= speedFactor * (float)gametime.ElapsedGameTime.TotalSeconds;

        }
        public void MoveDown(GameTime gameTime)
        {
            position.Y += speedFactor * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Checktopwallhit(Rectangle rect)
        {
            if (rectangle.Intersects(rect))
                position.Y = rect.Y + rect.Height + rectangle.Height/2.0f;
        }
        public void Checkbottomwallhit(Rectangle rect)
        {
            if (rectangle.Intersects(rect))
                position.Y = rect.Y-rectangle.Height/2.0f;
        }


    }
}
