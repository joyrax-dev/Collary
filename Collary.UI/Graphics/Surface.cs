using Collary.Native.SDL2;
using Collary.UI.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Collary.UI.Graphics;

public class Surface : Base
{
    public Surface(nint surface)
    {
        if (surface == nint.Zero)
            throw new NullReferenceException();

        this.Pointer = surface;
    }

    protected override void Destroy()
    {
        SDL.SDL_FreeSurface(this.Pointer);
    }
}