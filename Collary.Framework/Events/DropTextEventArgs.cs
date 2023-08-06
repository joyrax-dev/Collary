using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public class DropTextEventArgs : EventArgs
{
    public string Text;

    public DropTextEventArgs(SDL.SDL_DropEvent e)
    {
        this.Text = SDL.UTF8_ToManaged(e.file, true);
    }
}
