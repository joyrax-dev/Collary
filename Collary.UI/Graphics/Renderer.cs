using Collary.Native.SDL2;
using Collary.UI.System;
using Collary.UI.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.UI.Graphics;

public class Renderer : Base
{
    public Renderer(Window window)
    {
        IntPtr handle = SDL.SDL_CreateRenderer(window.Pointer, -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
            | SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

        if (handle == IntPtr.Zero)
            throw new RendererCreationException();

        this.Pointer = handle;
    }

    protected override void Destroy()
    {
        SDL.SDL_DestroyRenderer(this.Pointer);
    }

    public Color Color
    {
        get
        {
            SDL.SDL_GetRenderDrawColor(this.Pointer, out byte r, out byte g, out byte b, out byte a);
            return new Color(r, g, b, a);
        }
        set
        {
            SDL.SDL_SetRenderDrawColor(this.Pointer, value.R, value.G, value.B, value.A);
        }
    }
}
