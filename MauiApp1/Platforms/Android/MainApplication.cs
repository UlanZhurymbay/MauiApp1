using Android.App;
using Android.Media;
using Android.OS;
using Android.Runtime;
using CommunityToolkit.Mvvm.Messaging;

namespace MauiApp1;

[Application]
public class MainApplication : MauiApplication
{
    public static readonly string OrderId = "Order";
    public static readonly string OrderId2 = "Order2";
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
        var messenger = MauiApplication.Current.Services.GetService<IMessenger>();
    }

    public override void OnCreate()
    {
		try
		{
            var order = new NotificationChannel(OrderId, "Order", NotificationImportance.High);
            var alarmAttributes = new AudioAttributes.Builder()
                .SetContentType(AudioContentType.Sonification)
                .SetUsage(AudioUsageKind.Notification)
                .Build();
            var order2 = new NotificationChannel(OrderId, "Order2", NotificationImportance.High);
            var alarmAttributes2 = new AudioAttributes.Builder()
                .SetContentType(AudioContentType.Music)
                .SetUsage(AudioUsageKind.Alarm)
                .Build();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
#pragma warning disable CA1416

                var uri = Android.Net.Uri.Parse($"{Android.Content.ContentResolver.SchemeAndroidResource}//{Context.PackageName}/{Resource.Raw.order_1}");


                order.SetSound(uri, alarmAttributes);
                order2.SetSound(uri, alarmAttributes2);
                if (GetSystemService(NotificationService) is NotificationManager manager)
                {
                    manager.CreateNotificationChannel(order);
                    manager.CreateNotificationChannel(order2);
                }
                base.OnCreate();
            }
        }
		catch (Exception exe)
		{
            App.Current.MainPage.DisplayAlert("OnCreate", exe.Message, "sadad");
		}
        base.OnCreate();
    }
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
