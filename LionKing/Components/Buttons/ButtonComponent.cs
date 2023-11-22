using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LionKing.Components.Buttons;

public abstract class ButtonComponent
{
    private SpriteFont _font;
    private bool _isHovering;
    public bool IsClicked;
    private Rectangle _rectangle;
    private Color _idleColor;
    private Color _clickedColor;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;
    private GraphicsDevice _graphicsDevice;
    private Color buttonColor;
    private string _buttonText;
    private Color _hoverColor;
    public bool IsVisible;

    protected ButtonComponent(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Rectangle rectangle, Color idleColor, Color clickedColor, Color hoverColor, SpriteFont font, bool isVisible, string text)
    {
        _rectangle = rectangle;
        _idleColor = idleColor;
        _hoverColor = hoverColor;
        _clickedColor = clickedColor;
        _spriteBatch = spriteBatch;
        _graphicsDevice = graphicsDevice;
        _font = font;
        buttonColor = _idleColor;
        _buttonText = text;
        IsVisible = isVisible;
    }

    public virtual void Draw()
    {
        _pixel = new Texture2D(_graphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
        var measureTextSize = _font.MeasureString(_buttonText);
        var textX = _rectangle.X + (_rectangle.Width - measureTextSize.X) / 2;
        var textY = _rectangle.Y + (_rectangle.Height - measureTextSize.Y) / 2;

        _spriteBatch.Draw(_pixel, _rectangle, buttonColor);
        _spriteBatch.DrawString(_font, _buttonText, new Vector2(textX, textY), Color.Black);
    }

    public virtual void Update()
    {
        if (!IsVisible)
        {
            return;
        }

        var mouseState = Mouse.GetState();
        var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

        _isHovering = mouseRectangle.Intersects(_rectangle);
        buttonColor = _isHovering ? _hoverColor : _idleColor;
        IsClicked = _isHovering && mouseState.LeftButton == ButtonState.Pressed;

    }
}