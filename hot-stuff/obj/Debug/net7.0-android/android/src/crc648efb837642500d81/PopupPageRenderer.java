package crc648efb837642500d81;


public class PopupPageRenderer
	extends crc6452ffdc5b34af3a0f.ContentViewGroup
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\n" +
			"n_onDetachedFromWindow:()V:GetOnDetachedFromWindowHandler\n" +
			"n_onWindowVisibilityChanged:(I)V:GetOnWindowVisibilityChanged_IHandler\n" +
			"n_dispatchTouchEvent:(Landroid/view/MotionEvent;)Z:GetDispatchTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"n_onTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("Mopups.Platforms.Android.Renderers.PopupPageRenderer, Mopups", PopupPageRenderer.class, __md_methods);
	}


	public PopupPageRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == PopupPageRenderer.class) {
			mono.android.TypeManager.Activate ("Mopups.Platforms.Android.Renderers.PopupPageRenderer, Mopups", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public PopupPageRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == PopupPageRenderer.class) {
			mono.android.TypeManager.Activate ("Mopups.Platforms.Android.Renderers.PopupPageRenderer, Mopups", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public PopupPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == PopupPageRenderer.class) {
			mono.android.TypeManager.Activate ("Mopups.Platforms.Android.Renderers.PopupPageRenderer, Mopups", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public PopupPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == PopupPageRenderer.class) {
			mono.android.TypeManager.Activate ("Mopups.Platforms.Android.Renderers.PopupPageRenderer, Mopups", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);


	public void onAttachedToWindow ()
	{
		n_onAttachedToWindow ();
	}

	private native void n_onAttachedToWindow ();


	public void onDetachedFromWindow ()
	{
		n_onDetachedFromWindow ();
	}

	private native void n_onDetachedFromWindow ();


	public void onWindowVisibilityChanged (int p0)
	{
		n_onWindowVisibilityChanged (p0);
	}

	private native void n_onWindowVisibilityChanged (int p0);


	public boolean dispatchTouchEvent (android.view.MotionEvent p0)
	{
		return n_dispatchTouchEvent (p0);
	}

	private native boolean n_dispatchTouchEvent (android.view.MotionEvent p0);


	public boolean onTouchEvent (android.view.MotionEvent p0)
	{
		return n_onTouchEvent (p0);
	}

	private native boolean n_onTouchEvent (android.view.MotionEvent p0);

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
