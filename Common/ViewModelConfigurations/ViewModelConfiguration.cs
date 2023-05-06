namespace Common
{
    using Common.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class ViewModelConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseViewModel
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {

        }
    }
}
