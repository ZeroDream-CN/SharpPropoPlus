using System;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Threading;
using MvvmDialogs;
using SharpPropoPlus.Audio;
using SharpPropoPlus.Audio.Enums;
using SharpPropoPlus.Audio.EventArguments;
using SharpPropoPlus.Decoder;
//using SharpPropoPlus.Helpers;
using SharpPropoPlus.Interfaces;
using SharpPropoPlus.vJoyMonitor;
using SharpPropoPlus.ViewModels;
using SharpPropoPlus.Filter;
using SharpPropoPlus.Views;
using Unity;
using Unity.Lifetime;

namespace SharpPropoPlus
{
    public class Application : IDisposable
    {
        private static Application _instance;
        private static readonly object Sync = new object();
        private IUnityContainer _container;

        private Application()
        {
            //_decoderManager = new DecoderManager();

            Container.RegisterType<IDecoderManager, DecoderManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFilterManager, FilterManager>(new ContainerControlledLifetimeManager());

            DecoderManager = Container.Resolve<DecoderManager>();
            FilterManager = Container.Resolve<FilterManager>();

            JoystickHelper.Initialize();
            JoystickInteraction.Initialize();

            //TODO: Remove, this is only for testing...
            AudioHelper.Instance.DataAvailable += AudioDataAvailable;
        }

        //TODO: Remove, this is only for testing...
        private void AudioDataAvailable(object o, AudioDataEventArgs args)
        {
            var samplesDesired = args.BytesRecorded / args.Channels;
            var sampleRate = args.SampleRate;
            var bitsPerSample = args.BitsPerSample;
            var channels = args.Channels;

            var left = new int[samplesDesired];
            var right = new int[samplesDesired];
            var index = 0;

            for (var sample = 0; sample < args.BytesRecorded / (2 * channels); sample++)
            {
                switch (channels)
                {
                    case 1:
                        right[sample] = BitConverter.ToInt16(args.Buffer, index);
                        index += 2;
                        break;
                    case 2:
                    default:
                        left[sample] = BitConverter.ToInt16(args.Buffer, index);
                        index += 2;
                        right[sample] = BitConverter.ToInt16(args.Buffer, index);
                        index += 2;
                        break;
                }

                var data = (args.PreferedChannel == AudioChannel.Left && channels > 1) ? left[sample] : right[sample];

                DecoderManager.Decoder.ProcessPulse(sampleRate, data, FilterManager.IsEnabled, FilterManager.Filter);
            }
        }

