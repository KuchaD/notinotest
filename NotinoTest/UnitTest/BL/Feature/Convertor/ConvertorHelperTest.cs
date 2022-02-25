using System.Collections;
using System.Collections.Generic;
using NotinoTest.api.Convertor;
using NotinoTest.api.Convertor.Enums;
using Xunit;

namespace UnitTest.BL.Feature.Convertor;

public class TestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { DocumentTypeEnums.Json, "Name", "Name.json" };
        yield return new object[] { DocumentTypeEnums.Xml, "Name", "Name.xml" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
public class ConvertorHelperTest
{

    [Theory]
    [ClassData(typeof(TestData))]
    public void FileNameStrategyTest_ReturnSuccessNameExtension(DocumentTypeEnums type, string value, string expected)
    {
        var result = ConvertorHelper.FileNameStrategy[type](value);
        Assert.Equal(result, expected);
    }
}