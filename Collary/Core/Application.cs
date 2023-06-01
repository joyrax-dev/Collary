using System;
using System.Collections.Generic;
using Collary.Core;
using Collary.Graphics;
using Collary.Windowing;
using Collary.Components;
using Collary.Native.SDL2;

namespace Collary.Core;

public class Application
{
    public bool Quit { get; protected set; }
    public float Framerate { get; set; }
    protected Window Win { get; set; }
    protected Renderer Ren { get; set; }
    protected EventHost EventHost { get; set; }
    public ComponentHost ComponentHost { get; protected set; }
    public Timer ApplicationTimer { get; protected set; }
    public Color WindowBackground { get; set; }

    public static float DeltaTime { get; protected set; }

    public Application(Window window)
    {
        this.Quit = false;
        this.Framerate = 30;
        this.WindowBackground = new Color(0, 0, 0);

        this.Win = window;
        this.Win.Close += (_, __) => this.Quit = true;
        this.Ren = this.Win.Renderer;

        string fonts_folder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        Font font = new Font($"{fonts_folder}/segoeui.ttf", 16);
        this.Ren.Font = font;

        this.EventHost = new EventHost();
        this.EventHost.AddTarget(this.Win);

        this.ComponentHost = new ComponentHost();

        this.ApplicationTimer = new Timer();
    }

    public void Run()
    {
        this.ApplicationTimer.Start();

        while(!this.Quit)
        {
            ulong start = this.ApplicationTimer.Ticks;

            this.Ren.Clear(this.WindowBackground);

            this.EventHost.Dispatch();

            this.ComponentHost.Step(this.Ren);

            this.Ren.Present();

            ulong end = this.ApplicationTimer.Ticks;
            Application.DeltaTime = (end - start) / 1000.0f;

            float elapsed = (end - start) / (float)SDL.SDL_GetPerformanceFrequency();

            float tick_time = 1000 / this.Framerate;
            SDL.SDL_Delay((uint)Math.Floor(tick_time - (elapsed * 1000.0f)));
        }
    }
}