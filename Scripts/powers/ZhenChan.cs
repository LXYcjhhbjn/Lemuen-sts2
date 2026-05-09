using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

public sealed class ZhenChan : CustomPowerModel
{
	public override PowerType Type => PowerType.Debuff;

	public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/ZhenChan.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/ZhenChan.png";

	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext,PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		if (!(amount == 0m) && power is ZhenChan && power.Owner.IsEnemy)
		{
			Flash();
			if (power.Owner.GetPowerAmount<ZhenChan>() >= 3)
			{
				await CreatureCmd.Stun(power.Owner);
				await PowerCmd.Remove<ZhenChan>(power.Owner);
			}
		}
	}
}
