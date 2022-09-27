using Collary.Native.SDL2;
using System;

namespace Collary.UI.Windowing;

public unsafe class TextEventArgs : EventArgs
{
    public string Text;

    public TextEventArgs(SDL.SDL_TextInputEvent e)
    {
        this.Text = SDL.UTF8_ToManaged((IntPtr)e.text);
    }
}