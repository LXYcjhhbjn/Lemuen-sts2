using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
namespace LXY.Scripts;

public class ChongXieGaiZhuangPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/ChongXieGaiZhuangPower.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/ChongXieGaiZhuangPower.png";
	public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		if (cardPlay.Card.Owner.Creature == base.Owner && cardPlay.Card.Type == CardType.Skill)
		{
			int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Type == CardType.Skill && e.CardPlay.Card.Owner.Creature == base.Owner);
			if (num % 2m == 0)
			{
			    Flash();
			    await PowerCmd.Apply<ShiKeDan>(choiceContext,base.Owner, base.Amount, base.Owner, null);
			}
		}
	}

}