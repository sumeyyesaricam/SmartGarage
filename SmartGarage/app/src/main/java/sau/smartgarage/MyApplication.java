package sau.smartgarage;

import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.util.Log;

import org.altbeacon.beacon.Beacon;
import org.altbeacon.beacon.BeaconConsumer;
import org.altbeacon.beacon.BeaconManager;
import org.altbeacon.beacon.BeaconParser;
import org.altbeacon.beacon.Region;
import org.altbeacon.beacon.powersave.BackgroundPowerSaver;
import org.altbeacon.beacon.startup.BootstrapNotifier;
import org.altbeacon.beacon.startup.RegionBootstrap;

/**
 * Created by Sümeyye on 3.12.2016.
 */

public class MyApplication extends Application implements BootstrapNotifier, BeaconConsumer {

    private static final String TAG = "BeaconApp";
    private RegionBootstrap regionBootstrap;
    private BackgroundPowerSaver backgroundPowerSaver;
    private boolean haveDetectedBeaconsSinceBoot = false;
    private MainActivity mainActivity = null;
    Beacon firstbeacon;

    private BeaconManager beaconManager = null;
    public void onCreate() {
        super.onCreate();
        beaconManager = org.altbeacon.beacon.BeaconManager.getInstanceForApplication(this);
        beaconManager.getBeaconParsers().add(new BeaconParser().
                setBeaconLayout("m:2-3=0215,i:4-19,i:20-21,i:22-23,p:24-24,d:25-25"));


        Log.d(TAG, "setting up background monitoring for beacons and power saving");
        // wake up the app when a beacon is seen
        Region region = new Region("backgroundRegion", null, null, null);
        regionBootstrap = new RegionBootstrap(this, region);
        backgroundPowerSaver = new BackgroundPowerSaver(this);
    }

    public void didEnterRegion(Region arg0) {

        Log.d(TAG, "did enter region.");
        // regionBootstrap.disable();
        if (!haveDetectedBeaconsSinceBoot) {
            Log.d(TAG, "auto launching MainActivity");

            // The very first time since boot that we detect an beacon, we launch the MainActivity
            Intent intent = new Intent(this, MainActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            this.startActivity(intent);
            haveDetectedBeaconsSinceBoot = true;
        } else {

            //sendNotification();
        }

    }

    public void didExitRegion(Region region) {}
    public void didDetermineStateForRegion(int state, Region region) {}
    public void setMainActivity(MainActivity activity) {this.mainActivity = activity;}

    @Override
    public void onBeaconServiceConnect() {

        //beaconsize olanset range notifier
        //firstbeacon null sa ilk defa gördü yada o değere eşit değilse beacon değiştirdi get ıd2=major ıd3=minör
        beaconManager.setBackgroundMode(true);
    }
}
