using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics;

[Serializable]
public class RendererCreationException : Exception
{
    public RendererCreationException() : base($"Renderer creation error! \nMessage: {SDL.SDL_GetError()}") { }
}