namespace Database.Models.Component;

public class DictDb : Dict
{
    private int id;

    public int Id
    {
        get => id;
        set => SetProperty(ref id, value);
    }
}