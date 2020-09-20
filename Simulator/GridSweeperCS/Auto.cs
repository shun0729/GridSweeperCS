using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridSweeperCS
{
    public enum FireTruckDirection { North, East, South, West };//トラックの向きの変数
    public enum WindDirection { None, North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };
    public struct FireDataForOutput
    {
        public int x;
        public int y;
        public int level;
    }
    static public class Auto
    {
        static public List<DataIO.FireDataForOutput> fireCellInfos = new List<DataIO.FireDataForOutput>();
        static public List<DataIO.TruckDataForOutput> TruckCellInfos = new List<DataIO.TruckDataForOutput>();
        static int WindDirection;
        static public void GetInfo(DataIO.SimData Data)
        {
            foreach (DataIO.FireDataForOutput fire in Data.FireDataList)
            {
                fireCellInfos.Add(fire);
            }
            foreach (DataIO.TruckDataForOutput truck in Data.TruckDataList)
            {
                TruckCellInfos.Add(truck);
            }
        }
        static private void Automation(DataIO.SimData Data)
        {

        }
    }
}
