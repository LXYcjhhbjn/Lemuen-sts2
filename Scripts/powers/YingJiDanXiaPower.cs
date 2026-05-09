using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
namespace LXY.Scripts;

public class YingJiDanXiaPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/YingJiDanXiaPower.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/YingJiDanXiaPower.png";
	public override async Task AfterSideTurnStart(CombatSide side, ICombatState icombatState)
	{
		if (side == base.Owner.Side)
		{
			Flash();
			await PowerCmd.Apply<AMMO>(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount, base.Owner, null);
		}
	}
}