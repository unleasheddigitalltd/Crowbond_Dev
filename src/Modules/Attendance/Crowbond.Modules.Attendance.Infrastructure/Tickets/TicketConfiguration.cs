using Crowbond.Modules.Attendance.Domain.Attendees;
using Crowbond.Modules.Attendance.Domain.Events;
using Crowbond.Modules.Attendance.Domain.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Attendance.Infrastructure.Tickets;

internal sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Code).HasMaxLength(30);

        builder.HasIndex(t => t.Code).IsUnique();

        builder.HasOne<Attendee>().WithMany().HasForeignKey(t => t.AttendeeId);

        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
    }
}
