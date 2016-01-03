#if !BESTHTTP_DISABLE_ALTERNATE_SSL

using System;

namespace Org.BouncyCastle.Math.EC
{
    public interface ECPointMap
    {
        ECPoint Map(ECPoint p);
    }
}

#endif
