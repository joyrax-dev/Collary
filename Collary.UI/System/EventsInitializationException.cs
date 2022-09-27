using Collary.Native.SDL2;
using System;
using System.Runtime.Serialization;

namespace Collary.UI.System;

[Serializable]
public class EventsInitializationException : Exception
{
    public EventsInitializationException() : base($"Events Initialization error! \nMessage: {SDL.SDL_GetError()}") { }
}
