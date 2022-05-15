using NUnit.Framework;
using System;
using System.Linq;

namespace Collections.UnitTests
{
    public class CollectionTests
    {
        //    Collection<int> nums;
        //    Collection<string> names;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            //arrange
            var nums = new Collection<int>();

            //act
            Assert.That(nums.Count == 0, "Count Property");
            //Assert.That(nums.Capacity == 0, "Capacity Property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity Property");
            Assert.That(nums.ToString() == "[]");
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //arrange
            var nums = new Collection<int>(5);

            //act
            Assert.That(nums.Count == 1, "Count Property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity Property");
            Assert.That(nums.ToString() == "[5]");
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            //arrange
            var nums = new Collection<int>(5,6);

            //act
            Assert.That(nums.Count == 2, "Count Property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity Property");
            Assert.That(nums.ToString() == "[5, 6]");
        }

        [Test]
        public void Test_Collection_Add()
        {
            //arrange
            var nums = new Collection<int>();

            //act
            nums.Add(7);

            Assert.That(nums.Count == 1, "Count Property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity Property");
            Assert.That(nums.ToString() == "[7]");
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            //arrange
            var items = new int[] { 6, 7, 8 };
            var nums = new Collection<int>();

            //act
            nums.AddRange(items);

            //assert
            Assert.That(nums.Count == 3, "Count Property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity Property");
            Assert.That(nums.ToString() == "[6, 7, 8]");
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            // Arrange
            var names = new Collection<string>("John", "Gabrielle");
            // Act
            var item0 = names[0];
            var item1 = names[1];
            // Assert
            Assert.That(item0, Is.EqualTo("John"));
            Assert.That(item1, Is.EqualTo("Gabrielle"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var names = new Collection<string>("Bob", "Joe");
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Teddy", "Gerry");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();
            Assert.That(nestedToString,
              Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));
        }

        [Test]
        public void Test_Collection_InsertAt()
        {
            //Arrange
            var nums = new Collection<int>(3, 4, 5, 6 );

            //Act
            nums.InsertAt(2, 3);

            // Assert
            Assert.That(nums.Count == 5, "Count Property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity Property");
            Assert.That(nums.ToString() == "[3, 4, 3, 5, 6]");
        }

        [Test]
        public void Test_Collection_Exchange()
        {
            //Arrange
            var nums = new Collection<int>(3, 4, 5, 6);

            //Act
            nums.Exchange(2, 3);

            // Assert
            Assert.That(nums.Count == 4, "Count Property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity Property");
            Assert.That(nums.ToString() == "[3, 4, 6, 5]");
        }

        [Test]
        public void Test_Collection_Clear()
        {
            //Arrange
            var nums = new Collection<int>(3, 4, 5, 6);

            //Act
            nums.Clear();

            // Assert
            Assert.That(nums.Count == 0, "Count Property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity Property");
            Assert.That(nums.ToString() == "[]");
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            var nums = new Collection<int>();
            const int itemsCount = 10;
            for (int i = 1; i <= itemsCount; i++)
            {
                nums.Add(i);
                Assert.That(nums.Count, Is.EqualTo(i));
                Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            }
            for (int i = itemsCount; i >= 1; i--)
            {
                nums.RemoveAt(i - 1);
                Assert.That(nums.Count, Is.EqualTo(i - 1));
                Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            }
        }

        [Test]
        public void Test_Collection_AddWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;

            for (int i = 1; i <= 20; i++)
            {
                nums.Add(i);
            }
            string expectedNums = string.Join(", ", nums);

            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
        }

        [Test]
        public void Test_Collection_RemoveAll()
        {
            var nums = new Collection<int>();
            const int itemsCount = 10;
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            for (int i = 1; i <= itemsCount; i++)
            {
                var removed = nums.RemoveAt(0);
                Assert.That(removed, Is.EqualTo(i));
            }
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
            Assert.That(nums.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var names = new Collection<string>("George", "Grace");

            var removed = names.RemoveAt(1);
            Assert.That(removed, Is.EqualTo("Grace"));
            Assert.That(names.ToString(), Is.EqualTo("[George]"));
        }

        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            var names = new Collection<string>("George", "Grace");

            Assert.That(() => names.RemoveAt(-1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.RemoveAt(2), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[George, Grace]"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            var names = new Collection<string>("George", "Ivan");

            Assert.That(names.ToString(), Is.EqualTo("[George, Ivan]"));
            names[0] = "David";
            names[1] = "Grace";
            Assert.That(names.ToString(), Is.EqualTo("[David, Grace]"));
        }
    }
}
    