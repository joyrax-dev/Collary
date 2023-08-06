using Collary.Framework.Core;
using Collary.Framework.Mouse;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public class MouseWheelScrollEventArgs : EventArgs
{
    public WheelDirection Direction;
    public Vector2f PreciseVector;

    public MouseWheelScrollEventArgs(SDL.SDL_MouseWheelEvent e)
    {
        if (e.direction == (uint)SDL.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_NORMAL)
        {
            this.PreciseVector = new Vector2f(e.preciseX, e.preciseY);

            if (e.y > 0)
                this.Direction = WheelDirection.Up;

            else if (e.y < 0)
                this.Direction = WheelDirection.Down;
        }
        else if (e.direction == (uint)SDL.SDL_MouseWheelDirection.SDL_MOUSEWHEEL_FLIPPED)
        {
            this.PreciseVector = new Vector2f(e.preciseX * -1, e.preciseY * -1);

            if (e.y < 0)
                this.Direction = WheelDirection.Up;

            else if (e.y > 0)
                this.Direction = WheelDirection.Down;
        }
    }
}
