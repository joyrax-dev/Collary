using Collary.Native.SDL2;
using System;

namespace Collary.UI.Windowing;

public class DropFileEventArgs : EventArgs
{
    public string File;

    public DropFileEventArgs(SDL.SDL_DropEvent e)
    {
        this.File = SDL.UTF8_ToManaged(e.file, true);
    }
}