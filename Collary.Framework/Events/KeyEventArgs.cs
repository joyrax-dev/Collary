using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public class KeyEventArgs : EventArgs
{
    public SDL.SDL_Scancode Scancode;
    public SDL.SDL_Keycode Keycode;
    public SDL.SDL_Keymod Keymod;

    public KeyEventArgs(SDL.SDL_Keysym keysym)
    {
        this.Scancode = keysym.scancode;
        this.Keycode = keysym.sym;
        this.Keymod = keysym.mod;
    }
}
