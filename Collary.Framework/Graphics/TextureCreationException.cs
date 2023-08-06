using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics;

[Serializable]
public class TextureCreationException : Exception
{
    public TextureCreationException() : base($"Texture creation error! \nMessage: {SDL.SDL_GetError()}") { }
}