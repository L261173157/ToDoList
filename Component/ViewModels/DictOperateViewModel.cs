﻿using Prism.Commands;
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
using System.Threading;
using System.Windows;

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

        private string rate;

        public string Rate
        {
            get { return rate; }
            set { SetProperty(ref rate, value); }
        }

        #endregion 属性

        #region 命令

        private DelegateCommand _newDictCmd;

        public DelegateCommand NewDictCmd =>
            _newDictCmd ?? (_newDictCmd = new DelegateCommand(ExecuteNewDictCmd));

        #endregion 命令

        #region 内部方法

        private void ExecuteNewDictCmd()
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
            Thread thread = new Thread(CsvToDb);
            thread.IsBackground = true;
            thread.Start();
        }

        private async void CsvToDb()
        {
            //读取csv文件,缺少异步方法，目前发现会阻塞
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using var reader = new StreamReader(CsvFileName);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<Dict>();
            //写入数据库
            await using (var context = new Context())
            {
                long rateTotal = 0;
                long rateNew = 0;
                long rateUpdate = 0;
                foreach (var dict in records)
                {
                    rateTotal++;
                    var oldDictDb = (from d in context.DictDbs where d.Word == dict.Word select d).FirstOrDefault();
                    if (oldDictDb is null)
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
                        rateNew++;
                    }
                    else
                    {
                        oldDictDb.Word = dict.Word;
                        oldDictDb.Audio = dict.Audio;
                        oldDictDb.Bnc = dict.Bnc;
                        oldDictDb.Collins = dict.Collins;
                        oldDictDb.Definition = dict.Definition;
                        oldDictDb.Translation = dict.Translation;
                        oldDictDb.Pos = dict.Pos;
                        oldDictDb.Detail = dict.Detail;
                        oldDictDb.Exchange = dict.Exchange;
                        oldDictDb.Frq = dict.Frq;
                        oldDictDb.Oxford = dict.Oxford;
                        oldDictDb.Tag = dict.Tag;
                        oldDictDb.Phonetic = dict.Phonetic;
                        rateUpdate++;
                    }
                    Rate = "新增:" + rateNew.ToString() + "更新:" + rateUpdate.ToString() + "总数:" + rateTotal.ToString();
                }

                await context.SaveChangesAsync();
            }
            MessageBox.Show("读取成功");
        }

        #endregion 内部方法
    }
}