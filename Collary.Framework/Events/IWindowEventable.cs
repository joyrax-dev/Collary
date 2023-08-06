using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

public interface IWindowEventable
{
    public event EventHandler Initialization;
    public event EventHandler Close;
    public event EventHandler<SizeEventArgs> Resize;
    public event EventHandler<SizeEventArgs> SizeChange;
    public event EventHandler<SizeEventArgs> Restore;
    public event EventHandler<SizeEventArgs> Minimize;
    public event EventHandler<SizeEventArgs> Maximize;
    public event EventHandler LostFocus;
    public event EventHandler GainedFocus;
    public event EventHandler TakeFocus; //  SDL_SetWindowInputFocus ()
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

    public void OnInitialization(object sender);
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
