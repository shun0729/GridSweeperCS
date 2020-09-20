using GridSweeperCS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CsvHelper;


namespace GridSweeperCS
{
    public partial class Form1 : Form
    {

        //
        //  データ型の定義
        //

        public enum FireTruckDirection { North, East, South, West };//トラックの向きの変数
        public enum WindDirection { None, North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };


        //CSVに出力するための消防車のリスト
        public struct TruckCellData
        {
            public string elapsedTime;
            public DataGridViewCell FireTruck;
            public long previousWorkTimeMilliSec;
        }

        public struct FireCellData
        {
            public long fireTimeMilliSec;
            public int x;
            public int y;
            public int level;
        }

        public struct WindData
        {
            public long windSetTimeMilliSec;
            public WindDirection direction;
            public int level;
            public int isdisplay;
        }
        public struct GustData
        {
            public long windSetTimeMilliSec;
            public WindDirection direction;
            public int level;
            public int leftx;
            public int rightx;
            public int uppery;
            public int botomy;
        }

        struct FireTrackInfo
        {
            public DataGridViewCell FireTruckCell;
            public List<DataGridViewCell> WaterCellList;
            public List<DataGridViewCell> MovableCellList;
            public bool isStanbywithdraw;
            public bool isAvailable;
            public long stanbyTimeMilliSec;
            public long previousWorkTimeMilliSec;
        }

        struct NewFireCellInfo
        {
            public int xColumnIndex;
            public int yRowIndex;
            public int fireLevel;
        }





        //
        //  プロパティの定義
        //

        string expPhase = ""; // シナリオ読み込み制御用
        string exPhase = "";
        int scenarioNum; // シナリオ読み込み制御用
        int[] expScenario = { 1, 2};
        int[] testScenario = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        int[] traScenario = { 1, 2, 3, 4, 5, 6};
        int SelectNumber = 0;
         
        const int CELL_SIZE = 26; // セルのサイズ
        const int LATERAL_CELL_NUM = 32; // フィールドの幅
        const int VERTICAL_CELL_NUM = 21; // フィールドの高さ
        int numFireLv1 = 0; // Lv1火災の数
        int numFireLv2 = 0;// Lv2火災の数
        int numFireLv3 = 0;// Lv3火災の数
        int numFireLv4 = 0;// Lv4火災の数
        int numFireLv5 = 0;// Lv5火災の数
        int numFireTruck = 5;//消防車残数
        int numLossedFireTruck = 0; // 失った消防車数
        long WindChangeTime = 0;//風向が変わった時間

        Random rnd = new Random();
        System.Timers.Timer testTimer = new System.Timers.Timer(1000 * 5);

        string presentDirectory = Directory.GetCurrentDirectory();
        string resourceDirectory;

        Image ImageFireTruck;
        Image ImageCross;

        Image ImageArrowNorth;
        Image ImageArrowNorthEast;
        Image ImageArrowEast;
        Image ImageArrowSouthEast;
        Image ImageArrowSouth;
        Image ImageArrowSouthWest;
        Image ImageArrowWest;
        Image ImageArrowNorthWest;


        const int CELL_VALUE_NORMAL = 0;
        const int CELL_VALUE_FIRE_LV1 = -1;
        const int CELL_VALUE_FIRE_LV2 = -2;
        const int CELL_VALUE_FIRE_LV3 = -3;
        const int CELL_VALUE_TRUCK = 100;
        const int CELL_VALUE_WATER = 10;

        //ストップウォッチ
        Stopwatch stopwatch = new Stopwatch();

        //  新しく火がついたセルのリスト
        List<NewFireCellInfo> NewFireCellList = new List<NewFireCellInfo>();


        FireTruckDirection ftDirection = new FireTruckDirection();
        int windDirection=0;//風向の変数
        int windLevel=0;//風速レベル変数
        int spreadDirection = 0;
        int spreadLevel = 0;
        int GustDirection=0;//風向の変数
        int GustLevel=0;//風速レベル変数
        int Gustxl=0;//風向の変数
        int Gustxr=0;//風速レベル変数
        int Gustyu=0;//風向の変数
        int Gustyb=0;//風速レベル変数
        long GustChangeTime = 0;

        bool gameStarted; // ゲームを開始しているか
        bool isFireTrackDeployButtonPushed = false; //
        bool tra = false;
        bool exp = false;
        bool test = false;
        bool limited = true;
        bool setNumber = false;
        bool move = true;
        bool missedtruck = false;
        bool misstruck = false;
        bool tempavailable = false;
        string logText;

        int timeLimitSec = 0;
        long timerTickCounter = 0;
        int firemode = 0;
        int windmode = 0;

        //  タイマー表示用
        int h, m, s;

        bool isStanbyTimerStop = false;

        Random Rdm = new Random();

        long helpRequestTimeMilliSec = 0;
        bool isHelpRequest = false;


        List<FireTrackInfo> DeployedFireTruckInfoList = new List<FireTrackInfo>(); //配置された消防車のリスト
        List<TruckCellData> TruckDataList = new List<TruckCellData>(); //CSVに書き込むための消防車のデータのリスト 

        List<FireTrackInfo> SelectedFireTruckInfoList = new List<FireTrackInfo>(); // 選択された消防車
        List<FireTrackInfo> MovingFireTruckInfoList = new List<FireTrackInfo>(); // 移動中の消防車

        List<FireCellData> FireBreakoutSettingList = new List<FireCellData>(); // 出火設定用のリスト
        List<WindData> WindSettingList = new List<WindData>(); // 風設定用のリスト
        List<GustData> GustList = new List<GustData>(); // 突風設定用のリスト

        List<string> UserActionList = new List<string>();


        //
        //  メソッドの定義
        //

        public Form1()
        {
            resourceDirectory = presentDirectory + @"\Resources";

            //ImageFireTruck = Image.FromFile(resourceDirectory + @"\\Fire-Truck-Left-Red-icon.png");
            ImageFireTruck = Image.FromFile(resourceDirectory + @"\\syoka_machine.png");
            ImageCross = Image.FromFile(resourceDirectory + @"\\cross.png");

            ImageArrowNorth = Image.FromFile(resourceDirectory + @"\\ArrowNorth.png"); ;
            ImageArrowNorthEast = Image.FromFile(resourceDirectory + @"\\ArrowNorthEast.png"); ;
            ImageArrowEast = Image.FromFile(resourceDirectory + @"\\ArrowEast.png"); ;
            ImageArrowSouthEast = Image.FromFile(resourceDirectory + @"\\ArrowSouthEast.png"); ;
            ImageArrowSouth = Image.FromFile(resourceDirectory + @"\\ArrowSouth.png"); ;
            ImageArrowSouthWest = Image.FromFile(resourceDirectory + @"\\ArrowSouthWest.png"); ;
            ImageArrowWest = Image.FromFile(resourceDirectory + @"\\ArrowWest.png"); ;
            ImageArrowNorthWest = Image.FromFile(resourceDirectory + @"\\ArrowNorthWest.png"); ;

            InitializeComponent();

            picTrack.Image = ImageFireTruck;
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            // 以下はプロパティ・ウィンドウでも設定可能
            dgv.ReadOnly = true;
            dgv.ColumnHeadersVisible = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.ShowCellToolTips = false; // 答えが見えないように


            //// フィールドの作成
            dgv.RowTemplate.Height = CELL_SIZE; // 追加される行の高さ
            dgv.ColumnCount = LATERAL_CELL_NUM;

            dgv.RowCount = VERTICAL_CELL_NUM;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.Width = CELL_SIZE;
            }

            // フォームのサイズをDGVに合わせる
            DataGridViewCell cell = dgv[0, 0];
            this.ClientSize = new Size(cell.Size.Width * LATERAL_CELL_NUM + 3, cell.Size.Height * VERTICAL_CELL_NUM + 3);

            initGame();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            bool isWindChage = changeEnvSetting();

            if (timerTickCounter-WindChangeTime/1000 >= 5 && timerTickCounter % SimEnvSetting.fireSpredIntervalSec == 0) // 風速レベルの違いによる延焼速度のコントロール
            {
                if (isWindChage == true && SimEnvSetting.fireSpredIntervalSec <= 10) // 初回の延焼まで10秒ない場合は1回目は延焼させない
                {
                    isWindChage = false;
                }
                else
                {
                    spreadFire(); // 火の延焼処理
                }
            }

            actFireTruck(); // 消防車の活動（消火）
            withdrawFireTruck(); // 消防車の活動（撤退）
            //AutoTruck();//自動化
            waitingHelp(); // 救援要請に関する処理
            

            //  シミュレーションログの作成
            DataIO.SimData Data = new DataIO.SimData();

            Data.simTimeSec = (int)timerTickCounter;
            Data.windDirection = (WindDirection)windDirection;
            Data.windLevel = windLevel;
            Data.reservedFireTruckNum = numFireTruck;
            Data.deployedFireTruckNum = DeployedFireTruckInfoList.Count();
            Data.lossedFireTruckNum = numLossedFireTruck;
            Data.FireDataList = new List<DataIO.FireDataForOutput>();
            Data.TruckDataList = new List<DataIO.TruckDataForOutput>();

            for(int x = 0; x < LATERAL_CELL_NUM; ++x)
            {
                for(int y = 0; y < VERTICAL_CELL_NUM; ++y)
                {
                    if ((int)dgv[x, y].Value < 0)
                    {
                        DataIO.FireDataForOutput FData = new DataIO.FireDataForOutput();

                        FData.x = x;
                        FData.y = y;
                        FData.level = (int)dgv[x, y].Value;

                        switch (FData.level)
                        {
                            case CELL_VALUE_FIRE_LV1:
                                ++Data.leve1FireCellNum;
                                break;
                            case CELL_VALUE_FIRE_LV2:
                                ++Data.leve2FireCellNum;
                                break;
                            case CELL_VALUE_FIRE_LV3:
                                ++Data.leve3FireCellNum;
                                break;
                        }

                        Data.FireDataList.Add(FData);
                    }
                    else if ((int)dgv[x, y].Value == CELL_VALUE_TRUCK)
                    {
                        DataIO.TruckDataForOutput TData = new DataIO.TruckDataForOutput();

                        TData.x = x;
                        TData.y = y;

                        switch (dgv[x, y].Style.Format)
                        {
                            case "△":
                                TData.direction = FireTruckDirection.North;
                                break;
                            case "▷":
                                TData.direction = FireTruckDirection.East;
                                break;
                            case "▽":
                                TData.direction = FireTruckDirection.South;
                                break;
                            case "◁":
                                TData.direction = FireTruckDirection.West;
                                break;
                        }

                        Data.TruckDataList.Add(TData);
                    }
                }
            }

            Data.fireCellNum = Data.leve1FireCellNum + Data.leve2FireCellNum + Data.leve3FireCellNum;
            DataIO.SimDataList.Add(Data);
            Auto.GetInfo(Data);
            //  行動記録
            DataIO.ActionData aData = new DataIO.ActionData();
            aData.simTimeSec = (int)timerTickCounter;
            aData.ActionList = new List<string>();

            foreach (string action in UserActionList)
            {
                aData.ActionList.Add(action);
            }

            DataIO.ActionHistoryDataList.Add(aData);

            UserActionList.Clear(); // 記録用ログのクリア



            if (isStanbyTimerStop == true)
            {
                GameStop();
            }

            //  時刻の経過に関する処理
            proceedElapsedTime();

            if(timeLimitSec - timerTickCounter == 0)
            {
                textBoxElapsedTime.BackColor = Color.LightSalmon;
                GameStop();
                limited = false;
            }
            else if (timeLimitSec - timerTickCounter <= 60)
            {
                textBoxElapsedTime.BackColor = Color.Yellow;
            }

            if(limited)
            {
                ++timerTickCounter;
            }
            else
            {

            }
        }


        private void proceedElapsedTime()
        {
            s += 1;
            if (s == 60)
            {
                s = 0;
                m += 1;
            }
            if (m == 60)
            {
                m = 0;
                h += 1;

            }
            textBoxElapsedTime.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
        }


        //timerイベント
        private void btnStart_Click(object sender, EventArgs e)
        {
            isStanbyTimerStop = false;


            testTimer.Enabled = true;
            stopwatch.Start();

            tabPage2.Enabled = false;
            tabPage2.Visible = false;

            timerMain.Enabled = true;
        }


