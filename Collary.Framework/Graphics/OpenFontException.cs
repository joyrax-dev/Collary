using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics;

[Serializable]
public class OpenFontException : Exception
{
    public OpenFontException(string path) : base($"Open font '{path}' error! \nMessage: {SDL.SDL_GetError()}") { }
}