using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Mouse;

public enum Button : byte
{
    Left   = (byte)SDL.SDL_BUTTON_LEFT,
    Middle = (byte)SDL.SDL_BUTTON_MIDDLE,
    Right  = (byte)SDL.SDL_BUTTON_RIGHT,
    X1     = (byte)SDL.SDL_BUTTON_X1,
    X2     = (byte)SDL.SDL_BUTTON_X2
}
