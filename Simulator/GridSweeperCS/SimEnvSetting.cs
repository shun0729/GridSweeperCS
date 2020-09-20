
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSweeperCS
{
    static public class SimEnvSetting
    {
        public static int FIRE_SPRED_POSSIBILITY_CENTER_LV1 = 5;
        public static int FIRE_SPRED_POSSIBILITY_SIDE_LV1 = 3;
        public static int FIRE_SPRED_POSSIBILITY_CENTER_LV2 = 5;
        public static int FIRE_SPRED_POSSIBILITY_SIDE_LV2 = 3;
        public static int FIRE_SPRED_POSSIBILITY_CENTER_LV3 = 5;
        public static int FIRE_SPRED_POSSIBILITY_SIDE_LV3 = 3;
        public static int FIRE_SPRED_POSSIBILITY_CENTER_LV4 = 7;
        public static int FIRE_SPRED_POSSIBILITY_SIDE_LV4 = 5;
        public static int FIRE_SPRED_POSSIBILITY_CENTER_LV5 = 9;
        public static int FIRE_SPRED_POSSIBILITY_SIDE_LV5 = 7;

        public static int truckWorkIntervalMilliSec = 5000;
        public static int truckWithdrawIntervalMilliSec = 5000;

        public static int helpLatencyTimeMIlliSec = 10000;
        public static int supportFireTruckNum = 3;

        public static int fireSpredIntervalSec = 99999999;


        static public void setFireSpredIntervalSec(int windLevel)
        {
            switch (windLevel)
            {
                case 1:
                    fireSpredIntervalSec = 25;
                    break;
                case 2:
                    fireSpredIntervalSec = 20;
                    break;
                case 3:
                    fireSpredIntervalSec = 15;
                    break;
                case 4:
                    fireSpredIntervalSec = 10;
                    break;
                case 5:
                    fireSpredIntervalSec = 5;
                    break;
            }
        }
    }
}
