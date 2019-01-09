# Permissions.Xamarin
This project is an example to check and request for permissions in Xamarin Forms app.

## Getting Started
To check and request for Permissions, we're using a [Plugin.Permissions](https://www.nuget.org/packages/Plugin.Permissions/). You can go through the nuget's documentation for more info. Install the nuget package in Android and iOS project. You can check out this [video tutorial](https://www.youtube.com/watch?v=ng7oWHmi9Gs&t=2s) to see the code in action

##### Checking Permission status
**CheckPermissionStatusAsync** method will provide the status of the Permission on the device. For example, to check Permissions for Calendar, here's something you can use

```
var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Calendar);
```

##### Requesting Permissions
**RequestPermissionsAsync** method can be used to request for any particular permissions. You can also request for multiple permissions at the same time.

```
var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Calendar);
```
If the User has denied the permission, you might not be able to request for permissions again. The behavior is different on Android and iOS.

## Android Implementation
To get started, first we need to make sure that the permissions our application will be required are added in **AndroidManifest.XML**

```
	<uses-permission android:name="android.permission.READ_CALENDAR" />
	<uses-permission android:name="android.permission.WRITE_CALENDAR" />
	<uses-permission android:name="android.permission.READ_CONTACTS" />
	<uses-permission android:name="android.permission.WRITE_CONTACTS" />
	<uses-permission android:name="android.permission.CAMERA" />
	<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
	<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
```

We need to override **OnRequestPermissionsResult** method in **MainActivity.cs** with this code

```
 public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
```

Now, we need to create **MainApplication.cs** class and with the following code

```
 [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            //A great place to initialize Xamarin.Insights and Dependency Services!
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
```

Android is now ready to be tested.

## iOS Implementation
Just like **AndroidManifest.xml**, we need to specify the permissions in **Info.plist**. If not specified, our application won't be able to access the state of Permission and will crash.

```
Privacy - Calendars Usage Description
Privacy - Camera Usage Description
```

After specifying all required permissions, we're all set to set our iOS application. 

## License
This project is open source.

##### Thank you
