using Prism.Mvvm;

namespace Database.Models.Component;

public class Dict : BindableBase
{
    private string audio;

    private string bnc;

    private string collins;

    private string definition;

    private string detail;

    private string exchange;

    private string frq;

    private string oxford;

    private string phonetic;

    private string pos;

    private string tag;

    private string translation;

    private string word;

    public string Word
    {
        get => word;
        set => SetProperty(ref word, value);
    }

    public string Phonetic
    {
        get => phonetic;
        set => SetProperty(ref phonetic, value);
    }

    public string Definition
    {
        get => definition;
        set => SetProperty(ref definition, value);
    }

    public string Translation
    {
        get => translation;
        set => SetProperty(ref translation, value);
    }

    public string Pos
    {
        get => pos;
        set => SetProperty(ref pos, value);
    }

    public string Collins
    {
        get => collins;
        set => SetProperty(ref collins, value);
    }

    public string Oxford
    {
        get => oxford;
        set => SetProperty(ref oxford, value);
    }

    public string Tag
    {
        get => tag;
        set => SetProperty(ref tag, value);
    }

    public string Bnc
    {
        get => bnc;
        set => SetProperty(ref bnc, value);
    }

    public string Frq
    {
        get => frq;
        set => SetProperty(ref frq, value);
    }

    public string Exchange
    {
        get => exchange;
        set => SetProperty(ref exchange, value);
    }

    public string Detail
    {
        get => detail;
        set => SetProperty(ref detail, value);
    }

    public string Audio
    {
        get => audio;
        set => SetProperty(ref audio, value);
    }
}