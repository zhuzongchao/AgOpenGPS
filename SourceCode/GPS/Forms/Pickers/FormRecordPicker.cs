using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgOpenGPS.Forms.Pickers
{
    public partial class FormRecordPicker : Form
    {
        private readonly FormGPS mf = null;
        private bool isKML = false;
        private double easting, norting, latK, lonK;

        private readonly List<string> fileList = new List<string>();

        public FormRecordPicker(Form callingForm)
        {
            //get copy of the calling main form
            mf = callingForm as FormGPS;

            InitializeComponent();
        }

        private void FormRecordPicker_Load(object sender, EventArgs e)
        {
            if (ChkTool.Checked) ChkTool.Text = "ON";
            else ChkTool.Text = "OFF";
            LoadList();
        }

        private void LoadList()
        {
            ListViewItem itm;

            string fieldDir = mf.fieldsDirectory + mf.currentFieldDirectory;

            string[] files = Directory.GetFiles(fieldDir);

            fileList?.Clear();
            lvLines.Items.Clear();
            string ext = ".rec";
            if (isKML) ext = ".kml";

            // Here we use the filename of all .rec files in the current field dir.
            // The path and postfix is stripped off.

            foreach (string file in files)
            {
                if (file.EndsWith(ext))
                {
                    
                    string recordName = file.Replace(ext, "").Replace(fieldDir, "").Replace("\\", "");
                    string[] FieldNames = { recordName, ext };
                    itm = new ListViewItem(FieldNames);
                    lvLines.Items.Add(itm);

                }
            }

            if (lvLines.Items.Count == 0)
            {
                MessageBox.Show("No Recorded Paths! \nMay be able to load KML.", "Create A Path or Load KML",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Close();
            }
        }



        private void btnOpenExistingLv_Click(object sender, EventArgs e)
        {
            int count = lvLines.SelectedItems.Count;
            string ext = ".rec";
            if (isKML) ext = ".kml";
            double speed = (double)nudSpeed.Value;
            bool isTool = ChkTool.Checked;
            if (count > 0)
            {
                string selectedRecord = lvLines.SelectedItems[0].SubItems[0].Text;
                string selectedRecordPath = mf.fieldsDirectory + mf.currentFieldDirectory + "\\" + selectedRecord + ext;

                // Copy the selected record file to the original record name inside the field dir:
                // ( this will load the last selected path automatically when this field is opened again)
                if (!isKML)
                {
                    File.Copy(selectedRecordPath, mf.fieldsDirectory + mf.currentFieldDirectory + "\\RecPath.txt", true);
                }

                // and load the selected path into the recPath object:
                string line;
                if (File.Exists(selectedRecordPath))
                {
                    using (StreamReader reader = new StreamReader(selectedRecordPath))
                    {
                        if (isKML)
                        {
                            string coordinates = null;
                            int startIndex;
                            mf.recPath.recList.Clear();
                            try
                            {
                                while (!reader.EndOfStream)
                                {
                                    //start to read the file
                                    line = reader.ReadLine();

                                    startIndex = line.IndexOf("<coordinates>");

                                    if (startIndex != -1)
                                    {
                                        while (true)
                                        {
                                            int endIndex = line.IndexOf("</coordinates>");

                                            if (endIndex == -1)
                                            {
                                                //just add the line
                                                if (startIndex == -1) coordinates += line.Substring(0);
                                                else coordinates += line.Substring(startIndex + 13);
                                            }
                                            else
                                            {
                                                if (startIndex == -1) coordinates += line.Substring(0, endIndex);
                                                else coordinates += line.Substring(startIndex + 13, endIndex - (startIndex + 13));
                                                break;
                                            }
                                            line = reader.ReadLine();
                                            line = line.Trim();
                                            startIndex = -1;
                                        }

                                        line = coordinates;
                                        char[] delimiterChars = { ' ', '\t', '\r', '\n' };
                                        string[] numberSets = line.Split(delimiterChars);

                                        //at least 3 points
                                        if (numberSets.Length > 2)
                                        {
                                            

                                            foreach (string item in numberSets)
                                            {
                                                string[] fix = item.Split(',');
                                                double.TryParse(fix[0], NumberStyles.Float, CultureInfo.InvariantCulture, out lonK);
                                                double.TryParse(fix[1], NumberStyles.Float, CultureInfo.InvariantCulture, out latK);

                                                mf.pn.ConvertWGS84ToLocal(latK, lonK, out norting, out easting);
                                                CRecPathPt point = new CRecPathPt(
                                                easting,
                                                norting,
                                                0,
                                                speed,
                                                isTool);

                                                //add the point
                                                mf.recPath.recList.Add(point);

                                            }
                                            FixImportLine();
                                            CalculatePathLineHeadings();

                                            coordinates = "";
                                            mf.FileSaveRecPath();
                                            File.Copy(mf.fieldsDirectory + mf.currentFieldDirectory + "\\RecPath.txt", mf.fieldsDirectory + mf.currentFieldDirectory + "\\" + selectedRecord + ".rec",true);

                                        }
                                        else
                                        {
                                            mf.TimedMessageBox(2000, gStr.gsErrorreadingKML, gStr.gsChooseBuildDifferentone);
                                        }
                                        
                                            break;
                                       
                                    }
                                }
                                
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Bad KML File", "Error Loading KML File",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            try
                            {
                                //read header
                                line = reader.ReadLine();
                                line = reader.ReadLine();
                                int numPoints = int.Parse(line);
                                mf.recPath.recList.Clear();

                                while (!reader.EndOfStream)
                                {
                                    for (int v = 0; v < numPoints; v++)
                                    {
                                        line = reader.ReadLine();
                                        string[] words = line.Split(',');
                                        CRecPathPt point = new CRecPathPt(
                                            double.Parse(words[0], CultureInfo.InvariantCulture),
                                            double.Parse(words[1], CultureInfo.InvariantCulture),
                                            double.Parse(words[2], CultureInfo.InvariantCulture),
                                            double.Parse(words[3], CultureInfo.InvariantCulture),
                                            bool.Parse(words[4]));

                                        //add the point
                                        mf.recPath.recList.Add(point);
                                    }
                                }
                            }

                            catch (Exception ex)
                            {
                                var form = new FormTimedMessage(2000, gStr.gsRecordedPathFileIsCorrupt, gStr.gsButFieldIsLoaded);
                                form.Show(this);
                                mf.WriteErrorLog("Load Recorded Path" + ex.ToString());
                            }
                        }
                    }
                }
            }

        }

        public void FixImportLine()
        {
            double spacing = 2;
            double speed = (double)nudSpeed.Value;
            bool isTool = ChkTool.Checked;
            //boundary point spacing based on eq width
            //close if less then 30 ha, 60ha, more then 60

            int cnt = mf.recPath.recList.Count;
            double distance;

            //make sure distance isn't too big between points on boundary
            for (int i = 0; i < cnt-1; i++)
            {
                int j = i + 1;

                //if (j == cnt) j = 0;
                distance = glm.Distance(mf.recPath.recList[i].easting, mf.recPath.recList[i].northing, mf.recPath.recList[j].easting, mf.recPath.recList[j].northing);
                if (distance > spacing * 1.5)
                {
                    vec3 pointB = new vec3((mf.recPath.recList[i].easting + mf.recPath.recList[j].easting) / 2.0,
                        (mf.recPath.recList[i].northing + mf.recPath.recList[j].northing) / 2.0, mf.recPath.recList[i].heading);
                    CRecPathPt point = new CRecPathPt(pointB.easting,
                                                pointB.northing,
                                                0,
                                                speed,
                                                isTool);
                    mf.recPath.recList.Insert(j, point);
                    cnt = mf.recPath.recList.Count;
                    i--;
                }
            }

            //make sure distance isn't too big between points on boundary
            cnt = mf.recPath.recList.Count;

            for (int i = 0; i < cnt-1; i++)
            {
                int j = i + 1;

                //if (j == cnt) j = 0;
                distance = glm.Distance(mf.recPath.recList[i].easting, mf.recPath.recList[i].northing, mf.recPath.recList[j].easting, mf.recPath.recList[j].northing);
                if (distance > spacing * 1.5)
                {
                    vec3 pointB = new vec3((mf.recPath.recList[i].easting + mf.recPath.recList[j].easting) / 2.0,
                        (mf.recPath.recList[i].northing + mf.recPath.recList[j].northing) / 2.0, mf.recPath.recList[i].heading);
                    CRecPathPt point = new CRecPathPt(pointB.easting,
                                                pointB.northing,
                                                0,
                                                speed,
                                                isTool);
                    mf.recPath.recList.Insert(j, point);
                    cnt = mf.recPath.recList.Count;
                    i--;
                }
            }
        }



            public void CalculatePathLineHeadings()
        {
            //to calc heading based on next and previous points to give an average heading.
            int cnt = mf.recPath.recList.Count;


            //first point needs last, first, second points
            double heading = Math.Atan2(mf.recPath.recList[1].easting - mf.recPath.recList[0].easting, mf.recPath.recList[1].northing - mf.recPath.recList[0].northing);
            if (heading < 0) heading += glm.twoPI;
            mf.recPath.recList[0].heading = heading;

            //middle points
            for (int i = 1; i < cnt-1; i++)
            {
                
                heading = Math.Atan2(mf.recPath.recList[i+1].easting - mf.recPath.recList[i].easting, mf.recPath.recList[i+1].northing - mf.recPath.recList[i].northing);
                if (heading < 0) heading += glm.twoPI;
                mf.recPath.recList[i].heading = heading;
            }

            //last and first point
            heading = Math.Atan2(mf.recPath.recList[cnt-1].easting - mf.recPath.recList[cnt-2].easting, mf.recPath.recList[cnt-1].northing - mf.recPath.recList[cnt-2].northing);
            if (heading < 0) heading += glm.twoPI;
            mf.recPath.recList[cnt-1].heading = heading;
        }

        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            int count = lvLines.SelectedItems.Count;
            string dir2Delete;
            if (count > 0)
            {
                string selectedRecord = lvLines.SelectedItems[0].SubItems[0].Text;
                dir2Delete = mf.fieldsDirectory + mf.currentFieldDirectory + "\\" + selectedRecord + ".rec";
               
                DialogResult result3 = MessageBox.Show(
                    dir2Delete,
                    gStr.gsDeleteForSure,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (result3 == DialogResult.Yes)
                {
                    System.IO.File.Delete(dir2Delete);
                }
                else return;
            }
            else return;

            LoadList();
        }

        private void ChkTool_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTool.Checked) ChkTool.Text = "ON";
            else ChkTool.Text = "OFF";
        }

        private void btnTurnOffRecPath_Click(object sender, EventArgs e)
        {
            mf.recPath.StopDrivingRecordedPath();
            mf.recPath.recList.Clear();
            mf.FileSaveRecPath();
            mf.panelDrag.Visible = false;
            Close();
        }

        private void bntKML_Click(object sender, EventArgs e)
        {
            isKML = true;
            LoadList();

        }

        private void butRec_Click(object sender, EventArgs e)
        {
            isKML = false;
            LoadList();
        }
    }
}
