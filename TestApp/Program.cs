using Collary.Framework.Core;
using Collary.Framework.Graphics;
using Collary.Framework.Graphics.Components;
using Collary.Framework.Windowing;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TestApp;

public static class Program
{
    public static void Main()
    {
        Window Win = new Window("TestApp", WindowPosition.Centered, new Vector2i(710, 405));
        Panel p = new Panel(new Vector2i(100, 100), new Vector2i(150, 100), new Color(255, 0, 0), new Color(255, 255, 255), Win.Renderer);
        Win.Components.Add(p);
        TestComponent t = new TestComponent(new Vector2i(100, 100), new Vector2i(150, 100), Win.Renderer);
        Win.Components.Add(t);
        Win.Run();

        
    }

}

class TestComponent : Prototype
{
    public TestComponent(Vector2i position, Vector2i size, Renderer renderer)
        : base(position, size, renderer)
    {

    }
    private bool _redraw = true;

    public override void Draw(Renderer _renderer)
    {
        base.Draw(_renderer);

        if (!_redraw)
            return;

        _renderer.Clear(new Color(100, 100, 100));

        Color bkp = _renderer.Color;
        _renderer.Color = new Color(0, 0, 255);

        _renderer.DrawRectRounded(new Rectangle(Position.X, Position.Y, Size.X, Size.Y), 10);

        _renderer.Color = bkp;

        _redraw = false;
    }
}