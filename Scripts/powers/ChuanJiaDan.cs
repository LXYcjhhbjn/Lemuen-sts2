using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;
public class ChuanJiaDan: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/ChuanJiaDan.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/ChuanJiaDan.png";
	public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
	{
		if (dealer != null && (dealer == base.Owner || dealer.PetOwner?.Creature == base.Owner) && props.IsPoweredAttack_() && result.TotalDamage > 0)
		{
        	if (base.Owner.CombatState.HittableEnemies != null)
        	{
           		await CreatureCmd.Damage(choiceContext, base.Owner.CombatState.HittableEnemies, result.TotalDamage , ValueProp.Unpowered | ValueProp.Move , base.Owner);
        	}
			await PowerCmd.Apply<ChuanJiaDan>(choiceContext,base.Owner, -1 , base.Owner, null, silent: true);
		}
	}
}