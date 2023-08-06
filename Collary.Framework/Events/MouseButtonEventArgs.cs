using Collary.Framework.Core;
using Collary.Framework.Mouse;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public class MouseButtonEventArgs : EventArgs
{
    public Button Button;
    public byte Clicks;
    public Vector2i Position;

    public MouseButtonEventArgs(SDL.SDL_MouseButtonEvent e)
    {
        this.Clicks = e.clicks;
        this.Position = new Vector2i(e.x, e.y);

        switch (e.button)
        {
            case (byte)Button.Left:
                this.Button = Button.Left;
                break;

            case (byte)Button.Middle:
                this.Button = Button.Middle;
                break;

            case (byte)Button.Right:
                this.Button = Button.Right;
                break;

            case (byte)Button.X1:
                this.Button = Button.X1;
                break;

            case (byte)Button.X2:
                this.Button = Button.X2;
                break;
        }
    }
}
