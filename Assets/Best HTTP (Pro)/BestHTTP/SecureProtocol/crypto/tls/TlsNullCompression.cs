#if !BESTHTTP_DISABLE_ALTERNATE_SSL

using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	public class TlsNullCompression
		: TlsCompression
	{
		public virtual Stream Compress(Stream output)
		{
			return output;
		}

		public virtual Stream Decompress(Stream output)
		{
			return output;
		}
	}
}

#endif
