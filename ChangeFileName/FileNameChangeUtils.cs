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
            Regex regex = new Regex(@"\[[0.0-9.0]{2,4}\]");
            Regex numRegex = new Regex(@"\s[0.0-9.0]{2,4}\s");
            int fileListMediaCount = fileListMedia.Count;
            int fileListMediaRealCount = 0;
            for (int i = 0; i < fileListMedia.Count; i++)
            {
                string mediaDir = fileListMedia[i].fileUrl.Substring(0, fileListMedia[i].fileUrl.LastIndexOf("\\") + 1);
                string mediaName = fileListMedia[i].fileUrl.Substring(fileListMedia[i].fileUrl.LastIndexOf("\\") + 1);
                string mediaEqNumResult;
                if (regex.Match(mediaName).Success)
                {
                    mediaEqNumResult = regex.Match(mediaName).ToString();
                }
                else
                {
                    mediaEqNumResult = numRegex.Match(mediaName).ToString();
                }
                if (!string.IsNullOrEmpty(mediaEqNumResult))
                {
                    mediaEqNumResult = mediaEqNumResult.Replace(mediaEqNumResult, "[" + mediaEqNumResult.Trim() + "]");
                }
                double mediaNum;
                try
                {
                    mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("[", "").Replace("]", ""));
                }
                catch (Exception exception)
                {
                    continue;
                }
                for (int j = 0; j < fileList.Count; j++)
                {
                    string assName =
                        fileList[j].fileUrl.Substring(fileList[i].fileUrl.LastIndexOf("\\") + 1);
                    string assEqNumResult;
                    if (regex.Match(assName).Success)
                    {
                        assEqNumResult = regex.Match(assName).ToString();
                    }
                    else
                    {
                        assEqNumResult = numRegex.Match(assName).ToString();
                    }
                    if (!string.IsNullOrEmpty(assEqNumResult))
                    {
                        assEqNumResult = assEqNumResult.Replace(assEqNumResult, "[" + assEqNumResult.Trim() + "]");
                    }
                    double assNum;
                    try
                    {
                        assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", ""));
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
