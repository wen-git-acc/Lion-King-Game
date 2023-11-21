namespace LionKing.Animals;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Character
{
    public Texture2D Sprite { get; }
    public Vector2 Position { get; set; }

    private int RectangleWidth { get; set; }
    private int RectangleHeight { get; set; }

    public Rectangle Rectangle { get; set; }

    public int previousPosX;
    public int previousPosY;

    protected Character(GraphicsDevice graphicsDevice, string spritePath)
    {
        Sprite = Texture2D.FromFile(graphicsDevice, spritePath);
        Position = Vector2.Zero;
        Rectangle = Rectangle.Empty;
    }

    public void Sizing(int positionX, int positionY , int width, int height)
    {
        previousPosX = positionX; 
        previousPosY = positionY;
        RectangleWidth = width;
        RectangleHeight = height;
        Rectangle = new Rectangle(positionX,positionY, width, height);
    }

    
    public void ChangePosition(int newPositionX, int newPositionY)
    {
        previousPosX = Rectangle.X;
        previousPosY = Rectangle.Y;
        Rectangle = new Rectangle(newPositionX,newPositionY,RectangleWidth,RectangleHeight);
    }
}