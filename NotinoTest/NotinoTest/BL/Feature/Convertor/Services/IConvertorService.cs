using NotinoTest.api.Convertor.Enums;
using NotinoTest.Infrastructure.Error;
using OneOf;

namespace NotinoTest.api.Convertor;

public interface IConvertorService
{
    OneOf<string, ErrorType> Convert(string content, DocumentTypeEnums convertTo);
}