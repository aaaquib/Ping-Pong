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
    public class Walls : Microsoft.Xna.Framework.GameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;
        public Rectangle rectangle;
        Vector2 position;
        public Rectangle field;
        //const int size = 50;

        public Walls(Game game,float x,float y,int size)
            : base(game)
        {
            rectangle = new Rectangle(0, 0, game.Window.ClientBounds.Width, size);
            field = Game.Window.ClientBounds;
            field.Location = new Point(0, 0);

            setPosition(x,y);
            // TODO: Construct any child components here
        }
        public void setPosition(float x,float y)
        {
                position.X = x;
                position.Y = y;
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
            texture = Game.Content.Load<Texture2D>("wall");

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
            // TODO: Add your update code here
            rectangle.Location = new Point((int)(position.X), (int)(position.Y));
            base.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            // draw the wall
            if (texture != null)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                spriteBatch.Draw(texture, rectangle, Color.White);

                spriteBatch.End();
            }
        }
    }
}
