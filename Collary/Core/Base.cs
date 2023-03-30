using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Core;

public class Base : IDisposable
{
    public nint Pointer { get; set; }

    public Base() : this(nint.Zero) { }

    public Base(nint pointer)
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
