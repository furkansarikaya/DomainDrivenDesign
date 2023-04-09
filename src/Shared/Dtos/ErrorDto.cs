namespace Shared.Dtos;

public record ErrorDto
{
    public List<string> Errors { get; set; }
}