using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LionKing.Components.Buttons;

public class MenuButton : Component
{
    private MouseState _currentMouseState;
    private SpriteFont _font;
    private bool _isHovering;
    private MouseState _previousMouseState;
    public bool IsClicked = false;
    public bool WasClicked = false;
    private Rectangle _rectangle;
    private Color _idleColor;
    private Color _clickedColor;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;
    private GraphicsDevice _graphicsDevice;
    private Color buttonColor;
    private string _buttonText = "Menu";
    private Color _hoverColor;


    public MenuButton(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Rectangle rectangle, Color idleColor, Color clickedColor, Color hoverColor, SpriteFont font)
    {
        _rectangle = rectangle;
        _idleColor = idleColor;
        _hoverColor = hoverColor;
        _clickedColor = clickedColor;
        _spriteBatch = spriteBatch;
        _graphicsDevice = graphicsDevice;
        _font = font;
        buttonColor = _idleColor;


    }
    public override void Draw()
    {

        _pixel = new Texture2D(_graphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
        var measureTextSize = _font.MeasureString(_buttonText);
        var textX = _rectangle.X + (_rectangle.Width - measureTextSize.X) / 2;
        var textY = _rectangle.Y + (_rectangle.Height - measureTextSize.Y) / 2;

        _spriteBatch.Draw(_pixel, _rectangle, buttonColor);
        _spriteBatch.DrawString(_font, _buttonText, new Vector2(textX, textY), Color.Black);
    }

    public override void Update()
    {
        var mouseState = Mouse.GetState();
        var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);


        _isHovering = mouseRectangle.Intersects(_rectangle);
        buttonColor = _isHovering ? _hoverColor : _idleColor;
        if (_isHovering && mouseState.LeftButton == ButtonState.Pressed)
        {
            IsClicked = true;
        }
    

    }
}