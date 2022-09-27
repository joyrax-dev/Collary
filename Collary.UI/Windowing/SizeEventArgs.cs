using Collary.Native.SDL2;
using System;

namespace Collary.UI.Windowing;

public class SizeEventArgs : EventArgs
{
    public int Width;
    public int Height;

    public SizeEventArgs(SDL.SDL_WindowEvent e)
    {
        this.Width = e.data1;
        this.Height = e.data2;
    }
}