using Collary.Framework.Core;
using Collary.Framework.Windowing;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public class WindowEventDispatcher : Base
{
    #region Constructors
    protected Window? RefWindow { get; set; }

    public WindowEventDispatcher(Window window)
    {
        EventInitialize();
        RefWindow = window;
    }
    #endregion
    #region Dispatching
    public void Dispatch()
    {
        while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
            EventsCaller(e);
    }

    public void Dispatch(int wait_milliseconds)
    {
        Thread.Sleep(wait_milliseconds);
        Dispatch();
    }

    protected void EventsCaller(SDL.SDL_Event e)
    {
        if (e.window.windowID == RefWindow?.Id)
        {
            switch (e.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    RefWindow?.OnClose(RefWindow);
                    break;

                case SDL.SDL_EventType.SDL_WINDOWEVENT:
                    switch (e.window.windowEvent)
                    {
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                            RefWindow?.OnResize(RefWindow, new SizeEventArgs(e.window));
                            break;

                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                            RefWindow?.OnSizeChange(RefWindow, new SizeEventArgs(e.window));
                            break;

                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                            RefWindow?.OnRestore(RefWindow, new SizeEventArgs(e.window));
                            break;

                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                            RefWindow?.OnMinimize(RefWindow, new SizeEventArgs(e.window));
                            break;

                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                            RefWindow?.OnMaximize(RefWindow, new SizeEventArgs(e.window));
                            break;

                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                            RefWindow?.OnGainedFocus(RefWindow);
                            break;

                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                            RefWindow?.OnLostFocus(RefWindow);
                            break;

                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_TAKE_FOCUS:
                            RefWindow?.OnTakeFocus(RefWindow);
                            break;
                    }
                    break;

                case SDL.SDL_EventType.SDL_KEYDOWN:
                    RefWindow?.OnKeyPress(RefWindow, new KeyEventArgs(e.key.keysym));
                    break;

                case SDL.SDL_EventType.SDL_KEYUP:
                    RefWindow?.OnKeyRelease(RefWindow, new KeyEventArgs(e.key.keysym));
                    break;

                case SDL.SDL_EventType.SDL_TEXTINPUT:
                    RefWindow?.OnTextEnter(RefWindow, new TextEventArgs(e.text));
                    break;

                case SDL.SDL_EventType.SDL_MOUSEMOTION:
                    RefWindow?.OnMouseMove(RefWindow, new MouseMoveEventArgs(e.motion));
                    break;

                case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    RefWindow?.OnMouseButtonPress(RefWindow, new MouseButtonEventArgs(e.button));
                    break;

                case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                    RefWindow?.OnMouseButtonRelease(RefWindow, new MouseButtonEventArgs(e.button));
                    break;

                case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                    RefWindow?.OnMouseWheelScroll(RefWindow, new MouseWheelScrollEventArgs(e.wheel));
                    break;

                case SDL.SDL_EventType.SDL_CLIPBOARDUPDATE:
                    RefWindow?.OnClipboardUpdate(RefWindow);
                    break;

                case SDL.SDL_EventType.SDL_DROPFILE:
                    RefWindow?.OnDropFile(RefWindow, new DropFileEventArgs(e.drop));
                    break;

                case SDL.SDL_EventType.SDL_DROPTEXT:
                    RefWindow?.OnDropText(RefWindow, new DropTextEventArgs(e.drop));
                    break;

                case SDL.SDL_EventType.SDL_DROPBEGIN:
                    RefWindow?.OnDropBegin(RefWindow);
                    break;

                case SDL.SDL_EventType.SDL_DROPCOMPLETE:
                    RefWindow?.OnDropComplete(RefWindow);
                    break;

                case SDL.SDL_EventType.SDL_AUDIODEVICEADDED:
                    RefWindow?.OnAudioDeviceConnect(RefWindow, new AudioDeviceEventArgs(e.adevice));
                    break;

                case SDL.SDL_EventType.SDL_AUDIODEVICEREMOVED:
                    RefWindow?.OnAudioDeviceDisconnect(RefWindow, new AudioDeviceEventArgs(e.adevice));
                    break;
            }
        }
    }
    #endregion
    #region SDL Event System
    protected static bool IsEventsInitialize { get; private set; } = false;
    protected static int DispatchersCount { get; private set; } = 0;

    protected static void EventInitialize()
    {
        if (!IsEventsInitialize)
            if (SDL.SDL_InitSubSystem(SDL.SDL_INIT_EVENTS) != 0)
                throw new EventsInitializationException();
            else
                IsEventsInitialize = true;

        DispatchersCount++;
    }

    protected override void Destroy()
    {
        DispatchersCount--;
        if (DispatchersCount <= 0)
        {
            SDL.SDL_QuitSubSystem(SDL.SDL_INIT_EVENTS);
            IsEventsInitialize = false;
        }
    }
    #endregion
}
