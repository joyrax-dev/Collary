using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public class DropFileEventArgs : EventArgs
{
    public string File;

    public DropFileEventArgs(SDL.SDL_DropEvent e)
    {
        this.File = SDL.UTF8_ToManaged(e.file, true);
    }
}
