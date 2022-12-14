using System;
using System.Collections.Generic;
using System.IO;
using XLua;

namespace AESEncrypt
{
    [Hotfix]
    [LuaCallCSharp]
    public partial class FileGet
    {
        /// <summary>
        /// 私有变量
        /// </summary>
        private static List<FileInfo> lst = new List<FileInfo>();

        /// <summary>
        /// 获得目录下所有文件或指定文件类型文件(包含所有子文件夹)
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extName">扩展名可以多个 例如 .mp3.wma.rm</param>
        /// <returns>List<FileInfo></returns>
        public static List<FileInfo> getFile(string path, string extName)
        {
            getdir(path, extName);
            return lst;
        }

        /// <summary>
        /// 私有方法,递归获取指定类型文件,包含子文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extName"></param>
        private static void getdir(string path, string extName)
        {
            try
            {
                string[] dir = Directory.GetDirectories(path); //文件夹列表
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件
                    {
                        if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                        {
                            lst.Add(f);
                        }
                    }

                    foreach (string d in dir)
                    {
                        getdir(d, extName); //递归
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw ex;
            }
        }
    }


    public partial class FileGet
    {
        /// <summary>
        /// 获得目录下所有文件或指定文件类型文件(包含所有子文件夹)
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extName">扩展名可以多个 例如 .mp3.wma.rm</param>
        /// <returns>List<FileInfo></returns>
        public static List<FileInfo> getFile(string path, string extName, List<FileInfo> lst = null)
        {
            try
            {
                // List<FileInfo> lst = new List<FileInfo>();
                if (lst == null)
                {
                    lst = new List<FileInfo>();
                }

                string[] dir = Directory.GetDirectories(path); //文件夹列表
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件
                    {
                        if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                        {
                            lst.Add(f);
                        }
                    }

                    foreach (string d in dir)
                    {
                        getFile(d, extName, lst); //递归
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw ex;
            }
        }
    }

    public partial class FileGet
    {
        /// <summary>
        /// 获取路径下所有文件 名字包含name的文件
        /// </summary>
        /// <param name="path">查找的路径</param>
        /// <param name="name">包含的名字</param>
        /// <param name="lst"> 路径下的文件</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<FileInfo> getFileAllName(string path, string name, List<FileInfo> lst = null)
        {
            try
            {
                // List<FileInfo> lst = new List<FileInfo>();
                if (lst == null)
                {
                    lst = new List<FileInfo>();
                }

                string[] dir = Directory.GetDirectories(path); //文件夹列表
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件
                    {
                        if (f.FullName.ToLower().IndexOf(name.ToLower()) > 0)
                        {
                            lst.Add(f);
                        }
                    }

                    foreach (string d in dir)
                    {
                        getFile(d, name, lst); //递归
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw ex;
            }
        }
    }
}