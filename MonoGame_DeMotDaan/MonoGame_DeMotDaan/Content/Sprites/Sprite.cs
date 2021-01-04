using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_DeMotDaan.Models;

namespace MonoGame_DeMotDaan
{
    class Sprite
    {
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Texture2D _texture;
        protected Vector2 _position;
        public bool hasJumped;
        


        public Input Input;
        public Vector2 Position
        {
            get { return _position; }
            set {
                _position = value;
                if (_animationManager != null)
                {
                    _animationManager.Position = _position;
                }
            }
        }
        public float speed = 2.5f;
        public Vector2 Velocity;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, Position, Color.White);
            }
            else if (_animationManager != null)
            {
                _animationManager.Draw(spriteBatch);
            }
            else throw new Exception("Error, geen texture en animation");
            
        }

        bool lookedLeft = false;

        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up)) 
                //grav?
                Velocity.Y = -speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Left)) { 
                Velocity.X = -speed;
                lookedLeft = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right)) { 
                Velocity.X = +speed;
                lookedLeft = false;
            }
        }

        protected virtual void SetAnimation()
        {
            if (Velocity.X > 0)
            {
                _animationManager.Play(_animations["WalkRight"]);
            }
            else if (Velocity.X < 0)
            {
                _animationManager.Play(_animations["WalkLeft"]);
                lookedLeft = true;
            }
            else if (Velocity.Y > 0)
            {
                _animationManager.Play(_animations["jump"]);
            }
            else if (Velocity.Y < 0)
            {

                _animationManager.Play(_animations["jump"]);
            }
            else if (Velocity.X == 0 && lookedLeft == true)
            {
                _animationManager.Play(_animations["idleLeft"]);
            }
            else if (Velocity.X == 0 && lookedLeft == false)
            {
                _animationManager.Play(_animations["idle"]);
            }

        }

        

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            SetAnimation();
            _animationManager.Update(gameTime); 


            Position += Velocity;
            Velocity = Vector2.Zero;
        }

    }
}
