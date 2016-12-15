using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MT.Core
{
   public class DirectoryOper
    {
        #region 目录基本操作
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="parentPath">父级目录名</param>
        /// <param name="DirName">目录名</param>
        /// <returns></returns>
        public bool CreateDir(string parentPath, string DirName)
        {
            try
            {
                Directory.CreateDirectory(BaseOp.WebPathTran(parentPath) + DirName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 删除目录（包括子文件及文件夹）
        /// </summary>
        /// <param name="parentPath">父级目录</param>
        /// <param name="DirName">文件夹名</param>
        public bool DeleteDir(string parentPath, string DirName)
        {
            try
            {
                Directory.Delete(BaseOp.WebPathTran(parentPath) + DirName, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  删除目录（包括子文件及子文件夹）
        /// </summary>
        /// <param name="Dirfullname">删除目录的路径（完整名称）</param>
        /// <returns></returns>
        public bool DeleteDir(string Dirfullname)
        {
            try
            {
                Directory.Delete(Dirfullname, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion
    }
}
