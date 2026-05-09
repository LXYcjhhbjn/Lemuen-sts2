using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
namespace LXY.Scripts;

public class TaoFanYinDuShouXuPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/TaoFanYinDuShouXuPower.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/TaoFanYinDuShouXuPower.png";
	public override async Task AfterSideTurnStart(CombatSide side, ICombatState combatState)
	{
		if (side == base.Owner.Side)
		{
			Flash();
			await PowerCmd.Apply<AIM>(new ThrowingPlayerChoiceContext() , base.Owner, base.Amount, base.Owner,null);
		}
	}
}