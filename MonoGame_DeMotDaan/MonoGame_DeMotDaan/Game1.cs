using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_DeMotDaan.Models;
using System.Collections.Generic;


namespace MonoGame_DeMotDaan
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<Sprite> _sprites;
        private Texture2D texture;
        
        

        public Game1()  
        {

         
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var animations = new Dictionary<string, Animation>()
            {
                //Sprite naam , 10 is als frame
                {"idleLeft", new Animation(Content.Load<Texture2D>("Player/IdleLeft"),10 )},
                {"idle", new Animation(Content.Load<Texture2D>("Player/Idle"),10 )},
                {"jump", new Animation(Content.Load<Texture2D>("Player/jump"),10) },
                {"WalkRight", new Animation(Content.Load<Texture2D>("Player/walkRight"),10) },
                {"WalkLeft", new Animation(Content.Load<Texture2D>("Player/walkLeft"),10) },
                {"Crouch", new Animation(Content.Load<Texture2D>("Player/Idle"),10) },
            }
            ;
            _sprites = new List<Sprite>()
            {
                new Sprite(animations)
                {
                    Position = new Vector2(20,350),
                    Input = new Input()
                    {
                        Up= Keys.Z,
                        Down = Keys.S,
                        Left = Keys.Q,
                        Right= Keys.D,
                    },
                }
            };
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach(var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //achtergrond kleur?
            GraphicsDevice.Clear(Color.White);

            //texture tekenen via spritebatch
            spriteBatch.Begin();
            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
