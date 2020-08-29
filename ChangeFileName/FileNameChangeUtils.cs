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
        public void customeChange(List<BaseFile> fileList, string hasBeChanged, string changeTo)
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

        public void changeByMedia(List<BaseFile> fileListMedia, List<BaseFile> fileList)
        {
            RegexTypes types = RegexTypes.KH;
            Regex regex = new Regex(@"\[[0.0-9.0]{2,4}\]");
            Regex huabig5regex = new Regex(@"第[0.0-9.0]{2,4}話");
            Regex huagbregex = new Regex(@"第[0.0-9.0]{2,4}话");
            Regex numRegex = new Regex(@"\s[0.0-9.0]{2,4}\s");
            Regex OVARegex = new Regex(@"OVA");
            Regex OVAKHRegex = new Regex(@"OVA");
            int fileListMediaCount = fileListMedia.Count;
            int fileListMediaRealCount = 0;
            for (int i = 0; i < fileListMedia.Count; i++)
            {
                string mediaDir = fileListMedia[i].fileUrl.Substring(0, fileListMedia[i].fileUrl.LastIndexOf("\\") + 1);
                string mediaName = fileListMedia[i].fileUrl.Substring(fileListMedia[i].fileUrl.LastIndexOf("\\") + 1);
                string mediaEqNumResult;
                if (regex.Match(mediaName).Success)
                {
                    types = RegexTypes.KH;
                    mediaEqNumResult = regex.Match(mediaName).ToString();
                }
                else if (huabig5regex.Match(mediaName).Success)
                {
                    types = RegexTypes.BIG5;
                    mediaEqNumResult = huabig5regex.Match(mediaName).ToString();
                }
                else if (huagbregex.Match(mediaName).Success)
                {
                    types = RegexTypes.GB;
                    mediaEqNumResult = huagbregex.Match(mediaName).ToString();
                }
                else if (OVARegex.Match(mediaName).Success)
                {
                    types = RegexTypes.OVA;
                    mediaEqNumResult = OVARegex.Match(mediaName).ToString();
                }
                else if (OVAKHRegex.Match(mediaName).Success)
                {
                    types = RegexTypes.OVA;
                    mediaEqNumResult = OVAKHRegex.Match(mediaName).ToString();
                }
                else
                {
                    types = RegexTypes.NUM;
                    mediaEqNumResult = numRegex.Match(mediaName).ToString();
                }
                if (!string.IsNullOrEmpty(mediaEqNumResult))
                {
                    mediaEqNumResult = mediaEqNumResult.Replace(mediaEqNumResult, "[" + mediaEqNumResult.Trim() + "]");
                }
                double mediaNum;
                try
                {
                    mediaEqNumResult = mediaEqNumResult.Replace("[", "").Replace("]", "");
                    switch (types)
                    {
                        case RegexTypes.KH:
                            mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("[", "").Replace("]", ""));
                            break;
                        case RegexTypes.BIG5:
                            mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("話", ""));
                            break;
                        case RegexTypes.GB:
                            mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("话", ""));
                            break;
                        case RegexTypes.NUM:
                            mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("[", "").Replace("]", ""));
                            break;
                        case RegexTypes.OVA:
                            mediaNum = -1;
                            break;
                        default:
                            mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("[", "").Replace("]", ""));
                            break;
                    }

                }
                catch (Exception exception)
                {
                    continue;
                }
                for (int j = 0; j < fileList.Count; j++)
                {
                    string assName =
                        fileList[j].fileUrl.Substring(fileList[j].fileUrl.LastIndexOf("\\") + 1);
                    string assEqNumResult;
                    if (regex.Match(assName).Success)
                    {
                        types = RegexTypes.KH;
                        assEqNumResult = regex.Match(assName).ToString();
                    }
                    else if (huabig5regex.Match(mediaName).Success)
                    {
                        types = RegexTypes.BIG5;
                        assEqNumResult = huabig5regex.Match(assName).ToString();
                    }
                    else if (huagbregex.Match(assName).Success)
                    {
                        types = RegexTypes.GB;
                        assEqNumResult = huagbregex.Match(assName).ToString();
                    }
                    else if (OVARegex.Match(assName).Success)
                    {
                        types = RegexTypes.OVA;
                        assEqNumResult = OVARegex.Match(assName).ToString();
                    }
                    else if (OVAKHRegex.Match(assName).Success)
                    {
                        types = RegexTypes.OVA;
                        assEqNumResult = OVAKHRegex.Match(assName).ToString();
                    }
                    else
                    {
                        types = RegexTypes.NUM;
                        assEqNumResult = numRegex.Match(assName).ToString();
                    }
                    if (!string.IsNullOrEmpty(assEqNumResult))
                    {
                        assEqNumResult = assEqNumResult.Replace(assEqNumResult, "[" + assEqNumResult.Trim() + "]");
                    }
                    double assNum;
                    try
                    {
                        assEqNumResult = assEqNumResult.Replace("[", "").Replace("]", "");
                        switch (types)
                        {
                            case RegexTypes.KH:
                                assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", ""));
                                break;
                            case RegexTypes.BIG5:
                                assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("話", ""));
                                break;
                            case RegexTypes.GB:
                                assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("话", ""));
                                break;
                            case RegexTypes.NUM:
                                assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", ""));
                                break;
                            case RegexTypes.OVA:
                                assNum = -1;
                                break;
                            default:
                                assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", ""));
                                break;
                        }
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
                            System.IO.File.Copy(fileList[j].fileUrl, assDestName);
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

        public void renameSort(List<BaseFile> fileList)
        {
            RegexTypes types = RegexTypes.KH;
            Regex regex = new Regex(@"\[[0.0-9.0]{2,4}\]");
            Regex huabig5regex = new Regex(@"第[0.0-9.0]{2,4}話");
            Regex huagbregex = new Regex(@"第[0.0-9.0]{2,4}话");
            Regex numRegex = new Regex(@"\s[0.0-9.0]{2,4}\s");
            Regex OVARegex = new Regex(@"OVA");
            Regex OVAKHRegex = new Regex(@"OVA");
            for (int j = 0; j < fileList.Count; j++)
            {
                string assName =
                    fileList[j].fileUrl.Substring(fileList[j].fileUrl.LastIndexOf("\\") + 1);
                string assEqNumResult;
                if (regex.Match(assName).Success)
                {
                    types = RegexTypes.KH;
                    assEqNumResult = regex.Match(assName).ToString();
                }
                else if (huabig5regex.Match(assName).Success)
                {
                    types = RegexTypes.BIG5;
                    assEqNumResult = huabig5regex.Match(assName).ToString();
                }
                else if (huagbregex.Match(assName).Success)
                {
                    types = RegexTypes.GB;
                    assEqNumResult = huagbregex.Match(assName).ToString();
                }
                else if (OVARegex.Match(assName).Success)
                {
                    types = RegexTypes.OVA;
                    assEqNumResult = OVARegex.Match(assName).ToString();
                }
                else if (OVAKHRegex.Match(assName).Success)
                {
                    types = RegexTypes.OVA;
                    assEqNumResult = OVAKHRegex.Match(assName).ToString();
                }
                else
                {
                    types = RegexTypes.NUM;
                    assEqNumResult = numRegex.Match(assName).ToString();
                }
                if (!string.IsNullOrEmpty(assEqNumResult))
                {
                    assEqNumResult = assEqNumResult.Replace(assEqNumResult, "[" + assEqNumResult.Trim() + "]");
                }
                double assNum;
                try
                {
                    assEqNumResult = assEqNumResult.Replace("[", "").Replace("]", "");
                    string resultNum = "";
                    switch (types)
                    {
                        case RegexTypes.KH:
                            assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", ""));
                            break;
                        case RegexTypes.BIG5:
                            assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("話", ""));
                            break;
                        case RegexTypes.GB:
                            assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("话", ""));
                            break;
                        case RegexTypes.NUM:
                            assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", ""));
                            break;
                        case RegexTypes.OVA:
                            assNum = -1;
                            break;
                        default:
                            assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", ""));
                            break;
                    }

                    if (j < 9)
                    {
                        resultNum = assEqNumResult.Replace(assNum.ToString(), "0" + (j + 1));
                    }
                    else
                    {
                        resultNum = assEqNumResult.Replace(assNum.ToString(), (j + 1).ToString());
                    }
                    string assDestName = fileList[j].fileUrl.Replace(assEqNumResult, resultNum);
                    if (System.IO.File.Exists(fileList[j].fileUrl))
                    {
                        System.IO.File.Copy(fileList[j].fileUrl, assDestName);
                        System.IO.File.Delete(fileList[j].fileUrl);
                        fileList[j].fileUrl = assDestName;
                    }
                }
                catch (Exception exception)
                {
                    continue;
                }
            }
            MessageBox.Show("修改完成：" + fileList.Count + "个");
        }
    }
}
