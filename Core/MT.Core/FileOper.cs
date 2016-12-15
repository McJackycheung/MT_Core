/******************************************************
 * 项目名称: MT.Core
 * 项目描述:
 * 类 名 称: FileOper
 * 版 本 号:1.0.0.1
 * 说    明:
 * 作    者：戴威
 * 创建时间：2016/12/15 10:45:50
 *******************************************************
 * 更新时间: 2016/12/15 10:45:50
******************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;

namespace MT.Core
{
   public class FileOper
    {
        #region  检测文件是否存在
        /// <summary>
        /// 文件存在性
        /// </summary>
        /// <returns></returns>
        public bool FileExist(string filefullname)
        {
            string filepath = filefullname.Substring(0, filefullname.LastIndexOf("/") + 1);
            string filename = filefullname.Remove(0, filepath.Length);
            string Fileparentpath = BaseOp.WebPathTran(filepath);
            return File.Exists(Fileparentpath + filename);
        }

        
        public static string GetFileName(string webfilename)
        {
            try
            {
                int lastslash = webfilename.LastIndexOf('/');
                string filename = webfilename.Substring(lastslash + 1);
                return filename;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region  上传文件
        /// <summary>
        /// 上传文件（默认文件名）
        /// </summary>
        /// <param name="fileUploadControl">上传控件ID</param>
        /// <param name="SavePath">指定目录</param>
        /// <param name="IsTimeName">启用时间重命名</param>
        /// <returns></returns>
        public bool UploadFile(FileUpload fileUploadControl, string SavePath, bool IsTimeName)
        {
            string filename = fileUploadControl.FileName;
            if (IsTimeName)
            {
                //时间+扩展名
                filename = DateTime.Now.Ticks.ToString() + filename.Substring(filename.LastIndexOf("."));
            }
            try
            {

                string FileInServerName = BaseOp.WebPathTran(SavePath) + filename;
                fileUploadControl.SaveAs(FileInServerName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            } 
        }
        #endregion

        #region 文件下载
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="parentpath">所在目录</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static void DownLoadFile(string parentpath, string filename, HttpContext httpcontext)
        {
            string Filepath = BaseOp.WebPathTran(parentpath);
            HttpResponse response = httpcontext.Response;
            HttpRequest request = httpcontext.Request;
            if (File.Exists(Filepath + filename))
            {

                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment;filename=" + httpcontext.Server.UrlEncode(filename));
                response.WriteFile(Filepath + filename);
            }
            else
            {
                response.Redirect(request.ServerVariables["HTTP_REFERER"]);
            }

        }
        /// <summary>
        ///  小文件下载
        /// </summary>
        /// <param name="filefullname">完整文件名</param>
        public static void DownLoadSmallFile(string filefullname, HttpContext httpcontext)
        {
            HttpResponse response = httpcontext.Response;
            HttpRequest request = httpcontext.Request;
            string filepath = filefullname.Substring(0, filefullname.LastIndexOf("/") + 1);
            string filename = filefullname.Remove(0, filepath.Length);
            string Fileparentpath = BaseOp.WebPathTran(filepath);
            if (File.Exists(Fileparentpath + filename))
            {

                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment;filename=" + httpcontext.Server.UrlEncode(filename));
                response.TransmitFile(Fileparentpath + filename);
            }
            else
            {
                response.Redirect(request.ServerVariables["HTTP_REFERER"]);
            }

        }
        #endregion

        #region 备份文件
        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && System.IO.File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }

        #endregion

        #region  恢复文件
        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    else
                    {

                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        #endregion

        #region  文件删除
        /// <summary>
        /// 文件删除
        /// </summary>
        /// <param name="parentPath">所在父级目录</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static bool DeleteFile(string parentPath, string filename)
        {
            try
            {
                string filefullname = BaseOp.WebPathTran(parentPath) + filename;
                File.Delete(filefullname);
                return true;
            }
            catch
            {

                return false;
            }



        }
        /// <summary>
        /// 文件删除
        /// </summary>
        /// <param name="FileFullName">物理路径文件名</param>
        /// <returns></returns>
        public static bool DeleteFile(string FileFullName)
        {
            FileInfo fi = new FileInfo(FileFullName);
            if (fi.Exists)
            {
                try
                {
                    fi.Delete();
                    return true;

                }
                catch
                {
                    return false;
                }
            }
            return true;
        } 
    }
}
#endregion