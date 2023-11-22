using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;
using LionKing.Components.Buttons;
using LionKing.GameStateConfig;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LionKing.Components.Managers
{
    public class MenuButtonManager
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteFont _font;

        public StartButton StartButton;
        public ExitButton ExitButton;
        public MenuButton MenuButton;
        public ResumeButton ResumeButton;
        public RestartButton RestartButton;

        private readonly int _buttonBoxWidth = 250;
        private readonly int _buttonBoxHeight = 100;

        private GameState _gameState;
        public int GraphicBoxWidth;
        public int GraphicBoxHeight;

        private readonly Color _buttonIdleColor = Color.Gray;
        private readonly Color _buttonClickedColor = Color.SlateGray;
        private readonly Color _buttonHoverColor = Color.Green;
        
        public MenuButtonManager (SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, GameState gameState, SpriteFont font, int graphicBoxWidth, int graphicBoxHeight)
        {
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            _font = font;
            GraphicBoxHeight = graphicBoxHeight;
            GraphicBoxWidth = graphicBoxWidth;
            _gameState = gameState;
        }

        public void ConfigureButtons()
        {
            var centerX = GraphicBoxWidth / 2;
            var centerY = GraphicBoxHeight / 2;
            var startingY = centerY / 2;

            // Calculate the position for the button to be centered within the graphic box
            var buttonX = centerX - (_buttonBoxWidth / 2);
            var buttonStartingY = startingY - (_buttonBoxHeight / 2);
            var buttonExitY = buttonStartingY + _buttonBoxHeight + 50;

            var menuButtonX = GraphicBoxWidth - (_buttonBoxWidth)-50;
            var menuButtonY = 50;

            StartButton = new StartButton(_spriteBatch
                , _graphicsDevice
                , new Rectangle(buttonX, buttonStartingY, _buttonBoxWidth, _buttonBoxHeight)
                , _buttonIdleColor,
                _buttonClickedColor,
                _buttonHoverColor
                , _font
                , true);

            ExitButton = new ExitButton(_spriteBatch
                , _graphicsDevice
                , new Rectangle(buttonX, buttonExitY, _buttonBoxWidth, _buttonBoxHeight)
                , _buttonIdleColor,
                _buttonClickedColor,
                Color.Red
                , _font
                , true);

            MenuButton = new MenuButton(_spriteBatch
                , _graphicsDevice
                , new Rectangle(menuButtonX, menuButtonY, _buttonBoxWidth, _buttonBoxHeight)
                ,_buttonIdleColor
                ,_buttonClickedColor
                ,_buttonHoverColor
                ,_font
                , true);

            ResumeButton = new ResumeButton(_spriteBatch
                , _graphicsDevice
                , new Rectangle(buttonX, buttonStartingY, _buttonBoxWidth, _buttonBoxHeight)
                , _buttonIdleColor,
                _buttonClickedColor,
                _buttonHoverColor
                , _font
                , false);

            RestartButton = new RestartButton(_spriteBatch
                , _graphicsDevice
                , new Rectangle(buttonX, buttonStartingY, _buttonBoxWidth, _buttonBoxHeight)
                , _buttonIdleColor,
                _buttonClickedColor,
                _buttonHoverColor
                , _font
                , false);

        }

        public void Update(GameTime gameTime)
        {

            ExitButton.Update();
            MenuButton.Update();

            if (!_gameState.IsOverlapButtonClicked)
            {
                StartButton.Update();
                ResumeButton.Update();
                RestartButton.Update();
            }
            
            if (_gameState.IsGameEnd)
            {
                ResumeButton.IsVisible = false;
                RestartButton.IsVisible = true;
            }

            if (ResumeButton.IsClicked)
            {
                _gameState.IsMenuOpen = false;
                _gameState.RecordedStartTime = gameTime.TotalGameTime.TotalSeconds;
                _gameState.IsOverlapButtonClicked = true;
                ResumeButton.IsClicked = false;
            }

            if (MenuButton.IsClicked)
            { 
               _gameState.IsMenuOpen = true;
               _gameState.PauseTime = _gameState.TimerSeconds;
               _gameState.IsOverlapButtonClicked = false;
                MenuButton.IsClicked = false;
            }

            if (RestartButton.IsClicked)
            {
                _gameState.IsGameRestart = true;
                _gameState.IsGameStart = true;
                _gameState.IsMenuOpen = false;
                _gameState.IsGameEnd = false;
                _gameState.TimerSeconds = 0;
                _gameState.PauseTime = 0;
                _gameState.RecordedStartTime = gameTime.TotalGameTime.TotalSeconds;
                _gameState.IsOverlapButtonClicked = true;
                RestartButton.IsVisible = false;
            }

            if (StartButton.IsClicked)
            {
                _gameState.IsMenuOpen = false;
                _gameState.IsGameStart = true;
                _gameState.RecordedStartTime = gameTime.TotalGameTime.TotalSeconds;
                _gameState.IsOverlapButtonClicked = true;
                ResumeButton.IsVisible = true;
                StartButton.IsVisible = false;
                StartButton.IsClicked = false;
            }

        }

        public void Draw()
        {
            if (_gameState.IsGameEnd)
            {
                RestartButton.Draw();
                ExitButton.Draw();
                return;
            }

            if (_gameState.IsMenuOpen)
            {
                if (!_gameState.IsGameStart)
                {
                    StartButton.Draw();
                }
                else
                {
                    ResumeButton.Draw();
                }

                ExitButton.Draw();
            }
            else
            {
                MenuButton.Draw();
            }
        }
    }
}
