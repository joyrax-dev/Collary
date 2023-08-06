using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics.Text;

public enum FontHinting : int
{
    Normal = TTF.TTF_HINTING_NORMAL,
    Light = TTF.TTF_HINTING_LIGHT,
    LightSubpixel = TTF.TTF_HINTING_LIGHT_SUBPIXEL,
    Mono = TTF.TTF_HINTING_MONO,
    None = TTF.TTF_HINTING_NONE
}
