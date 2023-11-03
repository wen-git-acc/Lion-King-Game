namespace LionKing.Animals;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Character
{
    public Texture2D Sprite { get; }
    public Vector2 Position { get; set; }

    protected Character(GraphicsDevice graphicsDevice, string spritePath)
    {
        Sprite = Texture2D.FromFile(graphicsDevice, spritePath);
        Position = Vector2.Zero;
    }
}