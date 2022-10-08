using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collary.UI.Windowing;

namespace Collary.UI.System;

public interface IEventable
{
    // Closw Event
    public event EventHandler Close;

    // Window Size Events
    public event EventHandler<SizeEventArgs> Resize;
    public event EventHandler<SizeEventArgs> SizeChange;
    public event EventHandler<SizeEventArgs> Restore;
    public event EventHandler<SizeEventArgs> Minimize;
    public event EventHandler<SizeEventArgs> Maximize;

    // Focus Events
    public event EventHandler LostFocus;
    public event EventHandler GainedFocus;
    public event EventHandler TakeFocus; //  SDL_SetWindowInputFocus ()

    // Clipboard Event
    public event EventHandler ClipboardUpdate;

    // Key Events
    public event EventHandler<KeyEventArgs> KeyPress;
    public event EventHandler<KeyEventArgs> KeyRelease;

    // Text Event
    public event EventHandler<TextEventArgs> TextEnter;

    // Mouse Events
    public event EventHandler<MouseButtonEventArgs> MouseButtonPress;
    public event EventHandler<MouseButtonEventArgs> MouseButtonRelease;
    public event EventHandler<MouseMoveEventArgs> MouseMove;
    public event EventHandler<MouseWheelScrollEventArgs> MouseWheelScroll;

    // Drop Events
    public event EventHandler<DropFileEventArgs> DropFile;
    public event EventHandler<DropTextEventArgs> DropText;
    public event EventHandler DropBegin;
    public event EventHandler DropComplete;

    // Audio Device Events
    public event EventHandler<AudioDeviceEventArgs> AudioDeviceConnect;
    public event EventHandler<AudioDeviceEventArgs> AudioDeviceDisconnect;

    // Callers Section
    public void OnClose(object sender);
    public void OnResize(object sender, SizeEventArgs e);
    public void OnSizeChange(object sender, SizeEventArgs e);
    public void OnRestore(object sender, SizeEventArgs e);
    public void OnMinimize(object sender, SizeEventArgs e);
    public void OnMaximize(object sender, SizeEventArgs e);
    public void OnLostFocus(object sendere);
    public void OnGainedFocus(object sender);
    public void OnTakeFocus(object sender);
    public void OnClipboardUpdate(object sender);
    public void OnKeyPress(object sender, KeyEventArgs e);
    public void OnKeyRelease(object sender, KeyEventArgs e);
    public void OnTextEnter(object sender, TextEventArgs e);
    public void OnMouseButtonPress(object sender, MouseButtonEventArgs e);
    public void OnMouseButtonRelease(object sender, MouseButtonEventArgs e);
    public void OnMouseMove(object sender, MouseMoveEventArgs e);
    public void OnMouseWheelScroll(object sender, MouseWheelScrollEventArgs e);
    public void OnDropFile(object sender, DropFileEventArgs e);
    public void OnDropText(object sender, DropTextEventArgs e);
    public void OnDropBegin(object sender);
    public void OnDropComplete(object sender);
    public void OnAudioDeviceConnect(object sender, AudioDeviceEventArgs e);
    public void OnAudioDeviceDisconnect(object sender, AudioDeviceEventArgs e);
}
