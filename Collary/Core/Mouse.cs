using Collary.Native.SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Core;

public enum Button : byte // TODO: Move to the Mouse.cs
{
    Left = (byte)SDL.SDL_BUTTON_LEFT,
    Middle = (byte)SDL.SDL_BUTTON_MIDDLE,
    Right = (byte)SDL.SDL_BUTTON_RIGHT,
    X1 = (byte)SDL.SDL_BUTTON_X1,
    X2 = (byte)SDL.SDL_BUTTON_X2
}

public enum WheelDirection // TODO: Move to the Mouse.cs
{
    Up,
    Down
    // Left,
    // Right
}