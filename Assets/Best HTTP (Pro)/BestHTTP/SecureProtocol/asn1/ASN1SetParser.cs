#if !BESTHTTP_DISABLE_ALTERNATE_SSL
namespace Org.BouncyCastle.Asn1
{
	public interface Asn1SetParser
		: IAsn1Convertible
	{
		IAsn1Convertible ReadObject();
	}
}

#endif
