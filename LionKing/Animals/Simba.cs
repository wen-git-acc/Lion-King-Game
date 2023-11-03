namespace LionKing.Animals;

using Microsoft.Xna.Framework.Graphics;



public class Simba: Character
{
    private const string SpritePath = "../../../Images/simba.png";

    public Simba(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
    {
    }
}