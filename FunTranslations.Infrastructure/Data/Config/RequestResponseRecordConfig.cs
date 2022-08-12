using FunTranslations.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunTranslations.Infrastructure.Data.Config;
public class RequestResponseRecordConfig : IEntityTypeConfiguration<RequestResponseRecord>
{
    public void Configure(EntityTypeBuilder<RequestResponseRecord> builder)
    {
        builder.Property(x => x.ResponseText)
                .IsRequired()
                .HasMaxLength(ConfigConstants.DEFAULT_RESPONSE_TEXT_LENGTH);

        builder.Property(x => x.RequestUrl)
                .HasMaxLength(ConfigConstants.DEFAULT_URI_LENGTH);

        builder.Property(x => x.RequestedText)
                .HasMaxLength(ConfigConstants.DEFAULT_RESPONSE_TEXT_LENGTH);

        builder.Property(x => x.TranslatedText)
              .HasMaxLength(ConfigConstants.DEFAULT_RESPONSE_TEXT_LENGTH);
    }
}

