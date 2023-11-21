using Microsoft.Xna.Framework.Graphics;

namespace LionKing.Animals
{
    public class Banzai : Character
    {
        private const string SpritePath = "../../../Images/banzai.png";

        public Banzai(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
        {
        }
    }
}
