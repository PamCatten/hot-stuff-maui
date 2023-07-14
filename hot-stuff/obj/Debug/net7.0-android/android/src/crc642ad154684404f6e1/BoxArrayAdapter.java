package crc642ad154684404f6e1;


public class BoxArrayAdapter
	extends android.widget.ArrayAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("UraniumUI.Handlers.BoxArrayAdapter, UraniumUI", BoxArrayAdapter.class, __md_methods);
	}


	public BoxArrayAdapter (android.content.Context p0, int p1)
	{
		super (p0, p1);
		if (getClass () == BoxArrayAdapter.class) {
			mono.android.TypeManager.Activate ("UraniumUI.Handlers.BoxArrayAdapter, UraniumUI", "Android.Content.Context, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public BoxArrayAdapter (android.content.Context p0, int p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == BoxArrayAdapter.class) {
			mono.android.TypeManager.Activate ("UraniumUI.Handlers.BoxArrayAdapter, UraniumUI", "Android.Content.Context, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public BoxArrayAdapter (android.content.Context p0, int p1, int p2, java.util.List p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == BoxArrayAdapter.class) {
			mono.android.TypeManager.Activate ("UraniumUI.Handlers.BoxArrayAdapter, UraniumUI", "Android.Content.Context, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib:System.Collections.IList, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public BoxArrayAdapter (android.content.Context p0, int p1, int p2, java.lang.Object[] p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == BoxArrayAdapter.class) {
			mono.android.TypeManager.Activate ("UraniumUI.Handlers.BoxArrayAdapter, UraniumUI", "Android.Content.Context, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib:Java.Lang.Object[], Mono.Android", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public BoxArrayAdapter (android.content.Context p0, int p1, java.util.List p2)
	{
		super (p0, p1, p2);
		if (getClass () == BoxArrayAdapter.class) {
			mono.android.TypeManager.Activate ("UraniumUI.Handlers.BoxArrayAdapter, UraniumUI", "Android.Content.Context, Mono.Android:System.Int32, System.Private.CoreLib:System.Collections.IList, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public BoxArrayAdapter (android.content.Context p0, int p1, java.lang.Object[] p2)
	{
		super (p0, p1, p2);
		if (getClass () == BoxArrayAdapter.class) {
			mono.android.TypeManager.Activate ("UraniumUI.Handlers.BoxArrayAdapter, UraniumUI", "Android.Content.Context, Mono.Android:System.Int32, System.Private.CoreLib:Java.Lang.Object[], Mono.Android", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
