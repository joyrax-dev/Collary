using Collary.Framework.Core;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Windowing;

[Serializable]
public class WindowCreationException : CollaryException
{
    public WindowCreationException() : base($"Window creation error! \nSDL Message: {SDL.SDL_GetError()}", new WindowCreationException()) { }
}
