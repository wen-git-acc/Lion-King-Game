using Microsoft.Xna.Framework.Graphics;

namespace LionKing.Animals;

public class Nala : Character
{
    private const string SpritePath = "../../../Images/nala.png";

    public Nala(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
    {
    }
  
}