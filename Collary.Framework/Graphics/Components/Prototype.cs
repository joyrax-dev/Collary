using Collary.Framework.Core;
using Collary.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics.Components;

public class Prototype
{
    protected Texture _texture;

    public Texture Texture { get => _texture; }
    public Vector2i Position { get; set; }
    public Vector2i Size { get; set; }

    public Prototype(Vector2i position, Vector2i size, Renderer renderer)
    {
        Position = position;
        Size = size;
        _texture = new Texture(Size.X, Size.Y, renderer);
    }

    public virtual void Initialization() { }

    public virtual void Update() { }

    public virtual void Draw(Renderer _renderer) { }
    public virtual void GlobalDraw(Renderer _renderer) { }

    public event EventHandler<MouseMoveEventArgs>? MouseMove = null;
    public event EventHandler<MouseButtonEventArgs>? MouseButton = null;

    public void OnMouseMove(object sender, MouseMoveEventArgs e)
    {
        MouseMove?.Invoke(sender, e);
    }

    public void OnMouseButton(object sender, MouseButtonEventArgs e)
    {
        MouseButton?.Invoke(sender, e);
    }
}