using Collary.Native.SDL2;
using System;

namespace Collary.UI.Windowing;

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