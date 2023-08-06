using Collary.Framework.Events;
using Collary.Framework.Graphics;
using Collary.Framework.Graphics.Components;
using Collary.Framework.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

public class SampleApplication
{
    public bool Quit { get; protected set; }
    public float Framerate { get; set; }
    protected Window Win { get; set; }
    protected Renderer Ren { get; set; }
    public ComponentHost ComponentHost { get; protected set; }
    public Timer ApplicationTimer { get; protected set; }
    public Color WindowBackground { get; set; }

    public static float DeltaTime { get; protected set; }


    public SampleApplication(Window window)
    {
        this.Quit = false;
        this.Framerate = 30;
        this.WindowBackground = new Color(10, 10, 10);

        this.Win = window;
        this.Win.Close += (_, __) => this.Quit = true;
        this.Ren = this.Win.Renderer;
    }
}
