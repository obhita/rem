namespace Pillar.Domain.Tests.Fixture
{
    public class SimpleAggregateRoot : AbstractAggregateRoot
    {
        /// <summary>
        /// Make InitializeServices Method public
        /// </summary>
        public new void InitializeServices()
        {
            base.InitializeServices();
        }
    }
}
