using Collary.Framework.Core;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

[Serializable]
public class EventsInitializationException : CollaryException
{
    public EventsInitializationException() : base($"Events Initialization error! \nMessage: {SDL.SDL_GetError()}", new EventsInitializationException()) { }
}
