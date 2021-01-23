using ikl.web.Client.Shared;
using ikl.web.Server;
using ikl.web.Shared;
using NUnit.Framework;

namespace ikl.web.Tests
{
    public class DataServiceTests
    {
        private Data _data;
        private DataService _dataService;
        
        
        [SetUp]
        public void Setup()
        {
            _data = DataLoader.Load();
            _dataService = new DataService(_data);
        }

        [Test]
        public void Search_Exists_ReturnsValues()
        {
            // ARRANGE
            var text = "stol 1:1";
            
            // ACT
            var result = _dataService.SearchDrawings(text);
            
            // ASSERT
            Assert.That(result, Is.Not.Empty);
        }
    }
}