using Engine.Actions;
using Engine.Factories;

namespace TestEngine.Actions;

[TestClass]
public class TestAttackWithWeapon
{
    [TestMethod]
    public void Test_Constructor_GoodParameters()
    {
        var pointyStick = ItemFactory.CreateGameItem(1001);
        var attackWithWeapon = new AttackWithWeapon(pointyStick, "1d5");

        Assert.IsNotNull(attackWithWeapon);
    }

    [TestMethod]
    public void Test_Constructor_ItemIsNotAWeapon()
    {
        var chew_chewBar = ItemFactory.CreateGameItem(2001);

        Assert.ThrowsExactly<ArgumentException>(() =>
            new AttackWithWeapon(chew_chewBar, "1d5"));
    }

    [TestMethod]
    public void Test_Construction_MinimumDamageLessThanZero()
    {
        var excalibur = ItemFactory.CreateGameItem(1003);

        Assert.ThrowsExactly<ArgumentException>(() =>
            new AttackWithWeapon(excalibur, "-1d5"));
    }

    [TestMethod]
    public void Test_Constructor_MaximumDamageLessThanMinimumDamage()
    {
        var rustySword = ItemFactory.CreateGameItem(1002);

        Assert.ThrowsExactly<ArgumentException>(() =>
            new AttackWithWeapon(rustySword, "5d3"));
    }
}
