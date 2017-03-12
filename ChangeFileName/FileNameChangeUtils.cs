using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangeFileName
{
    class FileNameChangeUtils
    {
        public void customeChange(List<BaseFile> fileList,string hasBeChanged,string changeTo)
        {
            for (int i = 0; i < fileList.Count; i++)
            {
                BaseFile file = fileList[i];
                string fileName = file.fileUrl.Substring(file.fileUrl.LastIndexOf("\\") + 1);
                string fileDir = file.fileUrl.Substring(0, file.fileUrl.LastIndexOf("\\") + 1);
                if (fileName.Contains(hasBeChanged))
                {
                    string destFile = fileDir + fileName.Replace(hasBeChanged, changeTo);
                    if (System.IO.File.Exists(file.fileUrl))
                    {
                        System.IO.File.Move(file.fileUrl, destFile);
                        fileList[i].fileUrl = destFile;
                    }
                }
            }
            MessageBox.Show("修改完成");
        }

        public void changeByMedia(List<BaseFile> fileListMedia,List<BaseFile> fileList  )
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
                    mediaName = mediaName.Replace(mediaEqNumResult, "[" + mediaEqNumResult.Trim() + "]");
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
                        assName = assName.Replace(assEqNumResult, "[" + assEqNumResult.Trim() + "]");
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
                        string assDestName = mediaDir + fileListMedia[i].fileUrl.Substring(fileListMedia[i].fileUrl.LastIndexOf("\\") + 1).Replace(ext, "ass");
                        if (System.IO.File.Exists(fileList[j].fileUrl))
                        {
                            System.IO.File.Move(fileList[j].fileUrl, assDestName);
                            fileList[i].fileUrl = assDestName;
                            fileListMediaRealCount++;
                            break;
                        }
                    }
                }
            }
            if (fileListMediaCount != fileListMediaRealCount)
            {
                MessageBox.Show("修改完成：" + fileListMediaRealCount + "个,存在文件无 01 或者[01]关键字无法对应媒体和字幕无法修改");
            }
            else
            {
                MessageBox.Show("修改完成：" + fileListMediaRealCount + "个");
            }
        }
    }
}
