using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace HotStuff.Platforms.Android;

public class CustomShellRenderer : ShellRenderer
{
    protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
    {
        return new CustomToolbarAppearanceTracker();
    }
}

internal class CustomToolbarAppearanceTracker : IShellToolbarAppearanceTracker
{
    public void Dispose()
    {
    }

    public void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
    {
        toolbar.OverflowIcon?.SetTint(appearance.TitleColor.ToAndroid());
    }

    public void ResetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker)
    {
    }
}
