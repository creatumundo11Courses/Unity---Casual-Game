using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationsController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(RequestNotificationPermission());
        RegisterNotification();
        CancelAllNotifications();
        SentNotification("My Casual Game","Multiply your fun, play again!",1);
    }

  
    IEnumerator RequestNotificationPermission()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
            yield return null;

        //...
    }


    private void RegisterNotification()
    {
        var group = new AndroidNotificationChannelGroup()
        {
            Id = "Main",
            Name = "Main notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannelGroup(group);
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
            Group = "Main",  // must be same as Id of previously registered group
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
    private void CancelAllNotifications()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
    }


    private void SentNotification(string title, string text, int daysToShow)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = DateTime.Now.AddDays(daysToShow);
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";

        AndroidNotificationCenter.SendNotification(notification, "channel_id");

    }

}
