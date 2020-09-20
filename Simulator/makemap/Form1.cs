using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace makemap
{
    public partial class Form1 : Form
    {
        static public string DataFolderPath = Directory.GetCurrentDirectory() + "\\Data";
        static StreamWriter LogWriteMap; // ファイルへのデータ書き込み用
        public Form1()
        {
            InitializeComponent();
        }
        const int CELL_SIZE = 26; // セルのサイズ
        const int LATERAL_CELL_NUM = 32; // フィールドの幅
        const int VERTICAL_CELL_NUM = 21; // フィールドの高さ
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            handleCellClick(dataGridView1[e.ColumnIndex, e.RowIndex]);
        }

        void handleCellClick(DataGridViewCell cell)
        {
            int x = cell.ColumnIndex;
            int y = cell.RowIndex;

            if ((int)cell.Value == 0)
            {
                cell.Value = 1;
                cell.Style.BackColor = Color.IndianRed;
            }
            else
            {
                cell.Value = 0;
                cell.Style.BackColor = Color.WhiteSmoke;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ShowCellToolTips = false; // 答えが見えないように


            //// フィールドの作成
            dataGridView1.RowTemplate.Height = CELL_SIZE; // 追加される行の高さ
            dataGridView1.ColumnCount = LATERAL_CELL_NUM;

            dataGridView1.RowCount = VERTICAL_CELL_NUM;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Width = CELL_SIZE;
            }

            // フォームのサイズをDGVに合わせる
            DataGridViewCell cell = dataGridView1[0, 0];
            this.ClientSize = new Size(cell.Size.Width * LATERAL_CELL_NUM + 3, cell.Size.Height * VERTICAL_CELL_NUM + 3);

            for (int x = 0; x < LATERAL_CELL_NUM; x++)
            {
                for (int y = 0; y < VERTICAL_CELL_NUM; y++)
                {
                    dataGridView1[x, y].Value = 0;
                    dataGridView1[x, y].Style.ForeColor = Color.WhiteSmoke;
                    dataGridView1[x, y].Style.BackColor = Color.WhiteSmoke;
                    dataGridView1[x, y].Style.Format = "";
                }
            }
        }
        void Writemap()
        {
            DateTime dt = DateTime.Now;
            string streamForData = DataFolderPath + "\\map" + "_" + dt.ToString("yyMMdd_HHmmss") + ".txt";
            LogWriteMap = new StreamWriter(streamForData);

            string outputData1 = "180 7 1";
            LogWriteMap.WriteLine(outputData1);

            string outputData2 = " ";
            LogWriteMap.WriteLine(outputData2);
            for (int x = 0; x < LATERAL_CELL_NUM; x++)
            {
                for (int y = 0; y < VERTICAL_CELL_NUM; y++)
                {
                    if ((int)dataGridView1[x, y].Value == 1)
                    {
                        string outputDataText = "0" + " " + x.ToString() + " " + y.ToString() + " "+ "-1";

                        LogWriteMap.WriteLine(outputDataText);
                    }
                }
            }
            LogWriteMap.Close();
            for (int x = 0; x < LATERAL_CELL_NUM; x++)
            {
                for (int y = 0; y < VERTICAL_CELL_NUM; y++)
                {
                    dataGridView1[x, y].Value = 0;
                    dataGridView1[x, y].Style.ForeColor = Color.WhiteSmoke;
                    dataGridView1[x, y].Style.BackColor = Color.WhiteSmoke;
                    dataGridView1[x, y].Style.Format = "";
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Writemap();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            handleCellClick(dataGridView1[e.ColumnIndex, e.RowIndex]);
        }
    }
}
