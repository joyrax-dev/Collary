using Collary.Native.SDL2;
using Collary.UI.System;
using System;
using System.Numerics;

namespace Collary.UI.Windowing;

public class MouseMoveEventArgs : EventArgs
{
    public Button Button;
    public Vector2 Position;
    public Vector2 RelativePosition;

    public MouseMoveEventArgs(SDL.SDL_MouseMotionEvent e)
    {
        this.Position = new Vector2(e.x, e.y);
        this.RelativePosition = new Vector2(e.xrel, e.yrel);

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
