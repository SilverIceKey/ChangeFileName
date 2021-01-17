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
        public void customeChange(List<string> fileList, string hasBeChanged, string changeTo)
        {
            for (int i = 0; i < fileList.Count; i++)
            {
                string file = fileList[i];
                string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                string fileDir = file.Substring(0, file.LastIndexOf("\\") + 1);
                if (fileName.Contains(hasBeChanged))
                {
                    string destFile = fileDir + fileName.Replace(hasBeChanged, changeTo);
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Move(file, destFile);
                        fileList[i] = destFile;
                    }
                }
            }
            MessageBox.Show("修改完成");
        }

        public void sortChangeed(List<string> assList, List<string> mediaList)
        {
            assList.Sort();
            mediaList.Sort();

            for (int i = 0; i < mediaList.Count; i++)
            {
                string file = mediaList[i];
                string assFile = assList[i];
                string fileExt = file.Substring(file.LastIndexOf(".") + 1);
                string assExt = assFile.Substring(assFile.LastIndexOf(".") + 1);
                string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                string fileDir = file.Substring(0, file.LastIndexOf("\\") + 1);
                string destFile = fileDir + fileName.Replace(fileExt, assExt);
                if (System.IO.File.Exists(assFile)&&!System.IO.File.Exists(destFile))
                {
                    System.IO.File.Copy(assFile, destFile);
                }
            }
            MessageBox.Show("修改完成");
        }

        public void changeByMedia(List<string> fileListMedia, List<string> fileList)
        {
            RegexTypes types = RegexTypes.KH;
            Regex regex = new Regex(@"\[[0.0-9.0]{2,4}\]");
            Regex huabig5regex = new Regex(@"第[0.0-9.0]{2,4}話");
            Regex huagbregex = new Regex(@"第[0.0-9.0]{2,4}话");
            Regex numRegex = new Regex(@"[^0 - 9]");
            Regex juregex = new Regex(@"第[0.0-9.0]{2,4}局");
            Regex pointregex = new Regex(@"[0.0-9.0]{2,4}.\s");
            Regex EPregex = new Regex(@"EP[0.0-9.0]{2,2}\s");
            Regex OVARegex = new Regex(@"OVA");
            Regex OVAKHRegex = new Regex(@"OVA");
            int fileListMediaCount = fileListMedia.Count;
            int fileListMediaRealCount = 0;
            for (int i = 0; i < fileListMedia.Count; i++)
            {
                string mediaDir = fileListMedia[i].Substring(0, fileListMedia[i].LastIndexOf("\\") + 1);
                string mediaName = fileListMedia[i].Substring(fileListMedia[i].LastIndexOf("\\") + 1);
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
                else if (juregex.Match(mediaName).Success)
                {
                    types = RegexTypes.JU;
                    mediaEqNumResult = juregex.Match(mediaName).ToString();
                }
                else if (EPregex.Match(mediaName).Success)
                {
                    types = RegexTypes.EP;
                    mediaEqNumResult = EPregex.Match(mediaName).ToString();
                }
                else if (pointregex.Match(mediaName).Success)
                {
                    types = RegexTypes.POINT;
                    mediaEqNumResult = pointregex.Match(mediaName).ToString();
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
                        case RegexTypes.JU:
                            mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("局", ""));
                            break;
                        case RegexTypes.POINT:
                            mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("[", "").Replace("]", "").Replace(".", ""));
                            break;
                        case RegexTypes.EP:
                            mediaNum = Convert.ToDouble(mediaEqNumResult.Replace("EP", ""));
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
                        fileList[j].Substring(fileList[j].LastIndexOf("\\") + 1);
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
                    else if (juregex.Match(assName).Success)
                    {
                        types = RegexTypes.JU;
                        assEqNumResult = juregex.Match(assName).ToString();
                    }
                    else if (EPregex.Match(assName).Success)
                    {
                        types = RegexTypes.EP;
                        assEqNumResult = EPregex.Match(assName).ToString();
                    }
                    else if (pointregex.Match(assName).Success)
                    {
                        types = RegexTypes.POINT;
                        assEqNumResult = pointregex.Match(assName).ToString();
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
                            case RegexTypes.JU:
                                assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("局", ""));
                                break;
                            case RegexTypes.POINT:
                                assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", "").Replace(".", ""));
                                break;
                            case RegexTypes.EP:
                                assNum = Convert.ToDouble(assEqNumResult.Replace("EP", ""));
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
                        string assDestName = mediaDir + fileListMedia[i].Substring(fileListMedia[i].LastIndexOf("\\") + 1).Replace(ext, "ass");
                        if (System.IO.File.Exists(fileList[j]))
                        {
                            System.IO.File.Copy(fileList[j], assDestName);
                            fileList[i] = assDestName;
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

        public List<string> renameSort(List<string> fileList)
        {
            RegexTypes types = RegexTypes.KH;
            Regex regex = new Regex(@"\[[0.0-9.0]{2,4}\]");
            Regex huabig5regex = new Regex(@"第[0.0-9.0]{2,4}話");
            Regex huagbregex = new Regex(@"第[0.0-9.0]{2,4}话");
            Regex juregex = new Regex(@"第[0.0-9.0]{2,4}局");
            Regex pointregex = new Regex(@"[0.0-9.0]{2,4}.");
            Regex numRegex = new Regex(@"\s[0.0-9.0]{2,4}\s");
            Regex EPregex = new Regex(@"EP[0.0-9.0]{2,2}.");
            Regex OVARegex = new Regex(@"OVA");
            Regex OVAKHRegex = new Regex(@"OVA");
            for (int j = 0; j < fileList.Count; j++)
            {
                string assName =
                    fileList[j].Substring(fileList[j].LastIndexOf("\\") + 1);
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
                else if (juregex.Match(assName).Success)
                {
                    types = RegexTypes.JU;
                    assEqNumResult = juregex.Match(assName).ToString();
                }
                else if (EPregex.Match(assName).Success)
                {
                    types = RegexTypes.EP;
                    assEqNumResult = EPregex.Match(assName).ToString();
                }
                else if (pointregex.Match(assName).Success)
                {
                    types = RegexTypes.POINT;
                    assEqNumResult = pointregex.Match(assName).ToString();
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
                        case RegexTypes.JU:
                            assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", "").Replace("第", "").Replace("局", ""));
                            break;
                        case RegexTypes.POINT:
                            assNum = Convert.ToDouble(assEqNumResult.Replace("[", "").Replace("]", "").Replace(".", ""));
                            break;
                        case RegexTypes.EP:
                            assNum = Convert.ToDouble(assEqNumResult.Replace("EP", ""));
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
                    string assDestName = fileList[j].Replace(assEqNumResult, resultNum);
                    if (System.IO.File.Exists(fileList[j]))
                    {
                        System.IO.File.Copy(fileList[j], assDestName);
                        System.IO.File.Delete(fileList[j]);
                        fileList[j] = assDestName;
                    }
                }
                catch (Exception exception)
                {
                    continue;
                }
            }
            MessageBox.Show("修改完成：" + fileList.Count + "个");
            return fileList;
        }
    }
}
