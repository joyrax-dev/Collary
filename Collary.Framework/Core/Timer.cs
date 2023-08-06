using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

public class Timer
{
    protected ulong StartTicks { get; set; } = 0;
    protected ulong PausedTicks { get; set; } = 0;

    protected bool Started { get; set; } = false;
    protected bool Paused { get; set; } = false;

    public ulong Ticks
    {
        get
        {
            if (!Started)
                return 0;

            if (Paused)
                return PausedTicks;
            else
                return SDL.SDL_GetTicks64() - StartTicks;
        }
    }

    public float FromSeconds
    {
        get
        {
            return Ticks / 1000.0f;
        }
    }

    public void Start()
    {
        Started = true;
        Paused = false;

        PausedTicks = 0;
        StartTicks = SDL.SDL_GetTicks64();
    }

    public void Stop()
    {
        Started = false;
        Paused = false;

        PausedTicks = 0;
        StartTicks = 0;
    }

    public void Pause()
    {
        if (Started && !Paused)
        {
            Paused = true;

            PausedTicks = SDL.SDL_GetTicks64() - StartTicks;
            StartTicks = 0;
        }
    }

    public void Unpause()
    {
        if (Started && Paused)
        {
            Paused = false;

            StartTicks = SDL.SDL_GetTicks64() - PausedTicks;
            PausedTicks = 0;
        }
    }
}