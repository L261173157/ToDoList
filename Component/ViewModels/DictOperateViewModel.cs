using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Database.Models.Component;
using Database.Db;

namespace Component.ViewModels
{
    public class DictOperateViewModel : BindableBase
    {
        public DictOperateViewModel()
        {
        }

        #region 属性

        private string csvFileName;

        public string CsvFileName
        {
            get { return csvFileName; }
            set { SetProperty(ref csvFileName, value); }
        }
        
        private string test;

        public string Test
        {
            get { return test; }
            set { SetProperty(ref test, value); }
        }

        #endregion 属性

        #region 命令

        private DelegateCommand _newDictCmd;

        public DelegateCommand NewDictCmd =>
            _newDictCmd ?? (_newDictCmd = new DelegateCommand(ExecuteNewDictCmd));

        #endregion 命令

        #region 内部方法

        private async void ExecuteNewDictCmd()
        {
            //打开csv文件
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.FileName = "选取CSV文件";
            openFileDialog.DefaultExt = ".csv";
            openFileDialog.Filter = "Csv documents (.csv)|*.csv";

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
            {
                CsvFileName = openFileDialog.FileName;
            }
            //读取csv文件,缺少异步方法，目前发现会阻塞
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using var reader = new StreamReader(CsvFileName);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<Dict>();
            //写入数据库
            await using (var context=new Context())
            {
                foreach (var dict in records)
                {
                    var dictDb = new DictDb
                    {
                        Word = dict.Word,
                        Audio = dict.Audio,
                        Bnc = dict.Bnc,
                        Collins = dict.Collins,
                        Definition = dict.Definition,
                        Translation = dict.Translation,
                        Pos = dict.Pos,
                        Detail = dict.Detail,
                        Exchange = dict.Exchange,
                        Frq = dict.Frq,
                        Oxford = dict.Oxford,
                        Tag = dict.Tag,
                        Phonetic = dict.Phonetic,
                    };
                    context.DictDbs.Add(dictDb);
                }

                await context.SaveChangesAsync();
            }

            Test = "ok";
        }

        #endregion 内部方法
    }
}