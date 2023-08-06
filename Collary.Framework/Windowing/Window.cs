using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collary.Framework.Core;
using Collary.Framework.Events;
using Collary.Framework.Graphics;
using Collary.Framework.Graphics.Components;
using Collary.Native.SDL;

namespace Collary.Framework.Windowing;

public class Window : Base, IWindowEventable
{
    #region Constructors
    public Window(
        string title,
        Vector2i position,
        Vector2i size) : base()
    {
        WindowContext context = WindowContext.Default;
        IntPtr handle = Window.CreateWindow(title, position.X, position.Y, size.X,
                size.Y, context);

        Window.ConfigurateWindow(handle, context);
        Point = handle;

        Dispatcher = new WindowEventDispatcher(this);
        Renderer = new Renderer(this);
        Components = new ComponentHost(Renderer);
    }

    public Window(
        string title,
        Vector2i position,
        Vector2i size,
        WindowContext context) : base()
    {
        IntPtr handle = Window.CreateWindow(title, position.X, position.Y, size.X,
                size.Y, context);

        Window.ConfigurateWindow(handle, context);
        Point = handle;

        Dispatcher = new WindowEventDispatcher(this);
        Renderer = new Renderer(this);
        Components = new ComponentHost(Renderer);
    }

    public Window(
        string title,
        WindowPosition position,
        Vector2i size) : base()
    {
        WindowContext context = WindowContext.Default;
        IntPtr handle = Window.CreateWindow(title, (int)position, (int)position, size.X,
                size.Y, context);

        Window.ConfigurateWindow(handle, context);
        Point = handle;

        Dispatcher = new WindowEventDispatcher(this);
        Renderer = new Renderer(this);
        Components = new ComponentHost(Renderer);
    }

    public Window(
        string title,
        WindowPosition position,
        Vector2i size,
        WindowContext context) : base()
    {
        IntPtr handle = Window.CreateWindow(title, (int)position, (int)position, size.X,
                size.Y, context);

        Window.ConfigurateWindow(handle, context);
        Point = handle;

        Dispatcher = new WindowEventDispatcher(this);
        Renderer = new Renderer(this);
        Components = new ComponentHost(Renderer);
    }
    #endregion

    #region Window Methods
    public void Hide()
    {
        SDL.SDL_HideWindow(this.Point);
    }

    public void Show()
    {
        SDL.SDL_ShowWindow(this.Point);
    }

    public void Focus()
    {
        SDL.SDL_SetWindowInputFocus(this.Point);
    }
    #endregion

    #region Options
    public ComponentHost Components { get; protected set; }
    public Renderer Renderer { get; protected set; }

