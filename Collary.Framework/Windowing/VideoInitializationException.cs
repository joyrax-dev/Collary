using Collary.Framework.Core;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Windowing;

[Serializable]
public class VideoInitializationException : CollaryException
{
    public VideoInitializationException() : base($"Video Initialization error! \nMessage: {SDL.SDL_GetError()}", new VideoInitializationException()) { }
}
