using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics.Text;

public enum FontStyle : int
{
    Normal = TTF.TTF_STYLE_NORMAL,
    Bold = TTF.TTF_STYLE_BOLD,
    Italic = TTF.TTF_STYLE_ITALIC,
    Underline = TTF.TTF_STYLE_UNDERLINE,
    StrikeThrough = TTF.TTF_STYLE_STRIKETHROUGH
}