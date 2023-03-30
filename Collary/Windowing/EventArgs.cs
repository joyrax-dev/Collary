using Collary.Native.SDL2;
using Collary.Core;
using System;
using System.Numerics;

namespace Collary.Windowing;

public class AudioDeviceEventArgs : EventArgs
{
    public uint DeviceIndex;
    public bool IsCapture;

    public AudioDeviceEventArgs(SDL.SDL_AudioDeviceEvent e)
    {
        this.DeviceIndex = e.which;

        if (e.iscapture == 0)
            this.IsCapture = false;
        else
            this.IsCapture = true;
    }
}

public class DropFileEventArgs : EventArgs
{
    public string File;

    public DropFileEventArgs(SDL.SDL_DropEvent e)
    {
        this.File = SDL.UTF8_ToManaged(e.file, true);
    }
}

public class DropTextEventArgs : EventArgs
{
    public string Text;

    public DropTextEventArgs(SDL.SDL_DropEvent e)
    {
        this.Text = SDL.UTF8_ToManaged(e.file, true);
    }
}

public class KeyEventArgs : EventArgs
{
    public SDL.SDL_Scancode Scancode;
    public SDL.SDL_Keycode Keycode;
    public SDL.SDL_Keymod Keymod;

    public KeyEventArgs(SDL.SDL_Keysym keysym)
    {
        this.Scancode = keysym.scancode;
        this.Keycode = keysym.sym;
        this.Keymod = keysym.mod;
    }
}

public class MouseButtonEventArgs : EventArgs
{
    public Button Button;
    public byte Clicks;
    public Vector2 Position;

    public MouseButtonEventArgs(SDL.SDL_MouseButtonEvent e)
    {
        this.Clicks = e.clicks;
        this.Position = new Vector2(e.x, e.y);

        switch (e.button)
        {
            case (byte)Button.Left:
                this.Button = Button.Left;
                break;

            case (byte)Button.Middle:
                this.Button = Button.Middle;
                break;

            case (byte)Button.Right:
                this.Button = Button.Right;
                break;

            case (byte)Button.X1:
                this.Button = Button.X1;
                break;

            case (byte)Button.X2:
                this.Button = Button.X2;
                break;
        }
    }
}

public class MouseMoveEventArgs : EventArgs
{
    public Button Button;
    public Vector2 Position;
    public Vector2 RelativePosition;

    public MouseMoveEventArgs(SDL.SDL_MouseMotionEvent e)
    {
        this.Position = new Vector2(e.x, e.y);
        this.RelativePosition = new Vector2(e.xrel, e.yrel);

        switch (e.state)
        {
            case (byte)Button.Left:
                this.Button = Button.Left;
                break;

            case (byte)Button.Middle:
                this.Button = Button.Middle;
                break;

            case (byte)Button.Right:
                this.Button = Button.Right;
                break;

            case (byte)Button.X1:
                this.Button = Button.X1;
                break;

            case (byte)Button.X2:
                this.Button = Button.X2;
                break;
        }
    }
}

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

public class SizeEventArgs : EventArgs
{
    public int Width;
    public int Height;

    public SizeEventArgs(SDL.SDL_WindowEvent e)
    {
        this.Width = e.data1;
        this.Height = e.data2;
    }
}

public unsafe class TextEventArgs : EventArgs
{
    public string Text;

    public TextEventArgs(SDL.SDL_TextInputEvent e)
    {
        this.Text = SDL.UTF8_ToManaged((IntPtr)e.text);
    }
}