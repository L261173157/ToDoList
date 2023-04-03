using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper;
using CsvHelper.Configuration;
using Database.Db;
using Database.Models.Component;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace Component.ViewModels;

public class ParameterViewModel : BindableBase
{
    #region 属性

    private string _csvFileName;

    public string CsvFileName
    {
        get => _csvFileName;
        set => SetProperty(ref _csvFileName, value);
    }

    private string _rate;

    /// <summary>
    ///     当前进度，绑定界面进度条
    /// </summary>
    public string Rate
    {
        get => _rate;
        set => SetProperty(ref _rate, value);
    }

    private string _dictState;

    /// <summary>
    ///     当前词库状态，绑定界面词库状态
    /// </summary>
    public string DictState
    {
        get => _dictState;
        set => SetProperty(ref _dictState, value);
    }

    #endregion 属性

    #region 命令

    private DelegateCommand _newDictCmd;

    /// <summary>
    ///     读取csv文件，绑定界面选择文件按钮
    /// </summary>
    public DelegateCommand NewDictCmd =>
        _newDictCmd ?? (_newDictCmd = new DelegateCommand(ExecuteNewDictCmd));

    private DelegateCommand _importNewDictCmd;

    /// <summary>
    ///     读取csv文件，绑定界面确定导入按钮
    /// </summary>
    public DelegateCommand ImportNewDictCmd =>
        _importNewDictCmd ?? (_importNewDictCmd = new DelegateCommand(ExecuteImportNewDictCmd));

    private DelegateCommand _initializeDictCmd;

    /// <summary>
    ///     初始化数据库，绑定界面初始化按钮
    /// </summary>
    public DelegateCommand InitializeDictCmd =>
        _initializeDictCmd ?? (_initializeDictCmd = new DelegateCommand(ExecuteInitializeDictCmd));

    private DelegateCommand _dbQueryCmd;

    /// <summary>
    ///     查询数据库总数，绑定界面查询按钮
    /// </summary>
    public DelegateCommand DbQueryCmd =>
        _dbQueryCmd ?? (_dbQueryCmd = new DelegateCommand(ExecuteDbQueryCmd));

    #endregion 命令

    #region 内部方法

    /// <summary>
    ///     选取新CSV文件
    /// </summary>
    private void ExecuteNewDictCmd()
    {
        //打开csv文件
        var openFileDialog = new OpenFileDialog();
        openFileDialog.FileName = "选取CSV文件";
        openFileDialog.DefaultExt = ".csv";
        openFileDialog.Filter = "Csv documents (.csv)|*.csv";
        var result = openFileDialog.ShowDialog();
        if (result == true) CsvFileName = openFileDialog.FileName;
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };
        using var reader = new StreamReader(CsvFileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Dict>();
        Rate = "当前CVS词库数量:" + records.Count();
    }

    /// <summary>
    ///     导入CSV文件到数据库
    /// </summary>
    private void ExecuteImportNewDictCmd()
    {
        Task.Run(CsvToDb).ContinueWith(t => MessageBox.Show("导入成功"));
        DictState = "任务状态:" + "正在导入";
    }

    /// <summary>
    ///     CSV文件到数据库
    /// </summary>
    private async void CsvToDb()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };
        using var reader = new StreamReader(CsvFileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Dict>();
        //写入数据库
        await using (var context = new Context())
        {
            long rateNew = 0;
            long rateUpdate = 0;
            foreach (var record in records)
            {
                var oldDictDb = (from d in context.DictDbs where d.Word == record.Word select d).FirstOrDefault();
                if (oldDictDb is null)
                {
                    var dictDb = new DictDb
                    {
                        Word = record.Word,
                        Audio = record.Audio,
                        Bnc = record.Bnc,
                        Collins = record.Collins,
                        Definition = record.Definition,
                        Translation = record.Translation,
                        Pos = record.Pos,
                        Detail = record.Detail,
                        Exchange = record.Exchange,
                        Frq = record.Frq,
                        Oxford = record.Oxford,
                        Tag = record.Tag,
                        Phonetic = record.Phonetic
                    };
                    context.DictDbs.Add(dictDb);
                    rateNew++;
                }
                else
                {
                    oldDictDb.Word = record.Word;
                    oldDictDb.Audio = record.Audio;
                    oldDictDb.Bnc = record.Bnc;
                    oldDictDb.Collins = record.Collins;
                    oldDictDb.Definition = record.Definition;
                    oldDictDb.Translation = record.Translation;
                    oldDictDb.Pos = record.Pos;
                    oldDictDb.Detail = record.Detail;
                    oldDictDb.Exchange = record.Exchange;
                    oldDictDb.Frq = record.Frq;
                    oldDictDb.Oxford = record.Oxford;
                    oldDictDb.Tag = record.Tag;
                    oldDictDb.Phonetic = record.Phonetic;
                    rateUpdate++;
                }

                Rate = "新增:" + rateNew + "更新:" + rateUpdate;
            }

            await context.SaveChangesAsync();
        }
    }

    private void ExecuteInitializeDictCmd()
    {
        Task.Run(InitializeDictDb)
            .ContinueWith(t => MessageBox.Show("初始化成功"));
        DictState = "任务状态:" + "正在导入";
    }

    private async void InitializeDictDb()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };
        using var reader = new StreamReader(CsvFileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Dict>();
        //写入数据库
        await using (var context = new Context())
        {
            //清空词典表
            await context.Database.ExecuteSqlRawAsync("truncate table dictdbs");
            long rateNew = 0;
            foreach (var record in records)
            {
                var dictDb = new DictDb
                {
                    Word = record.Word,
                    Audio = record.Audio,
                    Bnc = record.Bnc,
                    Collins = record.Collins,
                    Definition = record.Definition,
                    Translation = record.Translation,
                    Pos = record.Pos,
                    Detail = record.Detail,
                    Exchange = record.Exchange,
                    Frq = record.Frq,
                    Oxford = record.Oxford,
                    Tag = record.Tag,
                    Phonetic = record.Phonetic
                };
                context.DictDbs.Add(dictDb);
                rateNew++;
                Rate = "新增:" + rateNew;
            }

            await context.SaveChangesAsync();
        }
    }

    private async void ExecuteDbQueryCmd()
    {
        await using (var context = new Context())
        {
            DictState = "词库数量:" + (from dict in context.DictDbs select dict).Count();
        }
    }

    #endregion 内部方法
}