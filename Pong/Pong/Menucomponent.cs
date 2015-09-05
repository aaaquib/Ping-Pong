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
    public class Menucomponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string[] menuItems;
        
        int selectedIndex;
        public bool visible;
        public bool pause;
	 
	        Color normal = Color.White;
	        Color hilite = Color.Yellow;
	 
	        KeyboardState keyboardState;
	        KeyboardState oldKeyboardState;
            GamePadState gamePadState1;
            GamePadState gamePadState2;

	        SpriteBatch spriteBatch;
	        SpriteFont spriteFont;
          
            Rectangle rectangle;
	        Vector2 position;
	        float width = 0f;
	        float height = 0f;
            
	        public int SelectedIndex
          {
	            get { return selectedIndex; }
	            set
	            {
	                selectedIndex = value;
	                if (selectedIndex < 0)
	                    selectedIndex = 0;
	                if (selectedIndex >= menuItems.Length)
	                    selectedIndex = menuItems.Length - 1;
	            }
	        }
      public Menucomponent(Game game,SpriteBatch spriteBatch,
	            SpriteFont spriteFont,
	            string[] menuItems)
            : base(game)
        {
            // TODO: Construct any child components here
                
                this.spriteBatch = spriteBatch;
	            this.spriteFont = spriteFont;
                this.menuItems = menuItems;
	            MeasureMenu();
                rectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
                
        }

        private void MeasureMenu()
	        {
	            height = 0;
	            width = 0;
	            foreach (string item in menuItems)
	            {
	                Vector2 size = spriteFont.MeasureString(item);
	                if (size.X > width)
	                    width = size.X;
	                height += spriteFont.LineSpacing + 5;
	            }
	 
	            position = new Vector2(
	                (Game.Window.ClientBounds.Width - width) / 2,
	                (Game.Window.ClientBounds.Height - height) / 2);
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

         private bool CheckKey(Keys theKey)
	        {
	            return keyboardState.IsKeyUp(theKey) &&
	                oldKeyboardState.IsKeyDown(theKey);
	        }
         public int selectedindex()
         {
             return selectedIndex;
         }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            keyboardState = Keyboard.GetState();
            gamePadState1 = GamePad.GetState(PlayerIndex.One);
            gamePadState2 = GamePad.GetState(PlayerIndex.Two);

            if (CheckKey(Keys.Down) || gamePadState1.ThumbSticks.Left.Y < 0 || gamePadState2.ThumbSticks.Left.Y < 0)
	            {
	                selectedIndex++;
	                if (selectedIndex == menuItems.Length)
	                    selectedIndex = 0;
	            }
            if (CheckKey(Keys.Up) || gamePadState1.ThumbSticks.Left.Y > 0 || gamePadState2.ThumbSticks.Left.Y > 0)
	            {
	                selectedIndex--;
               if (selectedIndex < 0)
	                    selectedIndex = menuItems.Length - 1;
	            }
                
            base.Update(gameTime);
            
            oldKeyboardState = keyboardState;

        }

       
        public override void Draw(GameTime gameTime)
	        {
	            base.Draw(gameTime);
                
                if (visible == true)
                {

                    Vector2 location = position;
                    Color tint;
                    
                    for (int i = 0; i <4; i++)
                    {
                        if (i == selectedIndex)
                            tint = hilite;
                        else
                            tint = normal;

                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                        spriteBatch.DrawString(
                            spriteFont,
                            menuItems[i],
                            location,
                            tint);
                        spriteBatch.End();
                        location.Y += spriteFont.LineSpacing + 5;
                    }
                   /* spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    spriteBatch.DrawString(
                            spriteFont,
                            extraText[0],
                            location,
                            Color.White);
                        spriteBatch.End();*/
                }
	        }
    }
}