        private void GameStop()
        {
            timerMain.Enabled = false;
            string elapsedTime = String.Format("{0}", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("RunTime" + elapsedTime);
            stopwatch.Stop();
            stopwatch.Reset();
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            isStanbyTimerStop = true;
            tabPage2.Enabled = true;
            tabPage2.Visible = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DataIO.WriteAndCloseLogWriterIfNotNull();

            isStanbyTimerStop = false;

            textBoxElapsedTime.Text = "00:00:00";
            h = 0;
            m = 0;
            s = 0;

            stopwatch.Reset();
            initGame();

            tabPage2.Enabled = true;
            tabPage2.Visible = true;

            DataIO.setStreamWriter(textBoxScenarioName.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataIO.WriteAndCloseLogWriterIfNotNull();

            System.Windows.Forms.Application.DoEvents();
        }


        // ゲームの初期化
        void initGame()
        {
            gameStarted = true;

            textBoxElapsedTime.Text = "00:00:00";
            setTimeLimit();

            // すべてのセルの初期化
            for (int x = 0; x < LATERAL_CELL_NUM; x++)
            {
                for (int y = 0; y < VERTICAL_CELL_NUM; y++)
                {
                    dgv[x, y].Value = CELL_VALUE_NORMAL;
                    dgv[x, y].Style.ForeColor = Color.WhiteSmoke;
                    dgv[x, y].Style.BackColor = Color.WhiteSmoke;
                    dgv[x, y].Style.Format = "";
                }
            }

            DeployedFireTruckInfoList.Clear();
            SelectedFireTruckInfoList.Clear();
            TruckDataList.Clear();
            //buttonHelp.Enabled = true;
            FireBreakoutSettingList.Clear();
            WindSettingList.Clear();
            GustList.Clear();
            BtnReset();
            buttonWithdraw.Enabled = false;
            buttonWithdraw.BackColor = Color.LightGray;
            missedtruck = false;


            if (expPhase != "")
            {
                DataIO.readScenarioSettingFiles(expPhase, scenarioNum, out timeLimitSec, out numFireTruck, out FireBreakoutSettingList, out WindSettingList, out firemode, out windmode, out GustList);
            }

            numLossedFireTruck = 0;
            timerTickCounter = 0;
            limited = true;
            timerMain.Enabled = false;

            GustDirection = 0;
            GustLevel = 0;
            GustChangeTime = 0;
            Gustxl = 0;//風向の変数
            Gustxr = 0;//風速レベル変数
            Gustyu = 0;//風向の変数
            Gustyb = 0;//風速レベル変数

            h = 0;
            m = 0;
            s = 0;

            numTruck.Text = numFireTruck.ToString();
            picTrack.Image = ImageFireTruck;
        }


        // (x, y)がグリッド内に含まれるか
        bool isInField(int x, int y)
        {
            if (x < 0 || y < 0 || x >= LATERAL_CELL_NUM || y >= VERTICAL_CELL_NUM)
            {
                return false;
            }
        
            return true;
        }


        // cellに隣接するセルを配列で返す
        //時間制限つけたい
        List<DataGridViewCell> getNeighborsNorth(DataGridViewCell cell)
        {
            int x = cell.ColumnIndex;
            int y = cell.RowIndex;
            List<DataGridViewCell> cc = new List<DataGridViewCell>();

            cc.Add(dgv[x, y]);
            if (isInField(x - 1, y - 1)) cc.Add(dgv[x - 1, y - 1]);
            if (isInField(x + 1, y - 1)) cc.Add(dgv[x + 1, y - 1]);
            if (isInField(x - 1, y)) cc.Add(dgv[x - 1, y]);
            if (isInField(x + 1, y)) cc.Add(dgv[x + 1, y]);
            if (isInField(x, y - 1)) cc.Add(dgv[x, y - 1]);

            return cc;
        }

        List<DataGridViewCell> getNeighborsEast(DataGridViewCell cell)
        {
            int x = cell.ColumnIndex;
            int y = cell.RowIndex;
            List<DataGridViewCell> cc = new List<DataGridViewCell>();

            cc.Add(dgv[x, y]);
            if (isInField(x + 1, y - 1)) cc.Add(dgv[x + 1, y - 1]);
            if (isInField(x + 1, y + 1)) cc.Add(dgv[x + 1, y + 1]);
            if (isInField(x + 1, y)) cc.Add(dgv[x + 1, y]);
            if (isInField(x, y - 1)) cc.Add(dgv[x, y - 1]);
            if (isInField(x, y + 1)) cc.Add(dgv[x, y + 1]);

            return cc;
        }

        List<DataGridViewCell> getNeighborsSouth(DataGridViewCell cell)
        {
            int x = cell.ColumnIndex;
            int y = cell.RowIndex;
            List<DataGridViewCell> cc = new List<DataGridViewCell>();

            cc.Add(dgv[x, y]);
            if (isInField(x - 1, y + 1)) cc.Add(dgv[x - 1, y + 1]);
            if (isInField(x + 1, y + 1)) cc.Add(dgv[x + 1, y + 1]);
            if (isInField(x - 1, y)) cc.Add(dgv[x - 1, y]);
            if (isInField(x + 1, y)) cc.Add(dgv[x + 1, y]);
            if (isInField(x, y + 1)) cc.Add(dgv[x, y + 1]);

            return cc;
        }

        List<DataGridViewCell> getNeighborsWest(DataGridViewCell cell)
        {
            int x = cell.ColumnIndex;
            int y = cell.RowIndex;
            List<DataGridViewCell> cc = new List<DataGridViewCell>();

            cc.Add(dgv[x, y]);
            if (isInField(x - 1, y - 1)) cc.Add(dgv[x - 1, y - 1]);
            if (isInField(x - 1, y + 1)) cc.Add(dgv[x - 1, y + 1]);
            if (isInField(x - 1, y)) cc.Add(dgv[x - 1, y]);
            if (isInField(x, y - 1)) cc.Add(dgv[x, y - 1]);
            if (isInField(x, y + 1)) cc.Add(dgv[x, y + 1]);

            return cc;
        }



        /// <summary>
        /// セルをクリックした場合の処理 
        /// </summary>
        /// <param name="cell"></param>
        void handleCellClick(DataGridViewCell cell)
        {
            int x = cell.ColumnIndex;
            int y = cell.RowIndex;

            if (gameStarted == false)
            {
                return;
            }

            FireTrackInfo FireTrakInfo = new FireTrackInfo();

            FireTrakInfo.WaterCellList = new List<DataGridViewCell>();
            FireTrakInfo.MovableCellList = new List<DataGridViewCell>();

            if (isFireTrackDeployButtonPushed)
            {
                if ((int)cell.Value == CELL_VALUE_NORMAL || (int)cell.Value == CELL_VALUE_WATER)
                {
                    if(firemode==3 && timerTickCounter>=30)
                    {
                        if ((int)makeRandomInt(1, 10) <= 3)
                        {
                           misstruck = true;
                           missedtruck = true;
                        }
                    }
                    cell.Style.BackColor = Color.DarkBlue;
                    cell.Value = 100;

                    if (ftDirection == FireTruckDirection.North)
                    {
                        if(misstruck)
                        {
                            UserActionList.Add("●無能消防車配置・北向き:" + " (" + cell.ColumnIndex.ToString() + "," + cell.RowIndex.ToString() + ")");
                            FireTrakInfo.isAvailable = false;
                            misstruck = false;
                        }
                        else
                        {
                            UserActionList.Add("●消防車配置・北向き:" + " (" + cell.ColumnIndex.ToString() + "," + cell.RowIndex.ToString() + ")");
                            FireTrakInfo.isAvailable = true;
                        }
                        cell.Style.Format = string.Format("△");

                        //配置した消防車の位置情報
                        FireTrakInfo.FireTruckCell = cell;

                        //配置した消防車の周囲の水の位置情報
                        if (CheckBorderCell(x - 1, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y - 1]);
                        }
                        if (CheckBorderCell(x + 1, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y - 1]);
                        }
                        if (CheckBorderCell(x, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x, y - 1]);
                        }

                        //  配置時刻
                        FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                        //消防車と周辺の水の位置情報のリスト
                        DeployedFireTruckInfoList.Add(FireTrakInfo);

                        //消防車の位置座標出力
                        Console.WriteLine(FireTrakInfo.FireTruckCell);

                        ////CSV出力するための消防車上の配置時間/位置情報
                        TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });

                        numFireTruck--;//消防車残数を減らす
                        numTruck.Text = numFireTruck.ToString();


                        if (numFireTruck <= 0)
                        {
                            isFireTrackDeployButtonPushed = false;
                            numFireTruck = 0;//残数を0にする
                            picTrack.Image = ImageCross;

                        }
                        else
                        {
                            picTrack.Image = ImageFireTruck;
                        }
                    }

