using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// 共通ユーティリティクラス
    /// </summary>
    internal static class CommonUtil
    {
        /// <summary>
        /// 作業フォルダを基準として相対表現を絶対パスに変える
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        static public string ToAbsolutePath(string _path)
        {
            string paramPath = _path;
            if (!Path.IsPathRooted(paramPath))
            {
                paramPath = Path.Combine(Directory.GetCurrentDirectory(), paramPath);
            }
            return paramPath;
        }

        /// <summary>
        /// 例外メッセージを内部までさかのぼって取得する
        /// </summary>
        /// <param name="ex">例外</param>
        /// <returns>メッセージ</returns>
        public static string GetExceptionMessage(Exception ex)
        {
            var sb = new System.Text.StringBuilder();
            Exception _inner = ex;
            while (_inner != null)
            {
                sb.AppendLine(_inner.Message);

                _inner = _inner.InnerException;
            }
            return sb.ToString();
        }
    }
}
