using Collary.Native.SDL2;
using System;

namespace Collary.UI.Windowing;

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