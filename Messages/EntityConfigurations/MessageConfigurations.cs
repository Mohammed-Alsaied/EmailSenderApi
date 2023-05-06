public class MessageConfigurations : BaseEntityConfiguration<Message>
{
    public override void Configure(EntityTypeBuilder<Message> builder)
    {
        base.Configure(builder);
        builder.Property(d => d.Id).ValueGeneratedOnAdd();
    }
}