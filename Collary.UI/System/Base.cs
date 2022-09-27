using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.UI.System;

public class Base : IDisposable
{
    public IntPtr Pointer { get; set; }

    public Base() : this(IntPtr.Zero) { }

    public Base(IntPtr pointer)
    {
        this.Pointer = pointer;
    }

    ~ Base()
    {
        this.Dispose();
    }

    protected virtual void Destroy() { }

    public void Dispose()
    {
        this.Destroy();
    }
}
