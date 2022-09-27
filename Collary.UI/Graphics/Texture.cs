using Collary.Native.SDL2;
using Collary.UI.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Collary.UI.Graphics
{
    public class Texture : Base
    {
        public Texture(int width, int height, Renderer renderer)
        {
            IntPtr handle = SDL.SDL_CreateTexture(renderer.Pointer,
                SDL.SDL_PIXELFORMAT_RGBA8888,
                (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET,
                width, height);

            if (handle == IntPtr.Zero)
                throw new TextureCreationException();

            this.Pointer = handle;
        }

        public Texture(IntPtr texture)
        {
            if (texture == IntPtr.Zero)
                throw new NullReferenceException();

            this.Pointer = texture;

        }

        protected override void Destroy()
        {
            SDL.SDL_DestroyTexture(this.Pointer);
        }

        public Vector2 Size
        {
            get
            {
                SDL.SDL_QueryTexture(this.Pointer, out uint format, out int access, out int w, out int h);
                return new Vector2(w, h);
            }
        }
    }
}
