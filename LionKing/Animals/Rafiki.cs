namespace LionKing.Animals;

using Microsoft.Xna.Framework.Graphics;
public class Rafiki: Character
{
    private const string SpritePath = "../../../Images/rafiki.png";

    public Rafiki(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
    {
    }
}