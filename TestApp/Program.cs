using Collary.Framework.Core;
using Collary.Framework.Graphics;
using Collary.Framework.Graphics.Components;
using Collary.Framework.Windowing;

namespace TestApp;

public static class Program
{
    public static void Main()
    {
        Window Win = new Window("TestApp", WindowPosition.Centered, new Vector2i(710, 405));
        Panel p = new Panel(new Vector2i(100, 100), new Vector2i(150, 100), new Color(255, 0, 0), new Color(255, 255, 255), Win.Renderer);
        Win.Components.Add(p);
        Win.Run();
    }
}