using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LionKing.Animals;

public class CharacterDrawer
{
    private SpriteBatch _spriteBatch;
    private GraphicsDevice _graphicsDevice;
    public Simba Simba;
    public Scar Scar;
    public int y = 100;

    public CharacterDrawer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        _spriteBatch = spriteBatch;
        _graphicsDevice = graphicsDevice;
        Simba = new Simba(_graphicsDevice);
        Scar = new Scar(_graphicsDevice);

    }

    public virtual void Update()
    {
        var currentKeyboardState = Keyboard.GetState();
        if (currentKeyboardState.IsKeyDown(Keys.Down))
        {
            y -= 100;
        }
        
        if (currentKeyboardState.IsKeyDown(Keys.Up))
        {
            y += 100;
        }
        
        
        Simba.Position = new Vector2(0, y);
        Scar.Position = new Vector2(0, y +300);
    }

    public void SetInitialTextureSize()
    {
        Simba.Sizing(0,100,100,100);
        Scar.Sizing(0,200,150,150);
    }


    public virtual void Draw()
    {
        SetInitialTextureSize();
        _spriteBatch.Draw(Simba.Sprite, Simba.Rectangle, Color.White);
        _spriteBatch.Draw(Scar.Sprite, Scar.Rectangle, Color.White);
    }
}