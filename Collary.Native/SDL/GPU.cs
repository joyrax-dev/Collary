using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Collary.Native.SDL.SDL;

namespace Collary.Native.SDL;

public static class GPU
{
    #region SDL_GPU# Variables

    private const string nativeLibName = "SDL2_gpu";

    #endregion

    public enum GPU_bool
    {
        GPU_FALSE = 0,
        GPU_TRUE = 1
    }

    /*! \ingroup Rendering
     * A struct representing a rectangular area with floating point precision.
     * \see GPU_MakeRect() 
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct GPU_Rect
    {
        public float x, y, w, h;
    }

    public enum GPU_RendererEnum : uint
    {
        GPU_RENDERER_UNKNOWN = 0,
        GPU_RENDERER_OPENGL_1_BASE = 1,
        GPU_RENDERER_OPENGL_1 = 2,
        GPU_RENDERER_OPENGL_2 = 3,
        GPU_RENDERER_OPENGL_3 = 4,
        GPU_RENDERER_OPENGL_4 = 5,
        GPU_RENDERER_GLES_1 = 11,
        GPU_RENDERER_GLES_2 = 12,
        GPU_RENDERER_GLES_3 = 13,
        GPU_RENDERER_D3D9 = 21,
        GPU_RENDERER_D3D10 = 22,
        GPU_RENDERER_D3D11 = 23
    }

    /*! \ingroup Initialization
     * \ingroup RendererSetup
     * \ingroup RendererControls
     * Renderer ID object for identifying a specific renderer.
     * \see GPU_MakeRendererID()
     * \see GPU_InitRendererByID()
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct GPU_RendererID
    {
        nint name;
        GPU_RendererEnum renderer;
        int major_version;
        int minor_version;
    }

    /*! \ingroup TargetControls
     * Comparison operations (for depth testing)
     * \see GPU_SetDepthFunction()
     * Values chosen for direct OpenGL compatibility.
     */
    public enum GPU_ComparisonEnum
    {
        GPU_NEVER = 0x0200,
        GPU_LESS = 0x0201,
        GPU_EQUAL = 0x0202,
        GPU_LEQUAL = 0x0203,
        GPU_GREATER = 0x0204,
        GPU_NOTEQUAL = 0x0205,
        GPU_GEQUAL = 0x0206,
        GPU_ALWAYS = 0x0207
    }

    /*! \ingroup ImageControls
     * Blend component functions
     * \see GPU_SetBlendFunction()
     * Values chosen for direct OpenGL compatibility.
     */
    public enum GPU_BlendFuncEnum
    {
        GPU_FUNC_ZERO = 0,
        GPU_FUNC_ONE = 1,
        GPU_FUNC_SRC_COLOR = 0x0300,
        GPU_FUNC_DST_COLOR = 0x0306,
        GPU_FUNC_ONE_MINUS_SRC = 0x0301,
        GPU_FUNC_ONE_MINUS_DST = 0x0307,
        GPU_FUNC_SRC_ALPHA = 0x0302,
        GPU_FUNC_DST_ALPHA = 0x0304,
        GPU_FUNC_ONE_MINUS_SRC_ALPHA = 0x0303,
        GPU_FUNC_ONE_MINUS_DST_ALPHA = 0x0305
    }

    /*! \ingroup ImageControls
     * Blend component equations
     * \see GPU_SetBlendEquation()
     * Values chosen for direct OpenGL compatibility.
     */
    public enum GPU_BlendEqEnum
    {
        GPU_EQ_ADD = 0x8006,
        GPU_EQ_SUBTRACT = 0x800A,
        GPU_EQ_REVERSE_SUBTRACT = 0x800B
    }

    /*! \ingroup ImageControls
     * Blend mode storage struct */
    [StructLayout(LayoutKind.Sequential)]
    public struct GPU_BlendMode
    {
        GPU_BlendFuncEnum source_color;
        GPU_BlendFuncEnum dest_color;
        GPU_BlendFuncEnum source_alpha;
        GPU_BlendFuncEnum dest_alpha;

