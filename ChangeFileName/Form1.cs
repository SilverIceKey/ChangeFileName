using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangeFileName
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog;
        private List<BaseFile> fileList;
        private List<BaseFile> fileListMedia;
        public Form1()
        {
            InitializeComponent();
        }

        private void Select_Click(object sender, EventArgs e)
        {
            if (openFileDialog == null)
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = "D:\\MEDIA\\VIDEO";
                openFileDialog.Filter = "ass files(*.ass)|*.ass|All files(*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileList = new List<BaseFile>();
                for (int length = 0; length < openFileDialog.FileNames.Length; length++)
                {
                    BaseFile file = new BaseFile();
                    file.fileUrl = openFileDialog.FileNames[length];
                    fileList.Add(file);
                }
                foreach (BaseFile tmp in fileList)
                {
                    string fileName = tmp.fileUrl.Substring(tmp.fileUrl.LastIndexOf("\\") + 1);
                    fileSelected.Items.Add(fileName);
                }
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if (fileListMedia.Count == 0)
            {
                for (int i = 0; i < fileList.Count; i++)
                {
                    BaseFile file = fileList[i];
                    string fileName = file.fileUrl.Substring(file.fileUrl.LastIndexOf("\\") + 1);
                    string fileDir = file.fileUrl.Substring(0, file.fileUrl.LastIndexOf("\\") + 1);
                    if (fileName.Contains(hasBeChanged.Text.ToString()))
                    {
                        string destFile = fileDir + fileName.Replace(hasBeChanged.Text.ToString(), changeTo.Text.ToString());
                        if (System.IO.File.Exists(file.fileUrl))
                        {
                            System.IO.File.Move(file.fileUrl, destFile);
                            fileList[i].fileUrl = destFile;
                        }
                    }
                }

                MessageBox.Show("修改完成");
            }
            else
            {

                Regex regex = new Regex(@"\[\d{2,3}\]");
                Regex numRegex = new Regex(@"\s\d{2,3}\s");
                int fileListMediaCount = fileListMedia.Count;
                int fileListMediaRealCount = 0;
                for (int i = 0; i < fileListMedia.Count; i++)
                {
                    string mediaDir = fileListMedia[i].fileUrl.Substring(0, fileListMedia[i].fileUrl.LastIndexOf("\\") + 1);
                    string mediaName = fileListMedia[i].fileUrl.Substring(fileListMedia[i].fileUrl.LastIndexOf("\\") + 1);
                    string mediaEqNumResult = numRegex.Match(mediaName).ToString();
                    if (!string.IsNullOrEmpty(mediaEqNumResult))
                    {
                        mediaName.Replace(mediaEqNumResult, "[" + mediaEqNumResult.Trim() + "]");
                    }
                    int mediaNum;
                    try
                    {
                        mediaNum = Convert.ToInt32(regex.Match(mediaName).ToString().Replace("[", "").Replace("]", ""));
                    }
                    catch (Exception exception)
                    {
                        continue;
                    }
                    for (int j = 0; j < fileList.Count; j++)
                    {
                        string assName =
                            fileList[j].fileUrl.Substring(fileList[i].fileUrl.LastIndexOf("\\") + 1);
                        string assEqNumResult = numRegex.Match(assName).ToString();
                        if (!string.IsNullOrEmpty(assEqNumResult))
                        {
                            mediaName.Replace(assEqNumResult, "[" + assEqNumResult.Trim() + "]");
                        }
                        int assNum;
                        try
                        {
                            assNum = Convert.ToInt32(regex.Match(assName).ToString().Replace("[", "").Replace("]", ""));
                        }
                        catch (Exception exception)
                        {
                            continue;
                        }
                        if (assNum == mediaNum)
                        {
                            string ext = mediaName.Substring(mediaName.LastIndexOf(".") + 1);
                            string assDestName = mediaDir + mediaName.Replace(ext, "ass");
                            if (System.IO.File.Exists(fileList[j].fileUrl))
                            {
                                System.IO.File.Move(fileList[j].fileUrl, assDestName);
                                fileList[i].fileUrl = assDestName;
                                fileListMediaRealCount++;
                            }
                        }
                    }
                }
                if (fileListMediaCount != fileListMediaRealCount)
                {
                    MessageBox.Show("修改完成：" + fileListMediaRealCount + "个,存在文件无 01 或者[01]关键字无法对应媒体和字幕无法修改");
                }
            }
        }

        private void selectMedia_Click(object sender, EventArgs e)
        {
            if (openFileDialog == null)
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = "D:\\MEDIA\\VIDEO";
                openFileDialog.Filter = "mp4 files(*.mp4)|*.mp4|All files(*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileListMedia = new List<BaseFile>();
                for (int length = 0; length < openFileDialog.FileNames.Length; length++)
                {
                    BaseFile file = new BaseFile();
                    file.fileUrl = openFileDialog.FileNames[length];
                    fileListMedia.Add(file);
                }
                foreach (BaseFile tmp in fileListMedia)
                {
                    string fileName = tmp.fileUrl.Substring(tmp.fileUrl.LastIndexOf("\\") + 1);
                    fileSelectedMedia.Items.Add(fileName);
                }
            }
        }
    }
}