    public WindowState State
    {
        get
        {
            uint flags = SDL.SDL_GetWindowFlags(Point),
                 flag_max = (uint)SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED,
                 flag_min = (uint)SDL.SDL_WindowFlags.SDL_WINDOW_MINIMIZED,
                 flag_full = (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;

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
                SDL.SDL_RestoreWindow(Point);
            else if (value == WindowState.Maximize)
                SDL.SDL_MaximizeWindow(Point);
            else if (value == WindowState.Minimize)
                SDL.SDL_MinimizeWindow(Point);
            else if (value == WindowState.Fullscreen)
                SDL.SDL_SetWindowFullscreen(Point, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
        }
    }

    public Window Modal // Test
    {
        set
        {
            if (value != null)
                SDL.SDL_SetWindowModalFor(value.Point, Point);
            else
                SDL.SDL_SetWindowModalFor(nint.Zero, Point);
        }
    }

    public uint Id
    {
        get
        {
            return SDL.SDL_GetWindowID(Point);
        }
    }

    public Vector2i Size
    {
        get
        {
            SDL.SDL_GetWindowSize(Point, out int w, out int h);
            return new Vector2i(w, h);
        }
        set
        {
            SDL.SDL_SetWindowSize(Point, value.X, value.Y);
        }
    }

    public Vector2i Position
    {
        get
        {
            SDL.SDL_GetWindowPosition(Point, out int x, out int y);
            return new Vector2i(x, y);
        }
        set
        {
            SDL.SDL_SetWindowSize(Point, value.X, value.Y);
        }
    }

    public string Title
    {
        get
        {
            return SDL.SDL_GetWindowTitle(Point);
        }
        set
        {
            SDL.SDL_SetWindowTitle(Point, value);
        }
    }

    public Vector2i MinimumSize
    {
        get
        {
            SDL.SDL_GetWindowMinimumSize(Point, out int w, out int h);
            return new Vector2i(w, h);
        }
        set
        {
            SDL.SDL_SetWindowMinimumSize(Point, value.X, value.Y);
        }
    }

    public Vector2i MaximumSize
    {
        get
        {
            SDL.SDL_GetWindowMaximumSize(Point, out int w, out int h);
            return new Vector2i(w, h);
        }
        set
        {
            SDL.SDL_SetWindowMaximumSize(Point, value.X, value.Y);
        }
    }

    public float Opacity
    {
        get
        {
            SDL.SDL_GetWindowOpacity(Point, out float factor);
            return factor;
        }
        set
        {
            SDL.SDL_SetWindowOpacity(Point, value);
        }
    }

    public bool HasFocus
    {
        //get => (((uint)SDL.SDL_GetWindowFlags(Pointer) & (uint)SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS)
        //    == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS) ? true : false;
        get
        {
            uint flags = SDL.SDL_GetWindowFlags(Point),
                 flag_focus = (uint)SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS;

            return (flags & flag_focus) == flag_focus;
        }
    }

    public bool Resizeble
    {
        get
        {
            uint flags = SDL.SDL_GetWindowFlags(Point),
                 flag_resize = (uint)SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

            return (flags & flag_resize) == flag_resize;
        }
        set
        {
            if (value)
                SDL.SDL_SetWindowResizable(Point, SDL.SDL_bool.SDL_TRUE);
            else
                SDL.SDL_SetWindowResizable(Point, SDL.SDL_bool.SDL_FALSE);
        }
    }
    #endregion

    #region Window Loop
    public bool Quit { get; set; } = false;
    public float Framerate { get; set; } = 30;
    public Core.Timer WindowTimer { get; set; } = new Core.Timer();
    public float DeltaTime { get; protected set; } = 0f;

    public void Run()
    {
        /*MouseMove += (sender, args) =>
        {
            Components.SendEvent(EventType.MouseMove, args);
        };
        MouseButtonPress += (sender, args) =>
        {
            Components.SendEvent(EventType.MouseButton, args);
        };*/
        OnInitialization(this);
        /*new Thread(Loop).Start();*/
        Loop();
    }

    protected void Loop()
    {
        WindowTimer.Start();
        Components.Subscribe(this);
        Components.InitializationComponents();

        while (!Quit)
        {
            ulong start_tick = WindowTimer.Ticks;

            Dispatcher.Dispatch();

            Renderer.Target = null;
            Renderer.Clear();

            Components.Step();

            Renderer.Present();

            ulong end_tick = WindowTimer.Ticks;
            DeltaTime = (end_tick - start_tick) / 1000.0f;

            float elapsed = (end_tick - start_tick) / (float)SDL.SDL_GetPerformanceFrequency();
            float tick_time = 1000 / Framerate;

            int sleep_time = (int)Math.Floor(tick_time - (elapsed * 1000.0f));
            Thread.Sleep(sleep_time);
        }
    }
    #endregion

    #region Creation Window
    protected static nint CreateWindow(
        string title,
        int x,
        int y,
        int w,
        int h,
        WindowContext context)
    {
        VideoInitialize();

        SDL.SDL_WindowFlags flags;

        if (context.StartupHidden)
            flags = SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN;
        else
            flags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;

        if (context.Resizable)
            flags |= SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;

        if (context.AlwaysOnTop)
            flags |= SDL.SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP;

        if (context.Borderless)
            flags |= SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS;

        if (context.SkipTaskbar)
            flags |= SDL.SDL_WindowFlags.SDL_WINDOW_SKIP_TASKBAR;

        switch (context.Type)
        {
            case WindowType.PopupMenu:
                flags |= SDL.SDL_WindowFlags.SDL_WINDOW_POPUP_MENU;
                break;

            case WindowType.Tooltip:
                flags |= SDL.SDL_WindowFlags.SDL_WINDOW_TOOLTIP;
                break;

            case WindowType.Utility:
                flags |= SDL.SDL_WindowFlags.SDL_WINDOW_UTILITY;
                break;
        }

        switch (context.State)
        {
            case WindowState.Fullscreen:
                flags |= SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
                break;

            case WindowState.Minimize:
                flags |= SDL.SDL_WindowFlags.SDL_WINDOW_MINIMIZED;
                break;

            case WindowState.Maximize:
                flags |= SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED;
                break;
        }

        switch (context.Video)
        {
            case VideoContext.OpenGL:
                flags |= SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL;
                break;

            case VideoContext.Vulkan:
                flags |= SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN;
                break;
        }

        nint handle = SDL.SDL_CreateWindow(title, x, y, w, h, flags);

        if (handle == nint.Zero)
            throw new WindowCreationException();

        WindowCount++;

        return handle;
    }

    protected static void ConfigurateWindow(nint window, WindowContext context)
    {
        SDL.SDL_SetWindowOpacity(window, context.Opacity);

        SDL.SDL_SetWindowMaximumSize(window, context.MaximumSize.X, context.MaximumSize.Y);
        SDL.SDL_SetWindowMinimumSize(window, context.MinimumSize.X, context.MinimumSize.Y);

        SDL.SDL_SetWindowResizable(window, context.Resizable.SDL_Bool());
        SDL.SDL_SetWindowAlwaysOnTop(window, context.AlwaysOnTop.SDL_Bool());
        SDL.SDL_SetWindowBordered(window, context.Borderless.SDL_Bool());
    }
    #endregion

    #region SDL Video System
    public static bool IsVideoInitialize { get; protected set; } = false;
    public static byte WindowCount { get; protected set; } = 0;

    protected static void VideoInitialize()
    {
        if (!IsVideoInitialize)
            if (SDL.SDL_InitSubSystem(SDL.SDL_INIT_VIDEO) != 0)
                throw new VideoInitializationException();
            else
                IsVideoInitialize = true;
    }

    protected override void Destroy()
    {
        SDL.SDL_DestroyWindow(Point);

        Window.WindowCount--;
        if (Window.WindowCount <= 0)
        {
            SDL.SDL_VideoQuit();
            Window.IsVideoInitialize = false;
        }
    }
    #endregion

    #region Eventable
    protected WindowEventDispatcher Dispatcher { get; set; }

    public event EventHandler? Initialization;
    public event EventHandler? Close;
    public event EventHandler<SizeEventArgs>? Resize;
    public event EventHandler<SizeEventArgs>? SizeChange;
    public event EventHandler<SizeEventArgs>? Restore;
    public event EventHandler<SizeEventArgs>? Minimize;
    public event EventHandler<SizeEventArgs>? Maximize;
    public event EventHandler? LostFocus;
    public event EventHandler? GainedFocus;
    public event EventHandler? TakeFocus;
    public event EventHandler? ClipboardUpdate;
    public event EventHandler<KeyEventArgs>? KeyPress;
    public event EventHandler<KeyEventArgs>? KeyRelease;
    public event EventHandler<TextEventArgs>? TextEnter;
    public event EventHandler<MouseButtonEventArgs>? MouseButtonPress;
    public event EventHandler<MouseButtonEventArgs>? MouseButtonRelease;
    public event EventHandler<MouseMoveEventArgs>? MouseMove;
    public event EventHandler<MouseWheelScrollEventArgs>? MouseWheelScroll;
    public event EventHandler<DropFileEventArgs>? DropFile;
    public event EventHandler<DropTextEventArgs>? DropText;
    public event EventHandler? DropBegin;
    public event EventHandler? DropComplete;
    public event EventHandler<AudioDeviceEventArgs>? AudioDeviceConnect;
    public event EventHandler<AudioDeviceEventArgs>? AudioDeviceDisconnect;

    public void OnInitialization(object sender)
    {
        Initialization?.Invoke(sender, EventArgs.Empty);
    }

    public void OnClose(object sender)
    {
        Close?.Invoke(sender, EventArgs.Empty);
    }

    public void OnResize(object sender, SizeEventArgs e)
    {
        Resize?.Invoke(sender, e);
    }

    public void OnSizeChange(object sender, SizeEventArgs e)
    {
        SizeChange?.Invoke(sender, e);
    }

    public void OnRestore(object sender, SizeEventArgs e)
    {
        Restore?.Invoke(sender, e);
    }

    public void OnMinimize(object sender, SizeEventArgs e)
    {
        Minimize?.Invoke(sender, e);
    }

    public void OnMaximize(object sender, SizeEventArgs e)
    {
        Maximize?.Invoke(sender, e);
    }

    public void OnLostFocus(object sender)
    {
        LostFocus?.Invoke(sender, EventArgs.Empty);
    }

    public void OnGainedFocus(object sender)
    {
        GainedFocus?.Invoke(sender, EventArgs.Empty);
    }

    public void OnTakeFocus(object sender)
    {
        TakeFocus?.Invoke(sender, EventArgs.Empty);
    }

    public void OnClipboardUpdate(object sender)
    {
        ClipboardUpdate?.Invoke(sender, EventArgs.Empty);
    }

    public void OnKeyPress(object sender, KeyEventArgs e)
    {
        KeyPress?.Invoke(sender, e);
    }

    public void OnKeyRelease(object sender, KeyEventArgs e)
    {
        KeyRelease?.Invoke(sender, e);
    }

    public void OnTextEnter(object sender, TextEventArgs e)
    {
        TextEnter?.Invoke(sender, e);
    }

    public void OnMouseButtonPress(object sender, MouseButtonEventArgs e)
    {
        MouseButtonPress?.Invoke(sender, e);
    }

    public void OnMouseButtonRelease(object sender, MouseButtonEventArgs e)
    {
        MouseButtonRelease?.Invoke(sender, e);
    }

    public void OnMouseMove(object sender, MouseMoveEventArgs e)
    {
        MouseMove?.Invoke(sender, e);
    }

    public void OnMouseWheelScroll(object sender, MouseWheelScrollEventArgs e)
    {
        MouseWheelScroll?.Invoke(sender, e);
    }

    public void OnDropFile(object sender, DropFileEventArgs e)
    {
        DropFile?.Invoke(sender, e);
    }

    public void OnDropText(object sender, DropTextEventArgs e)
    {
        DropText?.Invoke(sender, e);
    }

    public void OnDropBegin(object sender)
    {
        DropBegin?.Invoke(sender, EventArgs.Empty);
    }

    public void OnDropComplete(object sender)
    {
        DropComplete?.Invoke(sender, EventArgs.Empty);
    }

    public void OnAudioDeviceConnect(object sender, AudioDeviceEventArgs e)
    {
        AudioDeviceConnect?.Invoke(sender, e);
    }

    public void OnAudioDeviceDisconnect(object sender, AudioDeviceEventArgs e)
    {
        AudioDeviceDisconnect?.Invoke(sender, e);
    }
    #endregion
}
