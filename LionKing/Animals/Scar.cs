namespace LionKing.Animals;

using Microsoft.Xna.Framework.Graphics;
public class Scar: Character
{
    private const string SpritePath = "../../../Images/scar.png";

    public Scar(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
    {
    }
}