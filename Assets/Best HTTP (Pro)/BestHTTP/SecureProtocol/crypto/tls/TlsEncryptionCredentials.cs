#if !BESTHTTP_DISABLE_ALTERNATE_SSL

using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
    public interface TlsEncryptionCredentials
        :   TlsCredentials
    {
        /// <exception cref="IOException"></exception>
        byte[] DecryptPreMasterSecret(byte[] encryptedPreMasterSecret);
    }
}

#endif
