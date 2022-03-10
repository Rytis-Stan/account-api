using AccountApi.Utilities;

namespace AccountApi.Domain;

public interface IAccountLineValidation
{
    List<string> ErrorsIn(TextLine line);
}