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
    public class Pong : Microsoft.Xna.Framework.Game
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Walls twall; 
        Walls bwall; 
        Ball ball;
        Paddle paddle; double p1 = 18;
        Paddle paddle2; double p2 = 1.01;
        SoundEffect hitsound;
        SoundEffect wallhitsound;
        SoundEffect whistle;
       
        private bool paused = false;
        private bool pauseKeyDown = false;
        int wallsize=50;
        
        IntroScreen intro_screen;
        Instructions instr_screen;
       
        Menucomponent menu;
        Menucomponent diffmenu;
        Menucomponent pauseScreen;

        Song bgmusic;
       
        Scorecard score;
       
        Texture2D background;
        Texture2D menu_background;
        Vector2 bgpos;
        GamePadState gamePadState;
        GamePadState gamePadState2;
        KeyboardState keyboardState;
        KeyboardState oldkeyboardState;
        public bool gameover;
        string[] difficulty = new string[1];
        string[] extraText = new string[1];
        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            twall = new Walls(this, 0, 0,wallsize);
            bwall = new Walls(this, 0, (this.Window.ClientBounds.Height-170),wallsize);

            paddle = new Paddle(this, p1);
            paddle2 = new Paddle(this, p2);
            ball = new Ball(this);
            score = new Scorecard(this);
            
            intro_screen = new IntroScreen(this);
            instr_screen = new Instructions(this);
            bgpos = new Vector2(0, 0);
            
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        protected override void Initialize()
        {
            // initialization logic
            gameover = true;
            
            intro_screen.Initialize();
            instr_screen.Initialize();
            ball.Initialize();
            paddle.Initialize();
            paddle2.Initialize();
            twall.Initialize();
            bwall.Initialize();
            score.Initialize();
            
           
            base.Initialize();
        }

        /// LoadContent will be called once per game and is the place to load
        /// all of your content.

        protected override void LoadContent()
        {
	        spriteBatch = new SpriteBatch(GraphicsDevice);
            // use this.Content to load your game content here
            
            string[] menuItems = { "New Game", "Difficulty", "Instructions", "Exit to Desktop" };    //Main Menu items
            string[] difflevel={"Easy","Medium","Hard","Insane"};                                       //Difficulty menu items
            string[] pausemenuItems = { "Resume Game", "Difficulty", "Instructions", "Exit to Main Menu" }; //Pause menu Items
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menu = new Menucomponent(this, spriteBatch, Content.Load<SpriteFont>("menufont"), menuItems);  //Initialize Menu
            diffmenu = new Menucomponent(this, spriteBatch, Content.Load<SpriteFont>("menufont"), difflevel); //Initialize Difficulty Menu
            pauseScreen = new Menucomponent(this, spriteBatch, Content.Load<SpriteFont>("menufont"), pausemenuItems); //Initialize Pause Menu

            Components.Add(menu);
            Components.Add(diffmenu);
            Components.Add(pauseScreen);
            menu.visible = false;
            diffmenu.visible=false;
            pauseScreen.visible = false;

            intro_screen.LoadContent();
            instr_screen.LoadContent();
            background = Content.Load<Texture2D>("background");        // Game Background
            menu_background = Content.Load<Texture2D>("menuscreen");   // Meny Background
            score.LoadContent();
            
            ball.LoadContent();
            paddle.LoadContent();
            paddle2.LoadContent();
            twall.LoadContent();
            bwall.LoadContent();
            hitsound = Content.Load<SoundEffect>("paddlehit");
            wallhitsound = Content.Load<SoundEffect>("wallhit");
            whistle = Content.Load<SoundEffect>("whistle");  // Sound when player wins/loses
            
            bgmusic = Content.Load<Song>("music");
            MediaPlayer.Play(bgmusic);
            MediaPlayer.IsRepeating = true;

            
        }

        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        
        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
        }

        private void BeginPause(bool UserInitiated)
        {
            paused = true;
            
            ball.pauseball(true);
            pauseScreen.visible = true;
            //MediaPlayer.Pause();    //Pause audio
        }

        private void EndPause()
        {
            paused = false;
            //MediaPlayer.Resume();   //Resume audio
            ball.pauseball(false);
            pauseScreen.visible = false;
        }

        private void checkPauseKey(KeyboardState keyboardState, GamePadState gamePadState)
        {
            bool pauseKeyDownThisFrame = (keyboardState.IsKeyDown(Keys.Escape) ||
                (gamePadState.Buttons.Start == ButtonState.Pressed));
            // If key was not down before, but is down now, we toggle the
            // pause setting
            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (!paused)
                    BeginPause(true);
                else
                    EndPause();
            }
            pauseKeyDown = pauseKeyDownThisFrame;
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
            oldkeyboardState.IsKeyDown(theKey);
        }

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);
            gamePadState2 = GamePad.GetState(PlayerIndex.Two);
            keyboardState = Keyboard.GetState();
            extraText[0] = "Difficulty set to: " + difficulty;
           
            // If the user hasn't paused, Update normally
            if (!paused) // If game is in unpaused state
            {
                //If Menu is visible
                if (menu.visible == true && intro_screen.visible == false && instr_screen.visible == false && diffmenu.visible == false)
                {
                    if (CheckKey(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState2.Buttons.A == ButtonState.Pressed)
                    {
                        int menuindex = menu.SelectedIndex;
                        if (menuindex == 0)                 //Start Game
                        {
                            ball.ResetPosition(false);
                            menu.visible = false;
                            gameover = false;
                        }
                        if (menuindex == 1)                 //Select difficulty level screen
                        {
                            menu.visible = false;
                            diffmenu.visible = true;
                        }

                        if (menuindex == 2)                 //View Instructions
                        {
                            instr_screen.visible = true;    
                            menu.visible = false;
                        }
                        if (menuindex == 3)                 //Exit Game
                        {
                            this.Exit();
                        }
                    }
                    
                    if (CheckKey(Keys.Escape) || gamePadState.Buttons.Y == ButtonState.Pressed || gamePadState2.Buttons.Y == ButtonState.Pressed) //Go back to Intro Screen
                    {
                        intro_screen.visible = true;
                        menu.visible = false;
                    }
                }
                //If difficulty level menu has been selected
                if (diffmenu.visible == true && intro_screen.visible == false && menu.visible == false && instr_screen.visible == false)
                {
                    if ((CheckKey(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState2.Buttons.A == ButtonState.Pressed))
                    {
                        
                        int diffindex = diffmenu.SelectedIndex;
                        if (diffindex == 0)                 //Easy
                        {
                            ball.initialSpeedFactor = 300;
                            difficulty[0] = "Easy";
                        }
                        if (diffindex == 1)                 //Medium
                        {
                            ball.initialSpeedFactor = 450;
                            difficulty[0] = "Medium";
                        }
                        if (diffindex == 2)                 //Hard
                        {
                            ball.initialSpeedFactor = 600;
                            difficulty[0] = "Hard";
                        }
                        if (diffindex == 3)                 //Insane
                        {
                            ball.initialSpeedFactor = 800;
                            difficulty[0] = "Insane";
                        }
                    }
                    //Go back to Menu
                    if (CheckKey(Keys.Escape) || gamePadState.Buttons.Y == ButtonState.Pressed || gamePadState2.Buttons.Y == ButtonState.Pressed)
                    {
                        diffmenu.visible = false;
                        menu.visible = true;
                    }
                }
                //If Start Game has been Selected
                if (intro_screen.visible == false && menu.visible == false && instr_screen.visible == false && diffmenu.visible == false)
                {
                    //Enter to reset ball at the center(restart turn)
                    if (CheckKey(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState2.Buttons.A == ButtonState.Pressed) 
                    {
                        ball.ResetPosition(true);
                        gameover = false;
                        score.startTimer = true;
                    }
                    //Escape to Pause Game
                    if (CheckKey(Keys.Escape) || gamePadState.Buttons.Start == ButtonState.Pressed || gamePadState2.Buttons.Start == ButtonState.Pressed)
                    {
                        BeginPause(true);
                    }
                    if (CheckKey(Keys.Space) || gamePadState.Buttons.Back == ButtonState.Pressed || gamePadState2.Buttons.Back == ButtonState.Pressed)   //Space to quick-restart Game
                    {
                        score.Reset();
                        ball.ResetPosition(false);
                        score.startTimer = false;
                        score.win2 = score.win1 = false;
                        gameover = true;
                    }
                }
                //If instruction screen is visible
                if (instr_screen.visible == true && menu.visible == false && intro_screen.visible == false && diffmenu.visible == false)
                {
                    if (CheckKey(Keys.Escape) || gamePadState.Buttons.Y == ButtonState.Pressed || gamePadState2.Buttons.Y == ButtonState.Pressed) // Escape to go back to menu
                    {
                        instr_screen.visible = false;
                        menu.visible = true;
                    }
                }
                //If intro Screen is visible
                if (intro_screen.visible == true && diffmenu.visible == false && instr_screen.visible == false && menu.visible == false)
                {
                    if (CheckKey(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState2.Buttons.A == ButtonState.Pressed) //Enter to go to menu
                    {
                        intro_screen.visible = false;
                        menu.visible = true;
                    }
                }
                // Check user input to update paddles
                if (Keyboard.GetState().IsKeyDown(Keys.W) || gamePadState.ThumbSticks.Left.Y > 0)
                {
                    paddle.MoveUp(gameTime);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S) || gamePadState.ThumbSticks.Left.Y < 0)
                {
                    paddle.MoveDown(gameTime);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || gamePadState2.ThumbSticks.Left.Y > 0)
                {
                    paddle2.MoveUp(gameTime);
                }
                // Check user input to update paddles

                if (Keyboard.GetState().IsKeyDown(Keys.Down) || gamePadState2.ThumbSticks.Left.Y < 0)
                {
                    paddle2.MoveDown(gameTime);
                }
                //Check Paddle-Ball Collision
                if (ball.CheckHorizontalHit(paddle.rectangle))
                    hitsound.Play();
                if (ball.CheckHorizontalHit(paddle2.rectangle))
                    hitsound.Play();
                if (ball.CheckVerticalHit(twall.rectangle))
                    wallhitsound.Play();
                if (ball.CheckVerticalHit(bwall.rectangle))
                    wallhitsound.Play();
                //Check Ball-Wall Collision
                paddle.Checktopwallhit(twall.rectangle);
                paddle2.Checktopwallhit(twall.rectangle);
                paddle.Checkbottomwallhit(bwall.rectangle);
                paddle2.Checkbottomwallhit(bwall.rectangle);
                //If Player 1 misses the ball
                if (ball.Leftoutofbound(paddle.rectangle))
                {
                    if (gameover == true)
                        ball.ResetPosition(false);
                    else
                    {
                        score.score2 += 1;
                        whistle.Play();
                        ball.ResetPosition(false);
                    }
                }
                //If Player 2 misses the ball
                if (ball.Rightoutofbound(paddle2.rectangle))
                {
                    if (gameover == true)
                        ball.ResetPosition(false);
                    else
                    {
                        score.score1 += 1;
                        whistle.Play();
                        ball.ResetPosition(false);
                    }
                }
                //If player 1 wins
                if (score.score1 == 5)
                {
                    ball.ResetPosition(false);
                    
                    gameover = true;
                    score.win1 = true;
                    score.win2 = false;

                    score.startTimer = false;
                }
                //If player 2 wins
                if (score.score2 == 5)
                {
                    ball.ResetPosition(false);

                    score.win2 = true;
                    score.win1 = false;

                    score.startTimer = false;
                }

                intro_screen.Update(gameTime);
                paddle.Update(gameTime);
                paddle2.Update(gameTime);
                ball.Update(gameTime);
                twall.Update(gameTime);
                bwall.Update(gameTime);
                score.Update(gameTime);
                menu.Update(gameTime);
                diffmenu.Update(gameTime);
                
                // TODO: Add your update logic here

                base.Update(gameTime);
                oldkeyboardState = keyboardState;
            }
            if (paused) //If game is in pause state
            {
                if (pauseScreen.visible == true)
                {
                    score.win1 = score.win2 = false;
                    if (CheckKey(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState2.Buttons.A == ButtonState.Pressed)
                    {
                        int index = pauseScreen.SelectedIndex;
                        if (index == 0)     //Resume Game
                        {
                            EndPause();
                            pauseScreen.visible = false;
                        }
                        if (index == 1)     //Change difficulty level
                        {
                            diffmenu.visible = true;
                            pauseScreen.visible = false;
                        }
                        if (index == 2)     //View Instructions
                        {
                            instr_screen.visible = true;
                            pauseScreen.visible = false;
                        }
                        if (index == 3)     //End Game and go to main menu
                        {
                            score.Reset();
                            score.win1 = score.win2 = false;
                            ball.ResetPosition(false);
                            score.startTimer = false;
                            gameover = true;
                            EndPause();
                            pauseScreen.visible = false;
                            menu.visible = true;
                        }
                    }
                    if (CheckKey(Keys.Escape) || gamePadState.Buttons.Y == ButtonState.Pressed || gamePadState2.Buttons.Y == ButtonState.Pressed)  //Unpause
                    {
                        EndPause();
                        pauseScreen.visible = false;
                    }
                }
                //If difficulty select menu has been select, choose difficulty level
                if (diffmenu.visible == true)
                {
                    if (CheckKey(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed || gamePadState2.Buttons.A == ButtonState.Pressed)
                    {
                        int diffindex = diffmenu.SelectedIndex;
                        if (diffindex == 0)
                        {
                            ball.initialSpeedFactor = 300;

                        }
                        if (diffindex == 1)
                        {
                            ball.initialSpeedFactor = 450;

                        }
                        if (diffindex == 2)
                        {
                            ball.initialSpeedFactor = 600;

                        }
                        if (diffindex == 3)
                        {
                            ball.initialSpeedFactor = 800;

                        }
                    }
                    if (CheckKey(Keys.Escape) || gamePadState.Buttons.Y == ButtonState.Pressed || gamePadState2.Buttons.Y == ButtonState.Pressed) //Go back to pause menu
                    {
                        diffmenu.visible = false;
                        pauseScreen.visible = true;
                    }

                }
                if (instr_screen.visible == true)
                {
                    if (CheckKey(Keys.Escape) || gamePadState.Buttons.Y == ButtonState.Pressed || gamePadState2.Buttons.Y == ButtonState.Pressed) // Go back to pause screen
                    {
                        instr_screen.visible = false;
                        pauseScreen.visible = true;
                    }
                }

            }
            pauseScreen.Update(gameTime);
            base.Update(gameTime);
            oldkeyboardState = keyboardState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.RosyBrown);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spriteBatch.Draw(background, bgpos, Color.White);

            spriteBatch.End();
            // TODO: Add your drawing code here
            if (intro_screen.visible == false && menu.visible == false && instr_screen.visible == false && diffmenu.visible == false)
            {

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                twall.Draw(gameTime);
                bwall.Draw(gameTime);
                paddle.Draw(gameTime);
                paddle2.Draw(gameTime);
                ball.Draw(gameTime);
                score.Draw(gameTime);


                spriteBatch.End();
            }
            if (intro_screen.visible == true)   
            {
                intro_screen.Draw(gameTime);        //Draw intro screen
            }
            if (menu.visible == true)       
            {
                extraText[0] = " ";
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                spriteBatch.Draw(menu_background, bgpos, Color.White);                   //Draw Background
                spriteBatch.End();
                menu.Draw(gameTime);                //Draw menu screen
            }
            if (instr_screen.visible == true)
            {
                instr_screen.Draw(gameTime);        //Draw instruction screen
            }
            if (diffmenu.visible == true)
            {
                
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                spriteBatch.Draw(menu_background, bgpos, Color.White);                   //Draw Background
                spriteBatch.End();
                diffmenu.Draw(gameTime);            //Draw Difficulty select menu
            }
            if (pauseScreen.visible == true) 
            {
                extraText[0] = " ";
                pauseScreen.Draw(gameTime);         //Draw Pause Menu
            }
            base.Draw(gameTime);
        }
    }
}
