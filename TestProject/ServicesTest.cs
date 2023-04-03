using Services.Services;

namespace TestProject;

public class ServicesTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Chat_inputIshello_return()
    {
        var result = OpenAiApi.Chat("hello");
        Assert.IsNotEmpty(result.Result);
    }

    //unit test for common.ContainChinese
    [Test]
    public void ContainChinese_inputIsHello_returnFalse()
    {
        var result = Common.ContainChinese("hello");
        Assert.IsFalse(result);
    }
}