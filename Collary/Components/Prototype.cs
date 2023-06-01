using System;
using Collary.Core;
using Collary.Graphics;

namespace Collary.Components;

public class Prototype
{
    public Texture Texture { get; protected set; }

    public Prototype(Vector2i size, Renderer renderer)
    {
        this.Texture = new Texture(size.X, size.Y, renderer);
    }

    public virtual void EventsSubscribe(IEventable window) { }

    public virtual void Update() { }

    public virtual void Draw(Renderer renderer) { }
    public virtual void AfterDraw(Renderer renderer) { }
}