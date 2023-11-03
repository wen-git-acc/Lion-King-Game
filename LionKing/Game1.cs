using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LionKing;

using System;
using Animals;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private const double FrameRate = 60;

    private Simba Simba { get; set; }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / FrameRate);
        
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: use this.Content to load your game content here
        
        Simba = new Simba(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        var xDiff = (int)Math.Min(100 * (gameTime.TotalGameTime.TotalSeconds + 1), 800);
        Simba.Position = new Vector2(xDiff, 0);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(Simba.Sprite, Simba.Position, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
