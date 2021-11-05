namespace Moonpig.PostOffice.Tests
{
    using System;
    using System.Collections.Generic;
    using Api.Controllers;
    using Shouldly;
    using Xunit;

    public class PostOfficeTests
    {
        readonly DespatchDateController controller = new DespatchDateController();

        [Fact]
        public void OneProductWithLeadTimeOfOneDay()
        {            
            var date = controller.Get(new List<int>() {1}, new DateTime(2021, 11, 03));
            date.Date.Date.ShouldBe(new DateTime(2021, 11, 03).AddDays(1));
        }

        [Fact]
        public void OneProductWithLeadTimeOfTwoDay()
        {
            var date = controller.Get(new List<int>() { 2 }, new DateTime(2021, 11, 03));
            date.Date.Date.ShouldBe(new DateTime(2021, 11, 03).AddDays(2));
        }

        [Fact]
        public void OneProductWithLeadTimeOfThreeDay()
        {
            var date = controller.Get(new List<int>() { 3 }, DateTime.Now);
            date.Date.Date.ShouldBe(DateTime.Now.Date.AddDays(3));
        }

        [Fact]
        public void SaturdayHasExtraTwoDays()
        {
            var date = controller.Get(new List<int>() { 1 }, new DateTime(2018,1,26));
            date.Date.ShouldBe(new DateTime(2018, 1, 26).Date.AddDays(3));
        }

        [Fact]
        public void SundayHasExtraDay()
        {
            var date = controller.Get(new List<int>() { 3 }, new DateTime(2018, 1, 25));
            date.Date.ShouldBe(new DateTime(2018, 1, 25).Date.AddDays(4));
        }
    }
}
