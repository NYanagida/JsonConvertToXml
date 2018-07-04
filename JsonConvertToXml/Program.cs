using Newtonsoft.Json;
using System;
using System.Text;
using System.Data;
using System.IO;
//using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace JsonConvertToXml
{
    class Program
    {

        private static string _InputFileName;
        private static string _OutputFileName;
        //_OutputFileName

        /// <summary>
        /// jsonを読み込みをxml出力する。
        /// usage:[-i InputPath] [-o OutputPath]
        /// </summary>
        /// <param name="args"></param>
        static int Main(string[] args)
        {
            string _msg = "正常終了しました";
            int _exitCd = 0;
            try
            {
                // 引数の取得
                _getOpt(args);

                // メイン処理
                _main();
            }
            catch (Exception ex)
            {
                _msg = Common.CommonUtil.GetExceptionMessage(ex);
                _exitCd = 1;
            }
            if (_exitCd == 0)
            {
                Console.WriteLine(_msg);
            }
            else
            {
                Console.Error.WriteLine("処理に失敗しました。" + _msg);
            }
            return _exitCd;
        }


        // <summary>
        // XMLとJSONを相互に変換する方法
        // https://webbibouroku.com/Blog/Article/xml-to-json
        // <summary>
        private static void _main()
        {

        //Console.WriteLine("INPUT [{0}]",_InputFileName);
        //Console.WriteLine("OUTPUT [{0}]",_OutputFileName);          

        var json = string.Empty;
        var fileName = _InputFileName;
        using (var reader = new StreamReader(fileName))
        {
            json = reader.ReadToEnd();
        }
        // JSON形式の文字列をXDocumentに変換
        var xdoc = JsonConvert.DeserializeXNode(json,"values");

        //変換結果を出力
        //Console.WriteLine(xdoc.Declaration);
        //Console.WriteLine(xdoc.ToString());
        xdoc.Save(_OutputFileName);

        }


        /// <summary>
        /// 起動引数の解析. 処理本体へのオプション設定
        /// </summary>
        /// <param name="args"></param>
        private static void _getOpt(string[] args)
        {
            // 起動引数の解析
            var go = new XGetoptCS.XGetopt();
            int argc = args.Length;
            char c;

            //usage:[-i InputPath] [-o OutputPath]
            while ((c = go.Getopt(argc, args, "i:o:")) != '\0')
            {
                switch (c)
                {
                    case 'i':
                        if (string.IsNullOrEmpty(go.Optarg) == false)
                        {
                            _InputFileName = go.Optarg.ToString();
                        }
                        break;
                    case 'o':
                        if (string.IsNullOrEmpty(go.Optarg) == false)
                        {
                            _OutputFileName = go.Optarg.ToString();
                        }
                        break;
                    case '?':
                        throw new ArgumentException("引数の指定が不正です。");
                }
            }

            // 引数の存在確認
            if (string.IsNullOrEmpty(_InputFileName))
            {
                throw new Exception("-i 入力ファイルパスの指定がありません");
            }
            if (string.IsNullOrEmpty(_OutputFileName))
            {
                throw new Exception("-o 出力ファイルパスの指定がありません");
            }

            _InputFileName = Common.CommonUtil.ToAbsolutePath(_InputFileName);
            _OutputFileName = Common.CommonUtil.ToAbsolutePath(_OutputFileName);

            // 拡張子の確認
            if (System.IO.Path.GetExtension(_InputFileName) != ".json")
            {
                throw new Exception("拡張子が .jsonのファイルを指定してください。");
            }

            // 入力ファイルパスの存在確認
            if (!System.IO.File.Exists(_InputFileName))
            {
                throw new Exception("'" + _InputFileName + "'は存在しません。");
            }

        }

    }
}
