using System;
using System.Collections.Generic;
using System.Diagnostics;
using LionKing.Animals;
using LionKing.GameStateConfig;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LionKing.Game;

public class RunSimbaGame
{
    private SpriteBatch _spriteBatch;
    private GraphicsDevice _graphicsDevice;
    private GameState _gameState;
    private SpriteFont _font;
    public Simba Simba;
    public Scar Scar;
    public Pumbaa Pumbaa;
    public Mufasa Mufasa;
    private List<HuntersConfig> _hunters = new();
    public int y = 100;
    private readonly double _totalGameTimeSecond = 100;
    private Color _timerColor = Color.Black;
    private int _secondStageTime = 5;
    private int _finalStageTime = 10;

    public RunSimbaGame(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameState gameState, SpriteFont font)
    {
        _spriteBatch = spriteBatch;
        _graphicsDevice = graphicsDevice;
        _gameState = gameState;
        _font = font;
        Simba = new Simba(_graphicsDevice);
        Scar = new Scar(_graphicsDevice);
        Pumbaa = new Pumbaa(_graphicsDevice);
        Mufasa = new Mufasa(_graphicsDevice);

    }

    public virtual void Update(GameTime gameTime)
    {
        if (!_gameState.IsGameStart || _gameState.IsMenuOpen || _gameState.IsGameEnd)
        {
            return;
        }
        
        UpdateTimer(gameTime);
        var currentKeyboardState = Keyboard.GetState();
        if (currentKeyboardState.GetPressedKeyCount() > 0)
        {
            UpdateSimbaNewPosition(currentKeyboardState);
        }

        UpdateHunterDeployStatus();

        foreach (var hunter in _hunters)
        {
            if (hunter.IsDeploy)
            {
                UpdateHunterNewPosition(hunter.Hunter);
                UpdateCheckIsCollide();
            }
        }
    }

    public virtual void Draw()
    {
        _spriteBatch.DrawString(_font, $"Timer: {_gameState.TimerSeconds:F2}", new Vector2(0, 10), _timerColor);
        _spriteBatch.Draw(Simba.Sprite, Simba.Rectangle, Color.White);
        foreach (var hunter in _hunters)
        {
            if (hunter.IsDeploy)
            {
                _spriteBatch.Draw(hunter.Hunter.Sprite, hunter.Hunter.Rectangle, Color.White);
            }
        }
    
    }
    public void SetInitialCharacterPositionSize()
    {
        Simba.Sizing(_gameState.SimbaStartingPosX
            , _gameState.SimbaStartingPosY,
            100,
            100);
        Scar.Sizing(_gameState.HunterStartingPosX - 150
            , _gameState.HunterStartingPosY - 150
            , 150
            , 150);
        Pumbaa.Sizing(_gameState.HunterStartingPosX - 150
            , _gameState.HunterStartingPosY - 150
            , 150
            , 150);
        Mufasa.Sizing(_gameState.HunterStartingPosX - 150
            , _gameState.HunterStartingPosY - 150
            , 150
            , 150);

        _hunters.Add(new HuntersConfig
        {
            Hunter = Scar,
            IsDeploy = true,
        });
        _hunters.Add(new HuntersConfig
        {
            Hunter = Pumbaa,
            IsDeploy = false,
        });
        _hunters.Add(new HuntersConfig
        {
            Hunter = Mufasa, 
            IsDeploy = false
        });
    }

    public void UpdateTimer(GameTime gameTime)
    {
        _gameState.TimerSeconds = _gameState.PauseTime + (gameTime.TotalGameTime.TotalSeconds - _gameState.RecordedStartTime);
    }

    public void UpdateHunterDeployStatus()
    {

        var time = _gameState.TimerSeconds;

        if (_hunters.Count <= 1 || _hunters.Count != 3)
        {
            return;
        }

        if (time > _secondStageTime)
        {
            _hunters[1].IsDeploy = true;
        }  
        
        if (time > _finalStageTime)
        {
            _hunters[2].IsDeploy = true;
        }
    }

