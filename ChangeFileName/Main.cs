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
    public partial class Main : Form
    {
        private OpenFileDialog openFileDialog;
        private List<string> fileList;
        private List<string> fileListMedia;
        private FileNameChangeUtils utils;
        public Main()
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
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
            }
            openFileDialog.Filter = "ass files(*.ass)|*.ass|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileSelected.Items.Clear();
                fileList = new List<string>();
                for (int length = 0; length < openFileDialog.FileNames.Length; length++)
                {
                    string file = "";
                    file = openFileDialog.FileNames[length];
                    fileList.Add(file);
                }
                fileList.Sort(delegate (string fir, string sec)
                {
                    return fir.CompareTo(sec);
                });
                foreach (string tmp in fileList)
                {
                    string fileName = tmp.Substring(tmp.LastIndexOf("\\") + 1);
                    fileSelected.Items.Add(fileName);
                }
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            utils = new FileNameChangeUtils();
            if (fileListMedia.Count == 0)
            {
                if (string.IsNullOrEmpty(hasBeChanged.Text.ToString()) || string.IsNullOrEmpty(changeTo.Text.ToString()))
                {
                    MessageBox.Show("操作无效");
                    return;
                }
                utils.customeChange(fileList, hasBeChanged.Text.ToString(), changeTo.Text.ToString());
            }
            //            else if (fileListMedia.Count==1)
            //            {
            //               
            //            }
            else
            {
                utils.changeByMedia(fileListMedia, fileList);
            }
        }

        private void selectMedia_Click(object sender, EventArgs e)
        {
            if (openFileDialog == null)
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
            }
            openFileDialog.Filter = "mp4 files(*.mp4)|*.mp4|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileSelectedMedia.Items.Clear();
                fileListMedia = new List<string>();
                for (int length = 0; length < openFileDialog.FileNames.Length; length++)
                {
                    string file = "";
                    file = openFileDialog.FileNames[length];
                    fileListMedia.Add(file);
                }
                fileListMedia.Sort(delegate (string fir, string sec)
                {
                    return fir.CompareTo(sec);
                });
                foreach (string tmp in fileListMedia)
                {
                    string fileName = tmp.Substring(tmp.LastIndexOf("\\") + 1);
                    fileSelectedMedia.Items.Add(fileName);
                }
            }
        }

        private void renamesort_Click(object sender, EventArgs e)
        {
            utils = new FileNameChangeUtils();
            if (fileList.Count > 0)
            {
                fileSelected.Items.Clear();
                fileList = utils.renameSort(fileList);
                foreach (string tmp in fileList)
                {
                    string fileName = tmp.Substring(tmp.LastIndexOf("\\") + 1);
                    fileSelected.Items.Add(fileName);
                }
            }
            else
            {
                MessageBox.Show("请先选择字幕文件");
            }
        }
        private void FileSelected_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
                // Copy file from external application 
                if (fileList==null)
                {
                    fileList = new List<string>();
                }
                foreach (string srcfile in files)
                {
                    string file = "";
                    file = srcfile;
                    fileList.Add(file);
                }
                fileList.Sort(delegate (string fir, string sec)
                {
                    return fir.CompareTo(sec);
                });
                fileSelected.Items.Clear();
                foreach (string tmp in fileList)
                {
                    string fileName = tmp.Substring(tmp.LastIndexOf("\\") + 1);
                    fileSelected.Items.Add(fileName);
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FileSelected_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void FileSelectedMedia_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
                // Copy file from external application 
                if (fileListMedia == null)
                {
                    fileListMedia = new List<string>();
                }
                foreach (string srcfile in files)
                {
                    string file = "";
                    file = srcfile;
                    fileListMedia.Add(file);
                }
                fileListMedia.Sort(delegate (string fir, string sec)
                {
                    return fir.CompareTo(sec);
                });
                fileSelectedMedia.Items.Clear();
                foreach (string tmp in fileList)
                {
                    string fileName = tmp.Substring(tmp.LastIndexOf("\\") + 1);
                    fileSelectedMedia.Items.Add(fileName);
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FileSelectedMedia_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void editsort_Click(object sender, EventArgs e)
        {
            utils = new FileNameChangeUtils();
            if (fileListMedia.Count == 0||fileList.Count==0)
            {
                MessageBox.Show("请选择字幕或视频");  
            }
            else
            {
                utils.sortChangeed(fileList,fileListMedia);
            }
        }
    }
}
