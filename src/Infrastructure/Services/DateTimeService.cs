using MvDb.Application.Common.Interfaces;

namespace MvDb.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
