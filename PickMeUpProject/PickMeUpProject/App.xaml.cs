﻿using PickMeUpProject.Common;
using System;
using PickMeUpProject.ViewModels;
using PickMeUpProject.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ApplicationSettings;
using Callisto.Controls;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Notifications;
using Windows.Networking.PushNotifications;
using Windows.Security.Cryptography;
using System.Net.Http;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace PickMeUpProject
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        private Color _background = Color.FromArgb(255, 1, 107, 207);
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
            {
                if (!String.IsNullOrEmpty(args.Arguments))
                {
                    DMArticleDetailsViewModel model = new DMArticleDetailsViewModel();

                    string[] arguments = args.Arguments.Split(';');
                    model.Title = arguments[0];
                    model.Description = arguments[1];
                    model.Link = arguments[2];
                    model.Content = arguments[3];
                    ((Frame)Window.Current.Content).Navigate(typeof(ArticleContentPage), model);
                }
                    
                Window.Current.Activate();
                return;
            }

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                    await SuspensionManager.RestoreAsync();
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (args.PreviousExecutionState == ApplicationExecutionState.ClosedByUser)
            {
                if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("Remember"))
                {
                    bool remember = (bool)ApplicationData.Current.RoamingSettings.Values["Remember"];
                    if (remember)
                        await SuspensionManager.RestoreAsync();
                }
            }


            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(Views.HomePage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;

            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Clear();

            // Register for push notifications
            var profile = NetworkInformation.GetInternetConnectionProfile();

            if (profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
            {
            //    var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            //    var buffer = CryptographicBuffer.ConvertStringToBinary(channel.Uri, BinaryStringEncoding.Utf8);
            //    var uri = CryptographicBuffer.EncodeToBase64String(buffer);
            //    var client = new HttpClient();

            //    try
            //    {
                    
            //        var response = await client.GetAsync(new Uri("http://greatday.com?uri=" + uri + "&type=tile"));
            //        if (!response.IsSuccessStatusCode)
            //        {
            //            var dialog = new MessageDialog("Unable to open push notification channel");
            //            dialog.ShowAsync();
            //        }
            //    }
            //    catch (HttpRequestException)
            //    {
            //        var dialog = new MessageDialog("Unable to open push notification channel");
            //        dialog.ShowAsync();
            //    }
            }

            if (!String.IsNullOrEmpty(args.Arguments))
            {
                DMArticleDetailsViewModel model = new DMArticleDetailsViewModel();
                string[] arguments = args.Arguments.Split(';');
                model.Title = arguments[0];
                model.Description = arguments[1];
                model.Link = arguments[2];
                model.Content = arguments[3];
                rootFrame.Navigate(typeof(ArticleContentPage), model);
                Window.Current.Content = rootFrame;
                Window.Current.Activate();
                return;
            }


            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        /// <summary>
        /// Invoked when the application is activated to display search results.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected async override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
        {
            // TODO: Register the Windows.ApplicationModel.Search.SearchPane.GetForCurrentView().QuerySubmitted
            // event in OnWindowCreated to speed up searches once the application is already running

            // If the Window isn't already using Frame navigation, insert our own Frame
            var previousContent = Window.Current.Content;
            var frame = previousContent as Frame;

            // If the app does not contain a top-level frame, it is possible that this 
            // is the initial launch of the app. Typically this method and OnLaunched 
            // in App.xaml.cs can call a common method.
            if (frame == null)
            {
                // Create a Frame to act as the navigation context and associate it with
                // a SuspensionManager key
                frame = new Frame();
                PickMeUpProject.Common.SuspensionManager.RegisterFrame(frame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await PickMeUpProject.Common.SuspensionManager.RestoreAsync();
                    }
                    catch (PickMeUpProject.Common.SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }
            }

            frame.Navigate(typeof(Views.SearchResultsPage), args.QueryText);
            Window.Current.Content = frame;
            SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;
            // Ensure the current window is active
            Window.Current.Activate();
        }

        void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            // Add an About command
            var about = new SettingsCommand("about", "About", (handler) =>
            {
                var settings = new SettingsFlyout();
                settings.Content = new Views.AboutUserControl();
                settings.HeaderBrush = new SolidColorBrush(_background);
                settings.Background = new SolidColorBrush(_background);
                settings.HeaderText = "About";
                settings.IsOpen = true;
            });

            args.Request.ApplicationCommands.Add(about);

            var preferences = new SettingsCommand("preferences", "Preferences", (handler) =>
            {
                var settings = new SettingsFlyout();
                settings.Content = new Views.PreferencesUserControl();
                settings.HeaderBrush = new SolidColorBrush(_background);
                settings.Background = new SolidColorBrush(_background);
                settings.HeaderText = "Preferences";
                settings.IsOpen = true;
            });

            args.Request.ApplicationCommands.Add(preferences);

            

        }
    }
}