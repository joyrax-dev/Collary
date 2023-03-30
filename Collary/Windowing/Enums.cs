using Collary.Native.SDL2;

namespace Collary.Windowing;

public enum VideoContext
{
    Default,
    OpenGL,
    Vulkan
}

public enum WindowPosition : int
{
    Centered = SDL.SDL_WINDOWPOS_CENTERED,
    CenteredMask = SDL.SDL_WINDOWPOS_CENTERED_MASK,
    Undefined = SDL.SDL_WINDOWPOS_UNDEFINED,
    UndefinedMask = SDL.SDL_WINDOWPOS_UNDEFINED_MASK
}

public enum WindowState
{
    Restore,
    Minimize,
    Maximize,
    Fullscreen
}

public enum WindowType
{
    Normal,
    Utility,
    Tooltip,
    PopupMenu
}
