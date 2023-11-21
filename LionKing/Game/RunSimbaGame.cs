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
    public int y = 100;
    private readonly double _totalGameTimeSecond = 100;
    private Color _timerColor = Color.Black;

    public RunSimbaGame(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameState gameState, SpriteFont font)
    {
        _spriteBatch = spriteBatch;
        _graphicsDevice = graphicsDevice;
        _gameState = gameState;
        _font = font;
        Simba = new Simba(_graphicsDevice);
        Scar = new Scar(_graphicsDevice);

    }

    public virtual void Update(GameTime gameTime)
    {
        var currentKeyboardState = Keyboard.GetState();
        if (currentKeyboardState.GetPressedKeyCount() > 0)
        {
            SimbaNewPosition(currentKeyboardState);
        }

        //if (currentKeyboardState.IsKeyDown(Keys.Down))
        //{
        //    y -= 100;
        //}


        //if (currentKeyboardState.IsKeyDown(Keys.Up))
        //{
        //    y += 100;
        //}


        //Simba.Position = new Vector2(0, y);
        //Scar.Position = new Vector2(0, y + 300);
    }

    public virtual void Draw()
    {
        _spriteBatch.DrawString(_font, $"Timer: {_gameState.TimerSeconds:F2}", new Vector2(0, 10), _timerColor);
        _spriteBatch.Draw(Simba.Sprite, Simba.Rectangle, Color.White);
        _spriteBatch.Draw(Scar.Sprite, Scar.Rectangle, Color.White);
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
    }



    

    public void SimbaNewPosition(KeyboardState keyboardState)
    {
        var currentSpeed = 10;
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

    public bool IsCharacterWithinBound(int posX, int posY, int characterWidth, int characterHeight)
    {
        return posX >= _gameState.playgroundBoundaryX.LowerBound
               && posX <= _gameState.playgroundBoundaryX.UpperBound - characterWidth
               && posY >= _gameState.playgroundBoundaryY.LowerBound
               && posY <= _gameState.playgroundBoundaryY.UpperBound - characterHeight;
    }
}