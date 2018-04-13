using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RADMonoGame.GameObjects
{
    class PlayerSprite : DrawableGameComponent
    {
        public Texture2D Image;
        public Point Position;
        public Rectangle BoundingRect;
        public bool Visible = true;
        public int speed = 5;
        public Point previousPosition;
        public Color tint = Color.White;

        public PlayerSprite(Game game, Texture2D spriteImage, Point startPosition) : base(game)
        {
            game.Components.Add(this);
            Image = spriteImage;
            previousPosition = Position = startPosition;
            BoundingRect = new Rectangle(
                Position.X,
                Position.Y,
                Image.Width,
                Image.Height);
        }

        public override void Update(GameTime gameTime)
        {
            previousPosition = Position;

            if(Keyboard.GetState().IsKeyDown(Keys.W))
                Position += new Point(0, -speed);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Position += new Point(0, speed);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Position += new Point(-speed, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Position += new Point(speed, 0);

            BoundingRect = new Rectangle(
                Position.X,
                Position.Y,
                Image.Width,
                Image.Height);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sp = new SpriteBatch(this.GraphicsDevice);
            if (sp == null) return;
            if (Image != null && Visible)
            {
                sp.Begin();
                sp.Draw(Image, BoundingRect, tint);
                sp.End();
            }
            base.Draw(gameTime);
        }
    }
}
