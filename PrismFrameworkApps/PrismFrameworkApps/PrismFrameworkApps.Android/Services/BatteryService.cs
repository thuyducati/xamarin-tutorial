﻿using Android.App;
using Android.Content;
using Android.OS;
using PrismFrameworkApps.src._08_NavigationPages.Services;
using Debug = System.Diagnostics.Debug;

namespace PrismFrameworkApps.Droid.Services
{
    class BatteryService : IBatteryService
    {
        public string GetBatteryStatus()
        {
            try
            {
                using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                {
                    using (var battery = Application.Context.RegisterReceiver(null, filter))
                    {
                        int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);

                        var isCharging = status == (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full;

                        var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);

                        var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                        var acCharge = chargePlug == (int)BatteryPlugged.Ac;

                        bool wirelessCharge = false;

                        wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                        isCharging = (usbCharge || acCharge || wirelessCharge);

                        if (isCharging)
                        {
                            return "Charging";
                        }

                        switch (status)
                        {
                            case (int)BatteryStatus.Charging:
                                return "Charging";

                            case (int)BatteryStatus.Discharging:
                                return "Discharging";

                            case (int)BatteryStatus.Full:
                                return "Full";

                            case (int)BatteryStatus.NotCharging:
                                return "Not Charging";

                            default:
                                return "Unknown";
                        }
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");

                throw;
            }
        }
    }
}