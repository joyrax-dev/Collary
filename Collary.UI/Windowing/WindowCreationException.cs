using Collary.Native.SDL2;
using System;

namespace Collary.UI.Windowing;

[Serializable]
public class WindowCreationException : Exception
{
    public WindowCreationException() : base($"Window creation error! \nSDL Message: {SDL.SDL_GetError()}") { }
}
