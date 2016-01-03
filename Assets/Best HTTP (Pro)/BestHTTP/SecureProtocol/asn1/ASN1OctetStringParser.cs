#if !BESTHTTP_DISABLE_ALTERNATE_SSL
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	public interface Asn1OctetStringParser
		: IAsn1Convertible
	{
		Stream GetOctetStream();
	}
}

#endif
