using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridSweeperCS
{
    static public class DataIO
    {

        public struct TruckDataForOutput
        {
            public int x;
            public int y;
            public Form1.FireTruckDirection direction;
        }

        public struct FireDataForOutput
        {
            public int x;
            public int y;
            public int level;
        }

        public struct SimData
        {
            public int simTimeSec;
            public Form1.WindDirection windDirection;
            public int windLevel;

            public int reservedFireTruckNum;
            public int deployedFireTruckNum;
            public int lossedFireTruckNum;

            public int fireCellNum;
            public int leve1FireCellNum;
            public int leve2FireCellNum;
            public int leve3FireCellNum;

            public List<TruckDataForOutput> TruckDataList;
            public List<FireDataForOutput> FireDataList;
        }

        public struct ActionData
        {
            public int simTimeSec;
            public List<string> ActionList;
        }


            //
            //  プロパティの定義
            //

        static public string SettingFolderPath = Directory.GetCurrentDirectory() + "\\SettingFiles"; // PCに応じて書き換え
        static public string DataFolderPath = Directory.GetCurrentDirectory() + "\\Data"; // PCに応じて書き換え

        static public string fireSettingFileName = ""; // 出火地点の読み込みファイル名
        static public string windSettingFileName = ""; // 風設定の読み込みファイル名

        static StreamWriter LogWriteSimDatar; // ファイルへのデータ書き込み用
        static StreamWriter LogWriteAction; // ファイルへのデータ書き込み用

        static public List<SimData> SimDataList = new List<SimData>();//    シミュレーションデータを格納するためのデータリスト
        static public List<ActionData> ActionHistoryDataList = new List<ActionData>();//    行動データを格納するためのデータリスト



        //
        //  メソッドの定義
        //



        static public string readScenarioSettingFiles(string expPhase, int scenarioNum, out int timeLimitSec, out int fireTruckNum, out List<Form1.FireCellData> FireBreakoutSettingList, out List<Form1.WindData> WindSettingList, out int firemode, out int windmode, out List<Form1.GustData> GustList)
        {
            string scenrioName;

            string scnerioNumTxt = scenarioNum.ToString();
            string fireSettingFileName = "fire_" + expPhase + "-" + scnerioNumTxt + ".txt";
            string windSettingFileName = "wind_" + expPhase + "-" + scnerioNumTxt + ".txt";
            scenrioName = expPhase + "-" + scnerioNumTxt;

            bool isFireFileReadSuccess = readFireSettingFile(fireSettingFileName, out timeLimitSec, out fireTruckNum, out FireBreakoutSettingList,out firemode);
            bool isWindFileReadSuccess = readWindSettingFile(windSettingFileName, out WindSettingList,out windmode,out GustList);

            if (isFireFileReadSuccess && isWindFileReadSuccess)
            {
                return scenrioName;
            }
            else
            {
                return "Error";
            }
        }


        //読み出したいデータ①(最初の火の位置）
        static bool readFireSettingFile(string fireSettingFileNameIn, out int timeLimitSec, out int fireTruckNum, out List<Form1.FireCellData> FireBreakoutSettingList,out int firemode)
        {
            FireBreakoutSettingList = new List<Form1.FireCellData>();
            timeLimitSec = 0;
            fireTruckNum = 0;
            firemode = 0;

            bool isReadSuccess = false;
            string ReadFilePath = SettingFolderPath + @"\" + fireSettingFileNameIn;
            bool isFirstLine = true;

            if (File.Exists(ReadFilePath))
            {
                //ファイルからテキストを読み出し
                using (StreamReader r = new StreamReader(ReadFilePath))
                {
                    string line;

                    while ((line = r.ReadLine()) != null) //一行ずつ読み出し
                    {
                        string readText;
                        readText = line.Trim();

                        Form1.FireCellData InitFire = new Form1.FireCellData();

                        char[] delimiterChars = { ' ' };//テキストファイル内の行内は空白で区切られている
                        string[] array_substrings = line.Split(delimiterChars, System.StringSplitOptions.None);
                        int index = 0; //読み出し位置を指定するインデックス

                        if (isFirstLine)
                        {
                            timeLimitSec = int.Parse(array_substrings[index]);
                            ++index;

                            fireTruckNum = int.Parse(array_substrings[index]);
                            ++index;

                            firemode = int.Parse(array_substrings[index]);

                            isFirstLine = false;

                            line = r.ReadLine(); //1行空白のため、1行分捨てる
                        }
                        else
                        {
                            InitFire.fireTimeMilliSec = int.Parse(array_substrings[index]) * 1000;
                            ++index;

                            InitFire.x = int.Parse(array_substrings[index]);
                            ++index;

                            InitFire.y = int.Parse(array_substrings[index]);
                            ++index;

                            InitFire.level = int.Parse(array_substrings[index]);

                            FireBreakoutSettingList.Add(InitFire);
                        }
                    }
                }

                isReadSuccess = true;
            }

            return isReadSuccess;
        }



        //  風の設定を読みこむ
        static bool readWindSettingFile(string windSettingFileNameIn, out List<Form1.WindData> WindDataList,out int windmode, out List<Form1.GustData> GustList)
        {
            WindDataList = new List<Form1.WindData>();
            GustList = new List<Form1.GustData>();
            windmode = 0;

            bool isReadSuccess = false;
            bool isfirstline = true;
            string ReadFilePath = SettingFolderPath + @"\" + windSettingFileNameIn;

            if (File.Exists(ReadFilePath))
            {
                //ファイルからテキストを読み出し
                using (StreamReader r = new StreamReader(ReadFilePath))
                {
                    string line;

                    while ((line = r.ReadLine()) != null) //一行ずつ読み出し
                    {
                        string readText;
                        readText = line.Trim();

                        Form1.WindData InitWind = new Form1.WindData();

                        char[] delimiterChars = { ' ' };//テキストファイル内の行内は空白で区切られている
                        string[] array_substrings = line.Split(delimiterChars, System.StringSplitOptions.None);
                        int index = 0; //読み出し位置を指定するインデックス
                        if (isfirstline)
                        {
                            windmode = int.Parse(array_substrings[index]);

                            isfirstline = false;

                            line = r.ReadLine(); //1行空白のため、1行分捨てる
                        }
                        else
                        {
                            if(array_substrings.Length==1)
                            {
                                line = r.ReadLine(); //1行空白のため、1行分捨てる
                                break;
                            }
                            InitWind.windSetTimeMilliSec = int.Parse(array_substrings[index]) * 1000;
                            ++index;

                            if (array_substrings[index] == "North")
                            {
                                InitWind.direction = Form1.WindDirection.North;
                            }
                            else if (array_substrings[index] == "NorthEast")
                            {
                                InitWind.direction = Form1.WindDirection.NorthEast;
                            }
                            else if (array_substrings[index] == "East")
                            {
                                InitWind.direction = Form1.WindDirection.East;
                            }
                            else if (array_substrings[index] == "SouthEast")
                            {
                                InitWind.direction = Form1.WindDirection.SouthEast;
                            }
                            else if (array_substrings[index] == "South")
                            {
                                InitWind.direction = Form1.WindDirection.South;
                            }
                            else if (array_substrings[index] == "SouthWest")
                            {
                                InitWind.direction = Form1.WindDirection.SouthWest;
                            }
                            else if (array_substrings[index] == "West")
                            {
                                InitWind.direction = Form1.WindDirection.West;
                            }
                            else if (array_substrings[index] == "NorthWest")
                            {
                                InitWind.direction = Form1.WindDirection.NorthWest;
                            }
                            else
                            {
                                InitWind.direction = Form1.WindDirection.None;
                            }
                            ++index;

                            InitWind.level = int.Parse(array_substrings[index]);
                            ++index;

                            InitWind.isdisplay = int.Parse(array_substrings[index]);

                            WindDataList.Add(InitWind);
                        }
                        
                    }
                    if(windmode==2)
                    {

                        Form1.GustData GustWind = new Form1.GustData();
                        string readText;
                        readText = line.Trim();

                        char[] delimiterChars = { ' ' };//テキストファイル内の行内は空白で区切られている
                        string[] array_substrings = line.Split(delimiterChars, System.StringSplitOptions.None);
                        int index = 0; //読み出し位置を指定するインデックス
                        GustWind.windSetTimeMilliSec = int.Parse(array_substrings[index]) * 1000;
                        ++index;

                        if (array_substrings[index] == "North")
                        {
                            GustWind.direction = Form1.WindDirection.North;
                        }
                        else if (array_substrings[index] == "NorthEast")
                        {
                            GustWind.direction = Form1.WindDirection.NorthEast;
                        }
                        else if (array_substrings[index] == "East")
                        {
                            GustWind.direction = Form1.WindDirection.East;
                        }
                        else if (array_substrings[index] == "SouthEast")
                        {
                            GustWind.direction = Form1.WindDirection.SouthEast;
                        }
                        else if (array_substrings[index] == "South")
                        {
                            GustWind.direction = Form1.WindDirection.South;
                        }
                        else if (array_substrings[index] == "SouthWest")
                        {
                            GustWind.direction = Form1.WindDirection.SouthWest;
                        }
                        else if (array_substrings[index] == "West")
                        {
                            GustWind.direction = Form1.WindDirection.West;
                        }
                        else if (array_substrings[index] == "NorthWest")
                        {
                            GustWind.direction = Form1.WindDirection.NorthWest;
                        }
                        else
                        {
                            GustWind.direction = Form1.WindDirection.None;
                        }
                        ++index;

                        GustWind.level = int.Parse(array_substrings[index]);
                        ++index;

                        GustWind.leftx = int.Parse(array_substrings[index]);
                        ++index;

                        GustWind.rightx = int.Parse(array_substrings[index]);
                        ++index;

                        GustWind.uppery = int.Parse(array_substrings[index]);
                        ++index;

                        GustWind.botomy = int.Parse(array_substrings[index]);
                        
                        GustList.Add(GustWind);
                    }
                }

                isReadSuccess = true;
            }

            return isReadSuccess;
        }


        static public void setStreamWriter(string scenrioName)
        {
            SimDataList.Clear();
            ActionHistoryDataList.Clear();

            if (Directory.Exists(DataFolderPath) == false)
            {
                Directory.CreateDirectory(DataFolderPath);
            }

            DateTime dt = DateTime.Now;

            string streamForData = DataFolderPath + "\\SimData" + "_" + dt.ToString("yyMMdd_HHmmss") + ".txt";
            LogWriteSimDatar = new StreamWriter(streamForData);

            LogWriteSimDatar.WriteLine(scenrioName);
            LogWriteSimDatar.WriteLine("");
            LogWriteSimDatar.WriteLine("<時刻(秒)>" + "\t" + "<風向>" + "\t" + "<風速レベル>" + "\t" + "<待機消防車数>" + "\t" + "<配置消防車数>" + "\t" + "<喪失消防車数>"
                 + "\t" + "<総火災セル数>" + "\t" + "<Lv1火災セル数>" + "\t" + "<Lv2火災セル数>" + "\t" + "<Lv3火災セル数>"
                 + "\t" + "<消防車データ>" + "\t" + "<火災セルデータ>");



            string streamForAction = DataFolderPath + "\\ActionLog" + "_" + dt.ToString("yyMMdd_HHmmss") + ".txt";
            LogWriteAction = new StreamWriter(streamForAction);

            LogWriteAction.WriteLine(scenrioName);
            LogWriteAction.WriteLine("");
            LogWriteAction.WriteLine("<時刻(秒)>" + "\t" + "<行動や状況の変化>");
        }


        //  データのファイル出力
        static public void WriteLog()
        {
            if (LogWriteSimDatar != null)
            {
                foreach (SimData data in SimDataList)
                {
                    string truckDataText = "";

                    foreach(TruckDataForOutput trk in data.TruckDataList)
                    {
                        truckDataText += (trk.x.ToString() + "," + trk.y.ToString() + "," + trk.direction.ToString() + ";");
                    }

                    string fireDataText = "";

                    foreach (FireDataForOutput fire in data.FireDataList)
                    {
                        fireDataText += (fire.x.ToString() + "," + fire.y.ToString() + "," + fire.level.ToString() + ";");
                    }

                    string outputDataText = data.simTimeSec.ToString() + "\t" + data.windDirection.ToString() + "\t" + data.windLevel.ToString() + "\t"
                        + data.reservedFireTruckNum.ToString() + "\t" + data.deployedFireTruckNum.ToString() + "\t" + data.lossedFireTruckNum.ToString() + "\t"
                        + data.fireCellNum.ToString() + "\t" + data.leve1FireCellNum.ToString() + "\t" + data.leve2FireCellNum.ToString() + "\t" + data.leve3FireCellNum.ToString() + "\t"
                        + truckDataText + "\t" + fireDataText;

                    LogWriteSimDatar.WriteLine(outputDataText);
                }
            }

            if (LogWriteAction != null)
            {
                string outPutText = "";

                foreach (ActionData aData in ActionHistoryDataList)
                {
                    foreach (string action in aData.ActionList)
                    {
                        outPutText = aData.simTimeSec + "\t" + action;
                        LogWriteAction.WriteLine(outPutText);
                    }
                }
            }

        }


        static public void WriteAndCloseLogWriterIfNotNull()
        {
            WriteLog();

            if (LogWriteSimDatar != null)
            {
                LogWriteSimDatar.Close();
            }

            if (LogWriteAction != null)
            {
                LogWriteAction.Close();
            }
        }


    }
}
