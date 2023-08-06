using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

public static class Utils
{
    public static SDL.SDL_bool SDL_Bool(this bool value)
    {
        if (value)
            return SDL.SDL_bool.SDL_TRUE;
        else
            return SDL.SDL_bool.SDL_FALSE;
    }
}
