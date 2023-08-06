using Collary.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Windowing;

public struct WindowContext
{
    public float Opacity { get; set; }

    public Vector2i MaximumSize { get; set; }
    public Vector2i MinimumSize { get; set; }

    public bool Resizable { get; set; }
    public bool StartupHidden { get; set; }
    public bool AlwaysOnTop { get; set; }
    public bool Borderless { get; set; }
    public bool SkipTaskbar { get; set; }

    public WindowType Type { get; set; }
    public WindowState State { get; set; }
    public VideoContext Video { get; set; }

    public WindowContext()
    {
        
    }

    public static WindowContext Default
    {
        get
        {
            return new WindowContext()
            {
                Opacity = 1.0f,

                MaximumSize = new Vector2i(1920, 1080),
                MinimumSize = new Vector2i(300, 300),

                Resizable = true,
                StartupHidden = false,
                AlwaysOnTop = false,
                Borderless = true,
                SkipTaskbar = false,

                Type = WindowType.Normal,
                State = WindowState.Restore,
                Video = VideoContext.Default
            };
        }
    }
}