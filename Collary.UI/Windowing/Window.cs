using Collary.Native.SDL2;
using Collary.UI.System;
using System;
using System.Numerics;

namespace Collary.UI.Windowing;

public class Window : Base, IEventable
{
    public static bool HasVideoInitialize { get; private set; } = false;
    public static int WindowCount { get; private set; } = 0;

    public Window(string title, Vector2 position, Vector2 size) : base()
    {
        WindowContext context = new WindowContext();
        IntPtr handle = Window.CreateWindow(title, (int)position.X, (int)position.Y, (int)size.X,
                (int)size.Y, context);

        this.Pointer = handle;

        Window.ConfigurateWindow(this.Pointer, context);
    }

    public Window(string title, Vector2 position, Vector2 size, WindowContext context) : base()
    {
        IntPtr handle = Window.CreateWindow(title, (int)position.X, (int)position.Y, (int)size.X,
                (int)size.Y, context);

        this.Pointer = handle;

        Window.ConfigurateWindow(this.Pointer, context);
    }

    public Window(string title, WindowPosition position, Vector2 size) : base()
    {
        WindowContext context = new WindowContext();
        IntPtr handle = Window.CreateWindow(title, (int)position, (int)position, (int)size.X,
                (int)size.Y, context);

        this.Pointer = handle;

        Window.ConfigurateWindow(this.Pointer, context);
    }

    public Window(string title, WindowPosition position, Vector2 size, WindowContext context) : base()
    {
        IntPtr handle = Window.CreateWindow(title, (int)position, (int)position, (int)size.X,
                (int)size.Y, context);

        this.Pointer = handle;

        Window.ConfigurateWindow(this.Pointer, context);
    }

    public event EventHandler Close = null;
    public event EventHandler<SizeEventArgs> Resize;
    public event EventHandler<SizeEventArgs> SizeChange;
    public event EventHandler<SizeEventArgs> Restore;
    public event EventHandler<SizeEventArgs> Minimize;
    public event EventHandler<SizeEventArgs> Maximize;
    public event EventHandler LostFocus;
    public event EventHandler GainedFocus;
    public event EventHandler TakeFocus;
    public event EventHandler ClipboardUpdate;
    public event EventHandler<KeyEventArgs> KeyPress;
    public event EventHandler<KeyEventArgs> KeyRelease;
    public event EventHandler<TextEventArgs> TextEnter;
    public event EventHandler<MouseButtonEventArgs> MouseButtonPress;
    public event EventHandler<MouseButtonEventArgs> MouseButtonRelease;
    public event EventHandler<MouseMoveEventArgs> MouseMove;
    public event EventHandler<MouseWheelScrollEventArgs> MouseWheelScroll;
    public event EventHandler<DropFileEventArgs> DropFile;
    public event EventHandler<DropTextEventArgs> DropText;
    public event EventHandler DropBegin;
    public event EventHandler DropComplete;
    public event EventHandler<AudioDeviceEventArgs> AudioDeviceConnect;
    public event EventHandler<AudioDeviceEventArgs> AudioDeviceDisconnect;

    protected static IntPtr CreateWindow(string title, int x, int y, int w, int h, WindowContext context)
    {
        if (!Window.HasVideoInitialize)
            if (SDL.SDL_InitSubSystem(SDL.SDL_INIT_VIDEO) != 0)
                throw new VideoInitializationException();
            else
                Window.HasVideoInitialize = true;

        SDL.SDL_WindowFlags flags;

        if (context.StartupHidden)
            flags = SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN;
        else
            flags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;

        if (context.Resizable)
            flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

        if (context.AlwaysOnTop)
            flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP;

        if (context.Borderless)
            flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS;

        if (context.SkipTaskbar)
            flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_SKIP_TASKBAR;

        switch (context.Type)
        {
            case WindowType.PopupMenu:
                flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_POPUP_MENU;
                break;

            case WindowType.Tooltip:
                flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_TOOLTIP;
                break;

            case WindowType.Utility:
                flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_UTILITY;
                break;
        }

        switch (context.State)
        {
            case WindowState.Fullscreen:
                flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
                break;

            case WindowState.Minimize:
                flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_MINIMIZED;
                break;

            case WindowState.Maximize:
                flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED;
                break;
        }

        switch (context.Video)
        {
            case VideoContext.OpenGL:
                flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL;
                break;

            case VideoContext.Vulkan:
                flags = flags | SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN;
                break;
        }

        IntPtr handle = SDL.SDL_CreateWindow(title, x, y, w, h, flags);

        if (handle == IntPtr.Zero)
            throw new WindowCreationException();

        Window.WindowCount++;

        return handle;
    }

