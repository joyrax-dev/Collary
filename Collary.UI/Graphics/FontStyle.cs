using Collary.Native.SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Collary.UI.Graphics;

public enum FontStyle : int
{
    Normal        = TTF.TTF_STYLE_NORMAL,
    Bold          = TTF.TTF_STYLE_BOLD,
    Italic        = TTF.TTF_STYLE_ITALIC,
    Underline     = TTF.TTF_STYLE_UNDERLINE,
    StrikeThrough = TTF.TTF_STYLE_STRIKETHROUGH
}
