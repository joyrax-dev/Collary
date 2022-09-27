using Collary.Native.SDL2;
using System;

namespace Collary.UI.Windowing;

public class DropTextEventArgs : EventArgs
{
    public string Text;

    public DropTextEventArgs(SDL.SDL_DropEvent e)
    {
        this.Text = SDL.UTF8_ToManaged(e.file, true);
    }
}