using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AgOpenGPS
{
    public partial class FormSteerGraph : Form
    {
        private readonly FormGPS mf = null;

        //chart data
        private string dataSteerAngle = "0";

        private string dataPWM = "-1";

        private string dataXTE = "0";
        private string dataXTEActual = "0";
        private string dataError = "0";

        public FormSteerGraph(Form callingForm)
        {
            mf = callingForm as FormGPS;
            InitializeComponent();

            this.label5.Text = gStr.gsSetPoint;
            this.label1.Text = gStr.gsActual;

            this.Text = gStr.gsSteerChart;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (tabControl1.TabIndex == 0) DrawChart();
            //if (tabControl1.TabIndex == 1) DrawChartTool();
            DrawChart();
            DrawChartTool();
        }

        private void DrawChart()
        {
            {
                //word 0 - steerangle, 1 - pwmDisplay
                dataSteerAngle = mf.mc.actualSteerAngleChart.ToString();
                dataPWM = mf.guidanceLineSteerAngle.ToString();

                lblSteerAng.Text = mf.ActualSteerAngle;
                lblPWM.Text = mf.SetSteerAngle;
            }

            //chart data
            Series s = unoChart.Series["S"];
            Series w = unoChart.Series["PWM"];
            double nextX = 1;
            double nextX5 = 1;

            if (s.Points.Count > 0) nextX = s.Points[s.Points.Count - 1].XValue + 1;
            if (w.Points.Count > 0) nextX5 = w.Points[w.Points.Count - 1].XValue + 1;

            unoChart.Series["S"].Points.AddXY(nextX, dataSteerAngle);
            unoChart.Series["PWM"].Points.AddXY(nextX5, dataPWM);

            //if (isScroll)
            {
                while (s.Points.Count > 30)
                {
                    s.Points.RemoveAt(0);
                }
                while (w.Points.Count > 30)
                {
                    w.Points.RemoveAt(0);
                }
                unoChart.ResetAutoValues();
            }
        }

        private void DrawChartTool()
        {
            {
                dataXTE = mf.guidanceLineDistanceOffTool.ToString();
                dataXTEActual = mf.mc.toolActualDistance.ToString();
                dataError = mf.mc.toolError.ToString();

                label2.Text = mf.guidanceLineDistanceOffTool.ToString();
                label3.Text = mf.mc.toolActualDistance.ToString();
                label7.Text = mf.mc.toolError.ToString();
            }

            //chart data
            Series s = chartTool.Series["Set"];
            Series w = chartTool.Series["Actual"]; 
            Series e = chartTool.Series["Error"];

            double nextX = 1;
            double nextX5 = 1;
            double nextE = 1;

            if (s.Points.Count > 0) nextX = s.Points[s.Points.Count - 1].XValue + 1;
            if (w.Points.Count > 0) nextX5 = w.Points[w.Points.Count - 1].XValue + 1;
            if (e.Points.Count > 0) nextE = e.Points[e.Points.Count - 1].XValue + 1;

            chartTool.Series["Set"].Points.AddXY(nextX, dataXTE);
            chartTool.Series["Actual"].Points.AddXY(nextX5, dataXTEActual);
            chartTool.Series["Error"].Points.AddXY(nextE, dataError);

            //if (isScroll)
            {
                while (s.Points.Count > 30)
                {
                    s.Points.RemoveAt(0);
                }
                while (w.Points.Count > 30)
                {
                    w.Points.RemoveAt(0);
                }
                while (e.Points.Count > 30)
                {
                    e.Points.RemoveAt(0);
                }
                chartTool.ResetAutoValues();
            }
        }

        private void FormSteerGraph_Load(object sender, EventArgs e)
        {
            timer1.Interval = (int)((1 / (double)mf.fixUpdateHz) * 1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}