        public static Application Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Sync)
                    {
                        if (_instance == null)
                            _instance = new Application();
                    }
                }

                return _instance;
            }
        }

        public IDecoderManager DecoderManager { get; }

        public IFilterManager FilterManager { get; }

        public IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (Sync)
                    {
                        if (_container == null)
                            _container = new UnityContainer();
                    }
                }

                return _container;
            }
        }

        public void ShowMainWindow()
        {
            try {
                Console.WriteLine("debug 1");
                Container.RegisterInstance<IDialogService>(new DialogService(), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.1");
                Container.RegisterInstance<IShellViewModel>(new ShellViewModel(), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.2");
                Container.RegisterInstance<IAudioConfigViewModel>(new AudioConfigViewModel(), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.3");
                Container.RegisterInstance<IJoystickConfigViewModel>(new JoystickConfigViewModel(), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.4");
                Container.RegisterInstance<ITransmitterConfigViewModel>(new TransmitterConfigViewModel(), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.5");
                Container.RegisterInstance<IFilterConfigViewModel>(new FilterConfigViewModel(), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.6");
                Container.RegisterType<IAdvancedConfigViewModel, AdvancedConfigViewModel>();
                
                Console.WriteLine("debug 1.7");
                Container.RegisterType<ILoggingTabViewModel, LoggingTabViewModel>();
                
                Console.WriteLine("debug 1.8");
                Container.RegisterType<IAboutTabViewModel, AboutTabViewModel>();
                
                Console.WriteLine("debug 1.9");
                Container.RegisterType(typeof(UserControl), typeof(AudioConfig), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.91");
                Container.RegisterType(typeof(UserControl), typeof(FilterConfig), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.92");
                Container.RegisterType(typeof(UserControl), typeof(JoystickConfig), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.93");
                Container.RegisterType(typeof(UserControl), typeof(TransmitterConfig), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.94");
                Container.RegisterType(typeof(UserControl), typeof(AdvancedConfig), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.95");
                Container.RegisterType(typeof(UserControl), typeof(LoggingTab), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 1.96");
                Container.RegisterType(typeof(UserControl), typeof(AboutTab), new ContainerControlledLifetimeManager());
                
                Console.WriteLine("debug 2");
                var mainWindow = Container.Resolve<Shell>(); // Creating Main window
                
                Console.WriteLine("debug 3");
                mainWindow.Loaded += (sender, args) =>
                {
                    //var windowHandle = new WindowInteropHelper(mainWindow).Handle;

                    //var source = HwndSource.FromHwnd(windowHandle);
                    //source?.AddHook(WndProc);

                    //DeviceNotification.RegisterDeviceNotification(windowHandle, true);
                };

                
                Console.WriteLine("debug 4");
                //System.Windows.Application.Current.MainWindow = mainWindow;
                mainWindow.Show();

                //System.Windows.Application.Current.MainWindow.Show();
            } catch(Exception ex) {
                Console.WriteLine(ex);
            }
        }

        //private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    //  do stuff
        //    if (msg == DeviceNotification.WM_DEVICECHANGE)
        //    {
        //        var action = (int) wParam;

        //        if (lParam.Equals(IntPtr.Zero) && !wParam.Equals(IntPtr.Zero))
        //        {

        //        }

        //        else if (!lParam.Equals(IntPtr.Zero) && !wParam.Equals(IntPtr.Zero))
        //        {
        //            var pHdr = Marshal.PtrToStructure<DeviceNotification.DEV_BROADCAST_HDR>(lParam);

        //            switch (pHdr.dbch_DeviceType)
        //            {
        //                case DeviceNotification.DbtDevtypDeviceinterface:

        //                    var pDevInf =
        //                        Marshal.PtrToStructure<DeviceNotification.DEV_BROADCAST_DEVICEINTERFACE>(lParam);

        //                    switch (action)
        //                    {
        //                        case DeviceNotification.DBT_DEVICEARRIVAL:
        //                            // do something...
        //                            break;
        //                        case DeviceNotification.DBT_DEVICEREMOVECOMPLETE:
        //                            // do something...
        //                            break;
        //                    }
        //                    break;

        //                //case DBT_DEVTYP_HANDLE:
        //                //    PDEV_BROADCAST_HANDLE pDevHnd = (PDEV_BROADCAST_HANDLE)pHdr;
        //                //    // do something...
        //                //    break;

        //                //case DBT_DEVTYP_OEM:
        //                //    PDEV_BROADCAST_OEM pDevOem = (PDEV_BROADCAST_OEM)pHdr;
        //                //    // do something...
        //                //    break;

        //                //case DBT_DEVTYP_PORT:
        //                //    PDEV_BROADCAST_PORT pDevPort = (PDEV_BROADCAST_PORT)pHdr;
        //                //    // do something...
        //                //    break;

        //                //case DBT_DEVTYP_VOLUME:
        //                //    PDEV_BROADCAST_VOLUME pDevVolume = (PDEV_BROADCAST_VOLUME)pHdr;
        //                //    // do something...
        //                //    break;
        //            }
        //        }

        //    }
        //    return IntPtr.Zero;
        //}

        public void Dispose()
        {
            AudioHelper.Instance.DataAvailable -= AudioDataAvailable;
            AudioHelper.Instance.Dispose();
            JoystickHelper.Instance.Dispose();
            //GlobalEventAggregator.Instance.AddListener<AudioDataEventArgs>(AudioDataAction);
        }
    }
}