using Microsoft.Xna.Framework.Graphics;

namespace LionKing.Animals
{
    public class Mufasa : Character
    {
        private const string SpritePath = "../../../Images/mufasa.png";

        public Mufasa(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
        {
        }
    }
}
