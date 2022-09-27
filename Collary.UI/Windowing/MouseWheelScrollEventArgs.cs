using Collary.Native.SDL2;
using Collary.UI.System;
using System;
using System.Numerics;

namespace Collary.UI.Windowing;


public class MouseWheelScrollEventArgs : EventArgs
{
    public WheelDirection Direction;
    public Vector2 PreciseVector;

    public MouseWheelScrollEventArgs(SDL.SDL_MouseWheelEvent e)
    {
        if (e.direction == (uint)SDL.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_NORMAL)
        {
            this.PreciseVector = new Vector2(e.preciseX, e.preciseY);

            if (e.y > 0)
                this.Direction = WheelDirection.Up;

            else if (e.y < 0)
                this.Direction = WheelDirection.Down;
        }
        else if (e.direction == (uint)SDL.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_FLIPPED)
        {
            this.PreciseVector = new Vector2(e.preciseX * -1, e.preciseY * -1);

            if (e.y < 0)
                this.Direction = WheelDirection.Up;

            else if (e.y > 0)
                this.Direction = WheelDirection.Down;
        }
    }
}
