using AccountApi.Domain;

namespace AccountApi.Dto;

// NOTE: DTO means "Data Transfer Object", as described in the following page: https://martinfowler.com/eaaCatalog/dataTransferObject.html
// Here the DTOs are part of the same code layer that contains the controllers (even though both the business logic and controller code is
// in the same project now, for the sake of simplicity for such a small "toy project")
public static class DtoExtensions
{
    public static AccountValidationResultDto ToDto(this AccountValidationResult result)
    {
        return new AccountValidationResultDto(
            result.FileValid,
            result.InvalidLines.Any() ? result.InvalidLines : null
        );
    }
}
