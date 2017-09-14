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
        private List<BaseFile> fileList;
        private List<BaseFile> fileListMedia;
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
                openFileDialog.Filter = "ass files(*.ass)|*.ass|All files(*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileSelected.Items.Clear();
                fileList = new List<BaseFile>();
                for (int length = 0; length < openFileDialog.FileNames.Length; length++)
                {
                    BaseFile file = new BaseFile();
                    file.fileUrl = openFileDialog.FileNames[length];
                    fileList.Add(file);
                }
                fileList.Sort(delegate (BaseFile fir,BaseFile sec)
                {
                    return fir.fileUrl.CompareTo(sec.fileUrl);
                });
                foreach (BaseFile tmp in fileList)
                {
                    string fileName = tmp.fileUrl.Substring(tmp.fileUrl.LastIndexOf("\\") + 1);
                    fileSelected.Items.Add(fileName);
                }
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            utils = new FileNameChangeUtils();
            if (fileListMedia.Count == 0)
            {
                utils.customeChange(fileList,hasBeChanged.Text.ToString(),changeTo.Text.ToString());
            }
//            else if (fileListMedia.Count==1)
//            {
//               
//            }
            else
            {
                utils.changeByMedia(fileListMedia,fileList);
            }
        }

        private void selectMedia_Click(object sender, EventArgs e)
        {
            if (openFileDialog == null)
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "mp4 files(*.mp4)|*.mp4|All files(*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileSelectedMedia.Items.Clear();
                fileListMedia = new List<BaseFile>();
                for (int length = 0; length < openFileDialog.FileNames.Length; length++)
                {
                    BaseFile file = new BaseFile();
                    file.fileUrl = openFileDialog.FileNames[length];
                    fileListMedia.Add(file);
                }
                fileListMedia.Sort(delegate (BaseFile fir, BaseFile sec)
                {
                    return fir.fileUrl.CompareTo(sec.fileUrl);
                });
                foreach (BaseFile tmp in fileListMedia)
                {
                    string fileName = tmp.fileUrl.Substring(tmp.fileUrl.LastIndexOf("\\") + 1);
                    fileSelectedMedia.Items.Add(fileName);
                }
            }
        }
    }
}
