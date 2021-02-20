using System.Linq;
using ikl.web.Server;
using NUnit.Framework;

namespace ikl.web.Tests
{
    public class DataLoaderTests
    {
        [TestCase("009-11")]
        [TestCase("013-13")]
        public void Load_ExcludedItems_NotPartOfData(string path)
        {
            // ARRANGE
            var data = DataLoader.Load(excludeListPath: "test_exclude_list.json");
            
            // ACT
            var result = data.Drawings.FirstOrDefault(d => d.Path.StartsWith(path));
            
            // ASSERT
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public void Load_IncludedItems_NotPartOfData()
        {
            // ARRANGE
            var data = DataLoader.Load(
                excludeListPath:"",
                includeListPath: "test_include_list.json");
            
            // ACT
            var result = data.Drawings.FirstOrDefault(d => d.Path.StartsWith("009-11"));
            
            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(data.Drawings.Length, Is.EqualTo(1));
        }
    }
}