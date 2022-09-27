using Collary.Native.SDL2;

namespace Collary.UI.Windowing;

public enum WindowPosition : int
{
    Centered = SDL.SDL_WINDOWPOS_CENTERED,
    CenteredMask = SDL.SDL_WINDOWPOS_CENTERED_MASK,
    Undefined = SDL.SDL_WINDOWPOS_UNDEFINED,
    UndefinedMask = SDL.SDL_WINDOWPOS_UNDEFINED_MASK
}