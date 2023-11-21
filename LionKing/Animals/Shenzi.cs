namespace LionKing.Animals;

using Microsoft.Xna.Framework.Graphics;
public class Shenzi: Character
{
    private const string SpritePath = "../../../Images/shenzi.png";

    public Shenzi(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
    {
    }
}