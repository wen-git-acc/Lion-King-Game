using Microsoft.Xna.Framework.Graphics;

namespace LionKing.Animals
{
    public class Ed : Character
    {
        private const string SpritePath = "../../../Images/ed.png";

        public Ed(GraphicsDevice graphicsDevice) : base(graphicsDevice, SpritePath)
        {
        }
    }
}
