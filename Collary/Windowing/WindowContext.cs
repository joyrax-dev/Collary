using System.Numerics;
using Collary.Core;

namespace Collary.Windowing;

// TODO: Add Constructors
public struct WindowContext
{
    public float Opacity       { get; set; }

    public Vector2i MaximumSize { get; set; }
    public Vector2i MinimumSize { get; set; }

    public bool Resizable      { get; set; }
    public bool StartupHidden  { get; set; }
    public bool AlwaysOnTop    { get; set; }
    public bool Borderless     { get; set; }
    public bool SkipTaskbar    { get; set; }

    public WindowType Type     { get; set; }
    public WindowState State   { get; set; }
    public VideoContext Video  { get; set; }

    public WindowContext()
    {
        this.Opacity = 1.0f;

        this.MaximumSize = new Vector2i(1920, 1080);
        this.MinimumSize = new Vector2i(300, 300);

        this.Resizable = true;
        this.StartupHidden = false;
        this.AlwaysOnTop = false;
        this.Borderless = false;
        this.SkipTaskbar = false;

        this.Type = WindowType.Normal;
        this.State = WindowState.Restore;
        this.Video = VideoContext.Default;
    }
}
