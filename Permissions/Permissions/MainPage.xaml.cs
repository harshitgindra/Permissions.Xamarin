using System;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Permissions
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Task.Run(async () =>
            {
                int commandParameter = Convert.ToInt32(((Button)sender).CommandParameter);

                Permission permission = (Permission)commandParameter;
                var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if (permissionStatus != PermissionStatus.Granted)
                {
                    var response = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                    var userResponse = response[permission];

                    Debug.WriteLine($"Permission {permission} {userResponse}");
                }
                else
                {
                    Debug.WriteLine($"Permission {permission} {permissionStatus}");
                }
            });
        }
    }
}
