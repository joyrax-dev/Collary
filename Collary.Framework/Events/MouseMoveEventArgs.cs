using Collary.Framework.Core;
using Collary.Framework.Mouse;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public class MouseMoveEventArgs : EventArgs
{
    public Button Button;
    public Vector2i Position;
    public Vector2i RelativePosition;

    public MouseMoveEventArgs(SDL.SDL_MouseMotionEvent e)
    {
        this.Position = new Vector2i(e.x, e.y);
        this.RelativePosition = new Vector2i(e.xrel, e.yrel);

        switch (e.state)
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
