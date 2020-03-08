using System;
using System.ComponentModel;

using Xamarin.Forms.Xaml;

namespace Xamarin.Forms
{
	public abstract class SegueTarget
	{
		internal SegueTarget()
		{
		}

		// UriSegueTarget:
		public static implicit operator SegueTarget(Uri uri) => (uri == null) ? null : new UriSegueTarget(uri);
		public static implicit operator SegueTarget(string str)
			=> (str == null) ? null : new UriSegueTarget(ShellUriHandler.CreateUri(str));

		// PageSegueTarget:
		public static implicit operator SegueTarget(Type ty) => (ty == null) ? null : new PageSegueTarget(new DataTemplate(ty));
		public static implicit operator SegueTarget(DataTemplate dt) => (dt == null) ? null : new PageSegueTarget(dt);

		// The conversion from Page is explicit because we don't want people accidently
		//  using raw Page in XAML when they should be using DataTemplate..
		public static explicit operator SegueTarget(Page page) => (page == null) ? null : new PageSegueTarget(page);
	}
}
