using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

public sealed class FenSuiDan : CustomPowerModel
{
	public override PowerType Type => PowerType.Buff;

	public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/FenSuiDan.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/FenSuiDan.png";

	public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
	{
		if (dealer != null && (dealer == base.Owner || dealer.PetOwner?.Creature == base.Owner) && props.IsPoweredAttack_() && result.TotalDamage > 0)
		{
			await PowerCmd.Apply<FenSuiDan>(choiceContext,base.Owner, -1 , base.Owner, null, silent: true);
            await PowerCmd.Apply<VulnerablePower>(choiceContext,target, 1 , base.Owner, null);
		}
	}
}
