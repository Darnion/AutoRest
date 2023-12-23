using Xunit;

namespace AutoRest.Api.Tests.Infrastructures
{
    [CollectionDefinition(nameof(AutoRestApiTestCollection))]
    public class AutoRestApiTestCollection
        : ICollectionFixture<AutoRestApiFixture>
    {
    }
}
