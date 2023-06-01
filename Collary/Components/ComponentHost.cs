using System;
using System.Collections.Generic;
using Collary.Core;
using Collary.Graphics;

namespace Collary.Components;

public class ComponentHost
{
    protected List<Prototype> Components { get; set; }

    public ComponentHost()
    {
        this.Components = new List<Prototype>();
    }

    public void Add(Prototype component)
    {
        this.Components.Add(component);
    }

    public void Step(Renderer renderer)
    {
        if (this.Components.Count >= 1)
            foreach (Prototype component in this.Components)
            {
                component.Update();
                component.Draw(renderer);
                component.AfterDraw(renderer);
            }
    }
}