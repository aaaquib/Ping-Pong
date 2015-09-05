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
    public class Ball : Microsoft.Xna.Framework.GameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;
        const int size = 25;
        Rectangle rectangle;
        Vector2 position;  /// The ball's position
        //Vector2 lastposition;
        Vector2 velocity;

        public float initialSpeedFactor = 600;
        float speedFactor;
        double angle;

        Rectangle field;

        public Ball(Game game)
            : base(game)
        {
            rectangle = new Rectangle(0, 0, size, size);
            // set the (initial) speed factor
            speedFactor = initialSpeedFactor;

            // store a rectangle representing the field 
            // (to determine initial position and out-of-field situations)
            field = Game.Window.ClientBounds;
            field.Location = new Point(0, 0);

            // set the initial position and velocity
      
            ResetPosition(false);
            // TODO: Construct any child components here
        }

        public void pauseball(bool p)
        {
            if (p == true)
            {
                
                speedFactor = 0;

            }
        }

        public void ResetPosition(bool x)
        {
            if (x == false)
            {
                speedFactor = 0;
            }
            else
            {
                speedFactor = initialSpeedFactor;
            }
            // place the ball in the middle of the field
            position.X = (float)Game.Window.ClientBounds.Width / 2.0f;
            position.Y = (float)Game.Window.ClientBounds.Height / 2.0f;
            // choose a random direction and set initial velocity
            double random = new Random().Next(0, 360);
            
            if (random >= 0 && random <= 90)
            {
                angle = (double)(new Random().Next(20,80))/ (double)180.0 * Math.PI;
                
            }
            if (random > 90 && random <= 180)
            {
                angle = (double)(new Random().Next(100,140)) / (double)180.0 * Math.PI;
                
            }
            if (random > 180 && random <= 270)
            {
                angle = (double)(new Random().Next(220,260)) / (double)180.0 * Math.PI;
                
            }
            if (random > 270 && random <= 360)
            {
                angle = (double)(new Random().Next(280,340)) / (double)180.0 * Math.PI;
                
            }
            velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
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
            texture = Game.Content.Load<Texture2D>("ball");
        }
        public void UnloadContent()
        {
        }

        public Boolean CheckHorizontalHit(Rectangle rect)
        {
            if (rect.Intersects(rectangle))
            {
                // adjust velocity vector
                velocity.X *= -1;
 
                // Adjust position so that it will not intersect again immediately (trapped ball)
                
                if (rect.Center.X > rectangle.Center.X)
                {
                    position.X = rect.X - rectangle.Width / 2;
                    //if (rectangle.Center.Y >= rect.Top && rectangle.Center.Y < (rect.Top + rect.Center.Y) / 2)
                    //{
                    //    angle = (double)60 * Math.PI / (double)180.0 ;
                    //    velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
                    //}
                    //if (rectangle.Center.Y >= (rect.Top + rect.Center.Y) / 2 && rectangle.Center.Y < (rect.Top + rect.Bottom) / 2)
                    //{
                    //    angle = (double)75 * Math.PI / (double)180.0;
                    //    velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
                    //}
                    //if (rectangle.Center.Y >= (rect.Top+rect.Bottom)/2 && rectangle.Center.Y < (rect.Bottom + rect.Center.Y) / 2)
                    //{
                    //    angle = (double)105 * Math.PI  / (double)180.0;
                    //    velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
                    //}
                    //if (rectangle.Center.Y >= (rect.Bottom + rect.Center.Y) / 2 && rectangle.Center.Y < rect.Bottom)
                    //{
                    //    angle = (double)135 * Math.PI / (double)180.0;
                    //    velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
                    //}
                }
                else 
                {
                    position.X = rect.X + rect.Width + rectangle.Width / 2;
                    //if (rectangle.Center.Y >= rect.Top && rectangle.Center.Y < (rect.Top + rect.Center.Y) / 2)
                    //{
                    //    angle = (double)315 * Math.PI / (double)180.0;
                    //    velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
                    //}
                    //if (rectangle.Center.Y >= (rect.Top + rect.Center.Y) / 2 && rectangle.Center.Y < (rect.Top + rect.Bottom) / 2)
                    //{
                    //    angle = (double)285 * Math.PI / (double)180.0;
                    //    velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
                    //}
                    //if (rectangle.Center.Y >= (rect.Top + rect.Bottom) / 2 && rectangle.Center.Y < (rect.Bottom + rect.Center.Y) / 2)
                    //{
                    //    angle = (double)255 * Math.PI / (double)180.0;
                    //    velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
                    //}
                    //if (rectangle.Center.Y >= (rect.Bottom + rect.Center.Y) / 2 && rectangle.Center.Y < rect.Bottom)
                    //{
                    //    angle = (double)225 * Math.PI / (double)180.0;
                    //    velocity = new Vector2(speedFactor * ((float)Math.Cos(angle)), speedFactor * ((float)Math.Sin(angle)));
                    //}
                }
                
                return true;
            }
            else
                return false;
        }
        public Boolean CheckVerticalHit(Rectangle rect)
        {
            if (rect.Intersects(rectangle))
            {
                // adjust velocity vector
                velocity.Y *= -1;
               
                return true;
            }
            else
                return false;
        }
        public Boolean Leftoutofbound(Rectangle rect)
        {
            if (position.X < 0)
                return true;
            else return false;
        }
        public Boolean Rightoutofbound(Rectangle rect)
        {
            if (position.X > Game.Window.ClientBounds.Width)
                return true;
            else return false;
        }
        
        
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rectangle.Location = new Point((int)(position.X - size / 2.0f), (int)(position.Y - size / 2.0f));
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            // draw the ball
            if (texture != null)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
                spriteBatch.Draw(texture, rectangle, Color.White);
                spriteBatch.End();
            }
        }
    }
}
