using Collary.Native.SDL2;
using System;
using System.Runtime.Serialization;

namespace Collary.UI.Graphics;

[Serializable]
public class RendererCreationException : Exception
{
    public RendererCreationException() : base($"Renderer creation error! \nMessage: {SDL.SDL_GetError()}") { }
}