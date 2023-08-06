using Collary.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics.Components;

// componentHost будет принимать sdl window events и переобразовывать их в proototype events 
// что бы дальше отправлять их в компоненты, а компоненты в свою очередь будут обрабатывать их
// и в случае если необходимо передавать их дальше дочерним компонентам.
// =====TEST===== При этом координаты будут локализироватся =====
public class ComponentHost
{
    protected List<Prototype> Components { get; set; }
    private Renderer _renderer { get; set; }
    private Dictionary<EventType, EventArgs> EventCollection { get; set; }

    public ComponentHost(Renderer renderer)
    {
        Components = new List<Prototype>();
        _renderer = renderer;
        EventCollection = new Dictionary<EventType, EventArgs>();
    }

    public void Subscribe(IWindowEventable win)
    {
        win.MouseButtonPress += (sender, args) =>
        {
            EventType type = EventType.MouseButton;

            if (EventCollection.ContainsKey(type))
                return;

            EventCollection.Add(type, args);
        };
        win.MouseMove += (sender, args) =>
        {
            EventType type = EventType.MouseMove;

            if (EventCollection.ContainsKey(type))
                return;

            EventCollection.Add(type, args);
        };
    }

    public void Add(Prototype component)
    {
        Components.Add(component);
    }

    // Todo: В будущем можно будет передавать Window в конструкторе и подписываться на события
    // заместь того что бы в Window вызывать SendEvent
    /*public void SendEvent(EventType type, EventArgs args)
    {
        if (EventCollection.ContainsKey(type))
            return;

        EventCollection.Add(type, args);
    }*/

    public void InitializationComponents()
    {
        foreach (Prototype component in Components)
        {
            component.Initialization();
        }
    }

    public void Step()
    {
        if (Components.Count >= 1)
        {
            foreach (Prototype component in Components)
            {
                foreach (KeyValuePair<EventType, EventArgs> el in EventCollection)
                {
                    switch (el.Key)
                    {
                        case EventType.MouseButton:
                            component.OnMouseButton(component, (MouseButtonEventArgs)el.Value);
                            break;

                        case EventType.MouseMove:
                            component.OnMouseMove(component, (MouseMoveEventArgs)el.Value);
                            break;
                    }
                }

                component.Update();

                _renderer.Target = component.Texture;
                component.Draw(_renderer);
                _renderer.Target = null;

                _renderer.Draw(component.Texture, component.Position);

                component.GlobalDraw(_renderer);
            }
        }

        EventCollection.Clear();
    }
}