                    else if (ftDirection == FireTruckDirection.East)
                    {
                        if (misstruck)
                        {
                            UserActionList.Add("●無能消防車配置・東向き:" + " (" + cell.ColumnIndex.ToString() + "," + cell.RowIndex.ToString() + ")");
                            FireTrakInfo.isAvailable = false;
                            misstruck = false;
                        }
                        else
                        {
                            UserActionList.Add("●消防車配置・東向き:" + " (" + cell.ColumnIndex.ToString() + "," + cell.RowIndex.ToString() + ")");
                            FireTrakInfo.isAvailable = true;
                        }
                        cell.Style.Format = string.Format("▷");

                        //配置した消防車の位置情報
                        FireTrakInfo.FireTruckCell = cell;

                        //配置した消防車の周囲の水の位置情報
                        if (CheckBorderCell(x + 1, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y - 1]);
                        }
                        if (CheckBorderCell(x + 1, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y + 1]);

                        }
                        if (CheckBorderCell(x + 1, y) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y]);
                        }

                        //  配置時刻
                        FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                        //消防車と周辺の水の位置情報のリスト
                        DeployedFireTruckInfoList.Add(FireTrakInfo);

                        ////CSV出力するための消防車上の配置時間/位置情報
                        TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });

                        //水の位置座標出力
                        while (true)
                        {
                            Console.WriteLine(FireTrakInfo.WaterCellList);
                            break;
                        }

                        numFireTruck--;//消防車残数を減らす
                        numTruck.Text = numFireTruck.ToString();

                        if (numFireTruck <= 0)
                        {
                            isFireTrackDeployButtonPushed = false;
                            numFireTruck = 0;//残数を0にする
                            picTrack.Image = ImageCross;
                        }
                        else
                        {
                            picTrack.Image = ImageFireTruck;
                        }
                    }
                    else if (ftDirection == FireTruckDirection.South)
                    {
                        if (misstruck)
                        {
                            UserActionList.Add("●無能消防車配置・南向き:" + " (" + cell.ColumnIndex.ToString() + "," + cell.RowIndex.ToString() + ")");
                            FireTrakInfo.isAvailable = false;
                            misstruck = false;
                        }
                        else
                        {
                            UserActionList.Add("●消防車配置・南向き:" + " (" + cell.ColumnIndex.ToString() + "," + cell.RowIndex.ToString() + ")");
                            FireTrakInfo.isAvailable = true;
                        }
                        cell.Style.Format = string.Format("▽");

                        //  配置時刻
                        FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                        //配置した消防車の位置情報
                        FireTrakInfo.FireTruckCell = cell;

                        if (CheckBorderCell(x - 1, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y + 1]);

                        }
                        if (CheckBorderCell(x, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x, y + 1]);
                        }
                        if (CheckBorderCell(x + 1, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y + 1]);
                        }
                        //消防車と周辺の水の位置情報のリスト
                        DeployedFireTruckInfoList.Add(FireTrakInfo);

                        //消防車の位置座標出力

                        ////CSV出力するための消防車上の配置時間/位置情報
                        TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });

                        //水の位置座標出力
                        while (true)
                        {
                            Console.WriteLine(FireTrakInfo.WaterCellList);
                            break;
                        }


                        numFireTruck--;//消防車残数を減らす
                        numTruck.Text = numFireTruck.ToString();

                        if (numFireTruck <= 0)
                        {
                            isFireTrackDeployButtonPushed = false;
                            numFireTruck = 0;//残数を0にする
                            picTrack.Image = ImageCross;
                        }
                        else
                        {
                            picTrack.Image = ImageFireTruck;
                        }
                    }
                    else if (ftDirection == FireTruckDirection.West)
                    {
                        if (misstruck)
                        {
                            UserActionList.Add("●無能消防車配置・西向き:" + " (" + cell.ColumnIndex.ToString() + "," + cell.RowIndex.ToString() + ")");
                            FireTrakInfo.isAvailable = false;
                            misstruck = false;
                        }
                        else
                        {
                            UserActionList.Add("●消防車配置・西向き:" + " (" + cell.ColumnIndex.ToString() + "," + cell.RowIndex.ToString() + ")");
                            FireTrakInfo.isAvailable = true;
                        }
                        cell.Style.Format = string.Format("◁");

                        //  配置時刻
                        FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                        //配置した消防車の位置情報
                        FireTrakInfo.FireTruckCell = cell;

                        //配置した消防車の周囲の水の位置情報
                        if (CheckBorderCell(x - 1, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y + 1]);
                        }
                        if (CheckBorderCell(x - 1, y) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y]);

                        }
                        if (CheckBorderCell(x - 1, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y - 1]);
                        }

                        //消防車と周辺の水の位置情報のリスト
                        DeployedFireTruckInfoList.Add(FireTrakInfo);
                        //消防車の位置座標出力

                        ////CSV出力するための消防車上の配置時間/位置情報
                        TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });

                        numFireTruck--;//消防車残数を減らす
                        numTruck.Text = numFireTruck.ToString();

                        if (numFireTruck <= 0)
                        {
                            isFireTrackDeployButtonPushed = false;
                            numFireTruck = 0;//残数を0にする
                            picTrack.Image = ImageCross;
                        }
                        else
                        {
                            picTrack.Image = ImageFireTruck;
                        }
                    }
                }

                isFireTrackDeployButtonPushed = false;
                btnTrack1.BackColor = Color.LightGray;
                btnTrack2.BackColor = Color.LightGray;
                btnTrack3.BackColor = Color.LightGray;
                btnTrack4.BackColor = Color.LightGray;
            }
            else //isPushed=false;
            {
                if (0 < SelectedFireTruckInfoList.Count)//消防車が選択されているとき
                {
                    FireTruckDirection mvFtDirection;
                    foreach (DataGridViewCell wc in SelectedFireTruckInfoList[0].MovableCellList)
                    {
                        if (wc.ColumnIndex == cell.ColumnIndex && wc.RowIndex == cell.RowIndex)
                        {
                            int oldPositionX = SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex;
                            int oldPositionY = SelectedFireTruckInfoList[0].FireTruckCell.RowIndex;
                            tempavailable = SelectedFireTruckInfoList[0].isAvailable;
                            if (SelectedFireTruckInfoList[0].isAvailable)
                            {
                                logText = "●消防車移動:" + " (" + oldPositionX.ToString() + "," + oldPositionY.ToString() + ") → (" + cell.ColumnIndex.ToString() + ", " + cell.RowIndex.ToString() + ")";
                            }
                            else
                            {
                                logText = "●無能消防車移動:" + " (" + oldPositionX.ToString() + "," + oldPositionY.ToString() + ") → (" + cell.ColumnIndex.ToString() + ", " + cell.RowIndex.ToString() + ")";
                            }
                            
                            UserActionList.Add(logText);


                            if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "△")
                            {
                                mvFtDirection = FireTruckDirection.North;
                            }
                            else if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "▷")
                            {
                                mvFtDirection = FireTruckDirection.East;
                            }
                            else if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "▽")
                            {
                                mvFtDirection = FireTruckDirection.South;
                            }
                            else
                            {
                                mvFtDirection = FireTruckDirection.West;
                            }


                            

                            //  再配置処理
                            if ((int)cell.Value == CELL_VALUE_NORMAL || (int)cell.Value == CELL_VALUE_WATER)
                            {
                                //  移動する消防車を一旦削除
                                int index = 0;
                                while (index < DeployedFireTruckInfoList.Count)
                                {
                                    if (DeployedFireTruckInfoList[index].FireTruckCell.ColumnIndex == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                        && DeployedFireTruckInfoList[index].FireTruckCell.RowIndex == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                    {
                                        SelectedFireTruckInfoList[0].FireTruckCell.Value = CELL_VALUE_NORMAL;
                                        SelectedFireTruckInfoList[0].FireTruckCell.Style.Format = string.Format("");
                                        SelectedFireTruckInfoList[0].FireTruckCell.Style.BackColor = setCellColor((int)SelectedFireTruckInfoList[0].FireTruckCell.Value); ;

                                        //  選択可能行動に関する表示更新
                                        foreach (DataGridViewCell cl in DeployedFireTruckInfoList[index].MovableCellList)
                                        {
                                            if (CELL_VALUE_NORMAL == (int)cl.Value)
                                            {
                                                cl.Style.BackColor = Color.WhiteSmoke;
                                            }
                                        }

                                        DeployedFireTruckInfoList.RemoveAt(index);

                                        buttonWithdraw.Enabled = false;
                                        buttonWithdraw.BackColor = Color.LightGray;

                                        SelectedFireTruckInfoList.Clear();

                                        break;
                                    }

                                    ++index;
                                }
                                cell.Style.BackColor = Color.DarkBlue;
                                cell.Value = 100;

                                if (mvFtDirection == FireTruckDirection.North)
                                {
                                    cell.Style.Format = string.Format("△");

                                    //配置した消防車の位置情報
                                    FireTrakInfo.FireTruckCell = cell;

                                    //配置した消防車の周囲の水の位置情報
                                    if (CheckBorderCell(x - 1, y - 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x - 1, y - 1]);
                                    }
                                    if (CheckBorderCell(x + 1, y - 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x + 1, y - 1]);

                                    }
                                    if (CheckBorderCell(x, y - 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x, y - 1]);
                                    }

                                    //  移動可能先座標の登録
                                    FireTrakInfo.MovableCellList = getNeighborsNorth(cell);

                                    //  配置時刻
                                    FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;
                                    FireTrakInfo.isAvailable = tempavailable;
                                    //消防車と周辺の水の位置情報のリスト
                                    DeployedFireTruckInfoList.Add(FireTrakInfo);


                                    ////CSV出力するための消防車上の配置時間/位置情報
                                    TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                                }

                                else if (mvFtDirection == FireTruckDirection.East)
                                {
                                    cell.Style.Format = string.Format("▷");

                                    //配置した消防車の位置情報
                                    FireTrakInfo.FireTruckCell = cell;

                                    //配置した消防車の周囲の水の位置情報
                                    if (CheckBorderCell(x + 1, y - 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x + 1, y - 1]);
                                    }
                                    if (CheckBorderCell(x + 1, y + 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x + 1, y + 1]);

                                    }
                                    if (CheckBorderCell(x + 1, y) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x + 1, y]);
                                    }

                                    //  移動可能先座標の登録
                                    FireTrakInfo.MovableCellList = getNeighborsEast(cell);

                                    //  配置時刻
                                    FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;
                                    FireTrakInfo.isAvailable = tempavailable;
                                    //消防車と周辺の水の位置情報のリスト
                                    DeployedFireTruckInfoList.Add(FireTrakInfo);

                                    ////CSV出力するための消防車上の配置時間/位置情報
                                    TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });                  
                                }
                                else if (mvFtDirection == FireTruckDirection.South)
                                {
                                    cell.Style.Format = string.Format("▽");

                                    //  移動可能先座標の登録
                                    FireTrakInfo.MovableCellList = getNeighborsSouth(cell);

                                    //  配置時刻
                                    FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;
                                    FireTrakInfo.isAvailable = tempavailable;
                                    //配置した消防車の位置情報
                                    FireTrakInfo.FireTruckCell = cell;

                                    if (CheckBorderCell(x - 1, y + 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x - 1, y + 1]);

                                    }
                                    if (CheckBorderCell(x, y + 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x, y + 1]);
                                    }
                                    if (CheckBorderCell(x + 1, y + 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x + 1, y + 1]);
                                    }

                                    //消防車と周辺の水の位置情報のリスト
                                    DeployedFireTruckInfoList.Add(FireTrakInfo);

                                    ////CSV出力するための消防車上の配置時間/位置情報
                                    TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                                }
                                else if (mvFtDirection == FireTruckDirection.West)
                                {
                                    cell.Style.Format = string.Format("◁");

                                    //  移動可能先座標の登録
                                    FireTrakInfo.MovableCellList = getNeighborsWest(cell);

                                    //  配置時刻
                                    FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                                    //配置した消防車の位置情報
                                    FireTrakInfo.FireTruckCell = cell;
                                    FireTrakInfo.isAvailable = tempavailable;
                                    //配置した消防車の周囲の水の位置情報
                                    if (CheckBorderCell(x - 1, y + 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x - 1, y + 1]);
                                    }
                                    if (CheckBorderCell(x - 1, y) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x - 1, y]);

                                    }
                                    if (CheckBorderCell(x - 1, y - 1) == true)
                                    {
                                        FireTrakInfo.WaterCellList.Add(dgv[x - 1, y - 1]);
                                    }

                                    //消防車と周辺の水の位置情報のリスト
                                    DeployedFireTruckInfoList.Add(FireTrakInfo);

                                    ////CSV出力するための消防車上の配置時間/位置情報
                                    TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                                }
                                move = true;
                                break;

                            }
                        }
                        else///移動できるcellではなかった　wcは移動可能なcell
                        {
                            wc.Style.BackColor = Color.WhiteSmoke;
                            wc.Value = CELL_VALUE_NORMAL;
                            wc.Style.Format = string.Format("");
                            move = false;
                        }
                    }
                    if (move==false)
                    {
                        if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "△")
                        {
                            mvFtDirection = FireTruckDirection.North;
                        }
                        else if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "▷")
                        {
                            mvFtDirection = FireTruckDirection.East;
                        }
                        else if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "▽")
                        {
                            mvFtDirection = FireTruckDirection.South;
                        }
                        else
                        {
                            mvFtDirection = FireTruckDirection.West;
                        }
                        cell = SelectedFireTruckInfoList[0].FireTruckCell;
                        cell.Style.BackColor = Color.DarkBlue;
                        cell.Value = 100;
                        tempavailable = SelectedFireTruckInfoList[0].isAvailable;
                        x = cell.ColumnIndex;
                        y = cell.RowIndex;

                        //  移動する消防車を一旦削除
                        int index = 0;
                        while (index < DeployedFireTruckInfoList.Count)
                        {
                            if (DeployedFireTruckInfoList[index].FireTruckCell.ColumnIndex == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                && DeployedFireTruckInfoList[index].FireTruckCell.RowIndex == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                            {
                                SelectedFireTruckInfoList[0].FireTruckCell.Value = CELL_VALUE_NORMAL;
                                SelectedFireTruckInfoList[0].FireTruckCell.Style.Format = string.Format("");
                                SelectedFireTruckInfoList[0].FireTruckCell.Style.BackColor = setCellColor((int)SelectedFireTruckInfoList[0].FireTruckCell.Value); ;

                                //  選択可能行動に関する表示更新
                                foreach (DataGridViewCell cl in DeployedFireTruckInfoList[index].MovableCellList)
                                {
                                    if (CELL_VALUE_NORMAL == (int)cl.Value)
                                    {
                                        cl.Style.BackColor = Color.WhiteSmoke;
                                    }
                                }

                                DeployedFireTruckInfoList.RemoveAt(index);

                                buttonWithdraw.Enabled = false;
                                buttonWithdraw.BackColor = Color.LightGray;

                                SelectedFireTruckInfoList.Clear();

                                break;
                            }

                            ++index;
                        }

                        //  再配置処理
                        if ((int)cell.Value == CELL_VALUE_NORMAL)
                        {

                            cell.Style.BackColor = Color.DarkBlue;
                            cell.Value = 100;

                            if (mvFtDirection == FireTruckDirection.North)
                            {
                                cell.Style.Format = string.Format("△");

                                //配置した消防車の位置情報
                                FireTrakInfo.FireTruckCell = cell;

                                //配置した消防車の周囲の水の位置情報
                                if (CheckBorderCell(x - 1, y - 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x - 1, y - 1]);
                                }
                                if (CheckBorderCell(x + 1, y - 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x + 1, y - 1]);

                                }
                                if (CheckBorderCell(x, y - 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x, y - 1]);
                                }

                                //  移動可能先座標の登録
                                FireTrakInfo.MovableCellList = getNeighborsNorth(cell);
                                FireTrakInfo.isAvailable = tempavailable;
                                //  配置時刻
                                FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                                //消防車と周辺の水の位置情報のリスト
                                DeployedFireTruckInfoList.Add(FireTrakInfo);


                                ////CSV出力するための消防車上の配置時間/位置情報
                                //TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                            }

                            else if (mvFtDirection == FireTruckDirection.East)
                            {
                                cell.Style.Format = string.Format("▷");

                                //配置した消防車の位置情報
                                FireTrakInfo.FireTruckCell = cell;

                                //配置した消防車の周囲の水の位置情報
                                if (CheckBorderCell(x + 1, y - 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x + 1, y - 1]);
                                }
                                if (CheckBorderCell(x + 1, y + 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x + 1, y + 1]);

                                }
                                if (CheckBorderCell(x + 1, y) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x + 1, y]);
                                }

                                //  移動可能先座標の登録
                                FireTrakInfo.MovableCellList = getNeighborsEast(cell);
                                FireTrakInfo.isAvailable = tempavailable;
                                //  配置時刻
                                FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                                //消防車と周辺の水の位置情報のリスト
                                DeployedFireTruckInfoList.Add(FireTrakInfo);

                            }
                            else if (mvFtDirection == FireTruckDirection.South)
                            {
                                cell.Style.Format = string.Format("▽");

                                //  移動可能先座標の登録
                                FireTrakInfo.MovableCellList = getNeighborsSouth(cell);

                                //  配置時刻
                                FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                                //配置した消防車の位置情報
                                FireTrakInfo.FireTruckCell = cell;
                                FireTrakInfo.isAvailable = tempavailable;
                                if (CheckBorderCell(x - 1, y + 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x - 1, y + 1]);

                                }
                                if (CheckBorderCell(x, y + 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x, y + 1]);
                                }
                                if (CheckBorderCell(x + 1, y + 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x + 1, y + 1]);
                                }

                                //消防車と周辺の水の位置情報のリスト
                                DeployedFireTruckInfoList.Add(FireTrakInfo);

                                ////CSV出力するための消防車上の配置時間/位置情報
                                TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                            }
                            else if (mvFtDirection == FireTruckDirection.West)
                            {
                                cell.Style.Format = string.Format("◁");

                                //  移動可能先座標の登録
                                FireTrakInfo.MovableCellList = getNeighborsWest(cell);

                                //  配置時刻
                                FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;
                                FireTrakInfo.isAvailable = tempavailable;
                                //配置した消防車の位置情報
                                FireTrakInfo.FireTruckCell = cell;

                                //配置した消防車の周囲の水の位置情報
                                if (CheckBorderCell(x - 1, y + 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x - 1, y + 1]);
                                }
                                if (CheckBorderCell(x - 1, y) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x - 1, y]);

                                }
                                if (CheckBorderCell(x - 1, y - 1) == true)
                                {
                                    FireTrakInfo.WaterCellList.Add(dgv[x - 1, y - 1]);
                                }

                                //消防車と周辺の水の位置情報のリスト
                                DeployedFireTruckInfoList.Add(FireTrakInfo);

                                ////CSV出力するための消防車上の配置時間/位置情報
                                TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                            }

                        }
                    }
                    SelectedFireTruckInfoList.Clear();
                }
                else ///not (0 < SelectedFireTruckInfoList.Count)
                {
                    int index = 0;
                    while (index < DeployedFireTruckInfoList.Count)
                    {
                        if (cell.ColumnIndex == DeployedFireTruckInfoList[index].FireTruckCell.ColumnIndex
                            && cell.RowIndex == DeployedFireTruckInfoList[index].FireTruckCell.RowIndex)
                        {
                            //  撤退準備状態
                            cell.Style.BackColor = Color.LimeGreen;

                            foreach (DataGridViewCell wc in DeployedFireTruckInfoList[index].WaterCellList)
                            {
                                if (0 <= (int)dgv[wc.ColumnIndex, wc.RowIndex].Value && (int)dgv[wc.ColumnIndex, wc.RowIndex].Value != CELL_VALUE_TRUCK)
                                {
                                    wc.Style.BackColor = Color.WhiteSmoke;
                                    wc.Value = CELL_VALUE_NORMAL;
                                    wc.Style.Format = string.Format("");
                                }
                            }

                            DeployedFireTruckInfoList[index].WaterCellList.Clear();

                            //  要素の入れ替え
                            FireTrackInfo Truck = new FireTrackInfo();
                            Truck.FireTruckCell = DeployedFireTruckInfoList[index].FireTruckCell;
                            Truck.WaterCellList = new List<DataGridViewCell>();
                            Truck.previousWorkTimeMilliSec = DeployedFireTruckInfoList[index].previousWorkTimeMilliSec;
                            Truck.isAvailable = DeployedFireTruckInfoList[index].isAvailable;

                            //  移動可能先座標の登録
                            Truck.MovableCellList = new List<DataGridViewCell>();
                            List<DataGridViewCell> MovableCandList = new List<DataGridViewCell>();

                            if (dgv[cell.ColumnIndex, cell.RowIndex].Style.Format == "△")
                            {
                                MovableCandList = getNeighborsNorth(cell);
                            }
                            else if (dgv[cell.ColumnIndex, cell.RowIndex].Style.Format == "▷")
                            {
                                MovableCandList = getNeighborsEast(cell);
                            }
                            else if (dgv[cell.ColumnIndex, cell.RowIndex].Style.Format == "▽")
                            {
                                MovableCandList = getNeighborsSouth(cell);
                            }
                            else
                            {
                                MovableCandList = getNeighborsWest(cell);
                            }

                            foreach (DataGridViewCell cd in MovableCandList)
                            {
                                if ((int)dgv[cd.ColumnIndex, cd.RowIndex].Value == CELL_VALUE_NORMAL
                                    || (int)dgv[cd.ColumnIndex, cd.RowIndex].Value == CELL_VALUE_WATER)
                                {
                                    Truck.MovableCellList.Add(cd);
                                }
                            }

                            //  選択可能行動に関する表示
                            foreach (DataGridViewCell cl in Truck.MovableCellList)
                            {
                                if (CELL_VALUE_NORMAL == (int)cl.Value || (int)cl.Value == CELL_VALUE_WATER)
                                {
                                    cl.Style.BackColor = Color.LightGreen;
                                }
                            }

                            buttonWithdraw.Enabled = true;
                            buttonWithdraw.BackColor = Color.LightGreen;

                            Truck.stanbyTimeMilliSec = stopwatch.ElapsedMilliseconds;

                            DeployedFireTruckInfoList.RemoveAt(index);
                            DeployedFireTruckInfoList.Insert(index, Truck);
                            SelectedFireTruckInfoList.Add(Truck);

                            break;
                        }

                        ++index;
                    }
                }
            }
        }


        private void withdrawFireTruck()
        {
            long presentTimeMillSec = stopwatch.ElapsedMilliseconds;

            int index = 0;
            while (index < DeployedFireTruckInfoList.Count)
            {
                if (DeployedFireTruckInfoList[index].isStanbywithdraw)
                {
                    if (SimEnvSetting.truckWorkIntervalMilliSec < presentTimeMillSec - DeployedFireTruckInfoList[index].stanbyTimeMilliSec)
                    {
                        dgv[DeployedFireTruckInfoList[index].FireTruckCell.ColumnIndex, DeployedFireTruckInfoList[index].FireTruckCell.RowIndex].Value = CELL_VALUE_NORMAL;
                        dgv[DeployedFireTruckInfoList[index].FireTruckCell.ColumnIndex, DeployedFireTruckInfoList[index].FireTruckCell.RowIndex].Style.Format = "";
                        dgv[DeployedFireTruckInfoList[index].FireTruckCell.ColumnIndex, DeployedFireTruckInfoList[index].FireTruckCell.RowIndex].Style.BackColor = Color.WhiteSmoke;
                        DeployedFireTruckInfoList.RemoveAt(index);

                        ++numFireTruck;
                        numTruck.Text = numFireTruck.ToString();

                        if (numFireTruck <= 0)
                        {
                            isFireTrackDeployButtonPushed = false;
                            numFireTruck = 0;//残数を0にする
                            picTrack.Image = ImageCross;
                        }
                        else
                        {
                            picTrack.Image = ImageFireTruck;
                        }
                    }
                    else
                    {
                        ++index;
                    }
                }
                else
                {

                    ++index;
                }
            }
        }



        private void BtnTrack1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 境界のセルであればtrueを返す
        /// </summary>
        /// <param name="cellPosX"></param>
        /// <param name="cellPosY"></param>
        /// <returns></returns>
        bool CheckBorderCell(int cellPosX, int cellPosY)
        {
            if (0 <= cellPosX && cellPosX <= LATERAL_CELL_NUM - 1 && 0 <= cellPosY && cellPosY <= VERTICAL_CELL_NUM - 1)
            {
                return true;
            }

            return false;
        }



        bool FireControl(int wf, int windLevel, int fireSourceX, int fireSourceY, out NewFireCellInfo NewFire)
        {
            NewFire = new NewFireCellInfo();
            bool isFireSpreded = false;

            int posXCand1 = 0, posXCand2 = 0, posXCand3 = 0;
            int posYCand1 = 0, posYCand2 = 0, posYCand3 = 0;

            //  風向に基づく、延焼先候補座標の決定
            switch (wf)
            {
                case 1:
                    posXCand1 = fireSourceX;//中央
                    posYCand1 = fireSourceY - 1;
                    posXCand2 = fireSourceX - 1;//左
                    posYCand2 = fireSourceY - 1;
                    posXCand3 = fireSourceX + 1;//右
                    posYCand3 = fireSourceY - 1;
                    break;

                case 2://北東
                    posXCand1 = fireSourceX + 1;//
                    posYCand1 = fireSourceY - 1;
                    posXCand2 = fireSourceX;//
                    posYCand2 = fireSourceY - 1;
                    posXCand3 = fireSourceX + 1;//
                    posYCand3 = fireSourceY;
                    break;

                case 3://東
                    posXCand1 = fireSourceX + 1;//
                    posYCand1 = fireSourceY;
                    posXCand2 = fireSourceX + 1;//
                    posYCand2 = fireSourceY - 1;
                    posXCand3 = fireSourceX + 1;//
                    posYCand3 = fireSourceY + 1;
                    break;


                case 4://南東
                    posXCand1 = fireSourceX + 1;//
                    posYCand1 = fireSourceY + 1;
                    posXCand2 = fireSourceX + 1;//
                    posYCand2 = fireSourceY;
                    posXCand3 = fireSourceX;//
                    posYCand3 = fireSourceY + 1;
                    break;

                case 5://南
                    posXCand1 = fireSourceX;//
                    posYCand1 = fireSourceY + 1;
                    posXCand2 = fireSourceX - 1;//
                    posYCand2 = fireSourceY + 1;
                    posXCand3 = fireSourceX + 1;//
                    posYCand3 = fireSourceY + 1;
                    break;

                case 6://南西
                    posXCand1 = fireSourceX - 1;//
                    posYCand1 = fireSourceY + 1;
                    posXCand2 = fireSourceX - 1;//
                    posYCand2 = fireSourceY;
                    posXCand3 = fireSourceX;//
                    posYCand3 = fireSourceY + 1;
                    break;

                case 7://西
                    posXCand1 = fireSourceX - 1;//
                    posYCand1 = fireSourceY;
                    posXCand2 = fireSourceX - 1;//
                    posYCand2 = fireSourceY + 1;
                    posXCand3 = fireSourceX - 1;//
                    posYCand3 = fireSourceY - 1;
                    break;

                case 8://北西
                    posXCand1 = fireSourceX - 1;//
                    posYCand1 = fireSourceY - 1;
                    posXCand2 = fireSourceX - 1;//
                    posYCand2 = fireSourceY;
                    posXCand3 = fireSourceX;//
                    posYCand3 = fireSourceY - 1;
                    break;
            }


            //  風速レベルに基づく、延焼確率の決定
            int possibilityCenter = 0;
            int possibilitySide = 0;

            switch (windLevel)
            {
                case 1:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV1;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV1;
                    break;
                case 2:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV2;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV2;
                    break;
                case 3:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV3;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV3;
                    break;
                case 4:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV4;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV4;
                    break;
                case 5:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV5;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV5;
                    break;
                default:
                    break;

            }


            //  延焼処理
            bool isCatchFire = false;

            if (CheckBorderCell(posXCand1, posYCand1) == true)
            {
                if ((int)makeRandomInt(1, 10) <= possibilityCenter)
                {
                    //Console.WriteLine(NewFireList.Count);

                    // 延焼先候補が水ではなかったら
                    if ((int)dgv[posXCand1, posYCand1].Value != 10)
                    {
                        // ①延焼先候補が消防車だった場合
                        if ((int)dgv[posXCand1, posYCand1].Value == CELL_VALUE_TRUCK)
                        {
                            foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                            {
                                if (posXCand1 == ft.FireTruckCell.ColumnIndex
                                        && posYCand1 == ft.FireTruckCell.RowIndex)
                                {
                                    NewFire.xColumnIndex = posXCand1;
                                    NewFire.yRowIndex = posYCand1;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    //  放水を解除
                                    int i = 0;
                                    while (i < ft.WaterCellList.Count())
                                    {
                                        if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                        {
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                        }

                                        ++i;
                                    }

                                    //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                    dgv[posXCand1, posYCand1].Style.Format = "";
                                    ft.WaterCellList.Clear();
                                    DeployedFireTruckInfoList.Remove(ft);
                                    ++numLossedFireTruck;

                                    isFireSpreded = true;

                                    break;

                                }

                            }
                            if (SelectedFireTruckInfoList.Count>0)
                            {
                                if (posXCand1 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                && posYCand1 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                {
                                    foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                    {
                                        if (CELL_VALUE_NORMAL == (int)cl.Value)
                                        {
                                            cl.Style.BackColor = Color.WhiteSmoke;
                                        }
                                    }
                                    SelectedFireTruckInfoList.Clear();
                                    buttonWithdraw.Enabled = false;
                                    buttonWithdraw.BackColor = Color.LightGray;
                                }
                            }
                        }
                        // ②延焼先候補にすでに火がついていた場合
                        else if ((int)dgv[posXCand1, posYCand1].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[posXCand1, posYCand1].Value)
                        {
                            switch ((int)dgv[posXCand1, posYCand1].Value)
                            {
                                case -1:
                                case -2:
                                case -3:
                                case -4:
                                    NewFire.xColumnIndex = posXCand1;
                                    NewFire.yRowIndex = posYCand1;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    isFireSpreded = true;
                                    break;
                                case -5:
                                    break;
                            }
                        }
                        // ③延焼先候補に火がついていない場合
                        else if ((int)dgv[posXCand1, posYCand1].Value == CELL_VALUE_NORMAL)
                        {
                            NewFire.xColumnIndex = posXCand1;
                            NewFire.yRowIndex = posYCand1;
                            NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                            isFireSpreded = true;

                        }
                    }

                    isCatchFire = true;
                    return isFireSpreded;
                }
            }

            if (isCatchFire == false && CheckBorderCell(posXCand2, posYCand2) == true)
            {
                if ((int)makeRandomInt(1, 10) <= possibilitySide)
                {
                    // 延焼先候補が水ではなかったら
                    if ((int)dgv[posXCand2, posYCand2].Value != 10)
                    {
                        // ①延焼先候補が消防車だった場合
                        if ((int)dgv[posXCand2, posYCand2].Value == CELL_VALUE_TRUCK)
                        {
                            foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                            {
                                if (posXCand2 == ft.FireTruckCell.ColumnIndex
                                        && posYCand2 == ft.FireTruckCell.RowIndex)
                                {
                                    NewFire.xColumnIndex = posXCand2;
                                    NewFire.yRowIndex = posYCand2;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    //  放水を解除
                                    int i = 0;
                                    while (i < ft.WaterCellList.Count())
                                    {
                                        if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                        {
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                        }

                                        ++i;
                                    }

                                    //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                    dgv[posXCand2, posYCand2].Style.Format = "";
                                    ft.WaterCellList.Clear();
                                    DeployedFireTruckInfoList.Remove(ft);
                                    ++numLossedFireTruck;

                                    isFireSpreded = true;

                                    break;

                                }

                            }
                            if (SelectedFireTruckInfoList.Count > 0)
                            {
                                if (posXCand2 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                && posYCand2 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                {
                                    foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                    {
                                        if (CELL_VALUE_NORMAL == (int)cl.Value)
                                        {
                                            cl.Style.BackColor = Color.WhiteSmoke;
                                        }
                                    }
                                    SelectedFireTruckInfoList.Clear();
                                    buttonWithdraw.Enabled = false;
                                    buttonWithdraw.BackColor = Color.LightGray;
                                }
                            }
                        }
                        // ②延焼先候補にすでに火がついていた場合
                        else if ((int)dgv[posXCand2, posYCand2].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[posXCand2, posYCand2].Value)
                        {
                            switch ((int)dgv[posXCand2, posYCand2].Value)
                            {
                                case -1:
                                case -2:
                                case -3:
                                case -4:
                                    NewFire.xColumnIndex = posXCand2;
                                    NewFire.yRowIndex = posYCand2;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                                    isFireSpreded = true;
                                    break;
                                case -5:
                                    break;
                            }
                        }
                        // ③延焼先候補に火がついていない場合
                        else if ((int)dgv[posXCand2, posYCand2].Value == CELL_VALUE_NORMAL)
                        {
                            NewFire.xColumnIndex = posXCand2;
                            NewFire.yRowIndex = posYCand2;
                            NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                            isFireSpreded = true;
                        }
                        
                    }

                    isCatchFire = true;
                    return isFireSpreded;
                }
            }


            if (isCatchFire == false && CheckBorderCell(posXCand3, posYCand3) == true)
            {
                if ((int)makeRandomInt(1, 10) <= possibilitySide)
                {
                    // 延焼先候補が水ではなかったら
                    if ((int)dgv[posXCand3, posYCand3].Value != 10)
                    {
                        // ①延焼先候補が消防車だった場合

                        if ((int)dgv[posXCand3, posYCand3].Value == CELL_VALUE_TRUCK)
                        {
                            foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                            {
                                if (posXCand3 == ft.FireTruckCell.ColumnIndex
                                        && posYCand3 == ft.FireTruckCell.RowIndex)
                                {
                                    NewFire.xColumnIndex = posXCand3;
                                    NewFire.yRowIndex = posYCand3;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    //  放水を解除
                                    int i = 0;
                                    while (i < ft.WaterCellList.Count())
                                    {
                                        if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                        {
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                        }

                                        ++i;
                                    }

                                    //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                    dgv[posXCand3, posYCand3].Style.Format = "";
                                    ft.WaterCellList.Clear();
                                    DeployedFireTruckInfoList.Remove(ft);
                                    ++numLossedFireTruck;

                                    isFireSpreded = true;

                                    break;

                                }

                            }
                            if (SelectedFireTruckInfoList.Count > 0)
                            {
                                if (posXCand3 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                && posYCand3 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                {
                                    foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                    {
                                        if (CELL_VALUE_NORMAL == (int)cl.Value)
                                        {
                                            cl.Style.BackColor = Color.WhiteSmoke;
                                        }
                                    }
                                    SelectedFireTruckInfoList.Clear();
                                    buttonWithdraw.Enabled = false;
                                    buttonWithdraw.BackColor = Color.LightGray;
                                }
                            }
                        }
                        // ②延焼先候補にすでに火がついていた場合
                        else if ((int)dgv[posXCand3, posYCand3].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[posXCand3, posYCand3].Value)
                        {
                            Console.WriteLine(String.Format("x: {0}, y: {1}", posXCand3, posYCand3));

                            switch ((int)dgv[posXCand3, posYCand3].Value)
                            {
                                case -1:
                                case -2:
                                case -3:
                                case -4:
                                    NewFire.xColumnIndex = posXCand3;
                                    NewFire.yRowIndex = posYCand3;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    isFireSpreded = true;
                                    break;
                                case -5:
                                    break;
                            }

                        }
                        else if ((int)dgv[posXCand3, posYCand3].Value == CELL_VALUE_NORMAL)
                        {
                            NewFire.xColumnIndex = posXCand3;
                            NewFire.yRowIndex = posYCand3;
                            NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                            isFireSpreded = true;

                        }
                    }

                }

                isCatchFire = true;
                return isFireSpreded;
            }

            return false;

        }

        bool FireControl1(int wf, int windLevel, int fireSourceX, int fireSourceY, out NewFireCellInfo NewFire, out NewFireCellInfo NewFire1)
        {
            NewFire = new NewFireCellInfo();
            NewFire1 = new NewFireCellInfo();
            bool isFireSpreded = false;

            int posXCand1 = 0, posXCand2 = 0, posXCand3 = 0;
            int posYCand1 = 0, posYCand2 = 0, posYCand3 = 0;
            int PosXCand1 = 0, PosXCand2 = 0, PosXCand3 = 0;
            int PosYCand1 = 0, PosYCand2 = 0, PosYCand3 = 0;

            //  風向に基づく、延焼先候補座標の決定
            switch (wf)
            {
                case 1:
                    posXCand1 = fireSourceX;//中央
                    posYCand1 = fireSourceY - 1;
                    posXCand2 = fireSourceX - 1;//左
                    posYCand2 = fireSourceY - 1;
                    posXCand3 = fireSourceX + 1;//右
                    posYCand3 = fireSourceY - 1;
                    PosXCand1 = fireSourceX;//中央
                    PosYCand1 = fireSourceY - 2;
                    PosXCand2 = fireSourceX - 1;//左
                    PosYCand2 = fireSourceY - 2;
                    PosXCand3 = fireSourceX + 1;//右
                    PosYCand3 = fireSourceY - 2;
                    break;

                case 2://北東
                    posXCand1 = fireSourceX + 1;//
                    posYCand1 = fireSourceY - 1;
                    posXCand2 = fireSourceX;//
                    posYCand2 = fireSourceY - 1;
                    posXCand3 = fireSourceX + 1;//
                    posYCand3 = fireSourceY;
                    PosXCand1 = fireSourceX + 2;//
                    PosYCand1 = fireSourceY - 2;
                    PosXCand2 = fireSourceX;//
                    PosYCand2 = fireSourceY - 2;
                    PosXCand3 = fireSourceX + 2;//
                    PosYCand3 = fireSourceY;
                    break;

                case 3://東
                    posXCand1 = fireSourceX + 1;//
                    posYCand1 = fireSourceY;
                    posXCand2 = fireSourceX + 1;//
                    posYCand2 = fireSourceY - 1;
                    posXCand3 = fireSourceX + 1;//
                    posYCand3 = fireSourceY + 1;
                    PosXCand1 = fireSourceX + 2;//
                    PosYCand1 = fireSourceY;
                    PosXCand2 = fireSourceX + 2;//
                    PosYCand2 = fireSourceY - 1;
                    PosXCand3 = fireSourceX + 2;//
                    PosYCand3 = fireSourceY + 1;
                    break;


                case 4://南東
                    posXCand1 = fireSourceX + 1;//
                    posYCand1 = fireSourceY + 1;
                    posXCand2 = fireSourceX + 1;//
                    posYCand2 = fireSourceY;
                    posXCand3 = fireSourceX;//
                    posYCand3 = fireSourceY + 1;
                    PosXCand1 = fireSourceX + 2;//
                    PosYCand1 = fireSourceY + 2;
                    PosXCand2 = fireSourceX + 2;//
                    PosYCand2 = fireSourceY;
                    PosXCand3 = fireSourceX;//
                    PosYCand3 = fireSourceY + 2;
                    break;

                case 5://南
                    posXCand1 = fireSourceX;//
                    posYCand1 = fireSourceY + 1;
                    posXCand2 = fireSourceX - 1;//
                    posYCand2 = fireSourceY + 1;
                    posXCand3 = fireSourceX + 1;//
                    posYCand3 = fireSourceY + 1;
                    PosXCand1 = fireSourceX;//
                    PosYCand1 = fireSourceY + 2;
                    PosXCand2 = fireSourceX - 1;//
                    PosYCand2 = fireSourceY + 2;
                    PosXCand3 = fireSourceX + 1;//
                    PosYCand3 = fireSourceY + 2;
                    break;

                case 6://南西
                    posXCand1 = fireSourceX - 1;//
                    posYCand1 = fireSourceY + 1;
                    posXCand2 = fireSourceX - 1;//
                    posYCand2 = fireSourceY;
                    posXCand3 = fireSourceX;//
                    posYCand3 = fireSourceY + 1;
                    PosXCand1 = fireSourceX - 2;//
                    PosYCand1 = fireSourceY + 2;
                    PosXCand2 = fireSourceX - 2;//
                    PosYCand2 = fireSourceY;
                    PosXCand3 = fireSourceX;//
                    PosYCand3 = fireSourceY + 2;
                    break;

                case 7://西
                    posXCand1 = fireSourceX - 1;//
                    posYCand1 = fireSourceY;
                    posXCand2 = fireSourceX - 1;//
                    posYCand2 = fireSourceY + 1;
                    posXCand3 = fireSourceX - 1;//
                    posYCand3 = fireSourceY - 1;
                    PosXCand1 = fireSourceX - 2;//
                    PosYCand1 = fireSourceY;
                    PosXCand2 = fireSourceX - 2;//
                    PosYCand2 = fireSourceY + 1;
                    PosXCand3 = fireSourceX - 2;//
                    PosYCand3 = fireSourceY - 1;
                    break;

                case 8://北西
                    posXCand1 = fireSourceX - 1;//
                    posYCand1 = fireSourceY - 1;
                    posXCand2 = fireSourceX - 1;//
                    posYCand2 = fireSourceY;
                    posXCand3 = fireSourceX;//
                    posYCand3 = fireSourceY - 1;
                    PosXCand1 = fireSourceX - 2;//
                    PosYCand1 = fireSourceY - 2;
                    PosXCand2 = fireSourceX - 2;//
                    PosYCand2 = fireSourceY;
                    PosXCand3 = fireSourceX;//
                    PosYCand3 = fireSourceY - 2;
                    break;
            }


            //  風速レベルに基づく、延焼確率の決定
            int possibilityCenter = 0;
            int possibilitySide = 0;

            switch (windLevel)
            {
                case 1:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV1;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV1;
                    break;
                case 2:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV2;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV2;
                    break;
                case 3:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV3;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV3;
                    break;
                case 4:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV4;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV4;
                    break;
                case 5:
                    possibilityCenter = SimEnvSetting.FIRE_SPRED_POSSIBILITY_CENTER_LV5;
                    possibilitySide = SimEnvSetting.FIRE_SPRED_POSSIBILITY_SIDE_LV5;
                    break;
                default:
                    break;

            }


            //  延焼処理
            bool isCatchFire = false;

            if (CheckBorderCell(posXCand1, posYCand1) == true)
            {
                if ((int)makeRandomInt(1, 10) <= possibilityCenter)
                {
                    // 延焼先候補が水ではなかったら
                    if ((int)dgv[posXCand1, posYCand1].Value != 10)
                    {
                        // ①延焼先候補が消防車だった場合
                        if ((int)dgv[posXCand1, posYCand1].Value == CELL_VALUE_TRUCK)
                        {
                            foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                            {
                                if (posXCand1 == ft.FireTruckCell.ColumnIndex
                                        && posYCand1 == ft.FireTruckCell.RowIndex)
                                {
                                    NewFire.xColumnIndex = posXCand1;
                                    NewFire.yRowIndex = posYCand1;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    //  放水を解除
                                    int i = 0;
                                    while (i < ft.WaterCellList.Count())
                                    {
                                        if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                        {
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                        }

                                        ++i;
                                    }

                                    //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                    dgv[posXCand1, posYCand1].Style.Format = "";
                                    ft.WaterCellList.Clear();
                                    DeployedFireTruckInfoList.Remove(ft);
                                    ++numLossedFireTruck;

                                    isFireSpreded = true;

                                    break;

                                }

                            }
                            if (SelectedFireTruckInfoList.Count > 0)
                            {
                                if (posXCand1 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                && posYCand1 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                {
                                    foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                    {
                                        if (CELL_VALUE_NORMAL == (int)cl.Value)
                                        {
                                            cl.Style.BackColor = Color.WhiteSmoke;
                                        }
                                    }
                                    SelectedFireTruckInfoList.Clear();
                                    buttonWithdraw.Enabled = false;
                                    buttonWithdraw.BackColor = Color.LightGray;
                                }
                            }
                        }
                        // ②延焼先候補にすでに火がついていた場合
                        else if ((int)dgv[posXCand1, posYCand1].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[posXCand1, posYCand1].Value)
                        {
                            switch ((int)dgv[posXCand1, posYCand1].Value)
                            {
                                case -1:
                                case -2:
                                case -3:
                                case -4:
                                    NewFire.xColumnIndex = posXCand1;
                                    NewFire.yRowIndex = posYCand1;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                                    isFireSpreded = true;
                                    break;
                                case -5:
                                    break;
                            }
                        }
                        // ③延焼先候補に火がついていない場合
                        else if ((int)dgv[posXCand1, posYCand1].Value == CELL_VALUE_NORMAL)
                        {
                            NewFire.xColumnIndex = posXCand1;
                            NewFire.yRowIndex = posYCand1;
                            NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                            isFireSpreded = true;

                        }
                        isCatchFire = true;
                    }
                    if(isCatchFire)
                    {
                        if (CheckBorderCell(PosXCand1, PosYCand1) == true)
                        {
                            if ((int)makeRandomInt(1, 10) <= possibilityCenter)
                            {
                                // 延焼先候補が水ではなかったら
                                if ((int)dgv[PosXCand1, PosYCand1].Value != 10)
                                {
                                    // ①延焼先候補が消防車だった場合
                                    if ((int)dgv[PosXCand1, PosYCand1].Value == CELL_VALUE_TRUCK)
                                    {
                                        foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                                        {
                                            if (PosXCand1 == ft.FireTruckCell.ColumnIndex
                                                    && PosYCand1 == ft.FireTruckCell.RowIndex)
                                            {
                                                NewFire1.xColumnIndex = PosXCand1;
                                                NewFire1.yRowIndex = PosYCand1;
                                                NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                                //  放水を解除
                                                int i = 0;
                                                while (i < ft.WaterCellList.Count())
                                                {
                                                    if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                                    {
                                                        dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                                        dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                                    }

                                                    ++i;
                                                }

                                                //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                                dgv[PosXCand1, PosYCand1].Style.Format = "";
                                                ft.WaterCellList.Clear();
                                                DeployedFireTruckInfoList.Remove(ft);
                                                ++numLossedFireTruck;
                                                break;

                                            }

                                        }
                                        if (SelectedFireTruckInfoList.Count > 0)
                                        {
                                            if (PosXCand1 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                            && PosYCand1 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                            {
                                                foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                                {
                                                    if (CELL_VALUE_NORMAL == (int)cl.Value)
                                                    {
                                                        cl.Style.BackColor = Color.WhiteSmoke;
                                                    }
                                                }
                                                SelectedFireTruckInfoList.Clear();
                                                buttonWithdraw.Enabled = false;
                                                buttonWithdraw.BackColor = Color.LightGray;
                                            }
                                        }
                                    }
                                    // ②延焼先候補にすでに火がついていた場合
                                    else if ((int)dgv[PosXCand1, PosYCand1].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[PosXCand1, PosYCand1].Value)
                                    {
                                        switch ((int)dgv[PosXCand1, PosYCand1].Value)
                                        {
                                            case -1:
                                            case -2:
                                            case -3:
                                            case -4:
                                                NewFire1.xColumnIndex = PosXCand1;
                                                NewFire1.yRowIndex = PosYCand1;
                                                NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                                                break;
                                            case -5:
                                                break;
                                        }
                                    }
                                    // ③延焼先候補に火がついていない場合
                                    else if ((int)dgv[PosXCand1, PosYCand1].Value == CELL_VALUE_NORMAL)
                                    {
                                        NewFire1.xColumnIndex = PosXCand1;
                                        NewFire1.yRowIndex = PosYCand1;
                                        NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    }
                                }

                            }
                        }
                    }
                    return isFireSpreded;
                }
            }

            if (isCatchFire == false && CheckBorderCell(posXCand2, posYCand2) == true)
            {
                if ((int)makeRandomInt(1, 10) <= possibilitySide)
                {
                    // 延焼先候補が水ではなかったら
                    if ((int)dgv[posXCand2, posYCand2].Value != 10)
                    {
                        // ①延焼先候補が消防車だった場合
                        if ((int)dgv[posXCand2, posYCand2].Value == CELL_VALUE_TRUCK)
                        {
                            foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                            {
                                if (posXCand2 == ft.FireTruckCell.ColumnIndex
                                        && posYCand2 == ft.FireTruckCell.RowIndex)
                                {
                                    NewFire.xColumnIndex = posXCand2;
                                    NewFire.yRowIndex = posYCand2;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    //  放水を解除
                                    int i = 0;
                                    while (i < ft.WaterCellList.Count())
                                    {
                                        if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                        {
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                        }

                                        ++i;
                                    }

                                    //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                    dgv[posXCand2, posYCand2].Style.Format = "";
                                    ft.WaterCellList.Clear();
                                    DeployedFireTruckInfoList.Remove(ft);
                                    ++numLossedFireTruck;

                                    isFireSpreded = true;

                                    break;

                                }

                            }
                            if (SelectedFireTruckInfoList.Count > 0)
                            {
                                if (posXCand2 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                && posYCand2 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                {
                                    foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                    {
                                        if (CELL_VALUE_NORMAL == (int)cl.Value)
                                        {
                                            cl.Style.BackColor = Color.WhiteSmoke;
                                        }
                                    }
                                    SelectedFireTruckInfoList.Clear();
                                    buttonWithdraw.Enabled = false;
                                    buttonWithdraw.BackColor = Color.LightGray;
                                }
                            }
                        }
                        // ②延焼先候補にすでに火がついていた場合
                        else if ((int)dgv[posXCand2, posYCand2].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[posXCand2, posYCand2].Value)
                        {
                            switch ((int)dgv[posXCand2, posYCand2].Value)
                            {
                                case -1:
                                case -2:
                                case -3:
                                case -4:
                                    NewFire.xColumnIndex = posXCand2;
                                    NewFire.yRowIndex = posYCand2;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    isFireSpreded = true;
                                    break;
                                case -5:
                                    break;
                            }
                        }
                        // ③延焼先候補に火がついていない場合
                        else if ((int)dgv[posXCand2, posYCand2].Value == CELL_VALUE_NORMAL)
                        {
                            NewFire.xColumnIndex = posXCand2;
                            NewFire.yRowIndex = posYCand2;
                            NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                            isFireSpreded = true;
                        }
                        isCatchFire = true;

                    }
                    if (isCatchFire)
                    {
                        if (CheckBorderCell(PosXCand2, PosYCand2) == true)
                        {
                            if ((int)makeRandomInt(1, 10) <= possibilityCenter)
                            {
                                // 延焼先候補が水ではなかったら
                                if ((int)dgv[PosXCand2, PosYCand2].Value != 10)
                                {
                                    // ①延焼先候補が消防車だった場合
                                    if ((int)dgv[PosXCand2, PosYCand2].Value == CELL_VALUE_TRUCK)
                                    {
                                        foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                                        {
                                            if (PosXCand2 == ft.FireTruckCell.ColumnIndex
                                                    && PosYCand2 == ft.FireTruckCell.RowIndex)
                                            {
                                                NewFire1.xColumnIndex = PosXCand2;
                                                NewFire1.yRowIndex = PosYCand2;
                                                NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                                //  放水を解除
                                                int i = 0;
                                                while (i < ft.WaterCellList.Count())
                                                {
                                                    if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                                    {
                                                        dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                                        dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                                    }

                                                    ++i;
                                                }

                                                //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                                dgv[PosXCand2, PosYCand2].Style.Format = "";
                                                ft.WaterCellList.Clear();
                                                DeployedFireTruckInfoList.Remove(ft);
                                                ++numLossedFireTruck;
                                                break;

                                            }

                                        }
                                        if (SelectedFireTruckInfoList.Count > 0)
                                        {
                                            if (PosXCand2 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                            && PosYCand2 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                            {
                                                foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                                {
                                                    if (CELL_VALUE_NORMAL == (int)cl.Value)
                                                    {
                                                        cl.Style.BackColor = Color.WhiteSmoke;
                                                    }
                                                }
                                                SelectedFireTruckInfoList.Clear();
                                                buttonWithdraw.Enabled = false;
                                                buttonWithdraw.BackColor = Color.LightGray;
                                            }
                                        }
                                    }
                                    // ②延焼先候補にすでに火がついていた場合
                                    else if ((int)dgv[PosXCand2, PosYCand2].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[PosXCand2, PosYCand2].Value)
                                    {
                                        switch ((int)dgv[PosXCand2, PosYCand2].Value)
                                        {
                                            case -1:
                                            case -2:
                                            case -3:
                                            case -4:
                                                NewFire1.xColumnIndex = PosXCand2;
                                                NewFire1.yRowIndex = PosYCand2;
                                                NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                                                break;
                                            case -5:
                                                break;
                                        }
                                    }
                                    // ③延焼先候補に火がついていない場合
                                    else if ((int)dgv[PosXCand2, PosYCand2].Value == CELL_VALUE_NORMAL)
                                    {
                                        NewFire1.xColumnIndex = PosXCand2;
                                        NewFire1.yRowIndex = PosYCand2;
                                        NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    }
                                }

                            }
                        }
                    }
                    return isFireSpreded;
                }
            }


            if (isCatchFire == false && CheckBorderCell(posXCand3, posYCand3) == true)
            {
                if ((int)makeRandomInt(1, 10) <= possibilitySide)
                {
                    // 延焼先候補が水ではなかったら
                    if ((int)dgv[posXCand3, posYCand3].Value != 10)
                    {
                        // ①延焼先候補が消防車だった場合

                        if ((int)dgv[posXCand3, posYCand3].Value == CELL_VALUE_TRUCK)
                        {
                            foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                            {
                                if (posXCand3 == ft.FireTruckCell.ColumnIndex
                                        && posYCand3 == ft.FireTruckCell.RowIndex)
                                {
                                    NewFire.xColumnIndex = posXCand3;
                                    NewFire.yRowIndex = posYCand3;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    //  放水を解除
                                    int i = 0;
                                    while (i < ft.WaterCellList.Count())
                                    {
                                        if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                        {
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                            dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                        }

                                        ++i;
                                    }

                                    //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                    dgv[posXCand3, posYCand3].Style.Format = "";
                                    ft.WaterCellList.Clear();
                                    DeployedFireTruckInfoList.Remove(ft);
                                    ++numLossedFireTruck;

                                    isFireSpreded = true;

                                    break;

                                }

                            }
                            if (SelectedFireTruckInfoList.Count > 0)
                            {
                                if (posXCand3 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                && posYCand3 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                {
                                    foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                    {
                                        if (CELL_VALUE_NORMAL == (int)cl.Value)
                                        {
                                            cl.Style.BackColor = Color.WhiteSmoke;
                                        }
                                    }
                                    SelectedFireTruckInfoList.Clear();
                                    buttonWithdraw.Enabled = false;
                                    buttonWithdraw.BackColor = Color.LightGray;
                                }
                            }
                        }
                        // ②延焼先候補にすでに火がついていた場合
                        else if ((int)dgv[posXCand3, posYCand3].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[posXCand3, posYCand3].Value)
                        {
                            Console.WriteLine(String.Format("x: {0}, y: {1}", posXCand3, posYCand3));

                            switch ((int)dgv[posXCand3, posYCand3].Value)
                            {
                                case -1:
                                case -2:
                                case -3:
                                case -4:
                                    NewFire.xColumnIndex = posXCand3;
                                    NewFire.yRowIndex = posYCand3;
                                    NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                    isFireSpreded = true;
                                    break;
                                case -5:
                                    break;
                            }

                        }
                        else if ((int)dgv[posXCand3, posYCand3].Value == CELL_VALUE_NORMAL)
                        {
                            NewFire.xColumnIndex = posXCand3;
                            NewFire.yRowIndex = posYCand3;
                            NewFire.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                            isFireSpreded = true;

                        }
                    }
                    isCatchFire = true;

                }
                if (isCatchFire)
                {
                    if (CheckBorderCell(PosXCand3, PosYCand3) == true)
                    {
                        if ((int)makeRandomInt(1, 10) <= possibilityCenter)
                        {
                            // 延焼先候補が水ではなかったら
                            if ((int)dgv[PosXCand3, PosYCand3].Value != 10)
                            {
                                // ①延焼先候補が消防車だった場合
                                if ((int)dgv[PosXCand3, PosYCand3].Value == CELL_VALUE_TRUCK)
                                {
                                    foreach (FireTrackInfo ft in DeployedFireTruckInfoList)
                                    {
                                        if (PosXCand3 == ft.FireTruckCell.ColumnIndex
                                                && PosYCand3 == ft.FireTruckCell.RowIndex)
                                        {
                                            NewFire1.xColumnIndex = PosXCand3;
                                            NewFire1.yRowIndex = PosYCand3;
                                            NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                            //  放水を解除
                                            int i = 0;
                                            while (i < ft.WaterCellList.Count())
                                            {
                                                if ((int)dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value == CELL_VALUE_WATER) // 放水対象のセルであれば
                                                {
                                                    dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Value = CELL_VALUE_NORMAL;
                                                    dgv[ft.WaterCellList[i].ColumnIndex, ft.WaterCellList[i].RowIndex].Style.BackColor = Color.WhiteSmoke;
                                                }

                                                ++i;
                                            }

                                            //  消失した消防車を削除（※消防車のセルの値の変更は1タイムステップごとにまとめて行う）
                                            dgv[PosXCand3, PosYCand3].Style.Format = "";
                                            ft.WaterCellList.Clear();
                                            DeployedFireTruckInfoList.Remove(ft);
                                            ++numLossedFireTruck;
                                            break;

                                        }

                                    }
                                    if (SelectedFireTruckInfoList.Count > 0)
                                    {
                                        if (PosXCand3 == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                                        && PosYCand3 == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                                        {
                                            foreach (DataGridViewCell cl in SelectedFireTruckInfoList[0].MovableCellList)
                                            {
                                                if (CELL_VALUE_NORMAL == (int)cl.Value)
                                                {
                                                    cl.Style.BackColor = Color.WhiteSmoke;
                                                }
                                            }
                                            SelectedFireTruckInfoList.Clear();
                                            buttonWithdraw.Enabled = false;
                                            buttonWithdraw.BackColor = Color.LightGray;
                                        }
                                    }
                                }
                                // ②延焼先候補にすでに火がついていた場合
                                else if ((int)dgv[PosXCand3, PosYCand3].Value <= -1 && (int)dgv[fireSourceX, fireSourceY].Value < (int)dgv[PosXCand3, PosYCand3].Value)
                                {
                                    switch ((int)dgv[PosXCand3, PosYCand3].Value)
                                    {
                                        case -1:
                                        case -2:
                                        case -3:
                                        case -4:
                                            NewFire1.xColumnIndex = PosXCand3;
                                            NewFire1.yRowIndex = PosYCand3;
                                            NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;
                                            break;
                                        case -5:
                                            break;
                                    }
                                }
                                // ③延焼先候補に火がついていない場合
                                else if ((int)dgv[PosXCand3, PosYCand3].Value == CELL_VALUE_NORMAL)
                                {
                                    NewFire1.xColumnIndex = PosXCand3;
                                    NewFire1.yRowIndex = PosYCand3;
                                    NewFire1.fireLevel = (int)dgv[fireSourceX, fireSourceY].Value;

                                }
                            }

                        }
                    }
                }
                return isFireSpreded;
            }

            return false;

        }

        /// <summary>
        /// 
        /// </summary>
        //////風による延焼方向設定
        void spreadFire()
        {
            //  延焼の判定
            NewFireCellList.Clear();
            if (firemode == 2 && timerTickCounter>=60)//2マス以上燃え広がる処理
            {
                for (int x = 0; x < LATERAL_CELL_NUM; x++)
                {
                    for (int y = 0; y < VERTICAL_CELL_NUM; y++)
                    {
                        if ((int)dgv[x, y].Value < 0)
                        {
                            NewFireCellInfo NewFire = new NewFireCellInfo();
                            NewFireCellInfo NewFire1 = new NewFireCellInfo();
                            if (Gustxl<=x && x<=Gustxr && Gustyu<=y && y<=Gustyb)
                            {
                                spreadDirection = GustDirection;
                                spreadLevel = GustLevel;
                            }
                            else
                            {
                                spreadDirection = windDirection;
                                spreadLevel = windLevel;
                            }
                            bool isFireSpreded = FireControl1(spreadDirection, spreadLevel, x, y, out NewFire, out NewFire1);

                            if (isFireSpreded)
                            {
                                NewFireCellList.Add(NewFire);
                                NewFireCellList.Add(NewFire1);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < LATERAL_CELL_NUM; x++)
                {
                    for (int y = 0; y < VERTICAL_CELL_NUM; y++)
                    {
                        if ((int)dgv[x, y].Value < 0)
                        {
                            NewFireCellInfo NewFire = new NewFireCellInfo();
                            if (Gustxl <= x && x <= Gustxr && Gustyu <= y && y <= Gustyb)
                            {
                                spreadDirection = GustDirection;
                                spreadLevel = GustLevel;
                            }
                            else
                            {
                                spreadDirection = windDirection;
                                spreadLevel = windLevel;
                            }
                            bool isFireSpreded = FireControl(spreadDirection, spreadLevel, x, y, out NewFire);

                            if (isFireSpreded)
                            {
                                NewFireCellList.Add(NewFire);
                            }
                        }
                    }
                }
            }
            

            //  延焼対象セルの値と色を変える
            foreach (NewFireCellInfo fc in NewFireCellList)
            {
                dgv[fc.xColumnIndex, fc.yRowIndex].Value = fc.fireLevel;
                dgv[fc.xColumnIndex, fc.yRowIndex].Style.BackColor = setCellColor(fc.fireLevel);

                if (dgv[fc.xColumnIndex, fc.yRowIndex].Style.Format != "")
                {
                    dgv[fc.xColumnIndex, fc.yRowIndex].Style.Format = string.Format("");
                }
            }
        }


        //セルの色を設定する
        Color setCellColor(int level)
        {
            switch (level)
            {
                case CELL_VALUE_NORMAL:
                    return Color.WhiteSmoke;
                case CELL_VALUE_WATER:
                    return Color.Blue;
                case CELL_VALUE_TRUCK:
                    return Color.DarkBlue;
                case CELL_VALUE_FIRE_LV1:
                    return Color.IndianRed;
                case CELL_VALUE_FIRE_LV2:
                    return Color.Red;
                case CELL_VALUE_FIRE_LV3:
                    return Color.OrangeRed;
                default:
                    return Color.Yellow;
            }
        }

        //自動化関数
        private void AutoTruck()
        {
            if (timerTickCounter == 3)
            {
                isFireTrackDeployButtonPushed = true;
                ftDirection = FireTruckDirection.South;
                handleCellClick(dgv[10,8]);
                isFireTrackDeployButtonPushed = true;
                ftDirection = FireTruckDirection.South;
                handleCellClick(dgv[5, 3]);
            }
            if (timerTickCounter == 5)
            {
                isFireTrackDeployButtonPushed = true;
                ftDirection = FireTruckDirection.North;
                handleCellClick(dgv[11, 12]);
                isFireTrackDeployButtonPushed = true;
                ftDirection = FireTruckDirection.North;
                handleCellClick(dgv[5, 6]);
            }
            if (timerTickCounter == 15)
            {
                handleCellClick(dgv[5, 6]);
                Withdraw();
                handleCellClick(dgv[5, 3]);
                Withdraw();
                handleCellClick(dgv[11, 12]);
                handleCellClick(dgv[10, 11]);
            }

        }

        private void actFireTruck()
        {
            int index = 0;
            while(index < DeployedFireTruckInfoList.Count)
            {
                if(DeployedFireTruckInfoList[index].isAvailable)
                {
                    long presentTimeSec = stopwatch.ElapsedMilliseconds;
                    if (SimEnvSetting.truckWorkIntervalMilliSec < presentTimeSec - DeployedFireTruckInfoList[index].previousWorkTimeMilliSec)
                    {
                        DoFireFight(DeployedFireTruckInfoList[index]);

                        FireTrackInfo NewData = new FireTrackInfo();
                        NewData = DeployedFireTruckInfoList[index];
                        NewData.previousWorkTimeMilliSec = presentTimeSec;

                        DeployedFireTruckInfoList.RemoveAt(index);
                        DeployedFireTruckInfoList.Insert(index, NewData);
                    }
                }
                ++index;
            }
        }




        //消化行動決定
        private void DoFireFight(FireTrackInfo TgtFireTrackInfo)
        {
            foreach (DataGridViewCell c in TgtFireTrackInfo.WaterCellList)
            {
                switch ((int)c.Value)
                {
                    case 10:
                        c.Style.BackColor = Color.Blue;
                        c.Value = 10;
                        break;

                    case 100:
                        c.Value = 100;
                        break;

                    case 0:
                        c.Style.BackColor = Color.Blue;
                        c.Value = 10;
                        break;

                    case -1:
                        c.Value = 10;
                        c.Style.BackColor = Color.Blue;
                        break;

                    case -2:
                        c.Value = -1;
                        c.Style.BackColor = Color.IndianRed;
                        //FireCellDataList.Add(new FireCellData() { fireTimeSec = stopwatch.ElapsedMilliseconds, x = c.ColumnIndex, y = c.RowIndex, Level = (int)c.Value });
                        break;

                    case -3:
                        c.Value = -2;
                        c.Style.BackColor = Color.Red;
                        //FireCellDataList.Add(new FireCellData() { fireTimeSec = stopwatch.ElapsedMilliseconds, x = c.ColumnIndex, y = c.RowIndex, Level = (int)c.Value });
                        break;
                }
            }


        }


        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            handleCellClick(dgv[e.ColumnIndex, e.RowIndex]);
        }
        // DGVのCellClickイベント・ハンドラ

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            handleCellClick(dgv[e.ColumnIndex, e.RowIndex]);

            string elapsedTime = String.Format("{0}", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("CellClick" + elapsedTime);

        }


        // DGVのSelectionChangedイベント・ハンドラ
        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            // セルを選択状態にさせない
            dgv.ClearSelection();
        }

        private void dgv_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("dgv_DragEnter");
        }


        private void dgv_DragDrop(object sender, DragEventArgs e)
        {

        }

        //FireTrackイベント
        private void btnTrack1_Click(object sender, EventArgs e)
        {
            ftDirection = FireTruckDirection.North;
            if (numFireTruck == 0)
            {
                isFireTrackDeployButtonPushed = false;
                BtnReset();
            }
            else
            {
                isFireTrackDeployButtonPushed = true;
                BtnReset();
                btnTrack1.BackColor = Color.LightBlue;
            }

            string elapsedTime = String.Format("{0}", stopwatch.ElapsedMilliseconds);
            UserActionList.Add("◆btnTrack1_Click");
        }


        private void btnTrack2_Click_1(object sender, EventArgs e)
        {
            ftDirection = FireTruckDirection.East;
            if (numFireTruck == 0)
            {
                isFireTrackDeployButtonPushed = false;
                BtnReset();
            }
            else
            {
                isFireTrackDeployButtonPushed = true;
                BtnReset();
                btnTrack2.BackColor = Color.LightBlue;
            }

            string elapsedTime = String.Format("{0}", stopwatch.ElapsedMilliseconds);
            UserActionList.Add("◆btnTrack2_Click");
        }


        private void btnTrack3_Click_1(object sender, EventArgs e)
        {
            ftDirection = FireTruckDirection.South;
            if (numFireTruck == 0)
            {
                isFireTrackDeployButtonPushed = false;
                BtnReset();
            }
            else
            {
                isFireTrackDeployButtonPushed = true;
                BtnReset();
                btnTrack3.BackColor = Color.LightBlue;
            }

            string elapsedTime = String.Format("{0}", stopwatch.ElapsedMilliseconds);
            UserActionList.Add("◆btnTrack3_Click");
        }


        private void btnTrack4_Click_1(object sender, EventArgs e)
        {
            ftDirection = FireTruckDirection.West;
            if (numFireTruck == 0)
            {
                isFireTrackDeployButtonPushed = false;
                BtnReset();
            }
            else
            {
                isFireTrackDeployButtonPushed = true;
                BtnReset();
                btnTrack4.BackColor = Color.LightBlue;
            }

            string elapsedTime = String.Format("{0}", stopwatch.ElapsedMilliseconds);
            UserActionList.Add("◆btnTrack4_Click");
        }

        private void BtnReset()
        {
            //盤面の消防車を選択してたらそれをリセット
            if (0 < SelectedFireTruckInfoList.Count)
            {
                FireTruckDirection mvFtDirection;
                FireTrackInfo FireTrakInfo = new FireTrackInfo();
                FireTrakInfo.WaterCellList = new List<DataGridViewCell>();
                FireTrakInfo.MovableCellList = new List<DataGridViewCell>();

                if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "△")
                {
                    mvFtDirection = FireTruckDirection.North;
                }
                else if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "▷")
                {
                    mvFtDirection = FireTruckDirection.East;
                }
                else if (SelectedFireTruckInfoList[0].FireTruckCell.Style.Format == "▽")
                {
                    mvFtDirection = FireTruckDirection.South;
                }
                else
                {
                    mvFtDirection = FireTruckDirection.West;
                }
                DataGridViewCell cell;
                cell = SelectedFireTruckInfoList[0].FireTruckCell;
                cell.Style.BackColor = Color.DarkBlue;
                cell.Value = 100;
                tempavailable = SelectedFireTruckInfoList[0].isAvailable;
                int x = cell.ColumnIndex;
                int y = cell.RowIndex;

                //  移動する消防車を一旦削除
                int index = 0;
                while (index < DeployedFireTruckInfoList.Count)
                {
                    if (DeployedFireTruckInfoList[index].FireTruckCell.ColumnIndex == SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex
                        && DeployedFireTruckInfoList[index].FireTruckCell.RowIndex == SelectedFireTruckInfoList[0].FireTruckCell.RowIndex)
                    {
                        SelectedFireTruckInfoList[0].FireTruckCell.Value = CELL_VALUE_NORMAL;
                        SelectedFireTruckInfoList[0].FireTruckCell.Style.Format = string.Format("");
                        SelectedFireTruckInfoList[0].FireTruckCell.Style.BackColor = setCellColor((int)SelectedFireTruckInfoList[0].FireTruckCell.Value); ;

                        //  選択可能行動に関する表示更新
                        foreach (DataGridViewCell cl in DeployedFireTruckInfoList[index].MovableCellList)
                        {
                            if (CELL_VALUE_NORMAL == (int)cl.Value)
                            {
                                cl.Style.BackColor = Color.WhiteSmoke;
                            }
                        }

                        DeployedFireTruckInfoList.RemoveAt(index);

                        buttonWithdraw.Enabled = false;
                        buttonWithdraw.BackColor = Color.LightGray;

                        SelectedFireTruckInfoList.Clear();

                        break;
                    }

                    ++index;
                }
                //  再配置処理
                if ((int)cell.Value == CELL_VALUE_NORMAL)
                {

                    cell.Style.BackColor = Color.DarkBlue;
                    cell.Value = 100;

                    if (mvFtDirection == FireTruckDirection.North)
                    {
                        cell.Style.Format = string.Format("△");

                        //配置した消防車の位置情報
                        FireTrakInfo.FireTruckCell = cell;

                        //配置した消防車の周囲の水の位置情報
                        if (CheckBorderCell(x - 1, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y - 1]);
                        }
                        if (CheckBorderCell(x + 1, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y - 1]);

                        }
                        if (CheckBorderCell(x, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x, y - 1]);
                        }

                        //  移動可能先座標の登録
                        FireTrakInfo.MovableCellList = getNeighborsNorth(cell);
                        FireTrakInfo.isAvailable = tempavailable;
                        //  配置時刻
                        FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                        //消防車と周辺の水の位置情報のリスト
                        DeployedFireTruckInfoList.Add(FireTrakInfo);


                        ////CSV出力するための消防車上の配置時間/位置情報
                        //TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                    }

                    else if (mvFtDirection == FireTruckDirection.East)
                    {
                        cell.Style.Format = string.Format("▷");

                        //配置した消防車の位置情報
                        FireTrakInfo.FireTruckCell = cell;

                        //配置した消防車の周囲の水の位置情報
                        if (CheckBorderCell(x + 1, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y - 1]);
                        }
                        if (CheckBorderCell(x + 1, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y + 1]);

                        }
                        if (CheckBorderCell(x + 1, y) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y]);
                        }

                        //  移動可能先座標の登録
                        FireTrakInfo.MovableCellList = getNeighborsEast(cell);
                        FireTrakInfo.isAvailable = tempavailable;
                        //  配置時刻
                        FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                        //消防車と周辺の水の位置情報のリスト
                        DeployedFireTruckInfoList.Add(FireTrakInfo);

                    }
                    else if (mvFtDirection == FireTruckDirection.South)
                    {
                        cell.Style.Format = string.Format("▽");

                        //  移動可能先座標の登録
                        FireTrakInfo.MovableCellList = getNeighborsSouth(cell);

                        //  配置時刻
                        FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;

                        //配置した消防車の位置情報
                        FireTrakInfo.FireTruckCell = cell;
                        FireTrakInfo.isAvailable = tempavailable;
                        if (CheckBorderCell(x - 1, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y + 1]);

                        }
                        if (CheckBorderCell(x, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x, y + 1]);
                        }
                        if (CheckBorderCell(x + 1, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x + 1, y + 1]);
                        }

                        //消防車と周辺の水の位置情報のリスト
                        DeployedFireTruckInfoList.Add(FireTrakInfo);

                        ////CSV出力するための消防車上の配置時間/位置情報
                        TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                    }
                    else if (mvFtDirection == FireTruckDirection.West)
                    {
                        cell.Style.Format = string.Format("◁");

                        //  移動可能先座標の登録
                        FireTrakInfo.MovableCellList = getNeighborsWest(cell);

                        //  配置時刻
                        FireTrakInfo.previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds;
                        FireTrakInfo.isAvailable = tempavailable;
                        //配置した消防車の位置情報
                        FireTrakInfo.FireTruckCell = cell;

                        //配置した消防車の周囲の水の位置情報
                        if (CheckBorderCell(x - 1, y + 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y + 1]);
                        }
                        if (CheckBorderCell(x - 1, y) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y]);

                        }
                        if (CheckBorderCell(x - 1, y - 1) == true)
                        {
                            FireTrakInfo.WaterCellList.Add(dgv[x - 1, y - 1]);
                        }

                        //消防車と周辺の水の位置情報のリスト
                        DeployedFireTruckInfoList.Add(FireTrakInfo);

                        ////CSV出力するための消防車上の配置時間/位置情報
                        TruckDataList.Add(new TruckCellData() { elapsedTime = stopwatch.ElapsedMilliseconds.ToString(), previousWorkTimeMilliSec = stopwatch.ElapsedMilliseconds, FireTruck = cell });
                    }

                }
            }
            SelectedFireTruckInfoList.Clear();
            btnTrack1.BackColor = Color.LightGray;
            btnTrack2.BackColor = Color.LightGray;
            btnTrack3.BackColor = Color.LightGray;
            btnTrack4.BackColor = Color.LightGray;
        }



        private void changeWindDirectionIcon()
        {
            cBWindDirection.MaxDropDownItems = 9;

            WindDirection wd = (WindDirection)windDirection;

            switch (wd)
            {
                case WindDirection.None:
                    picBoxWindDirection.Image = null;
                    break;
                case WindDirection.North:
                    picBoxWindDirection.Image = ImageArrowNorth;
                    break;
                case WindDirection.NorthEast:
                    picBoxWindDirection.Image = ImageArrowNorthEast;
                    break;
                case WindDirection.East:
                    picBoxWindDirection.Image = ImageArrowEast;
                    break;
                case WindDirection.SouthEast:
                    picBoxWindDirection.Image = ImageArrowSouthEast;
                    break;
                case WindDirection.South:
                    picBoxWindDirection.Image = ImageArrowSouth;
                    break;
                case WindDirection.SouthWest:
                    picBoxWindDirection.Image = ImageArrowSouthWest;
                    break;
                case WindDirection.West:
                    picBoxWindDirection.Image = ImageArrowWest;
                    break;
                case WindDirection.NorthWest:
                    picBoxWindDirection.Image = ImageArrowNorthWest;
                    break;
            }
        }


        ////風設定と風向画像の設定
        private void cBWindDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            cBWindDirection.MaxDropDownItems = 9;

            if (cBWindDirection.SelectedIndex == 0)
            {
                picBoxWindDirection.Image = null;
            }
            else if (cBWindDirection.SelectedIndex == 1)
            {
                windDirection = 1;
                picBoxWindDirection.Image = ImageArrowNorth;
            }
            else if (cBWindDirection.SelectedIndex == 2)
            {
                windDirection = 2;
                picBoxWindDirection.Image = ImageArrowNorthEast;
            }
            else if (cBWindDirection.SelectedIndex == 3)
            {
                windDirection = 3;
                picBoxWindDirection.Image = ImageArrowEast;
            }
            else if (cBWindDirection.SelectedIndex == 4)
            {
                windDirection = 4;
                picBoxWindDirection.Image = ImageArrowSouthEast;
            }
            else if (cBWindDirection.SelectedIndex == 5)
            {
                windDirection = 5;
                picBoxWindDirection.Image = ImageArrowSouth;
            }
            else if (cBWindDirection.SelectedIndex == 6)
            {
                windDirection = 6;
                picBoxWindDirection.Image = ImageArrowSouthWest;
            }
            else if (cBWindDirection.SelectedIndex == 7)
            {
                windDirection = 7;
                picBoxWindDirection.Image = ImageArrowWest;
            }
            else if (cBWindDirection.SelectedIndex == 8)
            {
                windDirection = 8;
                picBoxWindDirection.Image = ImageArrowNorthWest;
            }
        }


        //風速レベル設定
        public void nudWindLevel_ValueChanged(object sender, EventArgs e)
        {

            windLevel = (int)nudWindLevel.Value;
            lbWindLevel.Text = windLevel.ToString();

            SimEnvSetting.setFireSpredIntervalSec(windLevel);

        }

        private void numTrack_Click(object sender, EventArgs e)
        {

        }

        //消防車残数設定
        private void nudNumOfNormalFireTruck_ValueChanged(object sender, EventArgs e)
        {
            int a;
            a = (int)nudNumOfNormalFireTruck.Value;
            numTruck.Text = a.ToString();
        }

        //火災Level1設定
        private void nudNumOfFire1_ValueChanged(object sender, EventArgs e)
        {
            int ma;
            ma = (int)nudNumOfFire1.Value;
            numFireLv1 = ma;
        }

        //火災Level2設定
        private void nudNumOfFire2_ValueChanged(object sender, EventArgs e)
        {
            int mb;
            mb = (int)nudNumOfFire2.Value;
            numFireLv2 = mb;
        }
        //火災Level3設定
        private void nudNumOfFire3_ValueChanged(object sender, EventArgs e)
        {
            int mc;
            mc = (int)nudNumOfFire3.Value;
            numFireLv3 = mc;
        }
        //火災Level4設定
        private void nudNumOfFire4_ValueChanged(object sender, EventArgs e)
        {
            int md;
            md = (int)nudNumOfFire4.Value;
            numFireLv4 = md;
        }
        //火災Level5設定
        private void nudNumOfFire5_ValueChanged(object sender, EventArgs e)
        {
            int me;
            me = (int)nudNumOfFire5.Value;
            numFireLv5 = me;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void buttonHelp_Click(object sender, EventArgs e)
        {
            helpRequestTimeMilliSec = stopwatch.ElapsedMilliseconds;
            isHelpRequest = true;
            //buttonHelp.BackColor = Color.LightBlue;
            //                                                                                                                              buttonHelp.Enabled = false;

            UserActionList.Add("応援要請");
        }


        private void waitingHelp()
        {
            if(isHelpRequest)
            {
                if(SimEnvSetting.helpLatencyTimeMIlliSec < stopwatch.ElapsedMilliseconds - helpRequestTimeMilliSec)
                {
                    numFireTruck += SimEnvSetting.supportFireTruckNum;
                    numTruck.Text = numFireTruck.ToString();

                    if (numFireTruck <= 0)
                    {
                        isFireTrackDeployButtonPushed = false;
                        numFireTruck = 0;//残数を0にする
                        picTrack.Image = ImageCross;
                    }
                    else
                    {
                        picTrack.Image = ImageFireTruck;
                    }

                    isHelpRequest = false;
                    //buttonHelp.BackColor = Color.LightGray;
                }
            }
        }


        private void setTimeLimit()
        {
            int timeLimitH = timeLimitSec / 3600;
            int timeLimitM = (timeLimitSec - 3600 * timeLimitH) / 60;
            int timeLimitS = (timeLimitSec - 3600 * timeLimitH - 60 * timeLimitM);
            textBoxTimeLimit.Text = string.Format("{0}:{1}:{2}", timeLimitH.ToString().PadLeft(2, '0'), timeLimitM.ToString().PadLeft(2, '0'), timeLimitS.ToString().PadLeft(2, '0'));

            textBoxElapsedTime.BackColor = Color.White;
        }

        //シナリオ設定

        private void ToolStripMenuItemTraining1_Click(object sender, EventArgs e)
        {
            initGame();
            expPhase = "tra";
            DataIO.WriteAndCloseLogWriterIfNotNull();
            SelectNumber = 0;
            scenarioNum = traScenario[SelectNumber];
            ++SelectNumber;
            tra = true;
            exp = false;
            test = false;

            string scenarioName = DataIO.readScenarioSettingFiles(expPhase, scenarioNum, out timeLimitSec, out numFireTruck, out FireBreakoutSettingList, out WindSettingList, out firemode, out windmode, out GustList);

            textBoxScenarioName.Text = scenarioName;
            if (scenarioName == "Error")
            {
                textBoxScenarioName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxScenarioName.BackColor = Color.LightGray;
            }

            DataIO.setStreamWriter(scenarioName);

            numTruck.Text = numFireTruck.ToString();

            setTimeLimit();
        }


        private void ToolStripMenuItemTraining2_Click(object sender, EventArgs e)
        {
            initGame();
            exPhase = "Lv";
            SelectNumber = 0;
            test = true;
        }


        private void ToolStripMenuItemExp1_Click(object sender, EventArgs e)
        {
            initGame();
            expPhase = "test";
            DataIO.WriteAndCloseLogWriterIfNotNull();
            SelectNumber = 0;
            ScenarioNumber();
            scenarioNum = expScenario[SelectNumber];
            ++SelectNumber;
            tra = false;
            exp = true;
            test = false;

            string scenarioName = DataIO.readScenarioSettingFiles(expPhase, scenarioNum, out timeLimitSec, out numFireTruck, out FireBreakoutSettingList, out WindSettingList, out firemode, out windmode,out GustList);

            textBoxScenarioName.Text = scenarioName;
            if (scenarioName == "Error")
            {
                textBoxScenarioName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxScenarioName.BackColor = Color.LightGray;
            }

            DataIO.setStreamWriter(scenarioName);

            numTruck.Text = numFireTruck.ToString();

            setTimeLimit();
        }

        private void Lv1button_Click(object sender, EventArgs e)
        {
            ScenarioNumber();
            if (tra || exp || test)
            {
                DataIO.WriteAndCloseLogWriterIfNotNull();
                initGame();

                expPhase = exPhase + "1";
                if (test)
                {
                    scenarioNum = testScenario[SelectNumber];
                }
                ++SelectNumber;

                string scenarioName = DataIO.readScenarioSettingFiles(expPhase, scenarioNum, out timeLimitSec, out numFireTruck, out FireBreakoutSettingList, out WindSettingList, out firemode, out windmode,out GustList);

                textBoxScenarioName.Text = scenarioName;
                if (scenarioName == "Error")
                {
                    textBoxScenarioName.BackColor = Color.LightSalmon;
                }
                else
                {
                    textBoxScenarioName.BackColor = Color.LightGray;
                }

                DataIO.setStreamWriter(scenarioName);

                numTruck.Text = numFireTruck.ToString();

                setTimeLimit();
            }
            
        }

        private void Lv2button_Click(object sender, EventArgs e)
        {
            ScenarioNumber();
            SelectNumber = 0;
            if (tra || exp || test)
            {
                DataIO.WriteAndCloseLogWriterIfNotNull();
                initGame();

                expPhase = exPhase + "2";
                if (test)
                {
                    scenarioNum = testScenario[SelectNumber];
                }
                ++SelectNumber;

                string scenarioName = DataIO.readScenarioSettingFiles(expPhase, scenarioNum, out timeLimitSec, out numFireTruck, out FireBreakoutSettingList, out WindSettingList, out firemode, out windmode, out GustList);

                textBoxScenarioName.Text = scenarioName;
                if (scenarioName == "Error")
                {
                    textBoxScenarioName.BackColor = Color.LightSalmon;
                }
                else
                {
                    textBoxScenarioName.BackColor = Color.LightGray;
                }

                DataIO.setStreamWriter(scenarioName);

                numTruck.Text = numFireTruck.ToString();

                setTimeLimit();
            }
        }

        private void Lv3button_Click(object sender, EventArgs e)
        {
            ScenarioNumber();
            SelectNumber = 0;
            if (tra || exp || test)
            {
                DataIO.WriteAndCloseLogWriterIfNotNull();
                initGame();

                expPhase = exPhase + "3";
                if (test)
                {
                    scenarioNum = testScenario[SelectNumber];
                }
                ++SelectNumber;

                string scenarioName = DataIO.readScenarioSettingFiles(expPhase, scenarioNum, out timeLimitSec, out numFireTruck, out FireBreakoutSettingList, out WindSettingList, out firemode, out windmode, out GustList);

                textBoxScenarioName.Text = scenarioName;
                if (scenarioName == "Error")
                {
                    textBoxScenarioName.BackColor = Color.LightSalmon;
                }
                else
                {
                    textBoxScenarioName.BackColor = Color.LightGray;
                }

                DataIO.setStreamWriter(scenarioName);

                numTruck.Text = numFireTruck.ToString();

                setTimeLimit();
            }
        }

        private void Lv4button_Click(object sender, EventArgs e)
        {
            ScenarioNumber();
            SelectNumber = 0;
            if (tra || exp || test)
            {
                DataIO.WriteAndCloseLogWriterIfNotNull();
                initGame();

                expPhase = exPhase + "4";
                if (test)
                {
                    scenarioNum = testScenario[SelectNumber];
                }
                ++SelectNumber;

                string scenarioName = DataIO.readScenarioSettingFiles(expPhase, scenarioNum, out timeLimitSec, out numFireTruck, out FireBreakoutSettingList, out WindSettingList, out firemode, out windmode, out GustList);

                textBoxScenarioName.Text = scenarioName;
                if (scenarioName == "Error")
                {
                    textBoxScenarioName.BackColor = Color.LightSalmon;
                }
                else
                {
                    textBoxScenarioName.BackColor = Color.LightGray;
                }

                DataIO.setStreamWriter(scenarioName);

                numTruck.Text = numFireTruck.ToString();

                setTimeLimit();
            }
        }


        private void buttonNext_Click(object sender, EventArgs e)
        {
            DataIO.WriteAndCloseLogWriterIfNotNull();
            initGame();

            if(tra)
            {
                scenarioNum = SelectNumber;
            }

            if(test)
            {
                scenarioNum = testScenario[SelectNumber];
            }
            if (exp)
            {
                scenarioNum = SelectNumber;
            }
            ++SelectNumber;

            string scenarioName = DataIO.readScenarioSettingFiles(expPhase, scenarioNum, out timeLimitSec, out numFireTruck, out FireBreakoutSettingList, out WindSettingList, out firemode, out windmode, out GustList);
            DataIO.setStreamWriter(scenarioName);


            textBoxScenarioName.Text = scenarioName;
            if (scenarioName == "Error")
            {
                textBoxScenarioName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxScenarioName.BackColor = Color.Silver;
            }

            numTruck.Text = numFireTruck.ToString();

            setTimeLimit();
        }


        private bool changeEnvSetting()
        {
            //  火の設定
            int posX = 0;
            int posY = 0;

            int indexFire = 0;
            while (indexFire < FireBreakoutSettingList.Count)
            {
                if (FireBreakoutSettingList[indexFire].fireTimeMilliSec <= stopwatch.ElapsedMilliseconds)
                {
                    posX = FireBreakoutSettingList[indexFire].x;
                    posY = FireBreakoutSettingList[indexFire].y;
                    if((int)dgv[posX, posY].Value == 0)
                    {
                        dgv[posX, posY].Value = FireBreakoutSettingList[indexFire].level;
                        dgv[posX, posY].Style.BackColor = setCellColor(FireBreakoutSettingList[indexFire].level);

                        FireBreakoutSettingList.RemoveAt(indexFire);

                        UserActionList.Add("出火: (" + posX.ToString() + "," + posY.ToString() + ")");
                    }
                    else
                    {
                        ++indexFire;
                    }
                    
                }
                else
                {
                    ++indexFire;
                }
            }

            //  風の設定
            int indexWind = 0;
            bool isWindChange = false;

            while (indexWind < WindSettingList.Count)
            {
                if (WindSettingList[indexWind].windSetTimeMilliSec <= stopwatch.ElapsedMilliseconds)
                {

                    if(WindSettingList[indexWind].isdisplay==0)
                    {
                        windDirection = (int)WindSettingList[indexWind].direction;
                        windLevel = WindSettingList[indexWind].level;
                        changeWindDirectionIcon(); // 風向アイコンの変更
                        lbWindLevel.Text = windLevel.ToString();
                        UserActionList.Add("風の変化");
                        SimEnvSetting.setFireSpredIntervalSec(windLevel);
                        WindChangeTime = WindSettingList[indexWind].windSetTimeMilliSec;
                    }
                    else if(WindSettingList[indexWind].isdisplay == 1)
                    {
                        windDirection = (int)WindSettingList[indexWind].direction;
                        windLevel = WindSettingList[indexWind].level;
                        UserActionList.Add("風の変化(表示なし)");
                        SimEnvSetting.setFireSpredIntervalSec(windLevel);
                        WindChangeTime = WindSettingList[indexWind].windSetTimeMilliSec;
                    }
                    else
                    {
                        windDirection = (int)WindSettingList[indexWind].direction;
                        windLevel = WindSettingList[indexWind].level;
                        picBoxWindDirection.Image = null;
                        lbWindLevel.Text = windLevel.ToString();
                        UserActionList.Add("風の変化(消失)");
                    }

                    WindSettingList.RemoveAt(indexWind);

                    isWindChange = true;

                    

                }
                else
                {
                    ++indexWind;
                }
            }
            if(windmode==2)
            {
                int indexGust = 0;
                while (indexGust < GustList.Count)
                {
                    if (GustList[indexGust].windSetTimeMilliSec <= stopwatch.ElapsedMilliseconds)
                    {
                        GustDirection = (int)GustList[indexGust].direction;
                        GustLevel = GustList[indexGust].level;
                        GustChangeTime = GustList[indexGust].windSetTimeMilliSec;
                        Gustxl = GustList[indexGust].leftx;//風向の変数
                        Gustxr = GustList[indexGust].rightx;//風速レベル変数
                        Gustyu = GustList[indexGust].uppery;//風向の変数
                        Gustyb = GustList[indexGust].botomy;//風速レベル変数
                        UserActionList.Add("突風の発生");
                        GustList.RemoveAt(indexGust);

                    }
                    else
                    {
                        ++indexGust;
                    }
                }
            }

            return isWindChange;
        }
        //撤退操作
        private void Withdraw()
        {
            if (0 < SelectedFireTruckInfoList.Count)
            {
                if(SelectedFireTruckInfoList[0].isAvailable)
                {
                    UserActionList.Add("●消防車撤退指示:" + " (" + SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex.ToString() + "," + SelectedFireTruckInfoList[0].FireTruckCell.RowIndex.ToString() + ")");
                }
                else
                {
                    UserActionList.Add("●無能消防車撤退指示:" + " (" + SelectedFireTruckInfoList[0].FireTruckCell.ColumnIndex.ToString() + "," + SelectedFireTruckInfoList[0].FireTruckCell.RowIndex.ToString() + ")");
                }

                //  要素の入れ替え
                FireTrackInfo Truck = new FireTrackInfo();
                Truck.FireTruckCell = SelectedFireTruckInfoList[0].FireTruckCell;
                Truck.WaterCellList = new List<DataGridViewCell>();
                Truck.MovableCellList = SelectedFireTruckInfoList[0].MovableCellList;
                Truck.stanbyTimeMilliSec = stopwatch.ElapsedMilliseconds;
                Truck.isStanbywithdraw = true;

                int index = 0;
                while (index < DeployedFireTruckInfoList.Count)
                {
                    if (DeployedFireTruckInfoList[index].FireTruckCell.ColumnIndex == Truck.FireTruckCell.ColumnIndex
                        && DeployedFireTruckInfoList[index].FireTruckCell.RowIndex == Truck.FireTruckCell.RowIndex)
                    {
                        DeployedFireTruckInfoList.RemoveAt(index);
                        DeployedFireTruckInfoList.Insert(index, Truck);

                        //  選択可能行動に関する表示更新
                        foreach (DataGridViewCell cl in DeployedFireTruckInfoList[index].MovableCellList)
                        {
                            if (CELL_VALUE_NORMAL == (int)cl.Value)
                            {
                                cl.Style.BackColor = Color.WhiteSmoke;
                            }
                        }

                        buttonWithdraw.Enabled = false;
                        buttonWithdraw.BackColor = Color.LightGray;

                        SelectedFireTruckInfoList.Clear();

                        break;
                    }

                    ++index;
                }
            }
        }
        //withdrawを押したとき
        private void buttonWithdraw_Click(object sender, EventArgs e)
        {
            Withdraw();
            UserActionList.Add("◆withdrawbtn_Click");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            WindSettingList.Clear();
        }

        private void textBoxElapsedTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void ScenarioNumber()
        {
            int a = 1;
            setNumber = false;

            for(int i=0; i<10; i++)
            {
                while(setNumber == false)
                {
                    a = (int)makeRandomInt(1, 10);

                    for(int j=0; j<=i; j++)
                    {
                        if(testScenario[j] == a)
                        {
                            setNumber = false;
                            break;
                        }
                        else
                        {
                            setNumber = true;
                        }
                    }
                }

                testScenario[i] = a;
                setNumber = false;
            }
        }

        private void Dgv_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 乱数を生成し、返す関数
        /// </summary>
        /// <param name="maxNumber"></param>
        /// <returns></returns>
        private uint makeRandomInt(int minNumber, int maxNumber)
        {
            return RandomValue.GetRandomValue(minNumber, maxNumber);
        }



        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


    }
}

