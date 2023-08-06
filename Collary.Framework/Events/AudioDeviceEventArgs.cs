using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Events;

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
