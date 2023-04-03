using Prism.Mvvm;

namespace Database.Models.Component;

public class Reminder : BindableBase
{
    private string context;
    private int id;

    public int Id
    {
        get => id;
        set => SetProperty(ref id, value);
    }

    public string Context
    {
        get => context;
        set => SetProperty(ref context, value);
    }
}