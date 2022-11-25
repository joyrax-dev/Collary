using Collary.Native.SDL2;
using System;
using System.Runtime.Serialization;

namespace Collary.UI.Graphics;

[Serializable]
public class TTFInitializationException : Exception
{
    public TTFInitializationException() : base($"TTF Initialization error! \nMessage: {SDL.SDL_GetError()}") { }
}
