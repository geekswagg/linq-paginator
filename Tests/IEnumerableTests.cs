using NUnit.Framework;
using System.Collections.Generic;
using Paginator;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class IEnumerableTests : TestBase
    {
        [Test, TestCaseSource(typeof(Seed), "List")]
        public void ParamsTest(ICollection<Rate> Rates)
        {
            // Arrange
            int perpage = 2;
            int pages = GetPages(Rates.Count, perpage);
            var result = Rates.Paginate(1, perpage);

            Assert.That(result.ItemsPerPage, Is.EqualTo(2));
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.TotalItems, Is.EqualTo(Rates.Count));
            Assert.That(result.TotalPages, Is.EqualTo(pages));
        }

        [Test, TestCaseSource(typeof(Seed), "List")]
        public void Empty(ICollection<Rate> Rates)
        {
            int perpage = 10;
            int pages = GetPages(Rates.Count, perpage);
            var result = Rates.Page();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.TotalItems, Is.EqualTo(Rates.Count));
            Assert.That(result.TotalPages, Is.EqualTo(pages));
        }

        [Test, TestCaseSource(typeof(Seed), "NumStrings")]
        public void FuncTest(ICollection<string> nums)
        {
            int perpage = 5;

            var result = nums.Paginate(x => x.Equals("0"), 2, perpage, x => x);
            int pages = GetPages(result.TotalItems, perpage);

            Assert.That(result.Page, Is.EqualTo(2));
            Assert.That(result.ItemsPerPage, Is.EqualTo(5));
            Assert.That(result.TotalPages, Is.EqualTo(pages));
        }

        [Test, TestCaseSource(typeof(Seed), "Pages")]
        public void OrderByProperty(ICollection<Rate> Rates)
        {
            var result = Rates.Paginate(null, 1, 10, x => x.Value, "desc");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.TotalItems, Is.EqualTo(Rates.Count));
            Assert.That(result.Items[0].Value, Is.GreaterThan(result.Items[result.Items.Count - 1].Value));
        }
    }
}