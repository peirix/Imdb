using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Imdb.Tests.Helpers
{
    public static class TestExtensions
    {
        public static void ShouldEqual(this object objA, object objB)
        {
            Assert.AreEqual(objB, objA);
        }

        public static void ShouldBeTrue(this bool check)
        {
            Assert.IsTrue(check);
        }

        public static void ShouldContain(this IList collection, object item)
        {
            int index = collection.IndexOf(item);
            if (index == -1)
                Assert.Fail("Could not find expected object in list");
        }

        public static void ShouldContain(this IList collection, IEnumerable items)
        {
            foreach (var item in items)
            {
                int index = collection.IndexOf(item);
                if (index == -1)
                    Assert.Fail("Could not find expected object in list\r\n" + item.ToString());
            }
        }
    }
}
