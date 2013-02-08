namespace Pillar.FluentRuleEngine.Tests.Fixture
{
    public class StringRuleCollection : AbstractRuleCollection<string>
    {
        public IPropertyRule StringEmptyPropertyRule { get; set; }

        public IRule StringEmptyRule { get; set; }
    }
}
