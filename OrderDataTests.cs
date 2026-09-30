using AutoTestsForApplications.DTO.Orders;
using AutoTestsForApplications.Utils;
using FluentAssertions;

namespace AutoTestsForApplications;

public class OrderDataTests
{
    private OrderDataDTO _order = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _order = JsonFileReader.ReadAndDeserialize<OrderDataDTO>("Resources/OrderData.json");
    }

    [Test]
    public void Test1_Items_AreLoggedAndCountIsThree()
    {
        foreach (var item in _order.Items)
        {
            TestContext.WriteLine($"{item.ProductId} | {item.Name} | qty={item.Quantity} | price={item.Price}");
        }

        _order.Items.Should().HaveCount(3);
    }

    [Test]
    public void Test2_ItemsTotal_EqualsSumOfQuantityTimesPrice()
    {
        // Select не нужен: Sum принимает селектор и сразу считает сумму по каждому элементу
        decimal calculated = _order.Items.Sum(i => i.Quantity * i.Price);

        calculated.Should().Be(_order.Summary.ItemsTotal);
    }

    [Test]
    public void Test3_ElectronicsItems_AreHeadphonesAndCable()
    {
        var electronics = _order.Items.Where(i => i.Category == "Electronics").ToList();

        electronics.Should().HaveCount(2);
        electronics.Select(i => i.Name).Should()
            .BeEquivalentTo("Wireless Headphones", "USB-C Cable");
    }

    [Test]
    public void Test4_Payment_IsPaidAndHasTransactionId()
    {
        _order.Payment.Status.Should().Be("paid");
        _order.Payment.TransactionId.Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public void Test5_MostExpensiveItem_IsWirelessHeadphones()
    {
        var mostExpensive = _order.Items.OrderByDescending(i => i.Price).First();

        mostExpensive.Name.Should().Be("Wireless Headphones");
        mostExpensive.Price.Should().Be(129.99m);
    }

    [Test]
    public void Test6_ItemsAbove50_OnlyWirelessHeadphones()
    {
        var expensive = _order.Items.Where(i => i.Price > 50m).ToList();

        // ContainSingle: в списке ровно один элемент, и дальше его можно проверять через .Which
        expensive.Should().ContainSingle().Which.Name.Should().Be("Wireless Headphones");
    }

    [Test]
    public void Test7_Delivery_IsInProgress()
    {
        _order.Delivery.Status.Should().Be("in_progress");
    }
}
