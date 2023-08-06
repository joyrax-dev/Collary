using Collary.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics.Components;

public class Panel : Prototype
{
    public Color Background { get; set; }
    public Color Border { get; set; }
    private bool _redraw = true;

    public Panel(Vector2i position, Vector2i size, Color background, Color border, Renderer renderer) 
        : base(position, size, renderer)
    {
        Background = background;
        Border = border;
    }

    //if (pos.X > Position.X && pos.X < Position.X + Size.X && pos.Y > Position.Y && pos.Y < Position.Y + Size.Y)
    public override void Initialization()
    {
        base.Initialization();
    }

    public override void Update() 
    {
        base.Update();
    }

    public override void Draw(Renderer _renderer)
    {
        base.Draw(_renderer);

        if (!_redraw)
            return;

        _renderer.Clear(Background);

        Color bkp = _renderer.Color;
        _renderer.Color = Border;

        _renderer.DrawRect(new Rectangle(0, 0, Texture.Size.X, Texture.Size.Y));

        _renderer.Color = bkp;

        _redraw = false;
    }

    public override void GlobalDraw(Renderer _renderer) 
    {
        base.GlobalDraw(_renderer);
    }
}
