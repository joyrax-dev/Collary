using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

public class Base : IDisposable
{
    public nint Point { get; set; }

    public Base() : this(nint.Zero) { }

    public Base(nint point)
    {
        Point = point;
    }

    ~Base()
    {
        Dispose();
    }

    protected virtual void Destroy() { }

    public void Dispose()
    {
        Destroy();
        GC.SuppressFinalize(this);
    }
}