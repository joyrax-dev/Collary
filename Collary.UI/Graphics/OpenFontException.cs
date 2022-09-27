using Collary.Native.SDL2;
using System;
using System.Runtime.Serialization;

namespace Collary.UI.Graphics;

[Serializable]
public class OpenFontException : Exception
{
    public OpenFontException(string path) : base($"Open font '{path}' error! \nMessage: {SDL.SDL_GetError()}") { }
}