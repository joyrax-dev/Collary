using Collary.Native.SDL2;
using Collary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Core;

public class Timer
{
    protected ulong StartTicks { get; set;} = 0;
    protected ulong PausedTicks { get; set; } = 0;

    protected bool Started { get; set; } = false;
    protected bool Paused { get; set; } = false;

    public ulong Ticks 
    {
        get 
        {
            if (!this.Started)
                return 0;

            if (this.Paused)
                return this.PausedTicks;
            else
                return SDL.SDL_GetTicks64() - this.StartTicks;
        }
    }

    public float FromSeconds
    {
        get
        {
            return this.Ticks / 1000.0f;
        }
    }

    public void Start()
    {
        this.Started = true;
        this.Paused = false;

        this.PausedTicks = 0;
        this.StartTicks = SDL.SDL_GetTicks64();
    }

    public void Stop()
    {
        this.Started = false;
        this.Paused = false;

        this.PausedTicks = 0;
        this.StartTicks = 0;
    }

    public void Pause()
    {
        if (this.Started && !this.Paused)
        {
            this.Paused = true;

            this.PausedTicks = SDL.SDL_GetTicks64() - this.StartTicks;
            this.StartTicks = 0;
        }
    }

    public void Unpause()
    {
        if (this.Started && this.Paused)
        {
            this.Paused = false;

            this.StartTicks = SDL.SDL_GetTicks64() - this.PausedTicks;
            this.PausedTicks = 0;
        }
    }
}