    protected static void ConfigurateWindow(IntPtr window, WindowContext context)
    {
        SDL.SDL_SetWindowMaximumSize(window, (int)context.MaximumSize.X, (int)context.MaximumSize.Y);
        SDL.SDL_SetWindowMinimumSize(window, (int)context.MinimumSize.X, (int)context.MinimumSize.Y);
        SDL.SDL_SetWindowOpacity(window, context.Opacity);
    }

    protected override void Destroy()
    {
        SDL.SDL_DestroyWindow(this.Pointer);

        Window.WindowCount--;
        if (Window.WindowCount <= 0)
        {
            SDL.SDL_VideoQuit();
            Window.HasVideoInitialize = false;
        }
    }

    public void Hide()
    {
        SDL.SDL_HideWindow(this.Pointer);
    }

    public void Show()
    {
        SDL.SDL_ShowWindow(this.Pointer);
    }

    public void Focus()
    {
        SDL.SDL_SetWindowInputFocus(this.Pointer);
    }

    public void OnClose(object sender)
    {
        this.Close?.Invoke(this, EventArgs.Empty);
    }

    public void OnResize(object sender, SizeEventArgs e)
    {
        this.Resize?.Invoke(this, e);
    }

    public void OnSizeChange(object sender, SizeEventArgs e)
    {
        this.SizeChange?.Invoke(this, e);
    }

    public void OnRestore(object sender, SizeEventArgs e)
    {
        this.Restore?.Invoke(this, e);
    }

    public void OnMinimize(object sender, SizeEventArgs e)
    {
        this.Minimize?.Invoke(this, e);
    }

    public void OnMaximize(object sender, SizeEventArgs e)
    {
        this.Maximize?.Invoke(this, e);
    }

    public void OnLostFocus(object sender)
    {
        this.LostFocus?.Invoke(this, EventArgs.Empty);
    }

    public void OnGainedFocus(object sender)
    {
        this.GainedFocus?.Invoke(this, EventArgs.Empty);
    }

    public void OnTakeFocus(object sender)
    {
        this.TakeFocus?.Invoke(this, EventArgs.Empty);
    }

    public void OnClipboardUpdate(object sender)
    {
        this.ClipboardUpdate?.Invoke(this, EventArgs.Empty);
    }

    public void OnKeyPress(object sender, KeyEventArgs e)
    {
        this.KeyPress?.Invoke(this, e);
    }

    public void OnKeyRelease(object sender, KeyEventArgs e)
    {
        this.KeyRelease?.Invoke(this, e);
    }

    public void OnTextEnter(object sender, TextEventArgs e)
    {
        this.TextEnter?.Invoke(this, e);
    }

    public void OnMouseButtonPress(object sender, MouseButtonEventArgs e)
    {
        this.MouseButtonPress?.Invoke(this, e);
    }

    public void OnMouseButtonRelease(object sender, MouseButtonEventArgs e)
    {
        this.MouseButtonRelease?.Invoke(this, e);
    }

    public void OnMouseMove(object sender, MouseMoveEventArgs e)
    {
        this.MouseMove?.Invoke(this, e);
    }

    public void OnMouseWheelScroll(object sender, MouseWheelScrollEventArgs e)
    {
        this.MouseWheelScroll?.Invoke(this,e);
    }

    public void OnDropFile(object sender, DropFileEventArgs e)
    {
        this.DropFile?.Invoke(this, e);
    }

    public void OnDropText(object sender, DropTextEventArgs e)
    {
        this.DropText?.Invoke(this, e);
    }

    public void OnDropBegin(object sender)
    {
        this.DropBegin?.Invoke(this, EventArgs.Empty);
    }

    public void OnDropComplete(object sender)
    {
        this.DropComplete?.Invoke(this, EventArgs.Empty);
    }

    public void OnAudioDeviceConnect(object sender, AudioDeviceEventArgs e)
    {
        this.AudioDeviceConnect?.Invoke(this, e);
    }

