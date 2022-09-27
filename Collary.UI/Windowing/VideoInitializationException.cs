using Collary.Native.SDL2;
using System;

namespace Collary.UI.Windowing;

[Serializable]
public class VideoInitializationException : Exception
{
    public VideoInitializationException() : base($"Video Initialization error! \nMessage: {SDL.SDL_GetError()}") { }
}