        GPU_BlendEqEnum color_equation;
        GPU_BlendEqEnum alpha_equation;
    }

    /*! \ingroup ImageControls
     * Blend mode presets 
     * \see GPU_SetBlendMode()
     * \see GPU_GetBlendModeFromPreset()
     */
    public enum GPU_BlendPresetEnum
    {
        GPU_BLEND_NORMAL = 0,
        GPU_BLEND_PREMULTIPLIED_ALPHA = 1,
        GPU_BLEND_MULTIPLY = 2,
        GPU_BLEND_ADD = 3,
        GPU_BLEND_SUBTRACT = 4,
        GPU_BLEND_MOD_ALPHA = 5,
        GPU_BLEND_SET_ALPHA = 6,
        GPU_BLEND_SET = 7,
        GPU_BLEND_NORMAL_KEEP_ALPHA = 8,
        GPU_BLEND_NORMAL_ADD_ALPHA = 9,
        GPU_BLEND_NORMAL_FACTOR_ALPHA = 10
    }

    /*! \ingroup ImageControls
     * Image filtering options.  These affect the quality/interpolation of colors when images are scaled. 
     * \see GPU_SetImageFilter()
     */
    public enum GPU_FilterEnum
    {
        GPU_FILTER_NEAREST = 0,
        GPU_FILTER_LINEAR = 1,
        GPU_FILTER_LINEAR_MIPMAP = 2
    }

    /*! \ingroup ImageControls
     * Snap modes.  Blitting with these modes will align the sprite with the target's pixel grid.
     * \see GPU_SetSnapMode()
     * \see GPU_GetSnapMode()
     */
    public enum GPU_SnapEnum
    {
        GPU_SNAP_NONE = 0,
        GPU_SNAP_POSITION = 1,
        GPU_SNAP_DIMENSIONS = 2,
        GPU_SNAP_POSITION_AND_DIMENSIONS = 3
    }

    /*! \ingroup ImageControls
     * Image wrapping options.  These affect how images handle src_rect coordinates beyond their dimensions when blitted.
     * \see GPU_SetWrapMode()
     */
    public enum GPU_WrapEnum
    {
        GPU_WRAP_NONE = 0,
        GPU_WRAP_REPEAT = 1,
        GPU_WRAP_MIRRORED = 2
    }

    /*! \ingroup ImageControls
     * Image format enum
     * \see GPU_CreateImage()
     */
    public enum GPU_FormatEnum
    {
        GPU_FORMAT_LUMINANCE = 1,
        GPU_FORMAT_LUMINANCE_ALPHA = 2,
        GPU_FORMAT_RGB = 3,
        GPU_FORMAT_RGBA = 4,
        GPU_FORMAT_ALPHA = 5,
        GPU_FORMAT_RG = 6,
        GPU_FORMAT_YCbCr422 = 7,
        GPU_FORMAT_YCbCr420P = 8,
        GPU_FORMAT_BGR = 9,
        GPU_FORMAT_BGRA = 10,
        GPU_FORMAT_ABGR = 11
    }

    /*! \ingroup ImageControls
     * File format enum
     * \see GPU_SaveSurface()
     * \see GPU_SaveImage()
     * \see GPU_SaveSurface_RW()
     * \see GPU_SaveImage_RW()
     */
    public enum GPU_FileFormatEnum
    {
        GPU_FILE_AUTO = 0,
        GPU_FILE_PNG,
        GPU_FILE_BMP,
        GPU_FILE_TGA
    }

        // return GPU_Target
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint GPU_CreateTargetFromWindow(uint windowID);

    // return GPU_Image
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint GPU_CopyImageFromSurface(nint surface);

    // return GPU_Image
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint GPU_CopyImageFromSurfaceRect(nint surface, GPU_Rect surface_rect);

    // return GPU_Image
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint GPU_CopyImageFromTarget(nint target);

    // return SDL_Surface
    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint GPU_CopySurfaceFromTarget(nint target);
}
