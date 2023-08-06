using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

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
