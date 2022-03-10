namespace AccountApi.Dto;

public record AccountValidationResultDto(bool FileValid, string[] InvalidLines);
