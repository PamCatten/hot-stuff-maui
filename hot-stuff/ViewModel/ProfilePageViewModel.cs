using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotStuff.ViewModel;

public partial class ProfilePageViewModel : ObservableObject
{
    public ICommand BuildingSettingsCommand { get; protected set; }
    public ICommand OpenBuildingSettingsPopupCommand { get; protected set; }
    public ICommand OpenAddBuildingPopupCommand { get; protected set; }
    public ICommand AddBuildingCommand { get; protected set; }

    public ProfilePageViewModel()
    {
        BuildingSettingsCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(BuildingSettingsPage));
        });
    }
}
