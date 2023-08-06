using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Windowing;

public enum WindowPosition : int
{
    Centered      = SDL.SDL_WINDOWPOS_CENTERED,
    CenteredMask  = SDL.SDL_WINDOWPOS_CENTERED_MASK,
    Undefined     = SDL.SDL_WINDOWPOS_UNDEFINED,
    UndefinedMask = SDL.SDL_WINDOWPOS_UNDEFINED_MASK
}
