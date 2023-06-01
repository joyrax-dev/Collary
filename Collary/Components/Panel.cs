using System;
using Collary.Core;
using Collary.Graphics;

namespace Collary.Components;

public class Panel : Prototype
{
    public Color Background { get; set; }
    public Color Border { get; set; }

    public Panel(Vector2i size, Renderer renderer) : base(size, renderer)
    {
        this.Background = new Color(255, 255, 255);
        this.Border = new Color(215, 215, 215);
    }

    public override void Draw(Renderer renderer)
    {
        renderer.Color = this.Background;
        renderer.Clear();

        renderer.Color = this.Border;
        renderer.DrawRect(new Rect(0, 0, this.Texture.Size.X, this.Texture.Size.Y));
    }
}