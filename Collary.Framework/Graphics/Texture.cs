using Collary.Framework.Core;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics;

public class Texture : Base
{
    public Texture(int width, int height, Renderer renderer)
    {
        nint handle = SDL.SDL_CreateTexture(renderer.Point,
            SDL.SDL_PIXELFORMAT_RGBA8888,
            (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET,
            width, height);

        if (handle == nint.Zero)
            throw new TextureCreationException();

        Point = handle;
    }

    public Texture(nint texture)
    {
        if (texture == nint.Zero)
            throw new NullReferenceException();

        Point = texture;
    }

    protected override void Destroy()
    {
        SDL.SDL_DestroyTexture(Point);
    }

    public Vector2i Size
    {
        get
        {
            SDL.SDL_QueryTexture(Point, out uint format, out int access, out int w, out int h);
            return new Vector2i(w, h);
        }
    }
}