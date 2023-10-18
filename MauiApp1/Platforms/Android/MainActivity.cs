using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using AndroidX.Core.App;
using CommunityToolkit.Mvvm.Messaging;

namespace MauiApp1;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private NotificationManagerCompat _notificationManager;
    public MainActivity()
    {
        var messenger = MauiApplication.Current.Services.GetService<IMessenger>();

        messenger.Register<Order_1>(this, (recipient, message) =>
        {
            if (message.message == "1")
            {
                SendOnChannel1(message.message, message.message);
            }
            else
            {
                SendOnChannel2(message.message, message.message);
            }
        });
    }

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        _notificationManager = NotificationManagerCompat.From(this);
    }
    private void SendOnChannel1(string title, string message)
    {
        var notification = new NotificationCompat.Builder(this, MainApplication.OrderId)
          .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
          .SetContentTitle(title)
          .SetContentText(message)
          .SetPriority(NotificationCompat.PriorityHigh)
          .SetCategory(NotificationCompat.CategoryMessage)
          .Build();

        _notificationManager.Notify(1, notification);
    }
    private void SendOnChannel2(string title, string message)
    {
        var alarmAttributes = new AudioAttributes.Builder()
            .SetContentType(AudioContentType.Sonification)
            ?.SetUsage(AudioUsageKind.Alarm)
            ?.Build();
        var uri = Android.Net.Uri.Parse($"{Android.Content.ContentResolver.SchemeAndroidResource}//{Android.App.Application.Context.PackageName}/{Resource.Raw.order_1}");

        var notification = new NotificationCompat.Builder(this, MainApplication.OrderId2)
          .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
          .SetContentTitle(title)
          .SetContentText(message)
          .SetPriority(NotificationCompat.PriorityHigh)
          .SetCategory(NotificationCompat.CategoryMessage)
          .SetSound(uri)
          .Build();

        _notificationManager.Notify(2, notification);
    }
}
