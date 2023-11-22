using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LionKing.Components.Buttons;

public class StartButton : ButtonComponent
{
    private const string buttonText = "Start";

    public StartButton(SpriteBatch spriteBatch
        , GraphicsDevice graphicsDevice
        , Rectangle rectangle
        , Color idleColor
        , Color clickedColor
        , Color hoverColor
        , SpriteFont font
        , bool isVisible) : base(spriteBatch
        , graphicsDevice
        , rectangle
        , idleColor
        , clickedColor
        , hoverColor
        , font
        , isVisible
        , buttonText)
    {
    }
}