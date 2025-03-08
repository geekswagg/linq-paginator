using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Paginator;

namespace Tests
{
    [TestFixture]
    public class IQueryableTests : TestBase
    {
        [Test, TestCaseSource(typeof(Seed), "List")]
        public void ParamsTest(ICollection<Rate> Rates)
        {
            int perpage = 2;
            int pages = GetPages(Rates.Count, perpage);
            var result = Rates.AsQueryable().Paginate(1, 2);

            Assert.That(result.ItemsPerPage, Is.EqualTo(2));
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.TotalItems, Is.EqualTo(Rates.Count));
            Assert.That(result.TotalPages, Is.EqualTo(pages));
        }

        [Test, TestCaseSource(typeof(Seed), "List")]
        public void Empty(ICollection<Rate> Rates)
        {
            var result = Rates.AsQueryable().Page();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Page, Is.EqualTo(1));
            Assert.That(result.TotalItems, Is.EqualTo(Rates.Count));
        }


        [Test, TestCaseSource(typeof(Seed), "List")]
        public void FuncTest(ICollection<Rate> Rates)
        {
            var result = Rates.AsQueryable().Paged(x => x.Value > 9, 2, 2, x => x.Value);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Page, Is.EqualTo(2));
            Assert.That(result.ItemsPerPage, Is.EqualTo(2));
            Assert.That(result.TotalItems, Is.EqualTo(Rates.Count(x => x.Value > 9)));
        }
    }
}