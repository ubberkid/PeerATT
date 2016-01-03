#if !BESTHTTP_DISABLE_ALTERNATE_SSL

using System;

namespace Org.BouncyCastle.Crypto.Tls
{
    public abstract class AbstractTlsCredentials
        :   TlsCredentials
    {
        public abstract Certificate Certificate { get; }
    }
}

#endif
