namespace MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
public class SeasonBriefDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Order { get; set; }
    public string? PosterLink { get; set; }
    public string? TrailerLink { get; set; }
    public int EpisodeCount { get; set; }
    public int Duration { get; set; }
}
