﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.Infrastructure.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.HasMany(x => x.WorkLogs).WithOne(x => x.Task).OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.Title).HasMaxLength(100);
    }
}
