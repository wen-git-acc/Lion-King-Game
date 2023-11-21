namespace LionKing.Animals;

using Microsoft.Xna.Framework.Graphics;
public class Pumbaa: Character
{
    private const string SpritePath = "../../../Images/pumbaa.png";

    public Pumbaa(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
    {
    }
}