    public void UpdateSimbaNewPosition(KeyboardState keyboardState)
    {
        var currentSpeed = 10*SpeedMultiplier();
        var simbaCurrentPosX = Simba.Rectangle.X;
        var simbaCurrentPosY = Simba.Rectangle.Y;
        var simbaWidth = Simba.Rectangle.Width;
        var simbaHeight = Simba.Rectangle.Height;
        int simbaNewPosX;
        int simbaNewPosY;
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            simbaNewPosY = simbaCurrentPosY + currentSpeed;
            simbaNewPosX = simbaCurrentPosX;
            if (IsCharacterWithinBound(simbaNewPosX, simbaNewPosY, simbaWidth, simbaHeight))
                Simba.ChangePosition(simbaNewPosX,simbaNewPosY);
        } 
        
        if (keyboardState.IsKeyDown(Keys.Up))
        {
            simbaNewPosY = simbaCurrentPosY - currentSpeed;
            simbaNewPosX = simbaCurrentPosX;
            if (IsCharacterWithinBound(simbaNewPosX, simbaNewPosY, simbaWidth, simbaHeight))
                Simba.ChangePosition(simbaNewPosX,simbaNewPosY);
        } 
        
        if (keyboardState.IsKeyDown(Keys.Right))
        {
            simbaNewPosY = simbaCurrentPosY ;
            simbaNewPosX = simbaCurrentPosX + currentSpeed;
            if (IsCharacterWithinBound(simbaNewPosX, simbaNewPosY, simbaWidth, simbaHeight))
                Simba.ChangePosition(simbaNewPosX,simbaNewPosY);
        } 
        if (keyboardState.IsKeyDown(Keys.Left))
        {
            simbaNewPosY = simbaCurrentPosY ;
            simbaNewPosX = simbaCurrentPosX - currentSpeed;
            if (IsCharacterWithinBound(simbaNewPosX, simbaNewPosY, simbaWidth, simbaHeight))
                Simba.ChangePosition(simbaNewPosX,simbaNewPosY);
        }
        
    }

    public void UpdateHunterNewPosition(Character hunter)
    {
        var currentSpeed = 5 * SpeedMultiplier();
        var simbaCurrentPosX = Simba.Rectangle.X;
        var simbaCurrentPosY = Simba.Rectangle.Y;
        var hunterCurrentPosX = hunter.Rectangle.X;
        var hunterCurrentPosY = hunter.Rectangle.Y;
        var hunterWidth = hunter.Rectangle.Width;
        var hunterHeight = hunter.Rectangle.Height;


        var direction = Vector2.Normalize(new Vector2(simbaCurrentPosX - hunterCurrentPosX,
            simbaCurrentPosY - hunterCurrentPosY));

        var newPosX = hunterCurrentPosX + ((int)Math.Round(direction.X) * currentSpeed);
        var newPosY = hunterCurrentPosY + ((int)Math.Round(direction.Y) * currentSpeed);

        if (IsCharacterWithinBound(newPosX, newPosY, hunterWidth, hunterHeight))
            hunter.ChangePosition(newPosX, newPosY);
    }

    public int SpeedMultiplier()
    {
        switch (_gameState.TimerSeconds)
        {
            case var n when n > _finalStageTime:
                return 3;

            case var n when n > _secondStageTime:
                return 2;

            default:
                return 1;
        }
    }

    public bool IsCharacterWithinBound(int posX, int posY, int characterWidth, int characterHeight)
    {
        return posX >= _gameState.playgroundBoundaryX.LowerBound
               && posX <= _gameState.playgroundBoundaryX.UpperBound - characterWidth
               && posY >= _gameState.playgroundBoundaryY.LowerBound
               && posY <= _gameState.playgroundBoundaryY.UpperBound - characterHeight;
    }

    public void UpdateCheckIsCollide()
    {
        foreach (var hunter in _hunters)
        {
            var overlap = Rectangle.Intersect(Simba.Rectangle, hunter.Hunter.Rectangle);
            Debug.WriteLine(overlap.Width);
            Debug.WriteLine(overlap.Height);
            if (!overlap.IsEmpty && overlap.Width >= 100 && overlap.Height >= 100)
            {
                _gameState.IsGameEnd = true;
                _gameState.IsMenuOpen = true;
            }
        }

    }
}