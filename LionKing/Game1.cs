using System.Diagnostics;
using System.Xml.Schema;
using LionKing.Components.Managers;
using LionKing.Game;
using LionKing.GameStateConfig;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LionKing;

using System;
using Animals;
using LionKing.Components.Buttons;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont font;

    private const double FrameRate = 60;

    private RunSimbaGame _runSimbaGame {get; set; }

    private MenuButtonManager menuButtonManager { get; set; }

    private GameState gameState { get; set; }

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
        _graphics.PreferredBackBufferHeight = 1000;
        _graphics.ApplyChanges();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        font = Content.Load<SpriteFont>("ButtonFont");

        gameState = new GameState
        {
            SimbaStartingPosX = 0,
            SimbaStartingPosY = 150,
            HunterStartingPosX = _graphics.PreferredBackBufferWidth,
            HunterStartingPosY = _graphics.PreferredBackBufferHeight,
            playgroundBoundaryX = new BoundaryConfig
            {
                LowerBound = 0,
                UpperBound = _graphics.PreferredBackBufferWidth,
            }  ,
            playgroundBoundaryY = new BoundaryConfig
            {
                LowerBound = 150,
                UpperBound = _graphics.PreferredBackBufferHeight,
            }

        };
        _runSimbaGame = new RunSimbaGame(_spriteBatch, GraphicsDevice, gameState, font);
        _runSimbaGame.SetInitialCharacterPositionSize();
        menuButtonManager = new MenuButtonManager(_spriteBatch, GraphicsDevice, gameState, font,
            _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        menuButtonManager.ConfigureButtons();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape) || menuButtonManager.ExitButton.IsClicked)
            Exit();

        if (gameState.IsGameRestart && gameState.IsGameEnd)
        {
            LoadContent();
        }

        _runSimbaGame.Update(gameTime);
        menuButtonManager.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();
        _runSimbaGame.Draw();
        menuButtonManager.Draw();
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