    public void OnAudioDeviceDisconnect(object sender, AudioDeviceEventArgs e)
    {
        this.AudioDeviceDisconnect?.Invoke(this, e);
    }

    public WindowState State
    {
        get
        {
            uint flags = SDL.SDL_GetWindowFlags(this.Pointer),
                 flag_max = ((uint)SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED),
                 flag_min = ((uint)SDL.SDL_WindowFlags.SDL_WINDOW_MINIMIZED),
                 flag_full = ((uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);

            if ((flags & flag_full) == flag_full)
                return WindowState.Fullscreen;
            else if ((flags & flag_max) == flag_max)
                return WindowState.Maximize;
            else if ((flags & flag_min) == flag_min)
                return WindowState.Minimize;
            else
                return WindowState.Restore;
        }
        set
        {
            if (value == WindowState.Restore)
                SDL.SDL_RestoreWindow(this.Pointer);
            else if (value == WindowState.Maximize)
                SDL.SDL_MaximizeWindow(this.Pointer);
            else if (value == WindowState.Minimize)
                SDL.SDL_MinimizeWindow(this.Pointer);
            else if (value == WindowState.Fullscreen)
                SDL.SDL_SetWindowFullscreen(this.Pointer, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
        }
    }

    public Window Modal // Test
    {
        set
        {
            if (value != null)
                SDL.SDL_SetWindowModalFor(value.Pointer, this.Pointer);
            else
                SDL.SDL_SetWindowModalFor(IntPtr.Zero, this.Pointer);
        }
    }

    public uint Id
    {
        get
        {
            return SDL.SDL_GetWindowID(this.Pointer);
        }
    }

    public Vector2 Size
    {
        get 
        {
            SDL.SDL_GetWindowSize(this.Pointer, out int w, out int h);
            return new Vector2(w, h);
        }
        set 
        {
            SDL.SDL_SetWindowSize(this.Pointer, (int)value.X, (int)value.Y);
        }
    }

    public Vector2 Position
    {
        get 
        {
            SDL.SDL_GetWindowPosition(this.Pointer, out int x, out int y);
            return new Vector2(x, y);
        }
        set 
        {
            SDL.SDL_SetWindowSize(this.Pointer, (int)value.X, (int)value.Y);
        }
    }

    public string Title
    {
        get 
        {
            return SDL.SDL_GetWindowTitle(this.Pointer);
        }
        set 
        {
            SDL.SDL_SetWindowTitle(this.Pointer, value);
        }
    }

    public Vector2 MinimumSize
    {
        get 
        {
            SDL.SDL_GetWindowMinimumSize(this.Pointer, out int w, out int h);
            return new Vector2(w, h);
        }
        set 
        {
            SDL.SDL_SetWindowMinimumSize(this.Pointer, (int)value.X, (int)value.Y);
        }
    }

    public Vector2 MaximumSize
    {
        get 
        {
            SDL.SDL_GetWindowMaximumSize(this.Pointer, out int w, out int h);
            return new Vector2(w, h);
        }
        set 
        {
            SDL.SDL_SetWindowMaximumSize(this.Pointer, (int)value.X, (int)value.Y);
        }
    }

    public float Opacity
    {
        get 
        {
            SDL.SDL_GetWindowOpacity(this.Pointer, out float factor);
            return factor;
        }
        set
        {
            SDL.SDL_SetWindowOpacity(this.Pointer, value);
        }
    }

    public bool HasFocus
    {
        //get => (((uint)SDL.SDL_GetWindowFlags(this.Pointer) & (uint)SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS)
        //    == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS) ? true : false;
        get
        {
            uint flags = SDL.SDL_GetWindowFlags(this.Pointer),
                 flag_focus = ((uint)SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS);

            if ((flags & flag_focus) == flag_focus)
                return true;
            else
                return false;
        }
    }

    public bool Resizeble
    {
        get
        {
            uint flags = SDL.SDL_GetWindowFlags(this.Pointer),
                 flag_resize = ((uint)SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

            if ((flags & flag_resize) == flag_resize)
                return true;
            else
                return false;
        }
        set
        {
            if (value)
                SDL.SDL_SetWindowResizable(this.Pointer, SDL.SDL_bool.SDL_TRUE);
            else
                SDL.SDL_SetWindowResizable(this.Pointer, SDL.SDL_bool.SDL_FALSE);
        }
    }
}
