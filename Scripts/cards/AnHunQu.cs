using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class AnHunQu: CustomCardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AnHunQuPower>(1m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<AnHunQuPower>()];	
	public AnHunQu()
		: base(1, CardType.Power, CardRarity.Ancient, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		await PowerCmd.Apply<AnHunQuPower>(choiceContext , base.Owner.Creature, base.DynamicVars["AnHunQuPower"].BaseValue, base.Owner.Creature, this);
	}
	protected override void OnUpgrade()
	{
		AddKeyword(CardKeyword.Innate);
	}
}
