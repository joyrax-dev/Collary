using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public unsafe class TextEventArgs : EventArgs
{
    public string Text;

    public TextEventArgs(SDL.SDL_TextInputEvent e)
    {
        this.Text = SDL.UTF8_ToManaged((nint)e.text);
    }
}