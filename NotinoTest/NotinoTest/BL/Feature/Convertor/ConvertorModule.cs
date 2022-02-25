using NotinoTest.api.Convertor.Enums;
using NotinoTest.api.Convertor.Services;
using NotinoTest.BL.Feature.Convertor.Services;

namespace NotinoTest.api.Convertor;

public static class ConvertorModule
{
    public delegate IConvertorStrategy ConvertorResolver(DocumentTypeEnums cookingType);

    public static IServiceCollection AddConvertor(this IServiceCollection services)
    {
        services.AddTransient<IConvertorStrategy, JsonConvertorStrategy>();
        services.AddTransient<IConvertorStrategy, XmlConvertorStrategy>();
        services.AddTransient<ConvertResolver>();
        services.AddTransient<IConvertorService, ConvertService>();

        return services;
    }
}