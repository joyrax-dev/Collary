using Collary.Native.SDL2;
using System;
using System.Runtime.Serialization;

namespace Collary.Graphics;

[Serializable]
public class TextureCreationException : Exception
{
    public TextureCreationException() : base($"Texture creation error! \nMessage: {SDL.SDL_GetError()}") { }
}