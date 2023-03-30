using Collary.Native.SDL2;
using Collary.Windowing;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Collary.Core;

public class EventHost : Base
{
    protected List<IEventable> Targets { get; set; }

    public EventHost()
    {
        EventHost.InitializeEvents();

        this.Targets = new List<IEventable>();
    }

    public void AddTarget(IEventable target)
    {
        this.Targets.Add(target);
    }

    public void Dispatch()
    {
        while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
            foreach(IEventable target in this.Targets)
                this.EventsCaller(target, e);
    }

    public void Dispatch(int wait_milliseconds)
    {
        Thread.Sleep(wait_milliseconds);
        this.Dispatch();
    }

    protected void EventsCaller(IEventable target, SDL.SDL_Event e)
    {
        switch (e.type)
        {
            case SDL.SDL_EventType.SDL_QUIT:
                target.OnClose(this);
                break;

            case SDL.SDL_EventType.SDL_WINDOWEVENT:
                switch (e.window.windowEvent)
                {
                    case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                        target.OnResize(this, new SizeEventArgs(e.window));
                        break;

                    case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                        target.OnSizeChange(this, new SizeEventArgs(e.window));
                        break;

                    case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                        target.OnRestore(this, new SizeEventArgs(e.window));
                        break;

                    case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                        target.OnMinimize(this, new SizeEventArgs(e.window));
                        break;

                    case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                        target.OnMaximize(this, new SizeEventArgs(e.window));
                        break;

                    case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                        target.OnGainedFocus(this);
                        break;

                    case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                        target.OnLostFocus(this);
                        break;

                    case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_TAKE_FOCUS:
                        target.OnTakeFocus(this);
                        break;
                }
                break;

            case SDL.SDL_EventType.SDL_KEYDOWN:
                target.OnKeyPress(this, new KeyEventArgs(e.key.keysym));
                break;

            case SDL.SDL_EventType.SDL_KEYUP:
                target.OnKeyRelease(this, new KeyEventArgs(e.key.keysym));
                break;

            case SDL.SDL_EventType.SDL_TEXTINPUT:
                target.OnTextEnter(this, new TextEventArgs(e.text));
                break;

            case SDL.SDL_EventType.SDL_MOUSEMOTION:
                target.OnMouseMove(this, new MouseMoveEventArgs(e.motion));
                break;

            case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                target.OnMouseButtonPress(this, new MouseButtonEventArgs(e.button));
                break;

            case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                target.OnMouseButtonRelease(this, new MouseButtonEventArgs(e.button));
                break;

            case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                target.OnMouseWheelScroll(this, new MouseWheelScrollEventArgs(e.wheel));
                break;

            case SDL.SDL_EventType.SDL_CLIPBOARDUPDATE:
                target.OnClipboardUpdate(this);
                break;

            case SDL.SDL_EventType.SDL_DROPFILE:
                target.OnDropFile(this, new DropFileEventArgs(e.drop));
                break;

            case SDL.SDL_EventType.SDL_DROPTEXT:
                target.OnDropText(this, new DropTextEventArgs(e.drop));
                break;

            case SDL.SDL_EventType.SDL_DROPBEGIN:
                target.OnDropBegin(this);
                break;

            case SDL.SDL_EventType.SDL_DROPCOMPLETE:
                target.OnDropComplete(this);
                break;

            case SDL.SDL_EventType.SDL_AUDIODEVICEADDED:
                target.OnAudioDeviceConnect(this, new AudioDeviceEventArgs(e.adevice));
                break;

            case SDL.SDL_EventType.SDL_AUDIODEVICEREMOVED:
                target.OnAudioDeviceDisconnect(this, new AudioDeviceEventArgs(e.adevice));
                break;
        }
    }

    #region Initialize and Destroy SDL Event Subsystem
    protected static bool HasEventsInitialize { get; private set; } = false;
    protected static int HostsCount { get; private set; } = 0;

    protected static void InitializeEvents()
    {
        if (!EventHost.HasEventsInitialize)
            if (SDL.SDL_InitSubSystem(SDL.SDL_INIT_EVENTS) != 0)
                throw new EventsInitializationException();
            else
                EventHost.HasEventsInitialize = true;
        
        EventHost.HostsCount++;
    }

    protected override void Destroy()
    {
        EventHost.HostsCount--;
        if (EventHost.HostsCount <= 0)
        {
            SDL.SDL_QuitSubSystem(SDL.SDL_INIT_EVENTS);
            EventHost.HasEventsInitialize = false;
        }
    }
    #endregion
}

[Serializable]
public class EventsInitializationException : Exception
{
    public EventsInitializationException() : base($"Events Initialization error! \nMessage: {SDL.SDL_GetError()}") { }
}