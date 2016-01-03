#if !BESTHTTP_DISABLE_ALTERNATE_SSL

using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	public interface TlsCompression
	{
		Stream Compress(Stream output);

		Stream Decompress(Stream output);
	}
}

#endif
