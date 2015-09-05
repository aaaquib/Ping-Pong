using System;

namespace Pong
{
#if WINDOWS || XBOX
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Pong game = new Pong())
            {
                game.Run();
                
            }
            
        }
    }
#endif
}

/*The Model-View-Controller is a type of computer interface or design paradigm in which the Model, View and Controller are separated.
The Model consists of application data and business rules
The Controller handles inputs and performs actions on the data and the view
The View represents data to the outside world.
The XNA framework itself is based on the MVC paradigm.

How the code for this Pong game fits into the MVC architectural pattern: 
Each game component (including the main game itself) consists of the following methods:
1) A constructor for each game component
2) Initialize()
3) LoadContent()
4) UnloadContent()
5) Update()
6) Draw()

The following methods fall under the "Model" portion of the MVC architecture.
(1) Initialize() - Preparing each component by creating objects of each class or initializing variables
(2) LoadContent() - Loading sprites, sounds or fonts onto variables as well as loading other things into the memory
(3) UnloadContent() - Frees memory after closing the game. Mostly done automatically.
(4) Constructors are called each time a game object is created.
These 4 types of functions represent the aplication data and business rules of the game. For example, the game objects like ball,paddle and wall are loaded into memory 
in the LoadContent() method and their properties like size and initial speed of the ball etc. are defined in the constructors and Initialize() method.

The following method falls under the "Controller" portion of the MVC architecture.
Update()
Here, the Update() function contains the game logic and is responsible for receiving user input and manipulating Data and/or View accordingly. For example, the logic
that governs the movement of the paddle is defined in the Update() method of the Paddle class. Another example would be the collision detection logic 
between the ball,paddles and walls.

The following method falls under the "View" portion of the MVC architecture.
The Draw() method represents the "View" portion of the MVC architecture. It contains the code that actually draws the paddle, ball, background
etc onto the screen depending on the state of the Model.

The MVC architecture is an extremely useful paradigm for the design and development of games.
By keeping the Draw() and Update() method separate, we can change the View without affecting the game logic. Also, keeping the Model separate allows us to use
object-oriented principles in game design.
By keeping the Model,View and Controller separate, a programmer can edit different portions quickly and easily. For example a programmer can change the texture
of the ball by simply loading a different sprite in the LoadContent() method of the ball class. The programmer doesn't have to scan the entire code just to locate the snippet that
specifies the ball's sprite image. 

Sources: 
http://ubergamestudios.com/wordpress/archives/9
http://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller
*/