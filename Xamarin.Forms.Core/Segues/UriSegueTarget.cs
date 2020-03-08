using System;

namespace Xamarin.Forms
{
	public sealed class UriSegueTarget : SegueTarget
	{
		readonly Uri uri;

		public UriSegueTarget(Uri uri)
		{
			if (uri == null)
				throw new ArgumentNullException(nameof(uri));
			this.uri = uri;
		}

		public Uri ToUri()
		{
			return uri;
		}
	}
}
