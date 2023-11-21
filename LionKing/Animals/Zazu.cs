namespace LionKing.Animals;

using Microsoft.Xna.Framework.Graphics;
public class Zazu: Character
{
    private const string SpritePath = "../../../Images/zazu.png";

    public Zazu(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
    {
    }
}