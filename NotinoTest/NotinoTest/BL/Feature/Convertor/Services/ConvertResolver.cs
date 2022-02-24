using NotinoTest.api.Convertor.Enums;

namespace NotinoTest.api.Convertor.Services;

public class ConvertResolver
{
    private readonly IEnumerable<IConvertorStrategy> _strategies;
   
    public IConvertorStrategy this[DocumentTypeEnums type] => 
        _strategies.SingleOrDefault(x => x.ProcessType == type) ?? throw new KeyNotFoundException();
    
    public ConvertResolver(IEnumerable<IConvertorStrategy> strategies)
    {
        _strategies = strategies;
    }
    
}