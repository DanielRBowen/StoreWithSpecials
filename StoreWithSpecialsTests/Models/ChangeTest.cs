using StoreWithSpecials.Models;
using Xunit;

namespace StoreWithSpecialsTests
{
    public class ChangeTest
    {
        [Fact]
        public void GetChange_One50()
        {
            var change = new Change(100, 50);
            Assert.Equal(1, change.Denomination_50);
        }

        [Fact]
        public void GetChange_Given20_Due17_27_Expected2_73()
        {
            var change = new Change(20, (decimal)17.27);
            Assert.Equal((decimal)2.73, change.ChangeAmount);
            Assert.Equal(0, change.Denomination_50);
            Assert.Equal(0, change.Denomination_20);
            Assert.Equal(0, change.Denomination_10);
            Assert.Equal(0, change.Denomination_5);
            Assert.Equal(2, change.Denomination_1);
            Assert.Equal(2, change.Denomination_0_25);
            Assert.Equal(2, change.Denomination_0_10);
            Assert.Equal(0, change.Denomination_0_05);
            Assert.Equal(3, change.Denomination_0_01);
        }

        [Fact]
        public void GetChange_Given10_Due17_27_Owed7_27()
        {
            var change = new Change(10, (decimal)17.27);
            Assert.Equal((decimal)7.27, change.AmountRemainingDue);
        }
    }
}
