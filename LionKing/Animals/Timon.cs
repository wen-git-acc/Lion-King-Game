namespace LionKing.Animals;

using Microsoft.Xna.Framework.Graphics;
public class Timon: Character
{
    private const string SpritePath = "../../../Images/timon.png";

    public Timon(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
    {
    }
}